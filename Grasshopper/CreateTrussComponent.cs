using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using GBCore;
using GBCore.Truss;
using System.Runtime.CompilerServices;

namespace Grasshopper
{
    public static class Rhino3dExtensions
    {
        public static Point3d GetRhinoPoint(this double[] pt)
        {
            return new Point3d(pt[0], pt[1], pt[2]);
        }
    }
    public class CreateTrussComponent : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public CreateTrussComponent()
          : base("CreateTrussComponent", "CTC",
            "Construct a Truss.",
            "GB_Solutions", "Steel")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("StartPoint", "SP", "Start point", GH_ParamAccess.item);
            pManager.AddPointParameter("EndPoint", "EP", "End point", GH_ParamAccess.item);
            pManager.AddNumberParameter("Angle", "A", "Angle in degrees", GH_ParamAccess.item);
            pManager.AddNumberParameter("Height", "H", "Angle in degrees", GH_ParamAccess.item);
            pManager.AddNumberParameter("FirstDiagonal", "FD", "First diagonal offset", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Sections", "S", "Number of sections",GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddLineParameter("TopChords", "TCH", "Top chord", GH_ParamAccess.list);
            pManager.AddLineParameter("Diagonals", "D", "Diagonals", GH_ParamAccess.list);
            pManager.AddLineParameter("BottomChords", "BCH", "Bottom chords", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Point3d startPoint = new Point3d();
            DA.GetData<Point3d>(0, ref startPoint);
            Point3d endPoint = new Point3d();
            DA.GetData<Point3d>(1, ref endPoint);
            double angle = double.NaN;
            DA.GetData<double>(2,ref angle);
            double height = double.NaN;
            DA.GetData<double>(3,ref height);
            double firstDiagonalOffset = double.NaN;
            DA.GetData<double>(4,ref firstDiagonalOffset);
            int sections = int.MaxValue;
            DA.GetData<int>(5,ref sections);



            var truss =  TrussFactory.GenerateTrussForRhino(new double[] {startPoint.X,startPoint.Y,startPoint.Z },
                new double[] {endPoint.X,endPoint.Y,endPoint.Z },
                angle,height,sections,firstDiagonalOffset);

            var points = truss.GetTopChordPoints();
            List<Line> lines = new List<Line>();
            foreach (var p in points)
            {
                Point3d p1 = p.Item1.GetRhinoPoint();
                Point3d p2 = p.Item2.GetRhinoPoint();
                lines.Add(new Line(p.Item1.GetRhinoPoint(), p.Item2.GetRhinoPoint()));
            }
            DA.SetDataList(0, lines);
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// You can add image files to your project resources and access them like this:
        /// return Resources.IconForThisComponent;
        /// </summary>
        protected override System.Drawing.Bitmap Icon => null;

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid => new Guid("EB063764-A0D8-4874-B574-9EABEE23DD98");
    }
}