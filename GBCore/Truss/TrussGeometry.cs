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
        private Frame3f _frame;
        public TrussGeometry(List<Vector3d> points)
        {
            _points = points.OrderBy(p => p.x).ThenBy(p => p.y).ToList();
            Vector3f basePt = new Vector3f(_points[0]);
            Vector3f x = new Vector3f(_points.LastOrDefault() - basePt).Normalized;
            _frame = new Frame3f(basePt);
            _frame.AlignAxis(0, x);
            _points = _points.Select(p => _frame.ToFrameP(new Vector3d(p)))
                .Select(p => Vector3d.Round(p, 2))
                .ToList();
        }
        public List<Member> GetTopChords()
        {
            var indicies = _points.Select((point, i) => new { i, point })
                .Where(el => el.point.z >= _points[0].z)
                .Select(el=>el.i);
            var next = indicies.Skip(1);

            return indicies.Zip(next,(first,second)=> new Member(first,second)).ToList();
        }
        public List<Member> GetDiagnals()
        {
            var indicies = _points.Select((point, i) => new { i, point })
                .Select(el => el.i);
            var next = indicies.Skip(1);

            return indicies.Zip(next, (first, second) => new Member(first, second)).ToList();
        }
        public List<Member> GetBottomChords()
        {
            var indicies = _points.Select((point, i) => new { i, point })
                .Where(el => el.point.z < _points[0].z)
                .Select(el => el.i);
            var next = indicies.Skip(1);

            return indicies.Zip(next, (first, second) => new Member(first, second)).ToList();
        }

    }
}
