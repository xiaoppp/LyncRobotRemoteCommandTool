using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LyncRobotCommand;
using LyncRobotCommand.Command;

namespace LyncRobotConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            CommandManager manager = new CommandManager("console");

            Console.WriteLine(manager.ShowWelcome());
            
            while (true)
            {
                var command = Console.ReadLine();

                if (!string.IsNullOrEmpty(command))
                {
                    var content = manager.ExecuteCommand(command);
                    Console.WriteLine(content);
                }
            }
        }
    }
}
