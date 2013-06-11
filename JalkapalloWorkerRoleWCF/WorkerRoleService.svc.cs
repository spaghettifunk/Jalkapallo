using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using LINQ2Entities;

using MatchClient = Jalkapallo_Shared.Match;
using TeamClient = Jalkapallo_Shared.Team;
using PlayersClient = Jalkapallo_Shared.Player;
using JalkapalloWorkerRoleWCF.Engine;
using System.Diagnostics;

namespace JalkapalloWorkerRoleWCF
{
	public class WorkerRoleService : IWorkerRoleService
	{
		// compute all trainings
		public void ComputeTrainings()
		{
			TrainingEvaluator training = null;
			TeamClient team = null;

			ParallelQuery<Squadre> allSquadre = Entity.JpEntity.Squadres.AsParallel().Select(x => x);
			foreach (Squadre squadra in allSquadre)
			{
				team = (TeamClient)Serializer.DeserializeObject<TeamClient>(squadra.TeamObject);
				training = new TrainingEvaluator(team);
				squadra.TeamObject = Serializer.SerializeObject<TeamClient>(training.ComputeTraining());
				Entity.JpEntity.SaveChanges();
			}
		}

		// compute all directly purchase
		// TODO: controllare le performance! si potrà migliorare di sicuro
		public void ComputeDirectlyPurchase()
		{
			IEnumerable<Selling> soldDone = null;
			IQueryable<Selling> allSellings = Entity.JpEntity.Sellings.Select(x => x).Where(s => s.Sold == true);
			soldDone = MovePlayer<Selling>(allSellings);

			// delete all sold players from table
			foreach (Selling s in soldDone)
			{
				Entity.JpEntity.Sellings.DeleteObject(s);
				Entity.JpEntity.SaveChanges();
			}
		}

		// compute Auction House sells of today
		// TODO: controllare le performance! si potrà migliorare di sicuro
		public void ComputeAuctionHouse()
		{
			IEnumerable<AuctionHouse> soldDone = null;
			ParallelQuery<AuctionHouse> auctionHousePlayers = Entity.JpEntity.AuctionHouses.AsParallel().Select(x => x).Where(e => e.EndAuction.Value.Day == DateTime.Today.Day);
			soldDone = MovePlayer<AuctionHouse>(auctionHousePlayers);

			// delete all sold players from table
			foreach (AuctionHouse s in soldDone)
			{
				Entity.JpEntity.AuctionHouses.DeleteObject(s);
				Entity.JpEntity.SaveChanges();
			}
		}

		private IEnumerable<T> MovePlayer<T>(dynamic playersToSell)
		{
			List<T> soldDone = new List<T>();

			Squadre oldTeamServer = null;
			Squadre newTeamServer = null;
			TeamClient oldTeamClient = null;
			TeamClient newTeamClient = null;

			foreach (var sell in playersToSell)
			{
				// all players of old team
				ParallelQuery<Giocatori> oldTeamPlayersServer = Entity.JpEntity.Giocatoris.AsParallel().Select(x => x).Where(g => g.TeamName == sell.TeamName);

				// both Squadre
				oldTeamServer = Entity.JpEntity.Squadres.AsParallel().Select(x => x).Where(s => s.TeamName == sell.TeamName).FirstOrDefault();
				newTeamServer = Entity.JpEntity.Squadres.AsParallel().Select(x => x).Where(s => s.TeamName == sell.SoldToTeam).FirstOrDefault();

				// both Teams from JSON
				oldTeamClient = (TeamClient)Serializer.DeserializeObject<TeamClient>(oldTeamServer.TeamObject);
				newTeamClient = (TeamClient)Serializer.DeserializeObject<TeamClient>(newTeamServer.TeamObject);

				// both lists of players
				List<PlayersClient> oldTeamPlayers = oldTeamClient.TeamPlayers;
				List<PlayersClient> newTeamPlayers = newTeamClient.TeamPlayers;

				// find player in old team
				PlayersClient oldPlayer = oldTeamPlayers.Select(x => x).Where(g => g.Guid == sell.Id).FirstOrDefault();
				if (oldPlayer != null)
				{
					// edit the Giocatori Table
					Giocatori oldGiocatore = oldTeamPlayersServer.AsParallel().Select(x => x).Where(i => i.GuidGiocatore == sell.Id).FirstOrDefault();
					oldGiocatore.TeamName = sell.SoldToTeam;

					// move player
					oldTeamPlayers.Remove(oldPlayer);
					newTeamPlayers.Add(oldPlayer);

					// pay for player
					oldTeamClient.Budget += sell.Price;
					newTeamClient.Budget -= sell.Price;

					// serialize changes
					oldTeamServer.TeamObject = Serializer.SerializeObject<TeamClient>(oldTeamClient);
					newTeamServer.TeamObject = Serializer.SerializeObject<TeamClient>(newTeamClient);

					// save the "sell" object that have to delete from Db
					soldDone.Add(sell);

					// save everything
					Entity.JpEntity.SaveChanges();
				}
			}

			return soldDone;
		}

