using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Remoting;

namespace LyncRobotCommand.Command
{
    public class HelpCommand : CommandBase<HelpCommand, HelpArgs>
    {
        protected override string Execute(HelpArgs arg)
        {
            if (!string.IsNullOrEmpty(arg.CommandName))
            {
                var command = CommandManager.FindCommand(arg.CommandName);
            }

            StringBuilder builder = new StringBuilder();
            builder.Append(CommandManager.OutputAllCommandContent());
            
            return builder.ToString();
        }

        public override string Name
        {
            get { return "Help"; }
        }

        public override string Description
        {
            get { return "This is used to help topic"; }
        }
    }

    public class HelpArgs : CommandArgs
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
            var parms = new HelpArgs();

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
