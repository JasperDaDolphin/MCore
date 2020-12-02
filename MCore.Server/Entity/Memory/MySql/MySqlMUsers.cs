using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCore.Server.Entity.Memory.MySql
{
    /// <summary>
    /// A MySQL memory implementation of MUsers
    /// </summary>
    public class MySqlMUsers : MemoryMUsers
    {
        /// <summary>
        /// Force saves users to a MySQL database sync
        /// </summary>
        public override void ForceSave()
        {
            this.ForceSaveAsync().Wait();
        }

        /// <summary>
        /// Force saves users to a MySQL database async
        /// </summary>
        /// <returns>Task responsible</returns>
        public override async Task ForceSaveAsync()
        {
            // Get database and start a transaction
            Database database = MCoreServer.Db.Database;
            DbContextTransaction transaction = database.BeginTransaction();
            try
            {
                Debug.WriteLine(base.MUsers.Count.ToString());
                // Add users and save changes
                MCoreServer.Db.MUsers.AddRange(base.MUsers.Values);
                await MCoreServer.Db.SaveChangesAsync();

                transaction.Commit();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                // Oops, something went wrong, rollback to previous version
                transaction.Rollback();
            }
        }

        /// <summary>
        /// Loads users from a MySQL database
        /// </summary>
        public override void Load()
        {
            // Grab users with same steamid
            List<MUser> users = MCoreServer.Db.MUsers.ToList();

            // Clear users and start adding them
            base.MUsers.Clear();
            foreach (MUser user in users)
            {
                base.MUsers.Add(user.Id, user);
            }
        }

        /// <inheritdoc />
        public override MUser GenerateMUser(Player player)
        {
            Debug.WriteLine(player.Name);
            MUser mUser = new MUser(player);
            base.MUsers.Add(mUser.Id, mUser);
            MPlayer mPlayer = MCoreServer.Instance.GenerateMPlayer(mUser);
            return mUser;
        }

        /// <summary>
        /// Gets or creates a user from Id
        /// </summary>
        /// <param name="Id">Id of user</param>
        /// <returns>A new or retrieved user</returns>
        public static async Task<MUser> GetOrCreate(string Id)
        {
            // Pre-define a user
            MUser user = null;

            // Get database and start a transaction
            Database database = MCoreServer.Db.Database;
            DbContextTransaction transaction = database.BeginTransaction();

            // Start the dangerous stuff
            try
            {
                // Grab users with same steamid
                List<MUser> users = MCoreServer.Db.MUsers.Where(u => u.Id == Id).ToList();

                // If no user is found, create them
                if (!users.Any() || Id == null)
                {
                    // Create user
                    user = new MUser(Id);

                    // Add user and save changes
                    MCoreServer.Db.MUsers.Add(user);
                    await MCoreServer.Db.SaveChangesAsync();
                }
                else
                {
                    // User found, use them instead
                    user = users.First();
                }

                // Commit to transaction
                transaction.Commit();
            }
            catch (Exception ex)
            {
                // Oops, something went wrong, rollback to previous version
                transaction.Rollback();
                Debug.Write(ex.Message);
            }

            // Finally, return out user
            return user;
        }
    }
}
