using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Data.Common;
using System.Data;

using Jalkapallo_Shared;
using LINQ2Entities;

using CoachClient = Jalkapallo_Shared.Coach;
using CoachServer = LINQ2Entities.Coach;
using MatchClient = Jalkapallo_Shared.Match;
using MatchServer = LINQ2Entities.Match;

namespace JalkapalloWebRole
{
	public class RegistrationImplementation
	{
		private const string DEFAUL_TRAINING_TYPE = "Attack";

		private string username;
		private string password;
		private string email;
		private string teamName;

		private Random randomNumber = new Random(System.DateTime.Now.Millisecond);

		public RegistrationImplementation()
		{
		}

		public RegistrationImplementation(string username, string password, string email, string teamName)
		{
			this.username = username;
			this.password = password;
			this.email = email;
			this.teamName = teamName;
		}

		public int RegisterInto()
		{
			int result = 0;
			Guid registrationID = Guid.NewGuid();

			User newUser = new User { ID = registrationID.ToString(), Username = username, Password = password, Email = email, TeamName = teamName };

		DO_IT_AGAIN:
			try
			{
				using (Entity.JpEntity = new JpEntities())
				{
					Entity.JpEntity.ContextOptions.LazyLoadingEnabled = false;
					Entity.JpEntity.ContextOptions.ProxyCreationEnabled = false;

					#region Registration Queries

					IEnumerable<User> queryResult = Entity.JpEntity.Users.Select(x => x).Where(u => u.Username == username);
					if (queryResult.Count() > 0) { return 1; }

					queryResult = Entity.JpEntity.Users.Select(x => x).Where(u => u.Email == email);
					if (queryResult.Count() > 0) { return 2; }

					queryResult = Entity.JpEntity.Users.Select(x => x).Where(u => u.TeamName == teamName);
					if (queryResult.Count() > 0) { return 3; }

					#endregion

					Entity.JpEntity.Users.AddObject(newUser);
					Entity.JpEntity.SaveChanges();

					if (result == 0)
					{
						CreateTeamInDB(teamName, username);
						AddPlayersToTeam(teamName);
						SaveTeamObjectInJSONOnDB();
					}
				}
			}
			catch
			{
				goto DO_IT_AGAIN;
			}

			return result;
		}

		#region Private Methods

		private void CreateTeamInDB(string teamnName, string userName)
		{
		DO_IT_AGAIN:
			try
			{
				int id = Entity.JpEntity.Squadres.Count();
				Squadre newTeam = new Squadre { TeamName = teamnName, UserName = userName, Id = id + 1, Budget = 50000.0 };

				Entity.JpEntity.Squadres.AddObject(newTeam);
				Entity.JpEntity.SaveChanges();
			}
			catch
			{
				goto DO_IT_AGAIN;
			}
		}

