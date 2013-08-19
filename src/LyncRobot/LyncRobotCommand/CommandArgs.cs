using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LyncRobotCommand
{
    public abstract class CommandArgs
    {
        public CommandArgs()
        {
            IsArgumentError = false;
            IsShowHelp = false;
        }

        public abstract CommandArgs Parse(IEnumerable<string> args);
        public virtual void ValidateParams()
        {
            IsArgumentError = false;
        }
        public bool IsShowHelp { get; set; }

        public virtual string Warnings { get; private set; }
        public bool IsArgumentError { get; set; }
        public virtual string Help { get; private set; }
    }

    public class CommandArgsFactory
    {
        public CommandArgs Create<TCommand, Targs>() where Targs : new()
        {
            var args = new Targs();
            return args as CommandArgs;
        }
    }
}
