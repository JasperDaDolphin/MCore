using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCore.Server.Entity.Memory.MySql
{
	class MySqlEntities : Entity
	{
		public  override void ForceSave()
		{
			this.ForceSaveAsync().Wait();
		}

		public  override async Task ForceSaveAsync()
		{
			// Get database and start a transaction
			Database database = MCoreServer.Db.Database;
			DbContextTransaction transaction = database.BeginTransaction();
			try
			{
				// Add users and save changes
				MCoreServer.Db.MUsers.AddRange(MUsers.Instance.GetAllMUsersDictionary().Values);
				MCoreServer.Db.MPlayers.AddRange(MPlayers.Instance.GetAllMPlayersDictionary().Values);

				await MCoreServer.Db.SaveChangesAsync();

				transaction.Commit();
			}
			catch (Exception)
			{
				// Oops, something went wrong, rollback to previous version
				transaction.Rollback();
			}
		}

		public override void Load()
		{
			MUsers.Instance.Load();
			MPlayers.Instance.Load();
		}
	}
}
