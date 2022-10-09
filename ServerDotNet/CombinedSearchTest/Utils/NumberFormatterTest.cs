using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;
using CombinedSearch.Utils;

namespace SearchInSeveralEngineTest.Utils
{
    public class NumberFormatterTest
    {

        [Test]
        public void NumericFormattedNumberShould()
        {
            using (new AssertionScope())
            {
                NumberFormatter.GetNumericFormattedNumber("1756700").Should().Be("1,76M");
                NumberFormatter.GetNumericFormattedNumber("1756700").Should().NotBe("1,75M");

                NumberFormatter.GetNumericFormattedNumber("73954571234").Should().Be("73,955B");

                NumberFormatter.GetNumericFormattedNumber("1739").Should().Be("1,7K");
                NumberFormatter.GetNumericFormattedNumber("1739").Should().NotBe("1,739K");

                NumberFormatter.GetNumericFormattedNumber("hello").Should().Be("Invalid number");
            }
        }

        [Test]
        public void ToNumericFormattExtentionShould()
        {
            using (new AssertionScope())
            {
                100.ToNumericFormat().Should().Be("100");

                10000.ToNumericFormat().Should().Be("10K");
                1000.ToNumericFormat().Should().NotBe("10K");


                73954571234.ToNumericFormat().Should().Be("73,955B");
                73954471234.ToNumericFormat().Should().NotBe("73,955B");

                1756700.ToNumericFormat().Should().Be("1,76M");
                1756700.ToNumericFormat().Should().NotBe("1,75M");
            }
        }
    }
}
