﻿using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using MCore.Server.Command;
using MCore.Server.Chat;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCore.Server.Entity
{
	class MUser
	{
        /// <summary>
        /// A very unique id generated by MCore
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// Player's unique steam id
        /// </summary>
        [MaxLength(17)]
        public string SteamId { get; set; }

        /// <summary>
        /// Player's unique discord id
        /// </summary>
        [MaxLength(17)]
        public string DiscordId { get; set; }

        /// <summary>
        /// Player's MPlayer Accounts
        /// </summary>
        [MaxLength(17)]
        public string LiveId { get; set; }

        public MPlayer[] MPlayerAccounts { get; private set; }

        /// <summary>
        /// Time player was created
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Player's current network id. Do not map/save
        /// </summary>
        [NotMapped]
        public int NetworkId { get; set; }

        public MUser(string steamId)
        {
            this.Id = steamId; // just use steam id
            this.SteamId = steamId;
            this.Created = DateTime.UtcNow;
        }
    }
}
