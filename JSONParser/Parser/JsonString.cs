namespace JSONParser.Parser
{
    public class JsonString : JsonValue
    {
        public readonly string value;

        public JsonString(string value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return $"String({value})";
        }

        public override bool Equals(object obj)
        {
            var jsonString = obj as JsonString;
            if (jsonString == null)
            {
                return false;
            }
            return value == jsonString.value;
        }

        public override int GetHashCode()
        {
            var num = 0;
            if (value != null)
            {
                num += 1000000007 * value.GetHashCode();
            }
            return num;
        }
    }
}