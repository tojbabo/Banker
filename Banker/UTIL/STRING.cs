using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banker.UTIL
{
    public class STRING
    {
        /// <summary>
        /// 10000 -> 10,000
        /// </summary>
        /// <returns></returns>
        public static string Num2String(double num)
        {
            return String.Format("{0:#,###}", num); ;
        }

        /// <summary>
        /// {key1:val1},{key2:{val2,val3}} -> [key1:val1,key2:{val2,val3}]
        /// 
        /// data도 깨고싶으면 다시 돌려있!
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<string> BreakBracket(string input)
        {
            List<string> results = new List<string>();
            var temp = "";
            var count = 0;
            char[] target = { '{', '}' };
            while (true)
            {
                var idx = input.IndexOfAny(target);
                if (idx == -1) break;
                else if (input[idx] == '{')
                {
                    count++;
                }
                else if (input[idx] == '}')
                {
                    count--;
                }

                temp += input.Substring(0, idx + 1);
                input = input.Substring(idx + 1);

                if (count == 0)
                {
                    var start = temp.IndexOf('{');
                    var end = temp.LastIndexOf('}');
                    temp = temp.Substring(start + 1, end - start - 1);
                    results.Add(temp);
                    temp = "";
                }
            }
            return results;

        }
    }
}
