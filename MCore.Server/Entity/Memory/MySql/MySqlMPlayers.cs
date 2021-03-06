﻿using CitizenFX.Core;
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
                MCoreServer.Db.MPlayers.AddRange(base.MPlayers.Values);
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
            List<MPlayer> players = MCoreServer.Db.MPlayers.ToList();

            // Clear players and start adding them
            base.MPlayers.Clear();
            foreach (MPlayer player in players)
            {
                base.MPlayers.Add(player.Id, player);
            }
        }

        /// <inheritdoc />
        public override MPlayer GenerateMPlayer(MUser mUser)
        {
            MPlayer mPlayer = new MPlayer(mUser);
            base.MPlayers.Add(mPlayer.Id, mPlayer);
            return mPlayer;
        }

        /// <summary>
        /// Gets or creates a player from steamid
        /// </summary>
        /// <param name="steamId">SteamId of player</param>
        /// <returns>A new or retrieved player</returns>
        public static async Task<MPlayer> GetOrCreate(string userId)
        {
            // Pre-define a user
            MPlayer mUser = null;

            // Get database and start a transaction
            Database database = MCoreServer.Db.Database;
            DbContextTransaction transaction = database.BeginTransaction();

            // Start the dangerous stuff
            try
            {
                // Grab users with same steamid
                List<MPlayer> mUsers = MCoreServer.Db.MPlayers.Where(u => u.MUser.Id == userId).ToList();

                // If no user is found, create them
                if (!mUsers.Any())
                {
                    // Create user
                    mUser = new MPlayer();

                    // Add user and save changes
                    MCoreServer.Db.MPlayers.Add(mUser);
                    await MCoreServer.Db.SaveChangesAsync();
                }
                else
                {
                    // User found, use them instead
                    mUser = mUsers.First();
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
            return mUser;
        }
    }
}
