using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

using Jalkapallo_Shared;
using LINQ2Entities;

using PlayerClient = Jalkapallo_Shared.Player;

using AuctionHouseClient = Jalkapallo_Shared.AuctionHouse;
using AuctionHouseServer = LINQ2Entities.AuctionHouse;

namespace JalkapalloWebRole
{
	public class AuctionHouseImplementation
	{
		private PlayerClient player;
		private int startingPrice;

		public AuctionHouseImplementation() { }

		public AuctionHouseImplementation(PlayerClient player)
		{
			this.player = player;
		}

		public AuctionHouseImplementation(PlayerClient player, int startingPrice)
		{
			this.player = player;
			this.startingPrice = startingPrice;
		}

        internal void SellPlayerAuctionHouse(DateTime endOfAuctionHouse)
		{
			AuctionHouseServer auctionHouse = new AuctionHouseServer()
			{
				Id = player.Guid,
				Name = player.Name,
				Surname = player.Surname,
				TeamName = player.TeamName,
				SoldToTeam = player.TeamName,
				Price = startingPrice,
                EndAuction= endOfAuctionHouse
			};

			Entity.JpEntity.AuctionHouses.AddObject(auctionHouse);
			Entity.JpEntity.SaveChanges();

            // TODO: Aggiornare la tabella delle News
		}

		internal bool MakeAnOffer(int offer, string teamNameOfBuyer)
		{
			AuctionHouseServer playerOnSell = Entity.JpEntity.AuctionHouses.Select(x => x).Where(k => k.Id == player.Guid).SingleOrDefault();
			if (playerOnSell == null)
				return false;
			
			if (playerOnSell.Price >= offer)
				return false;

			playerOnSell.Price = offer;
			playerOnSell.SoldToTeam = teamNameOfBuyer;

			Entity.JpEntity.SaveChanges();

            // TODO: Aggiornare la tabella delle News

			return true;
		}

		internal Dictionary<PlayerClient, AuctionHouseClient> GetAllPlayersOnAuctionHouse()
		{			
			IEnumerable<AuctionHouseServer> allOnAuction = Entity.JpEntity.AuctionHouses.Select(x => x).Where(d => d.EndAuction >= DateTime.UtcNow);
			if (allOnAuction.Count() == 0)
				return null;

			Dictionary<PlayerClient, AuctionHouseClient> allPlayers = new Dictionary<PlayerClient, AuctionHouseClient>();
			foreach (AuctionHouseServer ah in allOnAuction)
			{
				AuctionHouseClient ahc = new AuctionHouseClient() 
				{
					ActualTeam = ah.SoldToTeam,
					OriginalTeam = ah.TeamName,
					CurrentOffer = (int)ah.Price,
					PlayerId = ah.Id,
					PlayerName = ah.Name,
					PlayerSurname = ah.Surname,
					EndAuction = DateTime.Parse(ah.EndAuction.ToString())
				};

				allPlayers.Add(GetPlayer(ah.Id), ahc);
			}				

			return allPlayers;
		}

		private PlayerClient GetPlayer(Guid playerId)
		{
			RegistrationImplementation reg = new RegistrationImplementation();
			Giocatori g = Entity.JpEntity.Giocatoris.AsParallel().Select(x => x).Where(i => i.GuidGiocatore == playerId).SingleOrDefault();

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