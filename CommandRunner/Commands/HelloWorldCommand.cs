using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandRunner.Commands
{
    internal class HelloWorldCommand : IGBCommand
    {
        public void Run()
        {
            Console.WriteLine("Hello World");
        }
    }
}
