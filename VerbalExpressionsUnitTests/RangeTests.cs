using System;
using CSharpVerbalExpressions;
using NUnit.Framework;

namespace VerbalExpressionsUnitTests
{
    [TestFixture]
    public class RangeTests
    {
        [Test]
        public void Range_WhenTooManyItemsInArray_ShouldThrowArgumentOutOfRangeException()
        {
            var verbEx = VerbalExpressions.DefaultExpression;
            object[] range = new object[4] { 1, 6, 7, 12 };

            //Act
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => verbEx.Range(range));
        }

        [Test]
        public void Range_WhenOddNumberOfItemsInArray_ShouldAppendLastElementWithOrClause()
        {
            //Arrange
            var verbEx = VerbalExpressions.DefaultExpression;
            string text = "abcd7sdadqascdaswde";
            object[] range = new object[3] { 1, 6, 7 };

            //Act
            verbEx.Range(range);
            //Assert
            Assert.IsTrue(verbEx.IsMatch(text));
        }

        [Test]
        public void Range_WhenOddNumberOfItemsInArray_ShouldAppendWithPipe()
        {
            //Arrange
            var verbEx = VerbalExpressions.DefaultExpression;
            object[] range = new object[3] { 1, 6, 7 };
            string expectedExpression = "[1-6]|7";

            //Act
            verbEx.Range(range);

            //Assert
            Assert.AreEqual(expectedExpression, verbEx.ToString());
        }

        [Test]
        public void Range_WhenNullParameterPassed_ShouldThrowArgumentNullException()
        {
            //Arrange
            var verbEx = VerbalExpressions.DefaultExpression;
            object[] value = null;

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => verbEx.Range(value));
        }

        [Test]
        public void Range_MultipleRanges_ShouldResult()
        {
            var email = "JStephens@testsite.com";

            var t = VerbalExpressions.DefaultExpression;
            //regex [A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}

            //t.Range('A', 'Z' ,'a', 'z', 0, 9, '.', '_', '%', '+', '-');

            t.Range(p => 
                p.R('A', 'Z')
                    .R('a', 'z')
                    .R(0, 9)
                    .R('.',false)
                    .R('_')
                    .R('%')
                    .R('+',false)
                    .R('-'));

            Assert.AreEqual("[A-Za-z0-9._%+-]",t.ToString());
        }


        [Test]
        public void Range_WhenArrayParameterHasOnlyOneValue_ShouldThrowArgumentOutOfRangeException()
        {
            //Arrange
            var verbEx = VerbalExpressions.DefaultExpression;
            object[] value = new object[1] { 0 };

            //Act
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => verbEx.Range(value));
        }

        [Test]
        public void Range_WhenArrayParameterHasValuesInReverseOrder_ReturnsCorrectResultForCorrectOrder()
        {
            //Arrange
            var verbEx = VerbalExpressions.DefaultExpression;
            object[] inversedOrderArray = new object[2] { 9, 2 };
            verbEx.Range(inversedOrderArray);
            string lookupString = "testing 8 another test";

            //Act
            bool isMatch = verbEx.IsMatch(lookupString);

            //Assert
            Assert.IsTrue(isMatch);
        }

        [Test]
        public void Range_WhenArrayContainsNullParameter_ItIsIgnoredAndRemovedFromList()
        {
            //Arrange
            var verbEx = VerbalExpressions.DefaultExpression;
            object[] inversedOrderArray = new object[4] { 1, null, null, 7 };
            verbEx.Range(inversedOrderArray);
            string lookupString = "testing 5 testing";

            //Act
            bool isMatch = verbEx.IsMatch(lookupString);

            //Assert
            Assert.IsTrue(isMatch);
        }
    }
}
