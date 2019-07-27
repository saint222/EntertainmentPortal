using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EP.WordsMaker.Logic.Models
{
    /// <summary>
    /// Represents <c>Rules</c> class.
    /// </summary>
    public class Rules
    {
        /// <summary>
        /// Description property
        /// </summary>
        /// <value>Game description</value>
        public string _description;

        /// <summary>
        /// Min lenght word property
        /// </summary>
        /// <value>Words lenght restriction</value>
        public int MinWordLenght { get; set; }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="word"></param>
		/// <returns></returns>
		public int ComputeScoring(string word)
		{
			return (word.Length * 10);
		}

        public Dictionary<char, int> scoreFromLetter = new Dictionary<char, int> {
            {'о', 1},
            {'е', 1},
            {'а', 2},
            {'и', 2},
            {'н', 2},
            {'т', 2},
            {'с', 3},
            {'р', 3},
            {'в', 3},
            {'л', 3},
            {'к', 3},
            {'м', 3},
            {'д', 4},
            {'п', 4},
            {'у', 4},
            {'я', 4},
            {'ы', 4},
            {'ь', 4},
            {'г', 4},
            {'з', 4},
            {'б', 4},
            {'ч', 4},
            {'й', 4},
            {'ж', 5},
            {'ш', 5},
            {'ю', 5},
            {'ц', 5},
            {'щ', 5},
            {'э', 5},
            {'ф', 5},
            {'ъ', 5},
            {'ё', 5}
            };

        public int HardScoring(string word)
        {
            var lettersDict = new Dictionary<char, int>();

            int score = 0;

            foreach (var letter in word.ToLower())
            {
                score += scoreFromLetter[letter];
            }

            return score;
        }

        public int CountingByLength(string word)
        {
            var bases = 10;
            double score = 0;

            if (word.Length <= 3)
            {
                score = word.Length * bases;
            }
            else if (word.Length <= 5)
            {
                score = word.Length * bases * 1.25;
            }
            else if (word.Length <= 7)
            {
                score = word.Length * bases * 1.50;
            }
            else if (word.Length <= 9)
            {
                score = word.Length * bases * 1.75;
            }
            else
            {
                score = word.Length * bases * 2.00;
            }

            var result = (Int32) score;

            return result;
        }

    }
}
