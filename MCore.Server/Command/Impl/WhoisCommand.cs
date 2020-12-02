using CitizenFX.Core;
using CitizenFX.Core.Native;
using MCore.Server.Chat;
using MCore.Server.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCore.Server.Command.Impl {

    /// <summary>
    /// A command to check who someone are
    /// </summary>
    public class WhoisCommand : BaseCommand {

        public WhoisCommand() : base(new CommandInfo(0, 1, "whois [network id]")) { }

        public override string Name => "whois";

        public override void Perform(CommandContext context) {

            ICommandSender sender = context.Sender;

            int netId;
            try 
            { 
                netId = context.ShiftInt();
            } 
            catch
            {
                sender.SendMessage("[network id] must be an Integer.");
                return;
            }

            if (netId == 0) return;
           
            // Shift player
            MPlayer target = MCoreServer.Instance.GetMPlayerBynetId(netId);

            if (target != null)
            {
                // Notify sender of information
                sender.SendMessage(ChatColor.LIGHT_RED + "Checking: " + ChatColor.LIGHT_GREEN + target.Name + ChatColor.LIGHT_RED + ", their identifiers are:");
                sender.SendMessage(ChatColor.LIGHT_RED + "- network: " + ChatColor.LIGHT_GREEN + Convert.ToString(target.netId));
                sender.SendMessage(ChatColor.LIGHT_RED + "- PlayerId: " + ChatColor.LIGHT_GREEN + target.Id);
                sender.SendMessage(ChatColor.LIGHT_RED + "- steamId: " + ChatColor.LIGHT_GREEN + target.MUser.SteamId);
                sender.SendMessage(ChatColor.LIGHT_RED + "and more information:");
            }
            else
            {
                sender.SendMessage(ChatColor.LIGHT_RED + "Player is not online.");
            }
        }

    }
}
