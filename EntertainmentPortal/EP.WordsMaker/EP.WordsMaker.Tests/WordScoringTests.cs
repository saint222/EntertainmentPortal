using NUnit.Framework;
using EP.WordsMaker.Logic.Models;

namespace Tests
{
	public class WordScoringTests
	{
		[SetUp]
		public void Setup()
		{
			
		}

        [Test]
        [TestCase("дятел", ExpectedResult = 50)]
        [TestCase("сок", ExpectedResult = 30)]
        public int CorrectComputeScoringTests(string word)
        {
            Rules rul = new Rules();
            return rul.ComputeScoring(word);
        }

        [Test]
        [TestCase("дятел", ExpectedResult = 14)]
        [TestCase("сок", ExpectedResult = 7)]
        public int CorrectHardScoringTests(string word)
        {
            Rules rul = new Rules();
            return rul.HardScoring(word);
        }

        [Test]
        [TestCase("дятел", ExpectedResult = 62)]
        [TestCase("сок", ExpectedResult = 30)]
        public int CorrectCountingByLengthScoringTests(string word)
        {
            Rules rul = new Rules();
            return rul.CountingByLength(word);
        }
    }
}