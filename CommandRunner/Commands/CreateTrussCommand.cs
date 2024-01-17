using CommandRunner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandRunner.Commands
{
    public class CreateTrussCommand : IGBCommand
    {
        public void Run()
        {
            // 9.4623 angle
            var truss = TrussFactory.GetSimpleTruss(
                new g3.Vector3d(0, 0, 0), 
                new g3.Vector3d(0, 24, 0), 
                9.4623, 3, 2
                );

        }
    }
}
