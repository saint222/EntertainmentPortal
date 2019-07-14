using NUnit.Framework;
using EP.WordsMaker.Logic.Models;

namespace Tests
{
	public class WordTests
	{
		[SetUp]
		public void Setup()
		{
			
		}

		[Test]
		public void Test1()
		{
			Word word = new Word("Соковыжималка");
			foreach(char letter in word.Letters)
			{
				char _letter = letter;
			}
			
			Assert.Pass();
		}

        [Test]
        public void Test2()
        {
            Rules rul = new Rules();
            var fd = rul.HardScoring("Соковыжималка");

        }
    }
}