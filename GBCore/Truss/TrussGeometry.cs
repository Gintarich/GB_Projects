using g4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBCore
{
    public class TrussGeometry
    {
        private List<Vector3d> _points;
        private List<Member> _topChords = new List<Member>();
        private List<Member> _bottomChords = new List<Member>();
        private List<Member> _diagnals = new List<Member>();
        private Frame3f _frame;
        public TrussGeometry(List<Vector3d> points)
        {
            var pts = points.OrderBy(p => p.x).ThenBy(p => p.y).ToList();
            Vector3f basePt = new Vector3f(pts[0]);
            Vector3f x = new Vector3f(pts.LastOrDefault() - basePt).Normalized;
            /*
            _frame = new Frame3f(basePt);
            var mat = new Matrix3d(x,y,z,false);
            _points = pts.Select(p=>new Vector3d(mat.Multiply(ref p))).ToList();
            */
            _frame = new Frame3f(basePt);
            _frame.AlignAxis(0, x);
            pts.Select(p => _frame.ToFrameP(new Vector3f(p)));

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
