using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyncRobotCommand.Command
{
    public class CurrentSetupCommand : CommandBase<CurrentSetupCommand, CurrentSetupArgs>
    {
        public override string Name
        {
            get { return "CurrentSetup"; }
        }

        public override string Description
        {
            get { return "Used to display the current setup"; }
        }

        public override string Alias
        {
            get
            {
                return "CS";
            }
        }

        protected override string Execute(CurrentSetupArgs args)
        {
            if (this.CommandManager.CurrentSetup == null)
                return "currently you were not in any labsetup";

            var setup = this.CommandManager.CurrentSetup;

            if (args.IsDisplayAll)
                return setup.SetupOutputDetailsAll();

            return setup.SetupOutputDetails();
        }
    }

    public class CurrentSetupArgs : CommandArgs
    {
        public string SetupName { get; set; }
        public bool IsDisplayAll { get; set; }
        public bool IsDisplayIpAddress { get; set; }
        public bool IsDisplayDateTime { get; set; }

        public CurrentSetupArgs()
        {
            this.IsDisplayAll = false;
            this.IsDisplayIpAddress = false;
            this.IsDisplayDateTime = false;
        }

        public override CommandArgs Parse(IEnumerable<string> arguments)
        {
            var parms = new CurrentSetupArgs();

            var options = new OptionSet()
                .Add("a|all", a => { parms.IsDisplayAll = true; })
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
currentsetup -a 
cs -a

Alias:
--------
currentsetup => cs

Options:
--------
-all -a, used to display all the params
-help -?, this help display

Examples:
---------
cs -a 
";
                return help;
            }
        }
    }
}
