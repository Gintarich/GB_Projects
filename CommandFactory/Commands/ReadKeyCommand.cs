using System;

namespace CommandFactory.Commands
{
    internal class ReadKeyCommand : IGBCommand
    {
        public void Run()
        {
            Console.ReadLine();
        }
    }
}
