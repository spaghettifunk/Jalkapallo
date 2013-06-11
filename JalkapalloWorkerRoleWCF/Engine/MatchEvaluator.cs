using System;
using System.Net;
using Jalkapallo_Shared;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using MatchClient = Jalkapallo_Shared.Match;

namespace JalkapalloWorkerRoleWCF
{
    public class MatchEvaluator
    {
        #region Properties

		private MatchClient _currentMatch;
        private Team home, away;
        private int countDfHome, countCcHome, countAtHome; //conto i giocatori in ogni reparto per squadra casa 
        private int countDfAway, countCcAway, countAtAway; //conto i giocatori in ogni reparto per squadra ospite
        private float skillVelHome, skillTiroHome, skillExpHome, skillDfHome, skillCcHome, skillAtHome; //somma delle abilità dei reparti casa 
        private float skillVelAway, skillTiroAway, skillExpAway, skillDfAway, skillCcAway, skillAtAway; //somma delle abilità dei reparti fuoricasa 

        #endregion

		public MatchEvaluator(MatchClient currentMatch)
        {
            _currentMatch = currentMatch;
        }

		public MatchClient ComputeMatch()
        {
            //init strutture dati temporanee
            List<Goal> tempGoalList = new List<Goal>();
            List<Player> ammoniti = new List<Player>();
            List<Player> espulsi = new List<Player>();
            int punteggioHome = 0;
            int punteggioAway = 0;
            home = _currentMatch.Home;
            away = _currentMatch.Away;

            //INIZIO ENGINE
            EvaluateFormation(home.Formation, away.Formation);
            SkillCount(home, away);

            //set ammoniti
            SetAmmoniti(ref ammoniti, home, away);
            //set espulsi
            SetEspulsi(ref espulsi, ref ammoniti, home, away);
            //set rigori/punizioni
            SetCalciPiazzati(home, away, ref tempGoalList);
            //set goal su azione 
            SetAzioni(home, away, ref tempGoalList);
            //autogoal
            SetAutogoal(home, away, ref tempGoalList);

            //set del match con i nuovi parametri e uscita
            foreach (Goal g in tempGoalList)
            {
                if (g.Team.Name.Equals(home.Name))
                    punteggioHome++;
                else
                    punteggioAway++;
            }

            _currentMatch.MatchGoals = tempGoalList;
            _currentMatch.ResultHome = punteggioHome;
            _currentMatch.ResultAway = punteggioAway;
            _currentMatch.RedCard = espulsi;
            _currentMatch.YellowCard = ammoniti;
            Debug.Assert(_currentMatch.MatchGoals.Count == punteggioAway + punteggioHome);
            return _currentMatch;
        }

        #region MatchEvents

        private void SetAmmoniti(ref List<Player> ammoniti, Team home, Team away)
        {
            Random rand = new Random(DateTime.Now.Millisecond);

            //HOME**************************
            double homeValue = rand.NextDouble();
            if (homeValue >= 0.25 && homeValue < 0.5)
            {
                //un ammonito
                int i = rand.Next(1, 12);
                ammoniti.Add(home.TeamPlayers[i]);
            }
            if (homeValue >= 0.05 && homeValue < 0.25)
            {
                //due ammoniti
                int i = rand.Next(1, 12);
                ammoniti.Add(home.TeamPlayers[i]);
                i = rand.Next(1, 12);
                ammoniti.Add(home.TeamPlayers[i]);
            }
            if (homeValue < 0.05)
            {
                // tre ammoniti
                int i = rand.Next(1, 12);
                ammoniti.Add(home.TeamPlayers[i]);
                int j = rand.Next(1, 12);
                ammoniti.Add(home.TeamPlayers[j]);
                int k = rand.Next(1, 12);
                if (k != j && k != i)
                    ammoniti.Add(home.TeamPlayers[k]);
            }

            //AWAY****************************
            double awayValue = rand.NextDouble();
            if (awayValue >= 0.25 && awayValue < 0.5)
            {
                //un ammonito
                int i = rand.Next(1, 12);
                ammoniti.Add(away.TeamPlayers[i]);
            }
            if (awayValue >= 0.05 && awayValue < 0.25)
            {
                //due ammoniti
                int i = rand.Next(1, 12);
                ammoniti.Add(away.TeamPlayers[i]);
                i = rand.Next(1, 12);
                ammoniti.Add(away.TeamPlayers[i]);
            }
            if (awayValue < 0.05)
            {
                // tre ammoniti
                int i = rand.Next(1, 12);
                ammoniti.Add(away.TeamPlayers[i]);
                int j = rand.Next(1, 12);
                ammoniti.Add(away.TeamPlayers[j]);
                int k = rand.Next(1, 12);
                if (k != j && k != i)
                    ammoniti.Add(away.TeamPlayers[k]);
            }

            return;
        }

