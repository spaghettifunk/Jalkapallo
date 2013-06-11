using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

using Jalkapallo_Shared;
using LINQ2Entities;

using PlayerClient = Jalkapallo_Shared.Player;
using TeamClient = Jalkapallo_Shared.Team;

namespace JalkapalloWebRole
{
	// TASK:0006 ++ 20130412 DB
	public class SellingImplementation
	{
		private PlayerClient player;
		private int price;
		private Guid idPlayer;
		private string teamName;

		public SellingImplementation() { }

		/// <summary>
		/// Usare questo metodo per il giocatore che vuole vendere un componente della sua squadra
		/// </summary>
		/// <param name="jpEntities"></param>
		/// <param name="player"></param>
		/// <param name="price"></param>
		public SellingImplementation(PlayerClient player, int price)
		{
			this.player = player;
			this.price = price;
			this.idPlayer = player.Guid;
			this.teamName = player.TeamName;
		}

		/// <summary>
		/// Usare questo metodo per il giocatore che vuole acquistare questo componenente!
		/// </summary>
		/// <param name="jpEntities"></param>
		/// <param name="player"></param>
		/// <param name="price"></param>
		/// <param name="teamName"></param>
		public SellingImplementation(PlayerClient player, int price, string teamName)
		{
			this.player = player;
			this.idPlayer = player.Guid;
			this.price = price;
			this.teamName = teamName;
		}

		internal void SellPlayer()
		{
			Selling playerToSell = new Selling()
			{
				Id = player.Guid,
				Name = player.Name,
				Surname = player.Surname,
				TeamName = player.TeamName,
				SoldToTeam = player.TeamName,
				SellDate = DateTime.UtcNow,
				Sold = false,
				Price = price
			};

			Entity.JpEntity.Sellings.AddObject(playerToSell);
			Entity.JpEntity.SaveChanges();

            // TODO: Aggiornare la tabella delle News
		}

		internal void BuyPlayerDirectly()
		{
			Selling playerOnSell = Entity.JpEntity.Sellings.Select(x => x).Where(k => k.Id == idPlayer).SingleOrDefault();
			playerOnSell.Sold = true;
			playerOnSell.SoldToTeam = teamName;

			Entity.JpEntity.SaveChanges();

			// TODO: Aggiorna la tabella delle News
		}

		internal Dictionary<PlayerClient, int> GetAllPlayersOnSell()
		{
			IEnumerable<Selling> allOnAuction = Entity.JpEntity.Sellings.Select(x => x).Where(s => s.Sold == false);
			if (allOnAuction.Count() == 0)
				return null;

			Dictionary<PlayerClient, int> players = new Dictionary<PlayerClient, int>();
			foreach (Selling s in allOnAuction)			
				players.Add(GetPlayer(s.Id), (int)s.Price);

			return players;
		}

		private PlayerClient GetPlayer(Guid playerId)
		{
			RegistrationImplementation reg = new RegistrationImplementation();
			Giocatori g = Entity.JpEntity.Giocatoris.Select(x => x).Where(i => i.GuidGiocatore == playerId).SingleOrDefault();

			PlayerClient p = new PlayerClient()
			{
				Skills = new PlayerSkills()
				{
					Attacco = (float)g.Attacco,
					Difesa = (float)g.Difesa,
					Centrocampo = (float)g.Centrocampo,
					Exp = (float)g.Experience,
					Parata = (float)g.Parata,
					Tiro = (float)g.Tiro,
					Velocita = (float)g.Velocita,
				},

				Name = g.Nome,
				Surname = g.Surname,
				Role = reg.GetPlayerRole(g.Role),
				Country = g.Country,
				Birthday = g.Birthday,
				Height = (int)Convert.ToDouble(g.Height),
				Weight = (float)Convert.ToDouble(g.Weight),
				TeamName = g.TeamName,
				Guid = Guid.Parse(g.GuidGiocatore.ToString())
			};

			return p;
		}
	}
}