
using g3;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;

namespace CommandRunner.Models
{
    public class TrussOld
    {
        private Vector3f _startPoint;
        private Vector3f _endPoint;
        private float _angle;
        private float _height;
        private int _splits;
        private List<Vector3f> _points = new List<Vector3f>();
        private Frame3f _coordinateSystem;
        public TrussOld(Vector3f startPoint, Vector3f endPoint, 
            float degrees, int splits, float height)
        {
            CreateBasicTruss(startPoint, endPoint, degrees, splits, height);
        }

        private void CreateBasicTruss(Vector3f startPoint, Vector3f endPoint, float degrees, int splits, float height)
        {
            var rot = new Quaternionf(Vector3f.AxisX, endPoint - startPoint);
            _coordinateSystem = new Frame3f(startPoint, rot);

            var length = (endPoint - startPoint).Length;
            var midHeight = length / 2 / Math.Tan(MathUtil.Deg2Rad * degrees);

            Vector3f forwardVec = new Vector3f(length / 2, 0, midHeight);
            Vector3f backwardVec = new Vector3f(length, 0, 0) - forwardVec;
            Vector3f splitForwardVec = forwardVec / splits;
            Vector3f splitBackwardVec = backwardVec / splits;

            _points.Add(startPoint);
            for (int i = 0; i < splits; i++)
            {
                var last = _points.Last();
                _points.Add(last + splitForwardVec);
            }
            _points.Add(endPoint);
            for (int i = 0; i < splits; i++)
            {
                var last = _points.Last();
                _points.Add(last + splitBackwardVec);
            }

            var loverPoints = new List<Vector3f>();
            foreach (var p in _points)
            {
                loverPoints.Add(new Vector3f(p.x, p.y, -height));
            }
            _points.AddRange(loverPoints);
        }
    }
}
