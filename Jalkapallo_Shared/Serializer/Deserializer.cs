using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Jalkapallo_Shared
{
	public class Deserializer
	{
		public static T DeserializeObject<T>(string source)
		{
			if (source == null)
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