		private void AddPlayersToTeam(string teamName)
		{
			List<FakeName> fakeNamesToDelete = new List<FakeName>();
			List<Giocatori> giocatoriToInsert = new List<Giocatori>();

			int recordsCount = Entity.JpEntity.FakeNames.Count();

			for (int i = 1; i <= 21; i++)
			{

			GET_RANDOM:
				int randomId = (new System.Random().Next(1, recordsCount));
				IEnumerable<FakeName> fakeName = Entity.JpEntity.FakeNames.Select(x => x).Where(k => k.number == randomId);

				Debug.Assert(fakeName != null);
				FakeName newFakeName = fakeName.FirstOrDefault();

				if (newFakeName == null)
					goto GET_RANDOM;

				fakeNamesToDelete.Add(newFakeName);
				PlayerSkills skillsPlayer = new PlayerSkills();
				Giocatori newPlayer = null;
				RolePlayerEnum role;

				#region Set Skills

				switch (i)
				{
					case 1:
					case 12:
						SetSkills(skillsPlayer, RolePlayerEnum.Portiere);
						role = RolePlayerEnum.Portiere;
						break;
					case 2:
					case 3:
					case 4:
					case 5:
					case 13:
					case 14:
					case 15:
						SetSkills(skillsPlayer, RolePlayerEnum.Difensore);
						role = RolePlayerEnum.Difensore;
						break;
					case 6:
					case 7:
					case 8:
					case 9:
					case 16:
					case 17:
					case 18:
						SetSkills(skillsPlayer, RolePlayerEnum.CentroCampista);
						role = RolePlayerEnum.CentroCampista;
						break;
					case 10:
					case 11:
					case 19:
					case 20:
						SetSkills(skillsPlayer, RolePlayerEnum.Attaccante);
						role = RolePlayerEnum.Attaccante;
						break;
					case 21:
						SetSkills(skillsPlayer, RolePlayerEnum.Allenatore);
						role = RolePlayerEnum.Allenatore;
						break;
					default:
						SetSkills(skillsPlayer, RolePlayerEnum.Attaccante);
						role = RolePlayerEnum.Attaccante;
						break;
				}

				#endregion

				#region Create Giocatori and Coach

				if (i == 21)
				{
					CreateAndAddCoach(skillsPlayer, newFakeName);
				}
				else
				{
					newPlayer = new Giocatori
					{
						Id = randomId,
						Nome = newFakeName.givenname,
						Surname = newFakeName.surname,
						TeamName = teamName,
						Parata = skillsPlayer.Parata,
						Difesa = skillsPlayer.Difesa,
						Centrocampo = skillsPlayer.Centrocampo,
						Attacco = skillsPlayer.Attacco,
						Experience = skillsPlayer.Exp,
						Tiro = skillsPlayer.Tiro,
						Velocita = skillsPlayer.Velocita,
						Birthday = Convert.ToString(newFakeName.birthday),
						Country = newFakeName.countryfull,
						Weight = Convert.ToString(newFakeName.kilograms),
						Height = Convert.ToString(newFakeName.centimeters),
						Role = Convert.ToString(role),
						GuidGiocatore = Guid.Parse(newFakeName.guid),
						Stipendio = 35f
					};

					giocatoriToInsert.Add(newPlayer);
				}

				#endregion
			}

			foreach (Giocatori newPlayer in giocatoriToInsert)
			{
				//Entity.Counter = 0;
				Entity.JpEntity.Giocatoris.AddObject(newPlayer);
				Entity.JpEntity.SaveChanges();
			}

			// elimina i nomi dalla tabella fake names
			foreach (FakeName name in fakeNamesToDelete)
			{
				//Entity.Counter = 0;
				Entity.JpEntity.DeleteObject(name);
				Entity.JpEntity.SaveChanges();
			}
		}

		private void CreateAndAddCoach(PlayerSkills skillsPlayer, FakeName newFakeName)
		{
			int id = Entity.JpEntity.Coaches.Count();
			CoachServer newCoach = new CoachServer()
			{
				Attacco = skillsPlayer.Attacco,
				Centrocampo = skillsPlayer.Centrocampo,
				Birthday = newFakeName.birthday.ToString(),
				Coach_Ability = new Random().Next(50, 65),
				Cognome = newFakeName.surname,
				Country = newFakeName.countryfull,
				Difesa = skillsPlayer.Difesa,
				Experience = skillsPlayer.Exp,
				Nome = newFakeName.givenname,
				Parata = skillsPlayer.Parata,
				TeamName = teamName,
				Tiro = skillsPlayer.Tiro,
				Velocita = skillsPlayer.Velocita,
				TrainingType = DEFAUL_TRAINING_TYPE,
				GuidCoach = Guid.Parse(newFakeName.guid),
				Id = id + 1
			};

			Entity.JpEntity.AddToCoaches(newCoach);
			Entity.JpEntity.SaveChanges();
		}

		private CoachClient SetCoachParameters(CoachServer coachFromDb)
		{
			PlayerSkills coachSkills = new PlayerSkills()
			{
				Attacco = (float)coachFromDb.Attacco,
				Difesa = (float)coachFromDb.Difesa,
				Centrocampo = (float)coachFromDb.Centrocampo,
				Exp = (float)coachFromDb.Experience,
				Parata = (float)coachFromDb.Parata,
				Tiro = (float)coachFromDb.Tiro,
				Velocita = (float)coachFromDb.Velocita,
			};

			CoachClient obj = new CoachClient()
			{
				CoachGenericSkills = coachSkills,
				CoachAbility = coachFromDb.Coach_Ability,
				Role = RolePlayerEnum.Allenatore,
				Country = coachFromDb.Country,
				Birthday = coachFromDb.Birthday,
				Guid = Guid.NewGuid(),
				CoachName = coachFromDb.Nome,
				CoachSurname = coachFromDb.Cognome,
				TrainingType = coachFromDb.TrainingType
			};

			return obj;
		}

