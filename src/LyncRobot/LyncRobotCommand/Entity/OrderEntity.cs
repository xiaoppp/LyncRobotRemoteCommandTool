using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyncRobotCommand.Entity
{
    public static class OrderEntity
    {
        // key: participant name, value: choice
        public static Dictionary<string, int> OrderChoiceDic { get; set; }
        public static Dictionary<int, string> FoodDic { get; set; }

        static OrderEntity()
        {
            OrderChoiceDic = new Dictionary<string, int>();

            FoodDic = new Dictionary<int, string>();
            FoodDic.Add(1, "盒饭");
            FoodDic.Add(2, "山上食堂");
            FoodDic.Add(3, "大盘鸡");
            FoodDic.Add(4, "对面食堂");
            FoodDic.Add(5, "老板娘那");
            FoodDic.Add(6, "everything is ok...");
        }

        public static string ShowOrder()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("中午想吃点啥...");

            foreach (var key in OrderEntity.FoodDic.Keys)
            {
                builder.AppendLine(key + ". " + OrderEntity.FoodDic[key]);
            }

            builder.AppendLine("请回复我这种格式 order -c 1/2/3");
            return builder.ToString();
        }
    }
}
