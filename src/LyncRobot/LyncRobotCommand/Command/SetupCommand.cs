using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LyncRobotCommand.Entity;

namespace LyncRobotCommand.Command
{
    public class SetupCommand : CommandBase<SetupCommand, SetupArgs>
    {
        public override string Name
        {
            get { return "Setup"; }
        }

        public override string Description
        {
            get { return "Used to display the lab setups"; }
        }

        protected override string Execute(SetupArgs args)
        {
            //var a = args.IsAll;

            //if (args.IsAll)
            //    return Setups.SetupsOutputDetails;
            //else
            return Setups.SetupsOutput;
        }
    }

    public class SetupArgs : CommandArgs
    {
        public SetupArgs()
        {
            IsAll = false;
        }

        public bool IsAll { get; set; }

        public override CommandArgs Parse(IEnumerable<string> arguments)
        {
            var parms = new SetupArgs();

            var options = new OptionSet()
                .Add("a|all", a => { parms.IsAll = true; })
                .Add("h|?|help", p => parms.IsShowHelp = true);

            var result = options.Parse(arguments);

            return parms as CommandArgs;
        }

        public override string Help
        {
            get
            {
                const string help = @"
Help Content:
--------
setup -a 

Options:
--------
-all -a, used to display the details in the setup
-help -?, this help display
";
                return help;
            }
        }
    }
}
