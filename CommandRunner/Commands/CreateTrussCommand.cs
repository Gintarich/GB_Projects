using CommandRunner.Models;
using g3;
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
            var v = new Vector3d(Math.Cos(MathUtil.Deg2Rad*30),Math.Sin(MathUtil.Deg2Rad*30),0)*24;
            var len = v.Length;
            // 9.4623 angle
            var truss = TrussFactory.GetSimpleTruss(
                new g3.Vector3d(0, 0, 0), 
                new g3.Vector3d(v.x, v.y, 0), 
                9.4623, 3, 2
                );

        }
    }
}
