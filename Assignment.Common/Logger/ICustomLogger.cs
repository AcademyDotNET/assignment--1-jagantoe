namespace Assignment.Common.Logger
{
    public interface ICustomLogger
    {
        void Log(string message);
        void Log(string message, object data);
    }
}
