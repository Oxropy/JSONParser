using System;

namespace JSONParser.Parser
{
    public class LexException : Exception
    {
        public LexException(string message, int row, int column) : base($"Message: {message}; Row: {row}; Column: {column}")
        {
        }
    }
}