using NUnit.Framework;
using System.Collections.Generic;
using EP.Hangman.Data;
using EP.Hangman.Data.Models;
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
            alphabet = new Alphabets().EnglishAlphabet();
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
        GameDb newData;

        [SetUp]
        public void Setup()
        {
            newData = new GameDb();
        }

        [TearDown]
        public void AfterTests()
        {
            newData = null;
        }

        [Test]
        [TestCase(0, ExpectedResult = 1)]
        [TestCase(2, ExpectedResult = 3)]
        public int TextCorrectIncrementOfAttemptsOnWrongLetter(int x)
        {
            newData.UserErrors = x;
            newData.PickedWord = "OMG";
            newData.Alphabet = new Alphabets().EnglishAlphabet();
            newData.CorrectLetters = new List<string> {"_", "_", "_"};

            return new HangmanGame(newData).Play("A").UserErrors;
        }

        [Test]
        [TestCase("O", 0)]
        [TestCase("G", 2)]
        public void TextCorrectReplaceOnCorrectLetter(string letter, int position)
        {
            newData.UserErrors = 0;
            newData.PickedWord = "OMG";
            newData.Alphabet = new Alphabets().EnglishAlphabet();
            newData.CorrectLetters = new List<string> { "_", "_", "_" };

            Assert.AreEqual(letter, new HangmanGame(newData).Play(letter).CorrectLetters[position]);
        }

        [Test]
        public void TestNoMoreAttempts()
        {
            newData.UserErrors = 6;
            Assert.IsNull(new Models.HangmanGame(newData).Play("A"));
        }

        [Test]
        [TestCase("O")]
        [TestCase("C")]
        public void TestDeleteLetterFromAlphabet(string letter)
        {
            newData.UserErrors = 0;
            newData.PickedWord = "YES";
            newData.Alphabet = new Alphabets().EnglishAlphabet();
            newData.CorrectLetters = new List<string> { "_", "_", "_" };

            Assert.False(new HangmanGame(newData).Play(letter).Alphabet.Contains(letter));
        }
    }
}