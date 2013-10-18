using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyncRobotCommand.Entity
{
    public class SetupEntity
    {
        public List<MachineEntity> Machines { get; set; }
        public string SetupName { get; set; }
        public string Version { get; set; }

        public SetupEntity()
        {
            Machines = new List<MachineEntity>();
        }

        public MachineEntity GetMachine(string machineName)
        {
            machineName = machineName.ToLower();
            return Machines.SingleOrDefault<MachineEntity>(m => m.MachineName.ToLower() == machineName.ToLower());
        }

        public MachineEntity GetMachinebyIpaddress(string ipaddress)
        {
            return Machines.SingleOrDefault<MachineEntity>(m => m.IPAddress == ipaddress);
        }

        public string SetupOutput
        {
            get
            {
                return "This is " + SetupName + " setup, it have the following machines...";
            }
        }

        public string SetupOutputDetails
        {
            get
            {
                StringBuilder builder = new StringBuilder();

                string header = String.Format("{0, -20} {1, -17} {2, 13}\n", "Machine Name", "IP Address", "Machine status");
                string split = string.Format("{0, -20} {1, -17} {2, 13}\n",  "------------", "----------", "--------------");

                builder.Append(header);
                builder.AppendLine(split);

                foreach (var machine in this.Machines)
                {
                    if (machine.IsCheckout)
                        builder.AppendLine(string.Format("{0, -20} {1, -17} {2, -13} ----- {3, -20}", machine.MachineName, machine.IPAddress, machine.Status, machine.UserName));
                    else
                        builder.AppendLine(string.Format("{0, -20} {1, -17} {2, -13}", machine.MachineName, machine.IPAddress, machine.Status));
                }

                return builder.ToString();
            }
        }
    }
}
