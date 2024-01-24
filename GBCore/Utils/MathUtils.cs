using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GBCore
{
    public static class MathUtils
    {
        public static double Round(this double nr, int digits)
        {
            return Math.Round(nr, digits);
        }
    }
}
