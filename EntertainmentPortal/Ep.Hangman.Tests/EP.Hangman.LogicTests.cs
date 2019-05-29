using NUnit.Framework;
using System.Collections.Generic;
using EP.Hagman.Data;
using EP.Hangman.Logic.Models;

namespace Tests
{
    [TestFixture]
    public class AlphabetTests
    {
        List<string> alphabet;

        [SetUp]
        public void Setup()
        {
            alphabet = new HangmanAlphabets().EnglishAlphabet();
        }

        [TearDown]
        public void AfterTests()
        {
            alphabet = null;
        }

        [Test]
        public void TestEnglishAlphabetIsCorrect()
        {
            Assert.AreEqual(26, alphabet.Count);
            Assert.AreEqual("A", alphabet[0]);
            Assert.AreEqual("Z", alphabet[alphabet.Count - 1]);
        }
    }

    [TestFixture]
    public class PlayHangmanTests
    {
        HangmanTemporaryData newData;

        [SetUp]
        public void Setup()
        {
        }

        [TearDown]
        public void AfterTests()
        {
            newData = null;
        }

        [Test]
        [TestCase(0, ExpectedResult = 6)]
        [TestCase(6, ExpectedResult = 0)]
        [TestCase(2, ExpectedResult = 4)]
        public int TestCorrectCalculatingOfUserAttempts(int x)
        {
            // todo: make ctor without letter
            newData.temp.UserAttempts = x;
            return new PlayHangman(newData, "a").UserAttempts;
        }

        [Test]
        [TestCase(0, ExpectedResult = 1)]
        [TestCase(2, ExpectedResult = 3)]
        public int TextCorrectIncrementOfAttemptsOnWrongLetter(int x)
        {
            newData.temp.UserAttempts = x;
            newData.temp.PickedWord = "OMG";
            newData.temp.AlphabetTempData = new HangmanAlphabets().EnglishAlphabet();
            newData.temp.CorrectLettersTempData = new List<string> {"_", "_", "_"};

            return new PlayHangman(newData, "A").UserAttempts;
        }

        [Test]
        [TestCase()]
        [TestCase("O", 0)]
        [TestCase("G", 2)]
        public void TextCorrectReplaceOnCorrectLetter(string letter, int position)
        {
            newData.temp.UserAttempts = 0;
            newData.temp.PickedWord = "OMG";
            newData.temp.AlphabetTempData = new HangmanAlphabets().EnglishAlphabet();
            newData.temp.CorrectLettersTempData = new List<string> { "_", "_", "_" };
            Assert.AreEqual(letter, new PlayHangman(newData, letter).PlayGame().temp.CorrectLettersTempData[position]);
        }

        [Test]
        public void TestNoMoreAttempts()
        {
            newData.temp.UserAttempts = 6;
            Assert.IsNull(new PlayHangman(newData, "A").PlayGame());
        }

        [Test]
        [TestCase("O")]
        [TestCase("C")]
        public void TestDeleteLetterFromAlphabet(string letter)
        {
            newData.temp.UserAttempts = 0;
            newData.temp.PickedWord = "YES";
            newData.temp.AlphabetTempData = new HangmanAlphabets().EnglishAlphabet();
            newData.temp.CorrectLettersTempData = new List<string> { "_", "_", "_" };
            Assert.False(new PlayHangman(newData, letter).PlayGame().temp.AlphabetTempData.Contains(letter));
        }
    }
}