using QiwiTest;
using Xunit;

namespace TestProject
{
    public class UnitTest1
    {
        private IParser<decimal> _parser = new DecimalParser();

        [Theory]
        [InlineData("test")]
        [InlineData("333.test")]
        [InlineData("-25.43")]
        [InlineData("0")]
        [InlineData("2000000000")]
        public void TestWrongNumber(string input)
        {
            var parseResult = _parser.ParseFromString(input, out decimal value);

            Assert.False(parseResult);
        }

        [Theory]
        [InlineData("1357256.32", "one million, three hundred and fifty seven thousand, two hundred and fifty six DOLLARS AND thirty two CENTS")]
        [InlineData("1357256012", "one billion, three hundred and fifty seven million, two hundred and fifty six thousand and twelve DOLLARS")] //без центов
        [InlineData("0.75", "seventy five CENTS")]                                                                                              //без долларов
        [InlineData("1.01", "one DOLLAR AND one CENT")]                                                                                         //проверка окончаний
        public void TestPositive(string input, string correctString)
        {
            var parseResult = _parser.ParseFromString(input, out decimal value);

            string result = StringTranslator.TranslateDecimal(value);

            Assert.Equal(correctString, result);
        }
    }
}