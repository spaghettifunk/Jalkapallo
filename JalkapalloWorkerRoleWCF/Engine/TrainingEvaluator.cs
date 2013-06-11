using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Jalkapallo_Shared;
using LINQ2Entities;

namespace JalkapalloWorkerRoleWCF
{
    public class TrainingEvaluator
    {
        #region Properties

        private Team _currentTeam;

        private const int partiteSettimanali = 2;
        private const int giorniAnno = 73;
        private const int settimaneAnno = 10;

        private const int partiteAnno = settimaneAnno * partiteSettimanali;
        private List<float> maxValueYearAge = new List<float>();

        private const int MaxAgeTraining = 42;

        private List<int> ages = new List<int>();
        private List<float> values= new List<float>(); // contiene i valori di allenamento
        private Dictionary<int, float> trainingValues = new Dictionary<int,float>(); //associo età con valore dell'allenamento 

        #endregion

        public TrainingEvaluator(Team currentTeam)
        {
            _currentTeam = currentTeam;

            //popolo età da 17 a 42 anni (età utili per allenamento)
            for (int i = 0; i < 25; i++)
                ages.Add(17 + i);
        }

        public Team ComputeTraining()
        {
			IQueryable<Giocatori> giocatori = Entity.JpEntity.Giocatoris.Select(x => x).Where(t => t.TeamName == _currentTeam.Name);

            //variabili locali di supporto
            Jalkapallo_Shared.Coach coach = _currentTeam.Coach;
            List<Player> players = _currentTeam.TeamPlayers;

            //popolo values (contando esperienza coach come moltiplicatore)
            for (int i = 0; i < 25; i++)
                values.Add(GetTrainingFunction(i));

            //associo alla tupla
            for (int i = 0; i < 25; i++)
                trainingValues.Add(ages[i], values[i]);

            //per ogni giocatore nell'abilità in cui alleno ci incremento il valore dell'allenamento
            foreach (Player p in players)
            {
				Giocatori g = giocatori.Select(x => x).Where(n => n.GuidGiocatore == p.Guid).SingleOrDefault();

                //switch su un nuovo campo del COACH (trainingType come sopra, può essere anche un intero e poi associo qui alla skill che sto allenando)
                //per ora esempio su ATTACCO
                int age = getPlayerAge(p);

                //se ho meno di 42 anni prendo l'allenamento
                if (age < MaxAgeTraining)
                {
                    switch (coach.TrainingType)
                    {
                        case "Goalkeeping":
                            p.Skills.Parata += trainingValues[age];
							g.Parata = (double)p.Skills.Parata;
                            break;
                        case "Defense":
                            p.Skills.Difesa += trainingValues[age];
							g.Difesa = (double)p.Skills.Difesa;
                            break;
                        case "Midfield":
                            p.Skills.Centrocampo += trainingValues[age];
							g.Centrocampo = (double)p.Skills.Centrocampo;
                            break;
                        case "Attack":
                            p.Skills.Attacco += trainingValues[age];
							g.Attacco = (double)p.Skills.Attacco;
                            break;
                        case "Shooting":
                            p.Skills.Tiro += trainingValues[age];
							g.Tiro = (double)p.Skills.Tiro;
                            break;
                        case "Speed":
                            p.Skills.Velocita += trainingValues[age];
							g.Velocita = (double)p.Skills.Velocita;
                            break;
                        default:
                            p.Skills.Parata += trainingValues[age];
							g.Parata = (double)p.Skills.Parata;
                            break;
                    }
                }

				Entity.JpEntity.SaveChanges();
            }

            //Uscita
            _currentTeam.TeamPlayers = players;
            return _currentTeam;
        }

        private float GetTrainingFunction(int age)
        {
            maxValueYearAge.Add(4);
            maxValueYearAge.Add(4);
            maxValueYearAge.Add(4);
            maxValueYearAge.Add(4);
            maxValueYearAge.Add(4);
            maxValueYearAge.Add(3);
            maxValueYearAge.Add(2);
            maxValueYearAge.Add(2);
            maxValueYearAge.Add(2);
            maxValueYearAge.Add(1);
            maxValueYearAge.Add(1);
            maxValueYearAge.Add(1);
            maxValueYearAge.Add(1);
            maxValueYearAge.Add(0.5f);
            maxValueYearAge.Add(0.5f);
            maxValueYearAge.Add(0.3f);
            maxValueYearAge.Add(0.3f);
            maxValueYearAge.Add(0.2f);
            maxValueYearAge.Add(0.2f);
            maxValueYearAge.Add(0.2f);
            maxValueYearAge.Add(0.2f);
            maxValueYearAge.Add(0.2f);
            maxValueYearAge.Add(0.2f);
            maxValueYearAge.Add(0.1f);
            maxValueYearAge.Add(0.1f);
            maxValueYearAge.Add(0.1f);

            float maxAllenPartita = maxValueYearAge[age] / partiteAnno;
            float allenamentoBase = maxAllenPartita / 2f;
            float allenamentoCoach = allenamentoBase * (float)_currentTeam.Coach.CoachAbility / 100;
            return allenamentoBase + allenamentoCoach;
        }

        //devo testare se funziona!
        private int getPlayerAge(Player p)
        {
            DateTime today = DateTime.Today;
            DateTime playerBirthday = Convert.ToDateTime(p.Birthday);
            int age = today.Year - playerBirthday.Year;
            return age;
        }
    }
}