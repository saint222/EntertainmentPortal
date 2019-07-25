using System;
using System.Collections.Generic;
using System.Linq;
using EP._15Puzzle.Data.Models;

namespace EP._15Puzzle.Logic.Models
{
    public class LogicDeck
    {

        public int Id { get; set; }
        public int Size { get; set; }
        public UserDb User { get; set; }
        public string UserId { get; set; }

        public int Score { get; set; } = 0;
        public bool Victory { get; set; } = false;

        public ICollection<LogicTile> Tiles { get; set; }



        public LogicDeck(int size)
        {
            User = new UserDb() { };
            Size = size;
            var tiles = new List<LogicTile>();
            for (int i = 0; i <= size * size - 1; i++)
            {
                tiles.Add(new LogicTile(i, size));
            }

            Tiles = SetNearbyTiles(tiles);
            
            Score = 0;
            Victory = false;

        }

        public LogicDeck()
        {

        }

        public void Unsort()
        {
            var tiles = Tiles.ToArray();
            Random random = new Random();
            for (int i = 15; i >= 0; i--)
            {
                int j = random.Next(i) + 1;

                var temp = tiles[i].Num;
                tiles[i].Num = tiles[j].Num;
                tiles[j].Num = temp;
            }

            var tile0 = tiles.First(t => t.Num == 0);
            var tile = tiles.First(t => t.Pos == 16);
            var tem = tile0.Num;
            tile0.Num = tile.Num;
            tile.Num = tem;

            Tiles = tiles;
        }

        public bool CheckWinIsPossible()
        {
            int[] tilesOnDeck = new int[16];

            foreach (var tile in Tiles)
            {
                tilesOnDeck[tile.Pos - 1] = tile.Num;
            }

            int rowOfEmpty = 0;
            for (int i = 0; i < 16; i++)
            {
                if (tilesOnDeck[i] == 0)
                {
                    rowOfEmpty = i / 4 + 1;
                    break;
                }
            }
            int chetnost = 1;
            for (int i = 0; i < 16; i++)
            {
                if (tilesOnDeck[i] != 0)
                {
                    int c = 0;
                    for (int j = i + 1; j < 16; j++)
                    {
                        if (tilesOnDeck[i] > tilesOnDeck[j])
                        {
                            c += 1;
                        }
                    }

                    chetnost += c;
                }

            }

            chetnost += rowOfEmpty;
            return chetnost % 2 == 0;
        }

        public bool TileCanMove(int tileNum)
        {
            var tile = Tiles.FirstOrDefault(t => t.Num == tileNum);

            if (tile != null && tile.NearbyTiles.Any(t=>t.Num==0))
            {
                return true;
            }

            return false;
        }

        public bool CheckWin()
        {
            foreach (var tile in Tiles)
            {
                if (tile.Num != tile.Pos)
                {
                    if (tile.Num==0 && tile.Pos==Size*Size)
                    {
                        continue;
                    }
                    return false;
                }
            }

            return true;
        }

        public void Move(int tileNum)
        {
            var tile = Tiles.First(t => t.Num == tileNum);
            var tile0 = tile.NearbyTiles.First(t => t.Num == 0);
            

            tile.Num = 0;
            tile0.Num = tileNum;
        }

        public List<LogicTile> SetNearbyTiles(List<LogicTile> tiles)
        {
            foreach (var tile in tiles)
            {
                var leftTile = tiles.FirstOrDefault(t => t.Pos == tile.Pos - 1);
                if (leftTile != null && leftTile.Pos % Size != 0) tile.NearbyTiles.Add(leftTile);

                var rightTile = tiles.FirstOrDefault(t => t.Pos == tile.Pos + 1);
                if (rightTile != null && (rightTile.Pos - 1) % Size != 0) tile.NearbyTiles.Add(rightTile);

                var topTile = tiles.FirstOrDefault(t => t.Pos == tile.Pos - Size);
                if (topTile != null && topTile.Pos > 0) tile.NearbyTiles.Add(topTile);

                var bottomTile = tiles.FirstOrDefault(t => t.Pos == tile.Pos + Size);
                if (bottomTile != null && bottomTile.Pos <= Size * Size) tile.NearbyTiles.Add(bottomTile);
            }
            return tiles;
        }

        public void SetNearbyTiles()
        {
            Tiles = SetNearbyTiles(Tiles.ToList());
        }
    }
}
