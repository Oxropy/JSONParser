namespace JSONParser.Parser
{
    public class Token
    {
        public readonly TokenType Type;
        public readonly string Value;
        public readonly int Row;
        public readonly int Column;

        public Token(TokenType type, string value, int row, int column)
        {
            Type = type;
            Value = value;
            Row = row;
            Column = column;
        }

        public override string ToString()
        {
            return $"Token(Type={Type}, Value={Value}, Row={Row}, Column={Column}";
        }
    }
}