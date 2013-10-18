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

        public SetupEntity CurrentSetup { get; set; }

        public void LoadAllCommands()
        {
            //OrderCommand order = new OrderCommand();
            CommandsCommand help = new CommandsCommand();
            EnterCommand enter = new EnterCommand();
            CheckinCommand checkin = new CheckinCommand();
            CheckoutCommand checkout = new CheckoutCommand();
            CurrentSetupCommand currentsetup = new CurrentSetupCommand();
            SetupCommand setup = new SetupCommand();

            this.Commands = new Collection<ICommand>() { help, enter, checkin, checkout, currentsetup, setup };
        }

        public string OutputAllCommandContent
        {
            get
            {
                StringBuilder builder = new StringBuilder();

                string header = String.Format("{0, -15} {1, 11}\n", "Name", "Description");
                string split = string.Format("{0, -15} {1, 11}\n",  "----", "-----------");

                builder.Append(header);
                builder.AppendLine(split);

                foreach (var command in Commands)
                    builder.AppendLine(string.Format("{0, -15} {1, 11}", command.Name, command.Description));
                
                return builder.ToString();
            }
        }

        public string ShowWelcome()
        {
            StringBuilder output = new StringBuilder();
            output.AppendLine("Welcome to access Lync setup robot, you can get help by typing commands...");
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
                    return "unknown command...";
                else
                {
                    command.CommandManager = this;

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine();
                    sb.AppendLine(command.Run(e));

                    return sb.ToString();
                }
            }
            else
                return "no input...";
        }
    }
}

