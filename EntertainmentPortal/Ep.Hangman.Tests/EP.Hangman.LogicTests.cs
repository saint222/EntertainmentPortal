using NUnit.Framework;
using System.Collections.Generic;
using EP.Hagman.Data;
using EP.Hangman.Logic.Models;

namespace EP.Hangman.Logic.Tests
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
        TemporaryData newData;
        HangmanTemporaryData newHangmanData;

        [SetUp]
        public void Setup()
        {
            newData = new TemporaryData();
            newHangmanData = new HangmanTemporaryData();

        }

        [TearDown]
        public void AfterTests()
        {
            newData = null;
            newHangmanData = null;
        }

        [Test]
        [TestCase(0, ExpectedResult = 6)]
        [TestCase(6, ExpectedResult = 0)]
        [TestCase(2, ExpectedResult = 4)]
        public int TestCorrectCalculatingOfUserAttempts(int x)
        {
            // todo: make ctor without letter
            newData.UserAttempts = x;
            newHangmanData.temp = newData;
            return new PlayHangman(newHangmanData).UserAttempts;
        }

        [Test]
        [TestCase(0, ExpectedResult = 1)]
        [TestCase(2, ExpectedResult = 3)]
        public int TextCorrectIncrementOfAttemptsOnWrongLetter(int x)
        {
            newData.UserAttempts = x;
            newData.PickedWord = "OMG";
            newData.AlphabetTempData = new HangmanAlphabets().EnglishAlphabet();
            newData.CorrectLettersTempData = new List<string> {"_", "_", "_"};
            newHangmanData.temp = newData;

            return new PlayHangman(newHangmanData, "A").PlayGame().temp.UserAttempts;
        }

        [Test]
        [TestCase("O", 0)]
        [TestCase("G", 2)]
        public void TextCorrectReplaceOnCorrectLetter(string letter, int position)
        {
            newData.UserAttempts = 0;
            newData.PickedWord = "OMG";
            newData.AlphabetTempData = new HangmanAlphabets().EnglishAlphabet();
            newData.CorrectLettersTempData = new List<string> { "_", "_", "_" };
            newHangmanData.temp = newData;
            Assert.AreEqual(letter, new PlayHangman(newHangmanData, letter).PlayGame().temp.CorrectLettersTempData[position]);
        }

        [Test]
        public void TestNoMoreAttempts()
        {
            newData.UserAttempts = 6;
            newHangmanData.temp = newData;
            Assert.IsNull(new PlayHangman(newHangmanData).PlayGame());
        }

        [Test]
        [TestCase("O")]
        [TestCase("C")]
        public void TestDeleteLetterFromAlphabet(string letter)
        {
            newData.UserAttempts = 0;
            newData.PickedWord = "YES";
            newData.AlphabetTempData = new HangmanAlphabets().EnglishAlphabet();
            newData.CorrectLettersTempData = new List<string> { "_", "_", "_" };
            newHangmanData.temp = newData;
            Assert.False(new PlayHangman(newHangmanData, letter).PlayGame().temp.AlphabetTempData.Contains(letter));
        }
    }
}