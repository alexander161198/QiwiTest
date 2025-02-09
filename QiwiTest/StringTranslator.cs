using System.Text;

namespace QiwiTest
{
    public static class StringTranslator
    {
        private static string[] fromZeroToNineteen = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        private static string[] tens = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

        public static string TranslateDecimal(decimal value)
        {
            StringBuilder builder = new StringBuilder();

            int intPart = (int)value;
            //Обработка дробной части (центов)
            decimal dec = value - intPart;
            if (dec > 0)
            {
                builder.Append(TranslateInteger((int)(dec*100)));
                //Если кол-во центов равно 1, то окончания 'S' не должно быть
                if (dec == (decimal)0.01)
                {
                    builder.Append("CENT");
                }
                else
                {
                    builder.Append("CENTS");
                }

                //Если есть целая часть (доллары), то нужно их соединить с помощью 'AND'
                if (intPart > 0)
                {
                    builder.Insert(0, " AND ");
                }
            }

            //Обработка целой части (долларов)
            if (intPart > 0)
            {
                //Если кол-во долларов равно 1, то окончания 'S' не должно быть
                if (intPart == 1)
                {
                    builder.Insert(0, "DOLLAR");
                }
                else
                {
                    builder.Insert(0, "DOLLARS");
                }

                builder.Insert(0, $"{TranslateInteger(intPart)}");
            }

            return builder.ToString();
        }

        public static string TranslateInteger(int number)
        {
            StringBuilder builder = new StringBuilder();

            int billions = number / 1000000000;
            if (billions > 0)
            {
                builder.Append($"{TranslateInteger(billions)}billion");
                //Если кол-во миллионов, тысяч или сотен равно нулю, то запятая не нужна
                builder.Append((number - billions * 1000000000)/100 > 0 ? ", " : " ");
                number %= 1000000000;
            }

            int millions = number / 1000000;
            if (millions > 0)
            {
                builder.Append($"{TranslateInteger(millions)}million");
                builder.Append((number - millions * 1000000)/100 > 0 ? ", " : " ");
                number %= 1000000;
            }

            int thousands = number / 1000;
            if (thousands > 0)
            {
                builder.Append($"{TranslateInteger(thousands)}thousand");
                builder.Append((number - thousands * 1000)/100 > 0 ? ", " : " ");
                number %= 1000;
            }

            int hundreds = number / 100;
            if (hundreds > 0)
            {
                builder.Append($"{TranslateInteger(hundreds)}hundred ");
                number %= 100;
            }

            if (number > 0)
            {
                if (builder.Length > 0)
                    builder.Append("and ");

                if (number < 20)
                    builder.Append($"{fromZeroToNineteen[number]} ");
                else
                {
                    builder.Append($"{tens[number / 10]} ");
                    if ((number % 10) > 0)
                        builder.Append($"{fromZeroToNineteen[number % 10]} ");
                }
            }

            return builder.ToString();
        }
    }
}
