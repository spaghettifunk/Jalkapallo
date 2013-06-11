using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;
using System.Data.Common;

using Jalkapallo_Shared;
using LINQ2Entities;

using MatchClient = Jalkapallo_Shared.Match;
using MatchServer = LINQ2Entities.Match;
using CoachServer = LINQ2Entities.Coach;

namespace JalkapalloWebRole
{
    public class TeamImplementation
    {
        private DbTransaction transaction = null;
        //private int entityCounter;
        private string teamName;

        public TeamImplementation(string teamName)
        {
            this.teamName = teamName;
        }

        public Team GetTeamFromDB()
        {
            IQueryable<Squadre> team = Entity.JpEntity.Squadres.Select(x => x).Where(s => s.TeamName == teamName);
            Squadre actualTeam = team.FirstOrDefault();

            return (Team)Serializer.DeserializeObject<Team>(actualTeam.TeamObject);
        }

        private List<string> GetMatchCardsFromString(string list)
        {
            List<string> result = new List<string>();
            string[] words = list.Split(';');

            foreach (string w in words)
                result.Add(w);
            return result;
        }

        public void SaveCurrentTeam(List<Player> currentFormation)
        {
            string formation = Serializer.SerializeObject<List<Player>>(currentFormation);

            DO_IT_AGAIN:
            try
            {
                //entityCounter = Entity.Counter;
				Entity.JpEntity = new JpEntities();
                using (transaction = Entity.JpEntity.Connection.BeginTransaction())
                {
                    //Entity.Counter = 0;

                    IQueryable<Squadre> team = Entity.JpEntity.Squadres.Select(x => x).Where(s => s.TeamName == teamName);
                    Squadre foundTeam = team.FirstOrDefault();
                    foundTeam.Formazione = formation;

                    Entity.JpEntity.SaveChanges();
                    transaction.Commit();

                    //Entity.Counter = entityCounter;
                }
            }
            catch
            {
                transaction.Rollback();
                goto DO_IT_AGAIN;
            }
        }

        public List<Player> GetSavedTeam(string teamName)
        {
            IQueryable<Squadre> team = Entity.JpEntity.Squadres.Select(x => x).Where(s => s.TeamName == teamName);
            Squadre foundTeam = team.FirstOrDefault();

            return (List<Player>)Serializer.DeserializeObject<List<Player>>(foundTeam.Formazione);
        }

        public void SetTrainingType(string trainingType)
        {
            DO_IT_AGAIN:
            try
            {
                //entityCounter = Entity.Counter;
				Entity.JpEntity = new JpEntities();
                using (transaction = Entity.JpEntity.Connection.BeginTransaction())
                {
                    //Entity.Counter = 0;

                    CoachServer coach = Entity.JpEntity.Coaches.AsParallel().Select(x => x).Where(c => c.TeamName == teamName).SingleOrDefault();
                    coach.TrainingType = trainingType;

                    Entity.JpEntity.SaveChanges();
                    transaction.Commit();

                    //Entity.Counter = entityCounter;
                }
            }
            catch
            {
                transaction.Rollback();
                goto DO_IT_AGAIN;
            }
        }
    }
}