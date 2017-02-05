using System;
using System.Collections.Generic;
using System.Linq;

namespace JSONParser.Parser
{
    public class JsonObject : JsonValue
    {
        public static readonly JsonObject EMPTY = new JsonObject();
        public readonly Dictionary<string, JsonValue> children;

        public JsonObject(Dictionary<string, JsonValue> children)
        {
            this.children = children;
        }

        public JsonObject() : this(new Dictionary<string, JsonValue>())
        {
        }

        public JsonObject(params Tuple<string, JsonValue>[] members) : this(ToDict(members))
        {
        }

        public JsonObject(List<Tuple<string, JsonValue>> members) : this(ToDict(members))
        {
        }

        private static Dictionary<string, JsonValue> ToDict(IEnumerable<Tuple<string, JsonValue>> members)
        {
            var dictionary = new Dictionary<string, JsonValue>();
            foreach (var member in members)
            {
                dictionary.Add(member.Item1, member.Item2);
            }
            return dictionary;
        }

        public override string ToString()
        {
            var str = "Object(";
            if (children.Count > 0)
            {
                var first = children.First();
                str = str + first.Key + ':' + first.Value;
                foreach (var e in children.Skip(1))
                {
                    str = str + ", " + e.Key + ':' + e.Value;
                }
            }
            return str + ')';
        }

        public override bool Equals(object obj)
        {
            var other = obj as JsonObject;
            if (other == null || children.Count != other.children.Keys.Count)
            {
                return false;
            }
            foreach (var e in children)
            {
                if (!other.children.ContainsKey(e.Key) || !Equals(e.Value, other.children[e.Key]))
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