        private void SetEspulsi(ref List<Player> espulsi, ref List<Player> ammoniti, Team home, Team away)
        {
            Random rand = new Random(DateTime.Now.Millisecond);

            //DA CONTROLLARE DOPPIA AMMONIZIONE
            IEnumerable<Player> duplicatedPlayers = ammoniti.GroupBy(x => x.Guid).Where(g => g.Count() > 1).SelectMany(r => r).Distinct();
            foreach (Player p in duplicatedPlayers)
                espulsi.Add(p);

            if (rand.NextDouble() > 0.92)
            {
                int i = rand.Next(1, 12);
                espulsi.Add(home.TeamPlayers[i]);
            }
            if (rand.NextDouble() > 0.92)
            {
                int i = rand.Next(1, 12);
                espulsi.Add(away.TeamPlayers[i]);
            }
            return;
        }

        private void SetAutogoal(Team home, Team away, ref List<Goal> tempGoalList)
        {
            Random rand = new Random(DateTime.Now.Millisecond);

            Player portiereAway = away.TeamPlayers[0];
            Player portiereHome = home.TeamPlayers[0];

            if (rand.NextDouble() < 0.05)
            {
                Player temp = new Player();
                temp = home.TeamPlayers[rand.Next(0, 12)];
                if (temp.Skills.Exp < portiereAway.Skills.Exp)
                {
                    Goal goal = new Goal();
                    goal.GoalType = GoalType.Autogol;
                    goal.Markers = temp;
                    goal.MinutesGoal = rand.Next(0, 90);
                    goal.Team = home;
                    tempGoalList.Add(goal);
                }
            }

            if (rand.NextDouble() < 0.05)
            {
                Player temp = new Player();
                temp = away.TeamPlayers[rand.Next(0, 12)];
                if (temp.Skills.Exp < portiereHome.Skills.Exp)
                {
                    Goal goal = new Goal();
                    goal.GoalType = GoalType.Autogol;
                    goal.Markers = temp;
                    goal.MinutesGoal = rand.Next(0, 90);
                    goal.Team = away;
                    tempGoalList.Add(goal);
                }
            }
        }

