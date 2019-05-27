// Filename: Game.cs
using System.Collections.Generic;

namespace EP.Balda.Logic.Models
{
    /// <summary>
    /// The model <c>Game</c> class.
    /// Represents an instance of the game.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Players property.
        /// </summary>
        /// <value>
        /// A value represents players (competitors) in the game.
        /// </value>
        /// <seealso cref="Player">
        public Player[] Players { get; set; }

        /// <summary>
        /// Playground property.
        /// </summary>
        /// <value>
        /// A value represents a playground with cells in the game.
        /// </value>
        /// <seealso cref="Models.Playground">
        public Playground Playground { get; set; }

        /// <summary>
        /// InitialWord property.
        /// </summary>
        /// <value>
        /// A value representsa a word to start the game with.
        /// </value>
        /// <remarks>
        /// The word will be located in the center of the playground.
        /// </remarks>
        public string InitialWord { get; set; }

        /// <summary>
        /// Game constructor with two players and initial word as parameters.
        /// </summary>
        /// <param name="player1">
        /// Parameter player1 requires a <c>Player</c> argument.
        /// </param>
        /// <param name="player2">
        /// Parameter player2 requires a <c>Player</c> argument.
        /// </param>
        /// <param name="initialWord">
        /// Parameter initialWord requires a <c>IReadOnlyList<char></c> argument.
        /// </param>
        public Game(Player player1, Player player2, IReadOnlyList<char> initialWord)
        {
            Players = new Player[2];
            Players[0] = player1;
            Players[1] = player2;

            Playground = new Playground();
            var center = Playground.Size / 2;

            for (var i = 0; i < Playground.Size; i++) //to add word to start
                Playground.Cells[center, i].Letter = initialWord[i];
        }
    }
}