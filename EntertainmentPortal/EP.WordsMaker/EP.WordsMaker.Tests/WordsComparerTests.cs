using NUnit.Framework;
using EP.WordsMaker.Logic.Models;

namespace Tests
{
	class WordsComparerTests
	{
		[SetUp]
		public void Setup()
		{

		}

		[Test]
        [TestCase("Киоск")]
        [TestCase("волосы")]
        public void CorrectWordCompareTests(string playerWord)
		{
			WordComparer wordComparer = new WordComparer();

            Assert.IsTrue(wordComparer.CompareWord("соковыжималка", playerWord));
        }

        [Test]
        [TestCase("Велосипед")]
        [TestCase("Костыль")]
        public void InCorrectWordCompareTests(string playerWord)
        {
            WordComparer wordComparer = new WordComparer();

            Assert.IsFalse(wordComparer.CompareWord("соковыжималка", playerWord));
        }

    }
}
