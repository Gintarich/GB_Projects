
using g3;
using System;
using System.Collections.Generic;

namespace CommandRunner.Models
{
    public class Truss
    {
        private List<Node> _nodes = new List<Node>();
        private List<Member> _members = new List<Member>();
        private List<Vector3f> _points = new List<Vector3f>();
        private Frame3f _coordinateSystem;
        public Truss(Vector3f startPoint, Vector3f endPoint, 
            float degrees, int splits, double height)
        {
            var rot = new Quaternionf(Vector3f.AxisX, endPoint - startPoint);
            _coordinateSystem = new Frame3f(startPoint, rot);

            var length = (endPoint - startPoint).Length;
            var midHeight = length/2 / Math.Tan(MathUtil.Deg2Rad*degrees);

            Vector3f forwardVec = new Vector3f(length / 2, 0, midHeight);
            Vector3f backwardVec =new Vector3f(length, 0, 0) - forwardVec;
            Vector3f splitForwardVec = forwardVec / splits;
            Vector3f splitBackwardVec = backwardVec / splits;

            

        }
    }
}
