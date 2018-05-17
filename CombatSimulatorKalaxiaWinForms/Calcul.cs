using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator
{
    static class Calcul
    {
        public static double Distance(int x0, int y0, int x1, int y1)
        {
            double distance;
            distance = Math.Sqrt((x0 - x1) * (x0 - x1) + (y0 - y1) * (y0 - y1));
            return distance;
        }
    }
}