		public RolePlayerEnum GetPlayerRole(string role)
		{
			switch (role)
			{
				case "Portiere":
					return RolePlayerEnum.Portiere;
				case "Difensore":
					return RolePlayerEnum.Difensore;
				case "CentroCampista":
					return RolePlayerEnum.CentroCampista;
				case "Attaccante":
					return RolePlayerEnum.Attaccante;
				case "Allenatore":
					return RolePlayerEnum.Allenatore;
				default:
					return RolePlayerEnum.Difensore;
			}
		}

		private void SaveTeamObjectInJSONOnDB()
		{
			Squadre squadra = Entity.JpEntity.Squadres.Select(x => x).Where(x => x.TeamName == teamName).FirstOrDefault();

			double budget = 0;
			double? nullableDouble = squadra.Budget;
			if (nullableDouble.HasValue)
				budget = nullableDouble.Value;

			List<Player> teamPlayers = new List<Player>();
			IQueryable<Giocatori> team = Entity.JpEntity.Giocatoris.Select(x => x).Where(g => g.TeamName == teamName);

			#region set players

			// crea gli oggetti Player che servono alla UI per visualizzare le skills
			foreach (Giocatori player in team)
			{
				Player pl = new Player()
				{
					Skills = new PlayerSkills()
					{
						Attacco = (float)player.Attacco,
						Difesa = (float)player.Difesa,
						Centrocampo = (float)player.Centrocampo,
						Exp = (float)player.Experience,
						Parata = (float)player.Parata,
						Tiro = (float)player.Tiro,
						Velocita = (float)player.Velocita,
					},

					Name = player.Nome,
					Surname = player.Surname,
					Role = GetPlayerRole(player.Role),
					Country = player.Country,
					Birthday = player.Birthday,
					Height = (int)Convert.ToDouble(player.Height),
					Weight = (float)Convert.ToDouble(player.Weight),
					TeamName = player.TeamName,
					Guid = Guid.Parse(player.GuidGiocatore.ToString())
				};

				teamPlayers.Add(pl);
			}

			#endregion

			#region set coach

			CoachClient coachObj = null;
			IQueryable<CoachServer> coachFromDb = Entity.JpEntity.Coaches.Select(x => x).Where(g => g.TeamName == teamName);

			coachObj = SetCoachParameters(coachFromDb.FirstOrDefault());

			#endregion

			Team clientTeam = new Team()
			{
				Budget = budget,
				Coach = coachObj,
				Formation = 442,
				League = Guid.NewGuid(),
				Matches = SetMatches(),
				Name = teamName,
				TeamPlayers = teamPlayers
			};

			squadra.TeamObject = Serializer.SerializeObject<Team>(clientTeam);
			squadra.Formazione = Serializer.SerializeObject<List<Player>>(teamPlayers);
			Entity.JpEntity.SaveChanges();
		}

		// aggiustare il metodo aggiungendo anche la RollBack
		private List<MatchClient> SetMatches()
		{
			List<MatchClient> allMatches = new List<MatchClient>();

			#region set matches

			IQueryable<MatchServer> matchesDb = from m in Entity.JpEntity.Matches
												where m.Home == teamName || m.Away == teamName
												select m;

			foreach (MatchServer match in matchesDb)
			{
				MatchClient m = new MatchClient()
				{
					AwayStringFromServer = match.Away,
					HomeStringFromServer = match.Home,
					RedCardStringFromServer = new List<string>(),//GetMatchCardsFromString(match.RedCard),
					ResultAway = 0,//Convert.ToInt16(match.Punteggio_Away),
					ResultHome = 0,//Convert.ToInt16(match.Punteggio_Home),
					YellowCardStringFromServer = new List<string>()//GetMatchCardsFromString(match.YellowCard),
				};

				allMatches.Add(m);
			}

			#endregion

			return allMatches;
		}

		#endregion

		#region SetSKills

