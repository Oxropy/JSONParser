using System.Collections.Generic;
using System.Linq;

namespace JSONParser.Parser
{
    public class JsonArray : JsonValue
    {
        public readonly List<JsonValue> children;

        public JsonArray()
        {
            children = new List<JsonValue>();
        }

        public JsonArray(List<JsonValue> children)
        {
            this.children = children;
        }

        public JsonArray(params JsonValue[] children) : this(children.ToList())
        {
        }

        public override string ToString()
        {
            return $"Array({string.Join(",", children)})";
        }

        public override bool Equals(object obj)
        {
            var other = obj as JsonArray;
            if (other == null || children.Count != other.children.Count)
            {
                return false;
            }
            for (var i = 0; i < children.Count; ++i)
            {
                if (!Equals(children[i], other.children[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            var num = 0;
            if (children != null)
            {
                num += 1000000007 * children.GetHashCode();
            }
            return num;
        }
    }
}