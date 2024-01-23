﻿using g4;
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
        public TrussGeometry(List<Vector3d> points, Frame3f frame)
        {
            _points = points.OrderBy(p => p.x).ToList();
            _frame = frame;
        }
        public List<Member> GetTopChords()
        {
            //TODO: FIX THESE
            var indicies = _points.Select((point, i) => new { i, point })
                .Where(el => (el.point.z >= _points[0].z )&&(el.i != 0)&&(el.i != _points.Count-1) )
                .Select(el=>el.i);
            var next = indicies.Skip(1);

            return indicies.Zip(next, (first, second) => new Member(first, second)).ToList();
        }
        public List<Member> GetDiagnals()
        {
            //TODO: FIX THESE
            var indicies = _points.Select((point, i) => new { i, point })
                .Where( el => (el.i != 0) && (el.i != _points.Count - 1))
                .Select(el => el.i);
            var next = indicies.Skip(1);

            return indicies.Zip(next, (first, second) => new Member(first, second)).ToList();
        }
        public List<Member> GetBottomChords()
        {
            //TODO: FIX THESE
            var indicies = _points.Select((point, i) => new { i, point })
                .Where(el => (el.point.z < _points[0].z) && (el.i != 0) && (el.i != _points.Count - 1))
                .Select(el => el.i);
            var next = indicies.Skip(1);

            return indicies.Zip(next, (first, second) => new Member(first, second)).ToList();
        }

        public List<(double[], double[])> GetTopChordPoints()
        {
            var indicies = _points.Select((point, i) => new { i, point })
                .Where(el => el.point.z >= _points[0].z)
                .Select(el => el.i);
            var next = indicies.Skip(1);

            var transformed = _points.Select(p => _frame.FromFrameP(p)).ToList();

            return indicies.Zip(next, (first, second) =>
            (new double[] { transformed[first].x, transformed[first].y, transformed[first].z },
            new double[] { transformed[second].x, transformed[second].y, transformed[second].z }))
                .ToList();
        }

    }
}
