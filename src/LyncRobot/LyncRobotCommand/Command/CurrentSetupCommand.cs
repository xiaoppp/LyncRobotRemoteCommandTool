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

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("you are enter " + setup.SetupName + "," + " it have the following machines...");

            foreach (var machine in setup.Machines)
            {
                builder.AppendLine();
                builder.Append(machine.Output);

                if (args.IsDisplayIpAddress)
                    builder.Append(machine.OutputIpAddress);

                if (args.IsDisplayDateTime)
                    builder.Append(machine.OutputDate);

                if (args.IsDisplayAll)
                    builder.Append(machine.OutputAll);
            }

            return builder.ToString();
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
            var parms = new EnterArgs();

            var options = new OptionSet()
                .Add("n=|setup", n => { parms.SetupName = n; })
                .Add("a|all", a => { parms.IsDisplayAll = true; })
                .Add("i|ipaddress", i => { parms.IsDisplayIpAddress = true; })
                .Add("t|datetime", t => { parms.IsDisplayDateTime = true; })
                .Add("h|?|help", p => parms.IsShowHelp = true);

            var result = options.Parse(arguments);
            if (result != null && result.Count > 1)
            {
                parms.SetupName = result[1];
            }

            return parms as CommandArgs;
        }

        public override string Help
        {
            get
            {
                const string help = @"
Help Content:
--------
currentsetup -a -i -t 
cs -a -i -t 

Alias:
--------
currentsetup => cs

Options:
--------
-all -a, used to display all the params
-ipaddress -i, used to display the Ipaddress
-datetime -t, used to display the datetime the user checkout
-help -?  ,this help display

Examples:
---------
cs -a 
cs -a -i -t
";
                return help;
            }
        }
    }
}
