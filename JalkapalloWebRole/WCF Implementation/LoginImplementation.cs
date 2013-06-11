using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Data.Objects;
using System.Diagnostics;

using LINQ2Entities;

namespace JalkapalloWebRole
{
	public class LoginImplementation
	{
		private string username;
		private string password;

		public LoginImplementation(string username, string password)
		{			
			this.username = username;
			this.password = password;
		}

		public string LoginInto()
		{
			IEnumerable<User> queryResult = from user in Entity.JpEntity.Users
											where user.Username == username && user.Password == password
											select user;

			User userLoginInto = queryResult.FirstOrDefault();

			if (userLoginInto != null)
				return userLoginInto.TeamName;
			
			return string.Empty;
		}
	}
}