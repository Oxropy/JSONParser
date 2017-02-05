using System.Collections.Generic;
using JSONParser.Parser;
using NUnit.Framework;

namespace Test
{
    [TestFixture]
    public class ParserTests
    {
        public static TokenStream Stream(string s)
        {
            return new TokenStream(JsonLexer.Lex(s));
        }

        public static JsonNumber Num(double d)
        {
            return new JsonNumber(d);
        }

        public static JsonString Str(string s)
        {
            return new JsonString(s);
        }

        public static JsonArray Arr(params JsonValue[] elems)
        {
            return new JsonArray(elems);
        }

        [Test]
        public void TestParseString()
        {
            Assert.That(JsonParser.ParseString(Stream("\"String\\\" \\\\ \\/ \\b \\f \\n \\r \\t \\u1337 \"")),
                Is.EqualTo(Str("String\" \\ / \b \f \n \r \t \u1337 ")), "ParseString");
        }

        [Test]
        public void TestParseNumber()
        {
            Assert.That(JsonParser.Start("132"), Is.EqualTo(Num(132.0)), "ParseNumber Int");
            Assert.That(JsonParser.Start("-132"), Is.EqualTo(Num(-132.0)), "ParseNumber Negativ Int");
            Assert.That(JsonParser.Start("456.123"), Is.EqualTo(Num(456.123)), "ParseNumber Double");
            Assert.That(JsonParser.Start("-456.123"), Is.EqualTo(Num(-456.123)),  "ParseNumber Negativ Double");
            Assert.That(JsonParser.Start("0.654"), Is.EqualTo(Num(0.654)), "ParseNumber Double Leading Null");
            Assert.That(JsonParser.Start("-0.654"), Is.EqualTo(Num(-0.654)), "ParseNumber Negativ Double Leading Null");
            Assert.That(JsonParser.Start("6e54"), Is.EqualTo(Num(6E+54)), "ParseNumber E (e)");
            Assert.That(JsonParser.Start("-6e54"), Is.EqualTo(Num(-6E+54)), "ParseNumber Negativ E (e)");
            Assert.That(JsonParser.Start("6E54"), Is.EqualTo(Num(6E+54)), "ParseNumber E (E)");
            Assert.That(JsonParser.Start("-6E54"), Is.EqualTo(Num(-6E+54)), "ParseNumber Negativ E (E)");
            Assert.That(JsonParser.Start("6e+54"), Is.EqualTo(Num(6E+54)), "ParseNumber E (e+)");
            Assert.That(JsonParser.Start("-6e+54"), Is.EqualTo(Num(-6E+54)), "ParseNumber Negativ E (e+)");
            Assert.That(JsonParser.Start("6E+54"), Is.EqualTo(Num(6E+54)), "ParseNumber E (E+)");
            Assert.That(JsonParser.Start("-6E+54"), Is.EqualTo(Num(-6E+54)), "ParseNumber Negativ E (E+)");
            Assert.That(JsonParser.Start("6e-54"), Is.EqualTo(Num(6E-54)), "ParseNumber E (e-)");
            Assert.That(JsonParser.Start("-6e-54"), Is.EqualTo(Num(-6E-54)), "ParseNumber Negativ E (e-)");
            Assert.That(JsonParser.Start("6E-54"), Is.EqualTo(Num(6E-54)), "ParseNumber E (E-)");
            Assert.That(JsonParser.Start("-6E-54"), Is.EqualTo(Num(-6E-54)), "ParseNumber Negativ E (E-)");
            Assert.That(JsonParser.Start("6.987e54"), Is.EqualTo(Num(6.987E+54)), "ParseNumber Double E (e)");
            Assert.That(JsonParser.Start("-6.987e54"), Is.EqualTo(Num(-6.987E+54)), "ParseNumber Negativ Double E (e)");
            Assert.That(JsonParser.Start("6.987E54"), Is.EqualTo(Num(6.987E+54)), "ParseNumber Double E (E)");
            Assert.That(JsonParser.Start("-6.987E54"), Is.EqualTo(Num(-6.987E+54)), "ParseNumber Negativ Double E (E)");
            Assert.That(JsonParser.Start("6.987e+54"), Is.EqualTo(Num(6.987E+54)), "ParseNumber Double E (e+)");
            Assert.That(JsonParser.Start("-6.987e+54"), Is.EqualTo(Num(-6.987E+54)), "ParseNumber Negativ Double E (e+)");
            Assert.That(JsonParser.Start("6.987E+54"), Is.EqualTo(Num(6.987E+54)), "ParseNumber Double E (E+)");
            Assert.That(JsonParser.Start("-6.987E+54"), Is.EqualTo(Num(-6.987E+54)), "ParseNumber Negativ Double E (E+)");
            Assert.That(JsonParser.Start("6.987e-54"), Is.EqualTo(Num(6.987E-54)), "ParseNumber Double E (e-)");
            Assert.That(JsonParser.Start("-6.987e-54"), Is.EqualTo(Num(-6.987E-54)), "ParseNumber Negativ Double E (e-)");
            Assert.That(JsonParser.Start("6.987E-54"), Is.EqualTo(Num(6.987E-54)), "ParseNumber Double E (E-)");
            Assert.That(JsonParser.Start("-6.987E-54"), Is.EqualTo(Num(-6.987E-54)), "ParseNumber Negativ Double E (E-)");
        }

        [Test]
        public void TestParseObject()
        {
            Assert.That(JsonParser.Start("{\"a\":\"b\"}"), Is.EqualTo(new JsonObject(new Dictionary<string, JsonValue>
            {
                {
                    "a",
                    Str("b")
                }
            })), "ParseNumber Object a b");
            Assert.That(JsonParser.Start("{\"a\":123}"), Is.EqualTo(new JsonObject(new Dictionary<string, JsonValue>
            {
                {
                    "a",
                    Num(123.0)
                }
            })), "ParseNumber Object a 123");
        }

        [Test]
        public void TestParseArray()
        {
            Assert.That(JsonParser.Start("[1,\"a\",2.5,true,null,false,{\"b\":654}]"), Is.EqualTo(
                Arr(
                    Num(1),
                    Str("a"),
                    Num(2.5),
                    JsonBoolean.TRUE,
                    JsonNull.NULL,
                    JsonBoolean.FALSE,
                    new JsonObject(new Dictionary<string, JsonValue> {{ "b", Num(654) }}))
                ), "ParseArray");
        }

        [Test]
        public void TestParseBoolean()
        {
            Assert.That(JsonParser.Start("true"), Is.EqualTo(JsonBoolean.TRUE), "ParseBoolean True");
            Assert.That(JsonParser.Start("false"), Is.EqualTo(JsonBoolean.FALSE), "ParseBoolean False");
        }
    }
}