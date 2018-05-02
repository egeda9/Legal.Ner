using System;

namespace Legal.Ner.Log
{
    public interface ILogger
    {
        void Info(string message);
        void Error(string message, Exception exception);
    }
}
