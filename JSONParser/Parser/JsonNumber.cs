namespace JSONParser.Parser
{
    public class JsonNumber : JsonValue
    {
        public readonly double value;

        public JsonNumber(double value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return $"Number({value})";
        }

        public override bool Equals(object obj)
        {
            var other = obj as JsonNumber;
            return other != null && Equals(value, other.value);
        }

        public override int GetHashCode()
        {
            return 0 + 1000000007 * value.GetHashCode();
        }
    }
}