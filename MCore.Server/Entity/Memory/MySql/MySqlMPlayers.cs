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
    /// A MySQL memory implementation of MPlayers
    /// </summary>
    public class MySqlMPlayers : MemoryMPlayers
    {

        /// <summary>
        /// Force saves players to a MySQL database sync
        /// </summary>
        public override void ForceSave()
        {
            this.ForceSaveAsync().RunSynchronously();
        }

        /// <summary>
        /// Force saves players to a MySQL database async
        /// </summary>
        /// <returns>Task responsible</returns>
        public override async Task ForceSaveAsync()
        {
            // Get database and start a transaction
            Database database = MCoreServer.Db.Database;
            DbContextTransaction transaction = database.BeginTransaction();
            try
            {

                // Add users and save changes
                MCoreServer.Db.Players.AddRange(base.Players.Values);
                await MCoreServer.Db.SaveChangesAsync();

                transaction.Commit();
            }
            catch (Exception)
            {
                // Oops, something went wrong, rollback to previous version
                transaction.Rollback();
            }
        }

        /// <summary>
        /// Loads players from a MySQL database
        /// </summary>
        public override void Load()
        {
            // Grab users with same steamid
            List<MPlayer> players = MCoreServer.Db.Players.ToList();

            // Clear players and start adding them
            base.Players.Clear();
            foreach (MPlayer player in players)
            {
                base.Players.Add(player.Id, player);
            }
        }

        /// <inheritdoc />
        public override MPlayer GeneratePlayer(string id)
        {
            MPlayer player = new MPlayer(id);
            base.Players.Add(id, player);
            return player;
        }

        /// <summary>
        /// Gets or creates a player from steamid
        /// </summary>
        /// <param name="steamId">SteamId of player</param>
        /// <returns>A new or retrieved player</returns>
        public static async Task<MPlayer> GetOrCreate(string steamId)
        {
            // Pre-define a user
            MPlayer user = null;

            // Get database and start a transaction
            Database database = MCoreServer.Db.Database;
            DbContextTransaction transaction = database.BeginTransaction();

            // Start the dangerous stuff
            try
            {
                // Grab users with same steamid
                List<MPlayer> users = MCoreServer.Db.Players.Where(u => u.SteamId == steamId).ToList();

                // If no user is found, create them
                if (!users.Any())
                {
                    // Create user
                    user = new MPlayer(steamId);

                    // Add user and save changes
                    MCoreServer.Db.Players.Add(user);
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
