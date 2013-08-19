using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyncRobotCommand.Entity
{
    public static class Setups
    {
        public static List<Setup> SetupList { get; set; }
        // key: setup name s1, s2
        // value: set
        private static Dictionary<string, Setup> SetupNameDic { get; set; }

        static Setups()
        {
            SetupList = new List<Setup>();
            SetupNameDic = new Dictionary<string, Setup>();

            Setup setup = new Setup() { SetupName = "s20" };
            setup.Machines = new List<Machine> {
                new Machine{ MachineName = "UIP7PDB".ToLower(), IPAddress="192.168.1.1" },
                new Machine { MachineName = "UIP7RDB".ToLower(), IPAddress = "192.168.1.2" },
                new Machine { MachineName = "UIP7Core".ToLower(), IPAddress = "192.168.1.3" },
                new Machine { MachineName = "UIP7Corex".ToLower(), IPAddress = "192.168.1.4" },
                new Machine { MachineName = "UIP7UCC".ToLower(), IPAddress = "192.168.1.5" }
            };

            SetupList.Add(setup);
            SetupNameDic.Add("s20", setup);
        }

        public static Setup GetSetupByName(string name)
        {
            if (SetupNameDic.ContainsKey(name))
                return SetupNameDic[name];

            return null;
        }
    }

    public class Setup
    {
        public List<Machine> Machines { get; set; }
        public string SetupName { get; set; }

        public Setup()
        {
            Machines = new List<Machine>();
        }

        public Machine GetMachine(string machineName)
        {
            machineName = machineName.ToLower();
            return Machines.SingleOrDefault<Machine>(m => m.MachineName == machineName);
        }

        public Machine GetMachinebyIpaddress(string ipaddress)
        {
            return Machines.SingleOrDefault<Machine>(m => m.IPAddress == ipaddress);
        }
    }

    public class Machine
    {
        private string machineName;
        public string MachineName
        {
            get { return machineName; }
            set {
                machineName = value.ToLower();
            }
        }
        public string UserName { get; set; }
        public string CheckoutDate { get; set; }
        public string CheckinDate { get; set; }
        public string IPAddress { get; set; }
        public bool IsCheckout { get; set; }

        public Machine()
        {
            IsCheckout = false;
        }

        public string Status
        {
            get 
            {
                if (IsCheckout)
                    return "check out";
                else
                    return "nobody used";
            }
        }

        public string Output
        {
            get {
                if (IsCheckout)
                    return MachineName + ": " + Status + " by " + UserName;
                else
                    return MachineName + ": " + Status;
            }
        }

        public string OutputIpAddress
        {
            get
            {
                return " " + this.IPAddress;
            }
        }

        public string OutputDate
        {
            get
            {
                return " " + this.CheckoutDate;
            }
        }

        public string OutputAll
        {
            get
            {
                return OutputIpAddress + OutputDate;
            }
        }
    }
}
