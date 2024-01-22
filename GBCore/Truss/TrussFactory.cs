using g4;
using GBCore.Truss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBCore
{
    public static class TrussFactory
    {
        public static TrussGeometry GetSimpleTruss(TrussGeometrySettings settings)
        {
            var points = new List<Vector3d>();
            double span = (settings.StartPoint - settings.EndPoint).Length;

            double slopeHeight = span / 2 * Math.Tan(settings.Angle * Math.PI / 180);
            slopeHeight = Math.Round(slopeHeight, 2);

            Vector3d midpoint = new Vector3d(
                (settings.StartPoint.x + settings.EndPoint.x) / 2,
                (settings.StartPoint.y + settings.EndPoint.y) / 2,
                (settings.StartPoint.z + settings.EndPoint.z) / 2 + slopeHeight
                );

            // Create top chord points in forward direction
            var forvardSlope = (midpoint - settings.StartPoint) / settings.Sections;
            points.Add(settings.StartPoint);
            for (int i = 0; i < settings.Sections - 1; i++)
            {
                var nextPoint = points[i] + forvardSlope;
                points.Add(nextPoint);
            }

            // Create top chord points in backward direction
            var index = points.Count;
            points.Add(settings.EndPoint);
            var backvardSlope = (midpoint - settings.EndPoint) / settings.Sections;
            for (int i = index; i < index + settings.Sections - 1; i++)
            {
                var nextPoint = points[i] + backvardSlope; ;
                points.Add(nextPoint);
            }
            points.Add(midpoint);

            // create bottom points
            double bottomChordZValue = midpoint.z - settings.Height;
            // Truss height has to be larger than 500
            if (bottomChordZValue > settings.StartPoint.z)
                bottomChordZValue = settings.StartPoint.z - 500;

            double bottomChordSPYValue = ((settings.StartPoint + forvardSlope).y - settings.StartPoint.y) / 2;
            double bottomChordSPXValue = ((settings.StartPoint + forvardSlope).x - settings.StartPoint.x) / 2;

            Vector3d bottomChordSP = new Vector3d(bottomChordSPXValue,
                bottomChordSPYValue, bottomChordZValue);

            points.Add(bottomChordSP);
            index = points.Count-1;
            for (int i = index; i < index + settings.Sections + 1; i++)
            {
                var nextPoint = points[i] + new Vector3d(forvardSlope.x, forvardSlope.y, 0);
                points.Add(nextPoint);
            }

            return new TrussGeometry(points);
        }
    }
}
