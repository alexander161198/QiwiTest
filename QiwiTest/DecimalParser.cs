using System.Globalization;

namespace QiwiTest
{
    public class DecimalParser : IParser<decimal>
    {
        public bool ParseFromString(string input, out decimal value)
        {
            //Если строку нельзя распарсить как decimal, число меньше нуля или больше 2 миллиардов, возвращаем fasle
            if (!decimal.TryParse(input, CultureInfo.InvariantCulture, out value) || value <= 0 || value >= (decimal)2_000_000_000.0)
            {
                return false;
            }

            //округляем до 2 знаков после запятой, т.к. кол-во центов может быть от 0 до 99
            value = Math.Round(value, 2);
            return true;
        }
    }
}
