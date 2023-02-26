using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Banker.MODEL
{
    public static class ENUM
    {

        public enum TypeBank
        {
            none = -1,
            [Description("수협")]
            suhyup,
            [Description("신한")]
            shinhan,
            [Description("기업")]
            ibk,
            [Description("카카오")]
            kakao,
            [Description("삼성 카드")]
            credit_samsung,

        }
        public enum TypeUsage
        {
            use = 0,
            make,
            move,
            pay,
        }
        public static string desc<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }
        public static string num<T>(this T source)
        {
            return $"{Convert.ToInt32(source)}";
        }
    }
}
