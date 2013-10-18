using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyncRobotCommand.Entity
{
    public static class Setups
    {
        public static List<SetupEntity> SetupList { get; set; }
        // key: setup name s1, s2
        // value: set
        //private static Dictionary<string, Setup> SetupNameDic { get; set; }

        static Setups()
        {
            SetupList = new List<SetupEntity>();

            SetupEntity setup = new SetupEntity() { SetupName = "S19", Version = "71CM02" };
            setup.Machines = new List<MachineEntity> {
                new MachineEntity { MachineName = "UIP71PDM", IPAddress = "10.200.47.51", MachineUser = "administrator", MachinePassword = "Aspect9", MachineDomain = "RD2" },
                new MachineEntity { MachineName = "UIP71BDM", IPAddress = "10.200.47.52", MachineUser = "administrator", MachinePassword = "Aspect9", MachineDomain = "RD2" },
                new MachineEntity { MachineName = "UIP71RDM", IPAddress = "10.200.47.53", MachineUser = "administrator", MachinePassword = "Aspect9", MachineDomain = "RD2" },
                new MachineEntity { MachineName = "UIP71CORE", IPAddress = "10.200.47.54", MachineUser = "administrator", MachinePassword = "Aspect9", MachineDomain = "RD2" },
                new MachineEntity { MachineName = "UIP71CORE-EXP", IPAddress = "10.200.47.55", MachineUser = "administrator", MachinePassword = "Aspect9", MachineDomain = "RD2" },
                new MachineEntity { MachineName = "UIP71CORER", IPAddress = "10.200.47.56", MachineUser = "administrator", MachinePassword = "Aspect9", MachineDomain = "RD2" },
                new MachineEntity { MachineName = "UIP71CORER-EXP", IPAddress = "10.200.47.57", MachineUser = "administrator", MachinePassword = "Aspect9", MachineDomain = "RD2" },
                new MachineEntity { MachineName = "UIP71UCC", IPAddress = "10.200.47.58", MachineUser = "administrator", MachinePassword = "Aspect9", MachineDomain = "RD2" },
                new MachineEntity { MachineName = "UIP71UCC-RED", IPAddress = "10.200.47.59", MachineUser = "administrator", MachinePassword = "Aspect9", MachineDomain = "RD2" },
                new MachineEntity { MachineName = "UIP71CLIENT-W7", IPAddress = "10.200.47.79", MachineUser = "administrator", MachinePassword = "Aspect9", MachineDomain = "RD2" }                
            };

            SetupEntity setup1 = new SetupEntity() { SetupName = "S21", Version = "71CM02" };
            setup1.Machines = new List<MachineEntity> {
                new MachineEntity { MachineName = "DLNS21UIPPDB", IPAddress="10.200.47.192", MachineUser = "administrator", MachinePassword = "Aspect9", MachineDomain = "RD2" },
                new MachineEntity { MachineName = "DLNS21UIPRDB", IPAddress = "10.200.47.193", MachineUser = "administrator", MachinePassword = "Aspect9", MachineDomain = "RD2"},
                new MachineEntity { MachineName = "DLNS21UIPCore", IPAddress = "10.200.47.194", MachineUser = "administrator", MachinePassword = "Aspect9", MachineDomain = "RD2" },
                new MachineEntity { MachineName = "DLNS21UIPCoreX", IPAddress = "10.200.47.195", MachineUser = "administrator", MachinePassword = "Aspect9", MachineDomain = "RD2" },
                new MachineEntity { MachineName = "DLNS21UIPUCC", IPAddress = "10.200.47.196", MachineUser = "administrator", MachinePassword = "Aspect9", MachineDomain = "RD2" },
                new MachineEntity { MachineName = "DLNS21UIPClient", IPAddress = "10.200.47.197", MachineUser = "administrator", MachinePassword = "Aspect9", MachineDomain = "RD2" },
                new MachineEntity { MachineName = "DLNS21UIPC2", IPAddress = "10.200.47.198", MachineUser = "administrator", MachinePassword = "Aspect9", MachineDomain = "RD2" }
            };

            SetupList.Add(setup);
            SetupList.Add(setup1);
        }

        public static SetupEntity GetSetupByName(string name)
        {
            var setup = SetupList.SingleOrDefault<SetupEntity>(s => s.SetupName.ToLower() == name.ToLower());
            return setup;
        }

        public static string SetupsOutput
        {
            get
            {
                StringBuilder builder = new StringBuilder();

                string header = String.Format("{0, -10} {1, 10}\n", "Setup Name", "Version");
                string split = string.Format("{0, -10} {1, 10}\n",  "----------", "-------");

                builder.Append(header);
                builder.AppendLine(split);

                foreach (var set in SetupList)
                {
                    var a = string.Format("{0, -10} {1, 10}", set.SetupName, set.Version);
                    builder.AppendLine(a);
                }

                return builder.ToString();
            }
        }

        public static string SetupsOutputDetails
        {
            get
            {
                StringBuilder builder = new StringBuilder();

                foreach (var set in SetupList)
                {
                    builder.AppendLine(set.SetupOutputDetails);
                    builder.AppendLine();
                }

                return builder.ToString();
            }
        }
    }
}
