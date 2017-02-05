namespace JSONParser.Parser
{
    public enum TokenType
    {
        BeginObject = 1,
        EndObject,
        BeginArray,
        EndArray,
        PairSeperator,
        ElementSeperator,
        Number,
        String,
        Whitespace,
        Boolean,
        Null,
        NewLine
    }
}