using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LINQ2Entities;

namespace JalkapalloWebRole
{
	public class SearchEngineImplementation
	{
		internal string GetAll()
		{
			Dictionary<string, List<string>> searchInfo = new Dictionary<string, List<string>>();
			IQueryable<string> allSquadre = Entity.JpEntity.Squadres.Select(x => x.TeamName);
			IQueryable<Giocatori> allGiocatori = Entity.JpEntity.Giocatoris.Select(x => x);

			foreach (string squadra in allSquadre)
			{
				List<string> giocatoriSquadra = new List<string>();
				foreach (Giocatori giocatore in allGiocatori)
				{
					if (string.Equals(squadra, giocatore.TeamName) == false)
						continue;

					giocatoriSquadra.Add(giocatore.Nome + " " + giocatore.Surname);
				}
				searchInfo.Add(squadra, giocatoriSquadra);
			}

			return Serializer.SerializeObject<Dictionary<string, List<string>>>(searchInfo);
		}
	}
}