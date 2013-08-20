using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LyncRobotCommand.Command;
using LyncRobotCommand.Entity;
using System.Collections.ObjectModel;

namespace LyncRobotCommand
{
    public enum LyncRobotCommandList
    { 
        setup,
        enter,
        checkin,
        checkout,
        currentsetup,
    }

    //hold all the commands
    public class CommandManager
    {
        private Collection<ICommand> Commands { get; set; }
        public string ParticipantURI { get; set; }

        public CommandManager(string participantName)
        {
            this.ParticipantURI = participantName;
            
            LoadAllCommands();
        }

        public Setup CurrentSetup { get; set; }

        public void LoadAllCommands()
        {
            OrderCommand order = new OrderCommand();
            HelpCommand help = new HelpCommand();
            EnterCommand enter = new EnterCommand();
            CheckinCommand checkin = new CheckinCommand();
            CheckoutCommand checkout = new CheckoutCommand();
            CurrentSetupCommand currentsetup = new CurrentSetupCommand();
            SetupCommand setup = new SetupCommand();

            this.Commands = new Collection<ICommand>() { help, enter, checkin, checkout, currentsetup, setup };
        }

        public string OutputAllCommandContent()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var command in Commands)
            {
                builder.AppendLine(command.Name + ": " + command.Description);
            }
            return builder.ToString();
        }

        public string ShowWelcome()
        {
            StringBuilder output = new StringBuilder();

            output.AppendLine("Welcome to access robot...");
            output.AppendLine("For details how to use the command, you can type help -a, help -command name...");

            return output.ToString();
        }

        public ICommand FindCommand(string commandName)
        {
            return Commands.SingleOrDefault<ICommand>(c => {
                return c.Name.ToLower() == commandName || c.Alias.ToLower() == commandName;
            });
        }

        public string ExecuteCommand(string content)
        {
            var result = string.Empty;
            string[] e = content.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (e != null && e.Length > 0)
            {
                var commandname = e[0].ToLower();

                ICommand command = null;

                command = FindCommand(commandname);

                if (command == null)
                    return "unknown command";
                else
                {
                    command.CommandManager = this;
                    return command.Run(e);
                }
            }
            else
                return "no input...";
        }
    }
}