        private void SetAzioni(Team home, Team away, ref List<Goal> tempGoalList)
        {
            Random rand = new Random(DateTime.Now.Millisecond);

            Player portiereAway = away.TeamPlayers[0];
            Player portiereHome = home.TeamPlayers[0];

            #region Valutazione Centrocampo
            if (rand.NextDouble() > 0.3)
            {
                if (skillCcHome > skillCcAway && rand.NextDouble() > 0.7)
                {
                    Goal goal = new Goal();
                    goal.GoalType = GetActionGoalType(rand);
                    //voglio prendere solo i cc
                    int lb = countDfHome + 1;
                    int ub = countDfHome + countCcHome + 1;
                    goal.Markers = home.TeamPlayers[rand.Next(lb, ub)];
                    goal.MinutesGoal = rand.Next(1, 90);
                    goal.Team = home;
                    tempGoalList.Add(goal);
                }
            }
            if (rand.NextDouble() > 0.3)
            {
                if (skillCcAway > skillCcHome && rand.NextDouble() > 0.7)
                {
                    Goal goal = new Goal();
                    goal.GoalType = GetActionGoalType(rand);
                    //voglio prendere solo i cc
                    int lb = countDfAway + 1;
                    int ub = countDfAway + countCcAway + 1;
                    goal.Markers = away.TeamPlayers[rand.Next(lb, ub)];
                    goal.MinutesGoal = rand.Next(1, 90);
                    goal.Team = away;
                    tempGoalList.Add(goal);
                }
            }
            #endregion

            #region Contropiede
            if (rand.NextDouble() > 0.5)
            {
                if (skillAtHome / 11 > skillDfAway / 11 && rand.NextDouble() > 0.4)
                {
                    Goal goal = new Goal();
                    goal.GoalType = GetActionGoalType(rand);
                    //voglio prendere solo gli attaccanti
                    int lb = countDfHome + countCcHome + 1;
                    int ub = 12;
                    goal.Markers = home.TeamPlayers[rand.Next(lb, ub)];
                    goal.MinutesGoal = rand.Next(1, 90);
                    goal.Team = home;
                    tempGoalList.Add(goal);
                }
            }
            if (rand.NextDouble() > 0.5)
            {
                if (skillAtAway / 11 > skillDfHome / 11 && rand.NextDouble() > 0.4)
                {
                    Goal goal = new Goal();
                    goal.GoalType = GetActionGoalType(rand);
                    //voglio prendere solo gli attaccanti
                    int lb = countDfAway + countCcAway + 1;
                    int ub = 12;
                    goal.Markers = away.TeamPlayers[rand.Next(lb, ub)];
                    goal.MinutesGoal = rand.Next(1, 90);
                    goal.Team = away;
                    tempGoalList.Add(goal);
                }
            }
            #endregion

            #region Tiro da fuori
            if (rand.NextDouble() > 0.7)
            {
                if (skillTiroHome / 11 > portiereAway.Skills.Parata && rand.NextDouble() > 0.5)
                {
                    Goal goal = new Goal();
                    goal.GoalType = GetActionGoalType(rand);
                    //voglio prendere solo i cc
                    int lb = countDfHome + countCcHome + 1;
                    int ub = 12;
                    goal.Markers = home.TeamPlayers[rand.Next(lb, ub)];
                    goal.MinutesGoal = rand.Next(1, 90);
                    goal.Team = home;
                    tempGoalList.Add(goal);
                }
            }

            if (rand.NextDouble() > 0.7)
            {
                if (skillTiroAway / 11 > portiereHome.Skills.Parata && rand.NextDouble() > 0.5)
                {
                    Goal goal = new Goal();
                    goal.GoalType = GetActionGoalType(rand);
                    //voglio prendere solo i cc
                    int lb = countDfHome + countCcHome + 1;
                    int ub = 12;
                    goal.Markers = away.TeamPlayers[rand.Next(lb, ub)];
                    goal.MinutesGoal = rand.Next(1, 90);
                    goal.Team = away;
                    tempGoalList.Add(goal);
                }
            }
            #endregion

            #region Calcio Angolo
            if (rand.NextDouble() > 0.8)
            {
                Player temp = home.TeamPlayers[rand.Next(1, 12)];

                if (temp.Skills.Exp > portiereAway.Skills.Exp && temp.Skills.Velocita > portiereAway.Skills.Velocita)
                {
                    Goal goal = new Goal();
                    goal.GoalType = GoalType.Testa;
                    goal.Markers = temp;
                    goal.MinutesGoal = rand.Next(1, 90);
                    goal.Team = home;
                    tempGoalList.Add(goal);
                }
            }
            if (rand.NextDouble() > 0.8)
            {
                Player temp = away.TeamPlayers[rand.Next(1, 12)];

                if (temp.Skills.Exp > portiereHome.Skills.Exp && temp.Skills.Velocita > portiereHome.Skills.Velocita)
                {
                    Goal goal = new Goal();
                    goal.GoalType = GoalType.Testa;
                    goal.Markers = temp;
                    goal.MinutesGoal = rand.Next(1, 90);
                    goal.Team = away;
                    tempGoalList.Add(goal);
                }
            }
            #endregion

            #region Agilità
            if (rand.NextDouble() > 0.4)
            {
                if (skillVelHome > skillVelAway && skillExpHome > skillExpAway)
                {
                    Goal goal = new Goal();
                    goal.GoalType = GetActionGoalType(rand);
                    goal.Markers = home.TeamPlayers[rand.Next(1, 12)];
                    goal.MinutesGoal = rand.Next(1, 90);
                    goal.Team = home;
                    tempGoalList.Add(goal);
                }
            }
            if (rand.NextDouble() > 0.4)
            {
                if (skillVelAway > skillVelHome && skillExpHome > skillExpAway)
                {
                    Goal goal = new Goal();
                    goal.GoalType = GetActionGoalType(rand);
                    goal.Markers = away.TeamPlayers[rand.Next(1, 12)];
                    goal.MinutesGoal = rand.Next(1, 90);
                    goal.Team = away;
                    tempGoalList.Add(goal);
                }
            }
            #endregion

        }

