using System;
using System.Collections.Generic;
using System.Text;
using EP._15Puzzle.Data;
using EP._15Puzzle.Data.Models;
using EP._15Puzzle.Logic.Models;

namespace EP._15Puzzle.Logic.Services
{
    public static class DeckService
    {
        /// <summary>
        /// starts new deck
        /// </summary>
        /// <param name="id">user Id</param>
        /// <returns>current state of deck</returns>
        public static Deck NewGame(int id)
        {
            var deck = DeckRepository.Create(id);
            return Parse(deck);
        }

        /// <summary>
        /// gets current state of deck
        /// </summary>
        /// <param name="id">user Id</param>
        /// <returns>current state of deck</returns>
        public static Deck GetDeck(int id)
        {
            var deck = DeckRepository.Get(id);
            return Parse(deck);
        }

        /// <summary>
        /// moves tile if it possible
        /// </summary>
        /// <param name="id">user Id</param>
        /// <param name="tileToMove">number of tile to move</param>
        /// <returns>current state of deck</returns>
        public static Deck Move(int id, int tileToMove)
        {
            var deck = DeckRepository.Get(id);
            var tiles = (List<int>)Parse(deck).Tiles;
            var tile = new Tile(tiles[tileToMove]);
            var tile0 = new Tile(tiles[0]);
            if (ComparePositions(tile,tile0))
            {
                var temp = deck.Tiles[0];
                deck.Tiles[0] = deck.Tiles[tileToMove];
                deck.Tiles[tileToMove] = temp;
                DeckRepository.Update(deck.Id, deck.Tiles, deck.Score, deck.Victory);
            }
            return Parse(deck);
        }

        /// <summary>
        /// converts DeckDB into Deck
        /// </summary>
        /// <param name="deck">DeckDB from Repository to parse into Deck</param>
        /// <returns>represented DeckDB into Deck</returns>
        private static Deck Parse(DeckDB deck)
        {
            return new Deck(){Score = deck.Score,Tiles = deck.Tiles,Victory = deck.Victory};
        }
        
        /// <summary>
        /// check if it possible to move selected tile to move into empty place (to swap with tile0)
        /// </summary>
        /// <param name="tile">position of selected tile</param>
        /// <param name="tile0">position of empty place</param>
        /// <returns>true - swap is possible, tiles are touching in one row or column</returns>
        private static bool ComparePositions(Tile tile, Tile tile0)
        {
            if (tile.PosX == tile0.PosX || tile.PosY == tile0.PosY)
            {
                if (Math.Abs(tile.PosX - tile0.PosX) == 1 || Math.Abs(tile.PosY - tile0.PosY) == 1)
                {
                    return true;
                }
            }
            return false;
        }
    }


    
    /*
    




    

        public static bool CheckWin()
        {
            if (_deck[_deck.Count - 1].Num == 0)
            {
                for (int i = 1; i < _deck.Count; i++)
                {
                    if (_deck[i].Num != i)
                    {
                        return false;
                    }
                }
                return true;
            }

            return false;
        }
     */
}
