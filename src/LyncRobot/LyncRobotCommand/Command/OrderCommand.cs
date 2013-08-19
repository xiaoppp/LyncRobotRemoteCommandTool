using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using LyncRobotCommand.Entity;

namespace LyncRobotCommand.Command
{
    public class OrderCommand : CommandBase<OrderCommand, OrderArgs>
    {
        protected override string Execute(OrderArgs args)
        {
            if (OrderEntity.OrderChoiceDic.ContainsKey(this.ParticipantURI))
                OrderEntity.OrderChoiceDic[this.ParticipantURI] = args.OrderChoice;
            else
                OrderEntity.OrderChoiceDic.Add(this.ParticipantURI, args.OrderChoice);

            StringBuilder ss = new StringBuilder();
            foreach (var participant in OrderEntity.OrderChoiceDic.Keys)
            {
                var name = string.Empty;
                string[] ccc = participant.Split(new char[] { ':' });
                if (ccc.Length == 2)
                    name = ccc[1];

                var choice = OrderEntity.FoodDic[OrderEntity.OrderChoiceDic[participant]];

                ss.AppendLine(name + ": " + choice);
            }

            return ss.ToString();        
        }

        public override string Name
        {
            get { return "Order"; }
        }

        public override string Description
        {
            get { return "Used to order some meal"; }
        }
    }

    public class OrderArgs: CommandArgs
    {
        public int OrderChoice { get; private set; }
        public string Description { get; private set; }
        public override string Warnings
        {
            get
            {
                return "your choice is not in known format, please change another one, thanks...";
            }
        }

        public override CommandArgs Parse(IEnumerable<string> arguments)
        {
            var parms = new OrderArgs();

            var options = new OptionSet()
                .Add("c=|choice", c =>
                    {
                        int v;
                        if (int.TryParse(c, out v))
                            parms.OrderChoice = int.Parse(c);
                        else
                            parms.OrderChoice = 0;
                    })
              .Add("h|?|help", p => parms.IsShowHelp = true);

            var result = options.Parse(arguments);
            if (result != null && result.Count > 1)
            {
                int v;
                if (int.TryParse(result[1], out v))
                    parms.OrderChoice = v;
            }

            if (parms.OrderChoice == 0)
                parms.IsArgumentError = true;

            return parms as CommandArgs;
        }
        
        public override string Help
        {
            get
            {
                const string help = @"
Help Content:
--------
order -c 1
order 1

Options:
--------
-help -?  ,this help display

Examples:
---------
order -c 1         - give the 1 choice 
order 1            - directly give the 1 choice
";

                return help;
            }
        }
    }
}
