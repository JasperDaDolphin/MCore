using CitizenFX.Core;
using CitizenFX.Core.Native;
using MCore.Server.Chat;
using MCore.Server.Command;
using MCore.Server.Command.Impl;
using MCore.Server.Entity;
using MCore.Server.Rpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCore.Server.Services {

    /// <summary>
    /// A service for commands
    /// </summary>
    public class CommandService : ServerService {

        protected readonly List<ICommandExecutor> commands = new List<ICommandExecutor>();

        /// <summary>
        /// Constructs a command service
        /// </summary>
        public CommandService() {
            Rpc.Client.Event(ServerEvents.chatMessage);
        }

        /// <summary>
        /// Initializes some essential commands
        /// </summary>
        public override void Initialize() {
            Register(new TestCommand());
            Register(new BroadcastCommand());
            Register(new SaveAllCommand());
            Register(new WhoisCommand());
        }

        /// <summary>
        /// Register a command
        /// </summary>
        /// <param name="command">Command to register</param>
        public void Register(ICommandExecutor command) {
            this.commands.Add(command);
            this.RegisterCommandApi(command);
        }

        public void RegisterCommandApi(ICommandExecutor command)
        {
            API.RegisterCommand(command.Name, new Action<int, List<object>, string>(OnChatMessage), false);
        }

        /// <summary>
        /// Handles a chat message
        /// </summary>
        /// <param name="playerId">Id of chat sender</param>
        /// <param name="playerName">Name of chat sender</param>
        /// <param name="message">Chat message sent</param>
        protected void OnChatMessage(int source, List<object> args, string rawCommand)
        {
            // Split command into arguments, extract name, and modify to exclude name
            var new_args = rawCommand.Split(' ').ToList();
            var name = new_args.First().ToLowerInvariant();
            new_args = new_args.Skip(1).ToList();
            //List<string> new_args = args.Select(m => m.ToString()).Skip(1).ToList();

            var command = this.commands.FirstOrDefault(c => c.Name.ToLowerInvariant() == name);
            if (command == null)
            {
                return;
            }

            ICommandSender sender;
            if (source > 0)
            {
                sender = MCoreServer.Instance.GetPlayerByNetworkId(source);
            } 
            else
            {
                sender = new ConsoleSender();
            }

            command.Execute(new CommandContext(command, sender, new_args));
        }

        /// <summary>
        /// A simple test command that can only be used by players
        /// </summary>
        class TestCommand : PlayerCommand {

            // "2" - means that 2 arguments are required for the command to go through,
            // which in this case is: [id] [id]
            //
            // "/test [id] [id]" - is the usage
            public TestCommand() : base(new CommandInfo(0, "/test [id] [id]")) { }

            // Specify the name of the command. In this case it is "/test"
            public override string Name => "test";

            // You are not required to override this method, as MCore has a default
            // sending message using the usage specified in the constructor
            protected override void SendUsageMessage(ICommandSender sender) {
                sender.SendMessage(ColorScheme.ERROR, "Usage!", ChatColor.DARK_RED + "That is not the correct usage? Use: " + this.info.Usage);
            }

            // Called when the command is performed
            // - "sender" is the sender of the command. In this case, we are extending "PlayerCommand" which is why that
            //   is a MPlayer, and not a ICommandExecutor
            //
            // - "context" is the command's context. Here you can find the sender, arguments, and many other useful methods to 
            //   make working with arguments easy!
            public override void Perform(MPlayer sender, CommandContext context) {
                // First argument is an id, which is an int, that means you can use:
                int id = context.ShiftInt();

                // However, you can also make MCore get the player for you from that id specified
                // in the arguments! This should also work for name, steam id, etc :)
                MPlayer target = context.ShiftPlayer();

                // Using "Shift" makes the context jump to the next argument automatically.
                // You can also get the raw ones that are not effected by shifting ;)
                string idInString = context.GetRawArgs()[0];
                string idInString2 = context.GetRawArgs()[1];
            }

            // We need to remember to register the command
            private static void RegisterThisCommand() {
                // Get the command service
                CommandService service = (CommandService)MCoreServer.Instance.Services[0];

                // Register command
                service.Register(new TestCommand());
            }
        }
    }
}
