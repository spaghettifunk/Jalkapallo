using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.Linq.SqlClient;
using System.Diagnostics;

using Jalkapallo_Shared;
using LINQ2Entities;

using MatchClient = Jalkapallo_Shared.Match;
using PlayerClient = Jalkapallo_Shared.Player;
using AuctionHouseClient = Jalkapallo_Shared.AuctionHouse;

namespace JalkapalloWebRole
{
    public class WebRoleService : IWebRoleService
    {
        public int Registration(string username, string password, string email, string teamName)
        {
			Entity.JpEntity = new JpEntities();

            RegistrationImplementation registrationImp = new RegistrationImplementation(username, password, email, teamName);
            return registrationImp.RegisterInto();
        }

        // ritorna il team Name
        public string LoginUser(string username, string password)
        {
			Entity.JpEntity = new JpEntities();

            LoginImplementation loginImp = new LoginImplementation(username, password);
            return loginImp.LoginInto();
        }

        // ritorna l'oggetto Team
        public string GetTeam(string teamName)
        {
			Entity.JpEntity = new JpEntities();

            TeamImplementation teamImp = new TeamImplementation(teamName);
            return Serializer.SerializeObject<Team>(teamImp.GetTeamFromDB());
        }

        // salva la formazione corrente in byte[] mettendolo poi nella tabella Squadre
        public void SaveCurrentTeam(List<PlayerClient> currentFormation, string teamName)
        {
			Entity.JpEntity = new JpEntities();

            TeamImplementation teamImp = new TeamImplementation(teamName);
            teamImp.SaveCurrentTeam(currentFormation);
        }

        // recupera l'ultima formazione salvata
        public string GetSavedTeam(string teamName)
        {
			Entity.JpEntity = new JpEntities();

            TeamImplementation teamImp = new TeamImplementation(teamName);
            return Serializer.SerializeObject<List<Player>>(teamImp.GetSavedTeam(teamName));
        }

        // metodo che serve a fare la ricerca
        public string SearchTeams(string teamName)
        {
			Entity.JpEntity = new JpEntities();

            List<Team> teamsClient = new List<Team>();
            ParallelQuery<Squadre> teamsServer = Entity.JpEntity.Squadres.AsParallel().Where(s => s.TeamName.ToLower().Contains(teamName.ToLower()));

            foreach (Squadre s in teamsServer)
                teamsClient.Add(Serializer.DeserializeObject<Team>(GetTeam(s.TeamName)));

            return Serializer.SerializeObject<List<Team>>(teamsClient);
        }

        // metodo che serve a fare la ricerca
        public string SearchPlayers(string playerName)
        {
			Entity.JpEntity = new JpEntities();

            RegistrationImplementation reg = new RegistrationImplementation();
            List<Player> players = new List<Player>();
            ParallelQuery<Giocatori> allGiocatori = Entity.JpEntity.Giocatoris.AsParallel().Where(g => g.Nome.ToLower().Contains(playerName.ToLower()) || g.Surname.ToLower().Contains(playerName.ToLower())).Distinct();

            foreach (Giocatori g in allGiocatori)
            {
                Player p = new Player()
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

                players.Add(p);
            }

            return Serializer.SerializeObject<List<Player>>(players);
        }

        // metodo per la vendita diretta del giocatore
        // TASK:0006 ++ 20130412 DB
        public void SellPlayerDirectly(PlayerClient player, int price)
        {
			Entity.JpEntity = new JpEntities();

            SellingImplementation sellPlayer = new SellingImplementation(player, price);
            sellPlayer.SellPlayer();
        }

        // TASK:0006 ++ 20130412 DB
        public void SellPlayerAuctionHouse(PlayerClient player, int startingPrice, DateTime endOfAuctionHouse)
        {
			Entity.JpEntity = new JpEntities();

            AuctionHouseImplementation auctionHouse = new AuctionHouseImplementation(player, startingPrice);
            auctionHouse.SellPlayerAuctionHouse(endOfAuctionHouse);
        }

        // TASK:0006 ++ 20130412 DB
        public void BuyPlayerDirectly(PlayerClient player, int price, string teamName)
        {
			Entity.JpEntity = new JpEntities();

            SellingImplementation buyPlayer = new SellingImplementation(player, price, teamName);
            buyPlayer.BuyPlayerDirectly();
        }

        // TASK:0006 ++ 20130412 DB
        public bool MakeAnOfferForAPlayer(PlayerClient player, int offer, string teamNameOfBuyer)
        {
			Entity.JpEntity = new JpEntities();

            AuctionHouseImplementation auctionhouse = new AuctionHouseImplementation(player);
            return auctionhouse.MakeAnOffer(offer, teamNameOfBuyer);
        }

        // TASK:0006 ++ 20130412 DB
        public string GetPlayersOnSellDirect()
        {
			Entity.JpEntity = new JpEntities();

            SellingImplementation playersOnSell = new SellingImplementation();
			return Serializer.SerializeObject<Dictionary<PlayerClient, int>>(playersOnSell.GetAllPlayersOnSell());
        }

        // TASK:0006 ++ 20130412 DB
        public string GetPlayersOnSellAuctionHouse()
        {
			Entity.JpEntity = new JpEntities();

            AuctionHouseImplementation playersOnAuctionHouse = new AuctionHouseImplementation();
			return Serializer.SerializeObject<Dictionary<PlayerClient, AuctionHouseClient>>(playersOnAuctionHouse.GetAllPlayersOnAuctionHouse());
        }

        // metodo per il set del training
        public void SetTrainingType(string trainingType, string teamName)
        {
			Entity.JpEntity = new JpEntities();

            TeamImplementation team = new TeamImplementation(teamName);
            team.SetTrainingType(trainingType);
        }

        public string GetAllTeamsAndPlayers()
        {
			Entity.JpEntity = new JpEntities();

            SearchEngineImplementation search = new SearchEngineImplementation();
            return search.GetAll();
        }

        // set the push notification channel uri in the entity
        public void SetPushNotificationChannel(string phoneId, string uri)
        {
			Entity.JpEntity = new JpEntities();

			int count = Entity.JpEntity.PushNotificationTables.Count();
			IEnumerable<PushNotificationTable> old = Entity.JpEntity.PushNotificationTables.Select(x => x).Where(k => string.Equals(k.PhoneID, phoneId) == true);
            if (old.Count() == 0)
            {
                CreateChannelUriEntry(phoneId, uri, count + 1);
                return;
            }

            // update URI per un determinato dispositivo
            if (old.Count() == 1)
            {
                PushNotificationTable p = old.FirstOrDefault();
                p.ChannelUri = uri;
                Entity.JpEntity.SaveChanges();
                return;
            }

			// NON DOVREBBE MAI ACCADERE!!!
			#region Warning!!
			
			if (old.Count() > 1)
            {
                foreach (PushNotificationTable p in old)
                {
                    Entity.JpEntity.PushNotificationTables.DeleteObject(p);
                    Entity.JpEntity.SaveChanges();
                }

                CreateChannelUriEntry(phoneId, uri, count + 1);
			}

			#endregion
		}

        private void CreateChannelUriEntry(string phoneId, string uri, int id)
        {
            PushNotificationTable push = new PushNotificationTable()
            { 
                Id = id,
                ChannelUri = uri,
				PhoneID = phoneId
            };

            Entity.JpEntity.PushNotificationTables.AddObject(push);
            Entity.JpEntity.SaveChanges();
        }
    }
}