		//chiama metodi divers
		private void SetSkills(PlayerSkills skillsPlayer, RolePlayerEnum role)
		{
			//settare le skills iniziali del giocatore al momento della crazione della squadra a seconda del ruolo
			switch (role)
			{
				case RolePlayerEnum.Portiere:
					SetSkillsPortiere(skillsPlayer);
					break;
				case RolePlayerEnum.Difensore:
					SetSkillsDifensore(skillsPlayer);
					break;
				case RolePlayerEnum.CentroCampista:
					SetSkillsCentroCampista(skillsPlayer);
					break;
				case RolePlayerEnum.Attaccante:
					SetSkillsAttacante(skillsPlayer);
					break;
				case RolePlayerEnum.Allenatore:
					SetSkillsAllenatore(skillsPlayer);
					break;
			}
		}

		// TODO: modificare le skills per l'allenatore
		private void SetSkillsAllenatore(PlayerSkills skillsPlayer)
		{
			//set difesa
			double mainSkillMaxValue = 65;
			double mainSkillMinValue = 45;
			//set altre
			double otherSkillMaxValue = 50;
			double otherSkillMinValue = 30;

			PlayerSkills skills = new PlayerSkills();
			List<KeyValuePair<string, float>> skillToBeSet = new List<KeyValuePair<string, float>>();
			List<string> skillLista = new List<string>();

			skillLista.Add(SkillListEnum.parata.ToString());
			skillLista.Add(SkillListEnum.difesa.ToString());
			skillLista.Add(SkillListEnum.centrocampo.ToString());
			skillLista.Add(SkillListEnum.attacco.ToString());
			skillLista.Add(SkillListEnum.tiro.ToString());
			skillLista.Add(SkillListEnum.velocita.ToString());
			skillLista.Add(SkillListEnum.exp.ToString());

			for (int i = 0; i < skillLista.Count - 1; i++)
			{
				//set difesa
				if (i == 1)
				{
					KeyValuePair<string, float> sk = new KeyValuePair<string, float>(skillLista[i], rnd(mainSkillMinValue, mainSkillMaxValue));
					skillToBeSet.Add(sk);
				}
				//set altre
				else
				{
					KeyValuePair<string, float> sk = new KeyValuePair<string, float>(skillLista[i], rnd(otherSkillMinValue, otherSkillMaxValue));
					skillToBeSet.Add(sk);
				}
			}

			float expValue = 0;
			foreach (KeyValuePair<string, float> t in skillToBeSet)
				expValue += t.Value;

			//set exp : per ora è la media dei valori
			KeyValuePair<string, float> exp = new KeyValuePair<string, float>(skillLista[6], (expValue / 5f) + 10);
			skillToBeSet.Add(exp);

			foreach (KeyValuePair<string, float> tupla in skillToBeSet)
			{
				if (tupla.Key.Equals(SkillListEnum.parata.ToString()))
					skillsPlayer.Parata = tupla.Value;
				if (tupla.Key.Equals(SkillListEnum.difesa.ToString()))
					skillsPlayer.Difesa = tupla.Value;
				if (tupla.Key.Equals(SkillListEnum.centrocampo.ToString()))
					skillsPlayer.Centrocampo = tupla.Value;
				if (tupla.Key.Equals(SkillListEnum.attacco.ToString()))
					skillsPlayer.Attacco = tupla.Value;
				if (tupla.Key.Equals(SkillListEnum.tiro.ToString()))
					skillsPlayer.Tiro = tupla.Value;
				if (tupla.Key.Equals(SkillListEnum.velocita.ToString()))
					skillsPlayer.Velocita = tupla.Value;
				if (tupla.Key.Equals(SkillListEnum.exp.ToString()))
					skillsPlayer.Exp = tupla.Value;
			}

		}

