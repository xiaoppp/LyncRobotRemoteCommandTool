//Enter:
//you are enter into the S20 setup, it contains following machines
//-status -s default
//-name -n default
//-ipaddress -i
//-user -u dafault
//-date -t

//cs:

//view
//-status -s
//-name -n
//-ipaddress -i
//-user -u
//-date -t


//checkin 
//-name -n
//-ipaddress -i


//checkout
//-name -n
//-ipaddress -i


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LyncRobotCommand.Entity;

namespace LyncRobotCommand.Command
{
    public class EnterCommand : CommandBase<EnterCommand, EnterArgs>
    {
        public override string Name
        {
            get { return "Enter"; }
        }

        public override string Description
        {
            get { return "Used to enter one setup"; }
        }

        protected override string Execute(EnterArgs args)
        {
            var setupName = args.SetupName;
            
            var setup = Setups.GetSetupByName(args.SetupName);
            if (setup == null)
                return "we didn't have this setup...";
            else
            {
                //set current setup
                this.CommandManager.CurrentSetup = setup;

                StringBuilder builder = new StringBuilder();
                builder.AppendLine("you are enter " + setupName + "," + " it have the following machines...");

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
    }

    public class EnterArgs : CommandArgs
    {
        public string SetupName { get; set; }
        public bool IsDisplayAll { get; set; }
        public bool IsDisplayIpAddress { get; set; }
        public bool IsDisplayDateTime { get; set; }

        public EnterArgs()
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
enter # -a -i -t -n=#

Options:
--------
-n -setup, which setup you want to enter
-all -a, used to display all the params
-ipaddress -i, used to display the Ipaddress
-datetime -t, used to display the datetime the user checkout
-help -?  ,this help display

Examples:
---------
ENTER S20 -a 
ENTER -nS20 -a -i -t
";
                return help;
            }
        }
    }
}
