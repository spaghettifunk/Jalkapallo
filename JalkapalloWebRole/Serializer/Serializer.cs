using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace JalkapalloWebRole
{
	public class Serializer
	{
		public static string SerializeObject<T>(object obj)
		{
			if (obj == null)
				return null;

			DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
			using (MemoryStream ms = new MemoryStream())
			{
				serializer.WriteObject(ms, obj);
				return Encoding.Default.GetString(ms.ToArray());
			}
		}

		public static T DeserializeObject<T>(string source)
		{
			if (string.IsNullOrEmpty(source) == true)
				return default(T);

			T obj = Activator.CreateInstance<T>();
			using (MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(source)))
			{
				DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
				obj = (T)serializer.ReadObject(ms);
				return obj;
			}
		}
	}
}