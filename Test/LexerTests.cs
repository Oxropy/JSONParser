using JSONParser.Parser;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class LexerTests
    {
        [Test]
        public void TestLex()
        {
            JsonLexer.Lex("nulltruenull");
        }
    }
}