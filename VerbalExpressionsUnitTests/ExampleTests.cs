using CSharpVerbalExpressions;
using NUnit.Framework;

namespace VerbalExpressionsUnitTests
{
    [TestFixture]
    public class ExampleTests
    {
        private VerbalExpressions e => VerbalExpressions.DefaultExpression;
        [Test]
        public void Email_Should_Match()
        {
            var email = "JStephens@testsite.com";

            var t = VerbalExpressions.DefaultExpression;
            //regex [A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}

            t.Multiple(e.Range('A', 'Z', 'a', 'z', 0, 9, '.', '_', '%', '+', '-').ToString())
                .Add("@")
                .Multiple(e.Range('A','Z','a','z',0,9,'.','-').ToString())
                .Add(@".")
                .Multiple(e.Range('A','Z','a','z').RepeatPrevious(2,4).ToString());

            Assert.True(t.IsMatch(email));

        }
    }
}