		//set come caratteristica primaria l'attacco e il tiro
		private void SetSkillsAttacante(PlayerSkills skillsPlayer)
		{
			//set attacco
			double mainSkillMaxValue = 60;
			double mainSkillMinValue = 45;
			//set tiro
			double secondSkillMaxValue = 55;
			double secondSkillMinValue = 35;
			//set altre
			double otherSkillMaxValue = 47;
			double otherSkillMinValue = 30;

			PlayerSkills skills = new PlayerSkills();
			List<KeyValuePair<string, float>> skillToBeSet = new List<KeyValuePair<string, float>>();
			List<string> skillLista = new List<string>();

			skillLista.Add(SkillListEnum.parata.ToString());
			skillLista.Add(SkillListEnum.difesa.ToString());
			skillLista.Add(SkillListEnum.centrocampo.ToString());
			skillLista.Add(SkillListEnum.attacco.ToString());
			skillLista.Add(SkillListEnum.tiro.ToString());
			skillLista.Add(SkillListEnum.velocita.ToString());
			skillLista.Add(SkillListEnum.exp.ToString());

			for (int i = 0; i < skillLista.Count - 1; i++)
			{
				//set attacco
				if (i == 3)
				{
					KeyValuePair<string, float> sk = new KeyValuePair<string, float>(skillLista[i], rnd(mainSkillMinValue, mainSkillMaxValue));
					skillToBeSet.Add(sk);
				}
				else
				{
					//set tiro (secondaria)
					if (i == 4)
					{
						KeyValuePair<string, float> sk = new KeyValuePair<string, float>(skillLista[i], rnd(secondSkillMinValue, secondSkillMaxValue));
						skillToBeSet.Add(sk);
					}
					//set altre
					else
					{
						KeyValuePair<string, float> sk = new KeyValuePair<string, float>(skillLista[i], rnd(otherSkillMinValue, otherSkillMaxValue));
						skillToBeSet.Add(sk);
					}
				}
			}

			float expValue = 0;
			foreach (KeyValuePair<string, float> t in skillToBeSet)
				expValue += t.Value;

			//set exp : per ora è la media dei valori
			KeyValuePair<string, float> exp = new KeyValuePair<string, float>(skillLista[6], expValue / 5f);
			skillToBeSet.Add(exp);

			foreach (KeyValuePair<string, float> tupla in skillToBeSet)
			{
				if (tupla.Key.Equals(SkillListEnum.parata.ToString()))
					skillsPlayer.Parata = tupla.Value;
				if (tupla.Key.Equals(SkillListEnum.difesa.ToString()))
					skillsPlayer.Difesa = tupla.Value;
				if (tupla.Key.Equals(SkillListEnum.centrocampo.ToString()))
					skillsPlayer.Centrocampo = tupla.Value;
				if (tupla.Key.Equals(SkillListEnum.attacco.ToString()))
					skillsPlayer.Attacco = tupla.Value;
				if (tupla.Key.Equals(SkillListEnum.tiro.ToString()))
					skillsPlayer.Tiro = tupla.Value;
				if (tupla.Key.Equals(SkillListEnum.velocita.ToString()))
					skillsPlayer.Velocita = tupla.Value;
				if (tupla.Key.Equals(SkillListEnum.exp.ToString()))
					skillsPlayer.Exp = tupla.Value;
			}
		}

