using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQ2Entities
{
	public static class Entity
	{
		private static JpEntities _jpEntity = null;
		public static JpEntities JpEntity
		{
			get
			{
				if (_jpEntity == null)			
					_jpEntity = new JpEntities();

				return _jpEntity;
			}
			set { _jpEntity = value; }
		}
	}
}
