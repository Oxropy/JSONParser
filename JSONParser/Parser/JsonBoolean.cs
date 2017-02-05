namespace JSONParser.Parser
{
    public class JsonBoolean : JsonValue
    {
        public static readonly JsonBoolean TRUE = new JsonBoolean(true);
        public static readonly JsonBoolean FALSE = new JsonBoolean(false);
        public readonly bool value;

        public JsonBoolean(bool value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return $"Bool({value})";
        }

        public override bool Equals(object obj)
        {
            var jsonBoolean = obj as JsonBoolean;
            if (jsonBoolean == null)
            {
                return false;
            }
            return value == jsonBoolean.value;
        }

        public override int GetHashCode()
        {
            return 0 + 1000000007 * value.GetHashCode();
        }
    }
}