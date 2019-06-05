using NUnit.Framework;
using System.Collections.Generic;
using EP.Hangman.Data.Models;
using EP.Hangman.Logic.Models;

namespace EP.Hangman.Logic.Tests
{
    [TestFixture]
    public class AlphabetTests
    {
        List<string> _alphabet;

        [SetUp]
        public void Setup()
        {
            _alphabet = new Alphabets().EnglishAlphabet();
        }

        [TearDown]
        public void AfterTests()
        {
            _alphabet = null;
        }

        [Test]
        public void TestEnglishAlphabetIsCorrect()
        {
            Assert.AreEqual(26, _alphabet.Count);
            Assert.AreEqual("A", _alphabet[0]);
            Assert.AreEqual("Z", _alphabet[_alphabet.Count - 1]);
        }
    }

    [TestFixture]
    public class PlayHangmanTests
    {
        UserGameData _newData;

        [SetUp]
        public void Setup()
        {
            _newData = new UserGameData();
        }

        [TearDown]
        public void AfterTests()
        {
            _newData = null;
        }

        [Test]
        [TestCase(0, ExpectedResult = 1)]
        [TestCase(2, ExpectedResult = 3)]
        public int TextCorrectIncrementOfAttemptsOnWrongLetter(int x)
        {
            _newData.UserErrors = x;
            _newData.PickedWord = "OMG";
            _newData.Alphabet = new Alphabets().EnglishAlphabet();
            _newData.CorrectLetters = new List<string> {"_", "_", "_"};

            return new HangmanGame(_newData).Play("A").UserErrors;
        }

        [Test]
        [TestCase("O", 0)]
        [TestCase("G", 2)]
        public void TextCorrectReplaceOnCorrectLetter(string letter, int position)
        {
            _newData.UserErrors = 0;
            _newData.PickedWord = "OMG";
            _newData.Alphabet = new Alphabets().EnglishAlphabet();
            _newData.CorrectLetters = new List<string> { "_", "_", "_" };

            Assert.AreEqual(letter, new HangmanGame(_newData).Play(letter).CorrectLetters[position]);
        }

        [Test]
        public void TestNoMoreAttempts()
        {
            _newData.UserErrors = 6;
            Assert.IsNull(new Models.HangmanGame(_newData).Play("A"));
        }

        [Test]
        [TestCase("O")]
        [TestCase("C")]
        public void TestDeleteLetterFromAlphabet(string letter)
        {
            _newData.UserErrors = 0;
            _newData.PickedWord = "YES";
            _newData.Alphabet = new Alphabets().EnglishAlphabet();
            _newData.CorrectLetters = new List<string> { "_", "_", "_" };

            Assert.False(new HangmanGame(_newData).Play(letter).Alphabet.Contains(letter));
        }
    }
}