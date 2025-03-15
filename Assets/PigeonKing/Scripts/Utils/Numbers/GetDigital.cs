using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PigeonKingGames.Utils
{
    public static class Digital
    {
        public static List<int> GetDigitals(int number, int length)
        {
            List<int> result = new List<int>();
            if(number == 0)
            {
                for(var i = 0; i < length; i++)
                {
                    result.Add(0);
                }
                return result;
            }
            for (int i = 0; i < length; i++)
            {
                result.Add(number % 10);
                number /= 10;
                if(number == 0)
                {
                    break;
                }
            }
            if(result.Count < length)
            {
                for (int i = result.Count; i < length; i++)
                {
                    result.Add(0);
                }
            }
            result.Reverse();
            return result;
        }

    }
}
