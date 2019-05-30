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
			Word keyWord = new Word("cоковыжималка");
			//Word userWord = new Word("киоск");
			Word userWord = new Word("киоск");

			WordComparer wordComparer = new WordComparer();
			bool result = wordComparer.CompareWord(keyWord.String, userWord.String);
			Assert.IsTrue(result);			
		}
	}
}
