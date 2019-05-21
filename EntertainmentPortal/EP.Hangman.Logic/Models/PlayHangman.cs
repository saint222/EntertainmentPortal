using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.Hangman.Logic.Models
{
    //public class PlayHangman
    //{
    //    private const int ATTEMPTS = 6;
    //    private int _userAttempts = 0;


    //    public PlayHangman()
    //    {
    //        var word = new PickedWord();
    //        _pickedWord = word.Content;
    //        for (int i = 0; i < _pickedWord.Length; i++)
    //        {
    //            _correctLetters.Add("_");
    //        }
    //    }

    //    public string CorrectLetters
    //    {
    //        get { return ListToString(_correctLetters); }
    //    }

    //    public string WrongLetters
    //    {
    //        get
    //        {
    //            return ListToString(_wrongLetters);
    //        }
    //    }

    //    public int UserAttempts
    //    {
    //        get { return (ATTEMPTS - _userAttempts); }
    //    }

    //    public string PlayGame(string letter)
    //    {
           
    //        if (_userAttempts < ATTEMPTS)
    //        {
    //            if (_pickedWord.Contains(letter))
    //            {
    //                _correctLetters[_pickedWord.IndexOf(letter)] = letter;
    //                return "Correct letter";
    //            }
    //            else
    //            {
    //                _userAttempts++;
    //                _wrongLetters.Add(letter);
    //                return "Wrong letter";
    //            }
    //        }
    //        else
    //        {
    //            return "Game over";
    //        }

    //    }

    //    private string ListToString(List<string> list)
    //    {
    //        string result = null;
    //        foreach (var letter in list)
    //        {
    //            result += letter;
    //        }

    //        return result;
    //    }
    //}
}
