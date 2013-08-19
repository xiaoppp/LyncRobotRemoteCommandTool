using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LyncRobotCommand.Entity;

namespace LyncRobotCommand.Command
{
    public class CheckoutCommand : CommandBase<CheckoutCommand, CheckinArgs>
    {
        public override string Name
        {
            get { return "Checkout"; }
        }

        public override string Description
        {
            get { return "Used to check out the machine"; }
        }

        public override string Alias
        {
            get
            {
                return "Out";
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
                machine.IsCheckout = true;
                machine.CheckoutDate = System.DateTime.Now.ToString();
                machine.UserName = CommandManager.ParticipantURI;
            }

            return "Check out successfully";
        }
    }
}
