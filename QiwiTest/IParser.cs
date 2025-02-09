namespace QiwiTest
{
    public interface IParser<T>
    {
        bool ParseFromString(string input, out T value);
    }
}
