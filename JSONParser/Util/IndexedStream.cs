using System;
using System.Collections.Generic;
using System.Linq;
using JSONParser.Parser;

namespace JSONParser.Util
{
    public class IndexedStream<TElement, TConstraint>
    {
        private int index;
        private readonly List<TElement> input;
        private readonly Func<TElement, TConstraint, bool> constraintChecker;

        public IndexedStream(IEnumerable<TElement> input, Func<TElement, TConstraint, bool> constraintChecker)
        {
            this.input = input.ToList();
            this.constraintChecker = constraintChecker;
        }

        public TElement Current()
        {
            if (End())
            {
                throw NewEndOfStreamException();
            }
            return input[index];
        }

        public TElement Consume()
        {
            if (End())
            {
                throw NewEndOfStreamException();
            }
            return input[index++];
        }

        public TElement Consume(TConstraint expectedType)
        {
            var elem = Consume();
            if (constraintChecker(elem, expectedType))
            {
                return elem;
            }
            throw NewParseException();
        }

        public bool End()
        {
            return index >= input.Count;
        }

        public Exception NewParseException()
        {
            return new ParseException($"Invalid token {Current()} at position {index}");
        }

        public Exception NewEndOfStreamException()
        {
            return new ParseException("End of stream");
        }

        public override string ToString()
        {
            return $"[CharStream Index={index}, Input={input}]";
        }
    }
}