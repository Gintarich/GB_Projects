using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandRunner
{
    public class Application
    {
        private CommandFactory _commandFactory;
        public void Run()
        {
            _commandFactory = new CommandFactory();
            _commandFactory.Initialize();
            _commandFactory.RunCommands();
        }
    }
}