		//set come caratteristica primaria il centrocampo
		private void SetSkillsCentroCampista(PlayerSkills skillsPlayer)
		{
			//set centrocampo
			double mainSkillMaxValue = 65;
			double mainSkillMinValue = 45;
			//set tiro
			double secondSkillMaxValue = 55;
			double secondSkillMinValue = 35;
			//set altre 
			double otherSkillMaxValue = 50;
			double otherSkillMinValue = 30;

			PlayerSkills skills = new PlayerSkills();
			List<KeyValuePair<string, float>> skillToBeSet = new List<KeyValuePair<string, float>>();
			List<string> skillLista = new List<string>();

			skillLista.Add(SkillListEnum.parata.ToString());
			skillLista.Add(SkillListEnum.difesa.ToString());
			skillLista.Add(SkillListEnum.centrocampo.ToString());
			skillLista.Add(SkillListEnum.attacco.ToString());
			skillLista.Add(SkillListEnum.tiro.ToString());
			skillLista.Add(SkillListEnum.velocita.ToString());
			skillLista.Add(SkillListEnum.exp.ToString());

			for (int i = 0; i < skillLista.Count - 1; i++)
			{
				//set centrocampo
				if (i == 2)
				{
					KeyValuePair<string, float> sk = new KeyValuePair<string, float>(skillLista[i], rnd(mainSkillMinValue, mainSkillMaxValue));
					skillToBeSet.Add(sk);
				}
				else
				{
					//set tiro (secondaria)
					if (i == 4)
					{
						KeyValuePair<string, float> sk = new KeyValuePair<string, float>(skillLista[i], rnd(secondSkillMinValue, secondSkillMaxValue));
						skillToBeSet.Add(sk);
					}
					//set altre
					else
					{
						KeyValuePair<string, float> sk = new KeyValuePair<string, float>(skillLista[i], rnd(otherSkillMinValue, otherSkillMaxValue));
						skillToBeSet.Add(sk);
					}
				}
			}

			float expValue = 0;
			foreach (KeyValuePair<string, float> t in skillToBeSet)
				expValue += t.Value;

			//set exp : per ora è la media dei valori
			KeyValuePair<string, float> exp = new KeyValuePair<string, float>(skillLista[6], expValue / 5f);
			skillToBeSet.Add(exp);

			foreach (KeyValuePair<string, float> tupla in skillToBeSet)
			{
				if (tupla.Key.Equals(SkillListEnum.parata.ToString()))
					skillsPlayer.Parata = tupla.Value;
				if (tupla.Key.Equals(SkillListEnum.difesa.ToString()))
					skillsPlayer.Difesa = tupla.Value;
				if (tupla.Key.Equals(SkillListEnum.centrocampo.ToString()))
					skillsPlayer.Centrocampo = tupla.Value;
				if (tupla.Key.Equals(SkillListEnum.attacco.ToString()))
					skillsPlayer.Attacco = tupla.Value;
				if (tupla.Key.Equals(SkillListEnum.tiro.ToString()))
					skillsPlayer.Tiro = tupla.Value;
				if (tupla.Key.Equals(SkillListEnum.velocita.ToString()))
					skillsPlayer.Velocita = tupla.Value;
				if (tupla.Key.Equals(SkillListEnum.exp.ToString()))
					skillsPlayer.Exp = tupla.Value;
			}
		}

		//set come caratteristica primaria la difesa
		private void SetSkillsDifensore(PlayerSkills skillsPlayer)
		{
			//set difesa
			double mainSkillMaxValue = 65;
			double mainSkillMinValue = 45;
			//set altre
			double otherSkillMaxValue = 50;
			double otherSkillMinValue = 30;

			PlayerSkills skills = new PlayerSkills();
			List<KeyValuePair<string, float>> skillToBeSet = new List<KeyValuePair<string, float>>();
			List<string> skillLista = new List<string>();

			skillLista.Add(SkillListEnum.parata.ToString());
			skillLista.Add(SkillListEnum.difesa.ToString());
			skillLista.Add(SkillListEnum.centrocampo.ToString());
			skillLista.Add(SkillListEnum.attacco.ToString());
			skillLista.Add(SkillListEnum.tiro.ToString());
			skillLista.Add(SkillListEnum.velocita.ToString());
			skillLista.Add(SkillListEnum.exp.ToString());

			for (int i = 0; i < skillLista.Count - 1; i++)
			{
				//set difesa
				if (i == 1)
				{
					KeyValuePair<string, float> sk = new KeyValuePair<string, float>(skillLista[i], rnd(mainSkillMinValue, mainSkillMaxValue));
					skillToBeSet.Add(sk);
				}
				//set altre
				else
				{
					KeyValuePair<string, float> sk = new KeyValuePair<string, float>(skillLista[i], rnd(otherSkillMinValue, otherSkillMaxValue));
					skillToBeSet.Add(sk);
				}
			}

			float expValue = 0;
			foreach (KeyValuePair<string, float> t in skillToBeSet)
				expValue += t.Value;

			//set exp : per ora è la media dei valori
			KeyValuePair<string, float> exp = new KeyValuePair<string, float>(skillLista[6], expValue / 5f);
			skillToBeSet.Add(exp);

			foreach (KeyValuePair<string, float> tupla in skillToBeSet)
			{
				if (tupla.Key.Equals(SkillListEnum.parata.ToString()))
					skillsPlayer.Parata = tupla.Value;
				if (tupla.Key.Equals(SkillListEnum.difesa.ToString()))
					skillsPlayer.Difesa = tupla.Value;
				if (tupla.Key.Equals(SkillListEnum.centrocampo.ToString()))
					skillsPlayer.Centrocampo = tupla.Value;
				if (tupla.Key.Equals(SkillListEnum.attacco.ToString()))
					skillsPlayer.Attacco = tupla.Value;
				if (tupla.Key.Equals(SkillListEnum.tiro.ToString()))
					skillsPlayer.Tiro = tupla.Value;
				if (tupla.Key.Equals(SkillListEnum.velocita.ToString()))
					skillsPlayer.Velocita = tupla.Value;
				if (tupla.Key.Equals(SkillListEnum.exp.ToString()))
					skillsPlayer.Exp = tupla.Value;
			}

		}

