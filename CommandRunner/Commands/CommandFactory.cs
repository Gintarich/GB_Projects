using CommandRunner.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandRunner
{
    public class CommandFactory
    {
        private List<IGBCommand> _commands = new List<IGBCommand>();

        public void Initialize()
        {
            _commands.Add(new HelloWorldCommand());
            _commands.Add(new ReadKeyCommand());
        }
        public void RunCommands()
        {
            foreach (var command in _commands)
            {
                command.Run();
            }
        }
    }
}
