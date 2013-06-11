using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Jalkapallo_Shared;

namespace JalkapalloWorkerRoleWCF.Engine
{
    public class BudgetEvaluator
    {
        Team _currentTeam = null;

        int maxStipendioWeek = 2000;
        int minStipendioWeek = 35;
        int mediaSkillTeamBase = 50;

        public BudgetEvaluator(Team team)
        {
            _currentTeam = team;
        }

        public Team ComputeBudget()
        {
            float budgetPlayers = 0;

            foreach (Player p in _currentTeam.TeamPlayers)
            {
                float totSkill = 0;
                totSkill += p.Skills.Attacco;
                totSkill += p.Skills.Centrocampo;
                totSkill += p.Skills.Difesa;
                totSkill += p.Skills.Exp;
                totSkill += p.Skills.Parata;
                totSkill += p.Skills.Tiro;
                totSkill += p.Skills.Velocita;
                float mediaSkill = totSkill / 7;

                p.Stipendio = EvaluateStipendioPlayer(mediaSkill);

            }

            foreach (Player p in _currentTeam.TeamPlayers)
                budgetPlayers += p.Stipendio;


            //da togliere spese stadio? da aggiungere entrate sponsor?
            _currentTeam.Budget = _currentTeam.Budget - budgetPlayers;

            return _currentTeam;
        }

        //valutazione su mediaskill
        private int EvaluateStipendioPlayer(float mediaSkill)
        {
            int stipendio = 0;

            //se la media del giocatore è meno di 51 allora il suo stipendio è il minimo
            if (mediaSkill <= mediaSkillTeamBase + 1)
                return minStipendioWeek;

            //Normalizzo stipendi a skill:
            //normalizzo stipendo max
            int stipendioNormalizzatoMax = maxStipendioWeek;
            //normalizzo skill max
            int skillNormalizzataMax = 100 - mediaSkillTeamBase;
            //normalizzo skill giocatore
            float skillNormalizzataPlayer = mediaSkill - mediaSkillTeamBase;

            //Trovo stipendio
            stipendio = ((int)skillNormalizzataPlayer * stipendioNormalizzatoMax) / skillNormalizzataMax;

            return stipendio;
        }

    }
}