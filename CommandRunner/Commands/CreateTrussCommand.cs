using g4;
using System;
using GBCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using GBCore.Truss;

namespace CommandRunner.Commands
{
    public class CreateTrussCommand : IGBCommand
    {
        public void Run()
        {
            var sw = Stopwatch.StartNew();
            try
            {
                var v = new Vector3d(Math.Cos(MathUtil.Deg2Rad * 30), Math.Sin(MathUtil.Deg2Rad * 30), 0) * 24;
                var len = v.Length;

                TrussGeometrySettings settings = new TrussGeometrySettings { 
                    StartPoint = new Vector3d(0.0, 0.0, 0.0),
                    EndPoint = new Vector3d(0.0, 24000.0, 0.0),
                    Angle = 9.4623,
                    Height = 5000.0,
                    Sections = 2,
                    FirstDiagonalOffset = 200
                };

                // 9.4623 angle
                var truss = TrussFactory.GetSimpleTruss(settings);
                //sw.Stop();
                //Console.WriteLine($"Took {sw.ElapsedMilliseconds}ms");
                //sw.Restart();
                var topChords = truss.GetTopChords();
                var bottomChords = truss.GetBottomChords();
                var diagonals = truss.GetDiagnals();
            }
            finally
            {
                Console.WriteLine($"Took {sw.ElapsedMilliseconds}ms");
            }
        }
    }
}
