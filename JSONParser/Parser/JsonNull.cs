namespace JSONParser.Parser
{
    public class JsonNull : JsonValue
    {
        public static readonly JsonNull NULL = new JsonNull();

        public override string ToString()
        {
            return "Null";
        }

        public override bool Equals(object obj)
        {
            return obj is JsonNull;
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}