        private void SetCalciPiazzati(Team home, Team away, ref List<Goal> tempGoalList)
        {
            Random rand = new Random(DateTime.Now.Millisecond);

            Player tiratoreHome = new Player();
            Player tiratoreAway = new Player();
            Player portiereAway = away.TeamPlayers[0];
            Player portiereHome = home.TeamPlayers[0];

            #region Set Tiratori
            float max = 0;
            for (int i = 0; i < 11; i++)
            {
                if (home.TeamPlayers[i].Skills.Tiro > max)
                {
                    max = home.TeamPlayers[i].Skills.Tiro;
                    tiratoreHome = home.TeamPlayers[i];
                }
                i++;
            }
            max = 0;
            for (int i = 0; i < 11; i++)
            {
                if (away.TeamPlayers[i].Skills.Tiro > max)
                {
                    max = away.TeamPlayers[i].Skills.Tiro;
                    tiratoreAway = away.TeamPlayers[i];
                }
                i++;
            }
            #endregion

        CALCIOPIAZZATO:

            if (rand.NextDouble() > 0.6f)
            {
                if (tiratoreHome.Skills.Tiro > portiereAway.Skills.Parata)
                {
                    if (tiratoreHome.Skills.Exp > portiereAway.Skills.Exp)
                    {
                        Goal goal = new Goal();
                        if (rand.NextDouble() > 0.5)
                            goal.GoalType = GoalType.Punizione;
                        else
                            goal.GoalType = GoalType.Rigore;
                        goal.Markers = tiratoreHome;
                        goal.MinutesGoal = rand.Next(1, 90);
                        goal.Team = home;
                        tempGoalList.Add(goal);
                    }
                }
            }

            if (rand.NextDouble() > 0.6f)
            {
                if (tiratoreAway.Skills.Tiro > portiereHome.Skills.Parata)
                {
                    if (tiratoreAway.Skills.Exp > portiereHome.Skills.Exp)
                    {
                        Goal goal = new Goal();
                        if (rand.NextDouble() > 0.5)
                            goal.GoalType = GoalType.Punizione;
                        else
                            goal.GoalType = GoalType.Rigore;
                        goal.Markers = tiratoreAway;
                        goal.MinutesGoal = rand.Next(1, 90);
                        goal.Team = away;
                        tempGoalList.Add(goal);
                    }
                }
            }

            if (rand.NextDouble() > 0.75)
            {
                goto CALCIOPIAZZATO;
            }
        }

        #endregion

        #region Utility

        private GoalType GetActionGoalType(Random rand)
        {
            if (rand.NextDouble() > 0.8)
                return GoalType.Testa;
            else
                return GoalType.Tiro;
        }

        private void SkillCount(Team home, Team away)
        {
            for (int i = 0; i < 11; i++)
            {
                skillVelHome += home.TeamPlayers[i].Skills.Velocita;
                skillTiroHome += home.TeamPlayers[i].Skills.Tiro;
                skillExpHome += home.TeamPlayers[i].Skills.Exp;
                skillDfHome += home.TeamPlayers[i].Skills.Difesa;
                skillCcHome += home.TeamPlayers[i].Skills.Centrocampo;
                skillAtHome += home.TeamPlayers[i].Skills.Attacco;
                i++;
            }
            for (int i = 0; i < 11; i++)
            {
                skillVelAway += away.TeamPlayers[i].Skills.Velocita;
                skillTiroAway += away.TeamPlayers[i].Skills.Tiro;
                skillExpAway += away.TeamPlayers[i].Skills.Exp;
                skillDfAway += away.TeamPlayers[i].Skills.Difesa;
                skillCcAway += away.TeamPlayers[i].Skills.Centrocampo;
                skillAtAway += away.TeamPlayers[i].Skills.Attacco;
                i++;
            }
        }

        private void EvaluateFormation(int F1, int F2)
        {
            string formationHome = F1.ToString();
            countDfHome = (int)Char.GetNumericValue(formationHome[0]);
            countCcHome = (int)Char.GetNumericValue(formationHome[1]);
            countAtHome = (int)Char.GetNumericValue(formationHome[2]);

            string formationAway = F2.ToString();
            countDfAway = (int)Char.GetNumericValue(formationAway[0]);
            countCcAway = (int)Char.GetNumericValue(formationAway[1]);
            countAtAway = (int)Char.GetNumericValue(formationAway[2]);
        }

        #endregion

    }
}
