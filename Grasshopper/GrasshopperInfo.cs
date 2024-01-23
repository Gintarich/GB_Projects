using Grasshopper;
using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace Grasshopper
{
    public class GrasshopperInfo : GH_AssemblyInfo
    {
        public override string Name => "GB_Solutions";

        //Return a 24x24 pixel bitmap to represent this GHA library.
        public override Bitmap Icon => null;

        //Return a short string describing the purpose of this GHA library.
        public override string Description => "";

        public override Guid Id => new Guid("AD2CBE0F-CA09-41F7-AD22-842D86020F21");

        //Return a string identifying you or your company.
        public override string AuthorName => "";

        //Return a string representing your preferred contact details.
        public override string AuthorContact => "";
    }
}