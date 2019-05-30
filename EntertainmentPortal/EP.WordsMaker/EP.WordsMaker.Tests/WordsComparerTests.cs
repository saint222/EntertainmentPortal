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
		public void Test1()
		{
			WordComparer wordComparer = new WordComparer();
			Word keyWord = new Word("соковыжималка");

			Word userWord = new Word("Киоск");
			bool result = wordComparer.CompareWord(keyWord.String, userWord.String);
			Assert.IsTrue(result);

			userWord = new Word("волосы");
			result = wordComparer.CompareWord(keyWord.String, userWord.String);
			Assert.IsTrue(result);

			userWord = new Word("Велосипед");
			result = wordComparer.CompareWord(keyWord.String, userWord.String);
			Assert.IsFalse(result);

			userWord = new Word("Костыль");
			result = wordComparer.CompareWord(keyWord.String, userWord.String);
			Assert.IsFalse(result);
		}
	}
}
