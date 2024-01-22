﻿using g4;
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
            Vector3f x = new Vector3f(settings.EndPoint - settings.StartPoint).Normalized;
            var frame = new Frame3f(settings.StartPoint);
            frame.AlignAxis(0, x);

            var localSP = frame.ToFrameP(settings.StartPoint);
            var localEP = frame.ToFrameP(settings.EndPoint);
            var tang = Math.Tan(settings.Angle * Math.PI / 180);
            var offsetetZ = settings.FirstDiagonalOffset*tang;

            points.Add(localSP);
            points.Add(localEP);

            var newStartPoint = new Vector3d(settings.FirstDiagonalOffset,
                0,offsetetZ);

            var newEndPoint = new Vector3d(localEP.x - settings.FirstDiagonalOffset,
                0, offsetetZ);

            double slopeHeight = localEP.x / 2 * tang;
            slopeHeight = Math.Round(slopeHeight, 2);

            Vector3d midpoint = new Vector3d((localEP.x) / 2, 0, slopeHeight );

            // Create top chord points in forward direction
            var forvardSlope = (midpoint - newStartPoint) / settings.Sections;
            var index = points.Count;
            points.Add(newStartPoint);
            for (int i = index; i < index + settings.Sections - 1; i++)
            {
                var nextPoint = points[i] + forvardSlope;
                points.Add(nextPoint);
            }

            // Create top chord points in backward direction
            index = points.Count;
            points.Add(newEndPoint);
            var backvardSlope = (midpoint - newEndPoint) / settings.Sections;
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

            double bottomChordSPXValue = ((newStartPoint + forvardSlope).x - newStartPoint.x) / 2;

            Vector3d bottomChordSP = new Vector3d(bottomChordSPXValue,
                0, bottomChordZValue);

            points.Add(bottomChordSP);
            index = points.Count-1;
            for (int i = index; i < index + settings.Sections + 1; i++)
            {
                var nextPoint = points[i] + new Vector3d(forvardSlope.x, forvardSlope.y, 0);
                points.Add(nextPoint);
            }

            return new TrussGeometry(points,frame);
        }
    }
}
