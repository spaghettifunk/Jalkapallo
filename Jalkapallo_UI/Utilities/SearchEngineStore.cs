using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.IO.IsolatedStorage;

namespace Jalkapallo_UI
{
	public static class SearchEngineStore
	{
		private const string FILENAME = "Jalkapallo_SearchInfo.xml";

		public static void Store<T>(T obj)
		{
			IsolatedStorageFile appStore = IsolatedStorageFile.GetUserStoreForApplication();
			using (IsolatedStorageFileStream fileStream = appStore.OpenFile(FILENAME, FileMode.Create))
			{
				DataContractSerializer serializer = new DataContractSerializer(typeof(T));
				serializer.WriteObject(fileStream, obj);
			}
		}

		public static T Retrieve<T>()
		{
			T obj = default(T);
			IsolatedStorageFile appStore = IsolatedStorageFile.GetUserStoreForApplication();
			if (appStore.FileExists(FILENAME))
			{
				using (IsolatedStorageFileStream fileStream = appStore.OpenFile(FILENAME, FileMode.Open))
				{
					DataContractSerializer serializer = new DataContractSerializer(typeof(T));
					obj = (T)serializer.ReadObject(fileStream);
				}
			}
			return obj;
		}
	}
}
