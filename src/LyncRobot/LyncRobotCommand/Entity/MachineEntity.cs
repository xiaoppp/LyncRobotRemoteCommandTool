using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyncRobotCommand.Entity
{
    public class MachineEntity
    {
        private string machineName;
        public string MachineName
        {
            get { return machineName; }
            set
            {
                machineName = value.ToLower();
            }
        }
        public string UserName { get; set; }
        public string CheckoutDate { get; set; }
        public string CheckinDate { get; set; }
        public string IPAddress { get; set; }
        public string MachineUser { get; set; }
        public string MachinePassword { get; set; }
        public string MachineDomain { get; set; }

        public bool IsCheckout { get; set; }

        public MachineEntity()
        {
            this.CheckoutDate = CheckoutDate ?? string.Empty;
            this.CheckinDate = CheckinDate ?? string.Empty;
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

        public string MachineOutput
        {
            get
            {
                return string.Format("{0, -15} {1, 11}", MachineName, Status);
            }
        }

        public string MachineDetailsOutput
        {
            // 0 machine name 
            // 1 ipaddress             
            // 2 machine user name
            // 3 machine password
            // 4 machine domain

            get
            {
                return string.Format("{0, -20} {1, -17} {2, -15} {3, -13} {4, -10} ", MachineName, IPAddress, MachineUser, MachinePassword, MachineDomain);
            }
        }
    }
}
