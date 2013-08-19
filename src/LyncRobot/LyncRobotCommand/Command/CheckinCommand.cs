using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LyncRobotCommand.Entity;

namespace LyncRobotCommand.Command
{
    public class CheckinCommand : CommandBase<CheckinCommand, CheckinArgs>
    {

        public override string Name
        {
            get { return "Checkin"; }
        }

        public override string Description
        {
            get { return "Used to check in the machine"; }
        }

        public override string Alias
        {
            get
            {
                return "In";
            }
        }

        protected override string Execute(CheckinArgs args)
        {
            var setup = CommandManager.CurrentSetup;
            Machine machine = null;
            if (!string.IsNullOrEmpty(args.IpAddress))
                machine = setup.GetMachinebyIpaddress(args.IpAddress);
            else if (!string.IsNullOrEmpty(args.MachineName))
                machine = setup.GetMachine(args.MachineName);
            else
                return "Can't find this machine, please check your input...";

            if (machine != null)
            {
                machine.IsCheckout = false;
                machine.CheckinDate = System.DateTime.Now.ToString();
                machine.UserName = CommandManager.ParticipantURI;
            }

            return "Check in successfully";
        }
    }

    public class CheckinArgs : CommandArgs
    {
        public string IpAddress { get; set; }
        public string MachineName { get; set; }

        public CheckinArgs()
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
checkin -n=# -i=# 
checkout -n=# -i=# 
in -n=# -i=# 
out -n=# -i=# 

Alias:
--------
checkin => in
checkout => out

Options:
--------
-machinename -n, which machine you want to checkin
-ipaddress -i, which ipaddress you want to checkin
-help -?  ,this help display

Examples:
---------
checkin -n uip7pdb || in -n uip7pdb
checkin -i 192.168.1.1 || in -i 192.168.1.1 
checkout -n uip7pdb || out -n uip7pdb
checkout -i 192.168.1.1 || out -i 192.168.1.1 
";

                return help;
            }
        }
    }
}
