using g4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBCore
{
    public static class TrussFactory
    {
        public static TrussGeometry GetSimpleTruss(Vector3d startPoint, Vector3d endPoint,
            double angle, double depth, double sections)
        {
            var points = new List<Vector3d>();
            double span = (endPoint - startPoint).Length;

            double slopeHeight = span / 2 * Math.Tan(angle * Math.PI / 180);
            slopeHeight = Math.Round(slopeHeight, 2);

            Vector3d midpoint = new Vector3d(
                (startPoint.x + endPoint.x) / 2,
                (startPoint.y + endPoint.y) / 2,
                (startPoint.z + endPoint.z) / 2 + slopeHeight
                );

            // Create top chord points in forward direction
            var forvardSlope = (midpoint - startPoint) / sections;
            points.Add(startPoint);
            for (int i = 0; i < sections - 1; i++)
            {
                var nextPoint = points[i] + forvardSlope;
                points.Add(nextPoint);
            }

            // Create top chord points in backward direction
            var index = points.Count;
            points.Add(endPoint);
            var backvardSlope = (midpoint - endPoint) / sections;
            for (int i = index; i < index + sections - 1; i++)
            {
                var nextPoint = points[i] + backvardSlope; ;
                points.Add(nextPoint);
            }
            points.Add(midpoint);

            // create bottom points
            double bottomChordZValue = midpoint.z - depth;
            // Truss height has to be larger than 500
            if (bottomChordZValue > startPoint.z)
                bottomChordZValue = startPoint.z - 500;

            double bottomChordSPYValue = ((startPoint + forvardSlope).y - startPoint.y) / 2;
            double bottomChordSPXValue = ((startPoint + forvardSlope).x - startPoint.x) / 2;

            Vector3d bottomChordSP = new Vector3d(bottomChordSPXValue,
                bottomChordSPYValue, bottomChordZValue);

            points.Add(bottomChordSP);
            index = points.Count-1;
            for (int i = index; i < index + sections + 1; i++)
            {
                var nextPoint = points[i] + new Vector3d(forvardSlope.x, forvardSlope.y, 0);
                points.Add(nextPoint);
            }

            return new TrussGeometry(points);
        }
    }
}
