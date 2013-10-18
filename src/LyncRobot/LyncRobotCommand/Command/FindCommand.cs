using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyncRobotCommand.Command
{
    public class FindCommand : CommandBase<FindCommand, CheckinArgs>
    {
        public override string Name
        {
            get { return "Find"; }
        }

        public override string Description
        {
            get { return "This command used to find details with one machine or setup"; }
        }

        public override string Alias
        {
            get
            {
                return "F";
            }
        }

        protected override string Execute(CheckinArgs args)
        {
            throw new NotImplementedException();
        }
    }

    public class FindArgs : CommandArgs
    {
        public string IpAddress { get; set; }
        public string MachineName { get; set; }

        public FindArgs()
        {
        }

        public override CommandArgs Parse(IEnumerable<string> arguments)
        {
            var parms = new CheckinArgs();

            var options = new OptionSet()
                .Add("n=|name", n => { parms.MachineName = n; })
                .Add("i=|ipaddress", i => { parms.IpAddress = i; })
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
find -s -m -n#

Alias:
--------
find => f

Options:
--------
-name -n, setup name or machine name
-setup -s, if you want to find setup, use this
-machine -m, if you want to find machine, use this
-help -?  ,this help display

Examples:
---------
find -ns19 -s / find -n s19 -s
find -n
";

                return help;
            }
        }
    }
}
