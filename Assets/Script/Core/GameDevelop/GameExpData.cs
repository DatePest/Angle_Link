using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Core.GameDevelop
{
    public static class ExperienceCalculator
    {
        public static int GetSmoothXP(int startXP, int endXP, int maxLevel, int level, double p = 2.0)
        {
            return (int)(startXP + (endXP - startXP) * Math.Pow((double)(level - 1) / (maxLevel - 1), p));
        }

    }
}