		//set come caratteristica primaria la parata
		private void SetSkillsPortiere(PlayerSkills skillsPlayer)
		{
			//set parata
			double mainSkillMaxValue = 60;
			double mainSkillMinValue = 45;
			//set velocità
			double secondSkillMaxValue = 55;
			double secondSkillMinValue = 35;
			//set altre
			double otherSkillMaxValue = 30;
			double otherSkillMinValue = 20;

			PlayerSkills skills = new PlayerSkills();
			List<KeyValuePair<string, float>> skillToBeSet = new List<KeyValuePair<string, float>>();
			List<string> skillLista = new List<string>();

			skillLista.Add(SkillListEnum.parata.ToString());
			skillLista.Add(SkillListEnum.difesa.ToString());
			skillLista.Add(SkillListEnum.centrocampo.ToString());
			skillLista.Add(SkillListEnum.attacco.ToString());
			skillLista.Add(SkillListEnum.tiro.ToString());
			skillLista.Add(SkillListEnum.velocita.ToString());
			skillLista.Add(SkillListEnum.exp.ToString());

			for (int i = 0; i < skillLista.Count - 1; i++)
			{
				//set parata
				if (i == 0)
				{
					KeyValuePair<string, float> sk = new KeyValuePair<string, float>(skillLista[i], rnd(mainSkillMinValue, mainSkillMaxValue));
					skillToBeSet.Add(sk);
				}
				else
				{
					//set velocità (secondaria)
					if (i == 5)
					{
						KeyValuePair<string, float> sk = new KeyValuePair<string, float>(skillLista[i], rnd(secondSkillMinValue, secondSkillMaxValue));
						skillToBeSet.Add(sk);
					}
					//set altre
					else
					{
						KeyValuePair<string, float> sk = new KeyValuePair<string, float>(skillLista[i], rnd(otherSkillMinValue, otherSkillMaxValue));
						skillToBeSet.Add(sk);
					}
				}
			}

			float expValue = 0;
			foreach (KeyValuePair<string, float> t in skillToBeSet)
				expValue += t.Value;

			//set exp : per ora è la media dei valori
			KeyValuePair<string, float> exp = new KeyValuePair<string, float>(skillLista[6], expValue / 5f);
			skillToBeSet.Add(exp);

			foreach (KeyValuePair<string, float> tupla in skillToBeSet)
			{
				if (tupla.Key.Equals(SkillListEnum.parata.ToString()))
					skillsPlayer.Parata = tupla.Value;
				if (tupla.Key.Equals(SkillListEnum.difesa.ToString()))
					skillsPlayer.Difesa = tupla.Value;
				if (tupla.Key.Equals(SkillListEnum.centrocampo.ToString()))
					skillsPlayer.Centrocampo = tupla.Value;
				if (tupla.Key.Equals(SkillListEnum.attacco.ToString()))
					skillsPlayer.Attacco = tupla.Value;
				if (tupla.Key.Equals(SkillListEnum.tiro.ToString()))
					skillsPlayer.Tiro = tupla.Value;
				if (tupla.Key.Equals(SkillListEnum.velocita.ToString()))
					skillsPlayer.Velocita = tupla.Value;
				if (tupla.Key.Equals(SkillListEnum.exp.ToString()))
					skillsPlayer.Exp = tupla.Value;
			}
		}

		//metodo di appoggio per generare i numeri random
		private float rnd(double a, double b)
		{
			return (float)(a + randomNumber.NextDouble() * (b - a));
		}

		#endregion
	}
}