using g3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandRunner.Models
{
    public class Truss
    {
        private List<Vector3d> _points;
        private List<Member> _topChords = new List<Member>();
        private List<Member> _bottomChords = new List<Member>();
        private List<Member> _diagnals = new List<Member>();
        public Truss(List<Vector3d> points)
        {
            _points = points;
        }
        public Truss(Vector3d startPoint, Vector3d endPoint, double angle, double depth)
        {

        }
        public List<Member> GetTopChords()
        {
            if (_topChords.Count != 0)
                return _topChords;
            else
            {
                // TODO: Logic here
                return new List<Member>();
            }
        }
        public List<Member> GetDiagnals()
        {
            if (_diagnals.Count != 0)
                return _diagnals;
            else
            {
                // TODO: Logic here
                return new List<Member>();
            }
        }
        public List<Member> GetBottomChords()
        {
            if (_bottomChords.Count != 0)
                return _bottomChords;
            else
            {
                // TODO: Logic here
                return new List<Member>();
            }
        }

    }
}