		// compute budget of a Team
		public void ComputeAllBudgets()
		{
			BudgetEvaluator budgetEval = null;
			ParallelQuery<Squadre> allSquadre = Entity.JpEntity.Squadres.AsParallel().Select(x => x);
			foreach (Squadre squadra in allSquadre)
			{
				TeamClient team = (TeamClient)Serializer.DeserializeObject<TeamClient>(squadra.TeamObject);
				budgetEval = new BudgetEvaluator(team);
				team = budgetEval.ComputeBudget();
				squadra.TeamObject = Serializer.SerializeObject<TeamClient>(team);
				Entity.JpEntity.SaveChanges();
			}
		}

		public void CreateChampionship()
		{
			// CREAZIONE DEI BOT

			//AGGIUNGERE LE SQUADRE A XML DELL'ELENCO SQUADRE

			//SET DELLE PARTITE NELLA TABELLA MATCH (VALUTARE TUTTI GLI INCROCI E LE DATE DELLE PARTITE FUTURE!! )

			//AGGIUNGERE XML CLASSIFICA INIZIALMENTE CON TUTTI PARAMETRI A ZERO
			//AGGIUNGERE NELLA COLONNA MATCH DELLA TABELLA CHAMPIONSHIP LE PROSSIME PARTITRE DA DISPUTARE 
		}

		// TEST!!!
		public void CreateMatch()
		{
			Squadre home = Entity.JpEntity.Squadres.Select(x => x).Where(t => t.TeamName == "Bazinga").FirstOrDefault();
			Squadre away = Entity.JpEntity.Squadres.Select(x => x).Where(t => t.TeamName == "Real Pilu").FirstOrDefault();

			MatchClient match = new MatchClient()
			{
				Home = (TeamClient)Serializer.DeserializeObject<TeamClient>(home.TeamObject),
				Away = (TeamClient)Serializer.DeserializeObject<TeamClient>(away.TeamObject),
				IdCampionato = Guid.NewGuid()
			};

			MatchClient result = ComputeMatch(match);

			IQueryable<Match> partita = Entity.JpEntity.Matches.Select(x => x).Where(k => k.Home == "Bazinga");
			Match m = partita.FirstOrDefault();
			m.Punteggio_Home = result.ResultHome.ToString();
			m.Punteggio_Away = result.ResultAway.ToString();
			Entity.JpEntity.SaveChanges();
		}

		// compute match
		public MatchClient ComputeMatch(MatchClient currentMatch)
		{
			MatchEvaluator eval = new MatchEvaluator(currentMatch);
			MatchClient m = eval.ComputeMatch();

			Debug.WriteLine(" *************************** \n");
			Debug.WriteLine("Result: " + m.Home.Name + " " + m.ResultHome + " - " + m.ResultAway + " " + m.Away.Name + "\n");
			Debug.WriteLine("Yellow cards: " + m.YellowCard.Count + "\n");
			Debug.WriteLine("Red cards: " + m.RedCard.Count + "\n");
			Debug.WriteLine(" *************************** \n");

			return m;
		}
    }
}