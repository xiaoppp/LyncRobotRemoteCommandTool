using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Remoting;

namespace LyncRobotCommand.Command
{
    public class CommandsCommand : CommandBase<CommandsCommand, CommandsArgs>
    {
        protected override string Execute(CommandsArgs arg)
        {
            return CommandManager.OutputAllCommandContent;
        }

        public override string Name
        {
            get { return "Commands"; }
        }

        public override string Description
        {
            get { return "Get the commands which you can use in this robot"; }
        }
    }

    public class CommandsArgs : CommandArgs
    {
        public string CommandName { get; private set; }
        public override string Warnings
        {
            get { return "some arguments is not in goode format"; }
        }
        public bool IsDisplayAll
        { get; set; }

        public override CommandArgs Parse(IEnumerable<string> arguments)
        {
            var parms = new CommandsArgs();

            var options = new OptionSet()
                .Add("a|all", a => { parms.IsDisplayAll = true; })
                .Add("c=|commandname", o => { parms.CommandName = o; });

            options.Parse(arguments);
            
            return parms as CommandArgs;
        }

        public override string Help
        {
            get
            {

                const string help = @"
Help Content:
--------
help -c=# -a

Options:
--------
-a          Whether to dispaly all commands
-?          This help display

Examples:
---------
help -c enter
help -center
help -a
";
                return help;
            }
        }
    }
}
