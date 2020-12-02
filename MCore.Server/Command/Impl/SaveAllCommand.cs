﻿using MCore.Server.Chat;
using MCore.Server.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCore.Server.Command.Impl {

    /// <summary>
    /// A command to sync data to the database
    /// </summary>
    public class SaveAllCommand : BaseCommand {

        public SaveAllCommand() : base(new CommandInfo(0, "/saveall")) { }

        public override string Name => "saveall";

        public override void Perform(CommandContext context) {
            ICommandSender sender = context.Sender;
            sender.SendMessage(ChatColor.LIGHT_GREEN + "Saving data async...");

            // Throws error because of #GetCitizenPlayer() being called and no network id being available

            Task playerTask = MPlayers.Instance.ForceSaveAsync();
            playerTask.ContinueWith(t => sender.SendMessage(ChatColor.LIGHT_GREEN + "Done saving."));

            Task userTask = MUsers.Instance.ForceSaveAsync();
            userTask.ContinueWith(t => sender.SendMessage(ChatColor.LIGHT_GREEN + "Done saving."));
        }
    }
}
