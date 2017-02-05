using System;

namespace JSONParser.Parser
{
  public class ParseException : Exception
  {
    public ParseException(string message) : base(message)
    {
    }
  }
}
