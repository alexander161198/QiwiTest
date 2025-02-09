namespace QiwiTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            var parser = new DecimalParser();

            while (true)
            {
                string input = Console.ReadLine();

                //Проверка на некорректный формат введённых данных
                if (!parser.ParseFromString(input, out decimal value))
                {
                    Console.WriteLine("Incorrect format");
                }
                else
                {
                    Console.WriteLine(StringTranslator.TranslateDecimal(value));
                }
            }
        }
    }
}
