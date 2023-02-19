using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demographic.FileOperations
{
    public class DeathRules
    {
        public DeathRules(int startAge, int endAge, double possibilityM, double posibilityF)
        {
            StartAge = startAge;
            EndAge = endAge;
            PossibilityM = possibilityM;
            PosibilityF = posibilityF;
        }

        public int StartAge { get; }

        public int EndAge { get; }

        public double PossibilityM { get; }
        
        public double PosibilityF { get; }



    }
}
