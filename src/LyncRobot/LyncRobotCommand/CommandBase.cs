using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyncRobotCommand
{
    public abstract class CommandBase<TCommand, TArgs> : ICommand
        where TCommand : class
        where TArgs : CommandArgs, new()
    {
        public abstract string Name { get; }
        public abstract string Description { get; }

        private string alias;
        public virtual string Alias
        {
            get { return alias ?? string.Empty; }
            set { alias = value; }
        }

        protected string ParticipantURI { get { return CommandManager.ParticipantURI; } }

        public CommandManager CommandManager { get; set; }

        protected abstract string Execute(TArgs args);
        
        protected TArgs CommandArgs { get; set; }

        public string Run(IEnumerable<string> args)
        {
            if (args != null)
            {
                var targs = new CommandArgsFactory().Create<TCommand, TArgs>();
                CommandArgs = targs.Parse(args) as TArgs;
            }

            if (CommandArgs.IsShowHelp)
                return CommandArgs.Help;

            if (CommandArgs.IsArgumentError)
            {
                StringBuilder ss = new StringBuilder();    
                ss.AppendLine(CommandArgs.Warnings);
                ss.AppendLine(CommandArgs.Help);
                return ss.ToString();
            }

            return Execute(CommandArgs);
        }
    }
}
