using MCore.Server.Entity.Memory.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCore.Server.Entity.Memory
{
	public abstract class MemoryEntity
	{
		public static Entity Instance { get; } = GetMemoryImpl();

		private static Entity GetMemoryImpl()
		{
			return new MySqlEntities();
		}
	}
}
