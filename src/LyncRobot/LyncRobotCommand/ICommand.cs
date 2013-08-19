using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyncRobotCommand
{
    public interface ICommand
    {
        string Name { get; }
        string Description { get; }
        string Alias { get; }
        CommandManager CommandManager { get; set; }
        
        string Run(IEnumerable<string> args);
    }
}
