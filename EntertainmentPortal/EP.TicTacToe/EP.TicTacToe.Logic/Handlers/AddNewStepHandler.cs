using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using EP.TicTacToe.Data.Context;
using EP.TicTacToe.Logic.Commands;
using EP.TicTacToe.Logic.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace EP.TicTacToe.Logic.Handlers
{
    public class
        AddNewStepHandler : IRequestHandler<AddNewStepCommand, Result<StepResult>>
    {
        private readonly TicTacDbContext _context;
        private readonly IMapper _mapper;

        public AddNewStepHandler(TicTacDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<StepResult>> Handle(AddNewStepCommand request,
                                                     CancellationToken cancellationToken)
        {
            var ticTacSymbol = 0;
            var currentPlayerId = "";
            var gameId = request.GameId;

            var firstPlayer = await _context.FirstPlayers
                .Where(p => p.HaunterId == request.PlayerId.ToString())
                .Where(x => x.Player.GameId == gameId)
                .FirstOrDefaultAsync(cancellationToken);
            if (firstPlayer != null)
            {
                ticTacSymbol = firstPlayer.TicTac;
                currentPlayerId = firstPlayer.HaunterId;
            }

            var secondPlayer = await _context.SecondPlayers
                .Where(p => p.HaunterId == request.PlayerId.ToString())
                .Where(x => x.Player.GameId == gameId)
                .FirstOrDefaultAsync(cancellationToken);
            if (secondPlayer != null)
            {
                ticTacSymbol = secondPlayer.TicTac;
                currentPlayerId = secondPlayer.HaunterId;
            }

            if (firstPlayer == null && secondPlayer == null)
                return Result.Fail<StepResult>(
                    $"This game has no player ID: {request.PlayerId}");

            var mapDb = await _context.Maps
                .Where(c => c.GameId == gameId)
                .FirstOrDefaultAsync(cancellationToken);

            var cell = IndexToCell(mapDb.Size, request.Index);

            var cellDb = await _context.Cells
                .Where(c => c.MapId == mapDb.Id).Where(x => x.X == cell.X)
                .Where(y => y.Y == cell.Y)
                .FirstOrDefaultAsync(cancellationToken);

            if (cellDb == null)
                return Result.Fail<StepResult>($"This Cell with X={cell.X} Y={cell.Y} " +
                                               $"in the game with ID [{gameId}], " +
                                               $"on this map with ID [{mapDb.Id}] does not exist.");

            if (cellDb.TicTac != 0)
                return Result.Fail<StepResult>("The cell is already taken.");

            cellDb.TicTac = ticTacSymbol;
            _context.Cells.Update(cellDb);

            try
            {
                await _context.SaveChangesAsync(cancellationToken);

                var cells = await _context.Cells
                    .Where(c => c.MapId == mapDb.Id)
                    .ToListAsync(cancellationToken);

                var cellList = new List<int>();
                foreach (var item in cells) cellList.Add(item.TicTac);

                var stepResultDb = await _context.StepResults
                    .Where(c => c.GameId == gameId)
                    .FirstOrDefaultAsync(cancellationToken);

                var status = GetStatus(mapDb.WinningChain, cellList, request.Index);
                stepResultDb.Status = (Data.Models.State) status;

                //Determining the order of players for the next step
                var gamePlayers = GetPlayers(_context, gameId);
                var nextPlayer = currentPlayerId == gamePlayers.One
                    ? gamePlayers.Two
                    : currentPlayerId;

                stepResultDb.NextPlayerId = Convert.ToInt32(nextPlayer);
                _context.StepResults.Update(stepResultDb);

                await _context.SaveChangesAsync(cancellationToken);

                var stepResult = new StepResult
                {
                    GameId = stepResultDb.GameId,
                    NextPlayerId = stepResultDb.NextPlayerId,
                    Status = status
                };

                return Result.Ok(_mapper.Map<StepResult>(stepResult));
            }
            catch (DbUpdateException ex)
            {
                return Result.Fail<StepResult>(ex.Message);
            }
        }

        private static (string One, string Two) GetPlayers(
            TicTacDbContext context, int id)
        {
            var playerOne = context.Players
                .Where(c => c.GameId == id)
                .Select(s => s.FirstPlayer)
                .Select(x => x.HaunterId)
                .FirstOrDefaultAsync()
                .Result;

            var playerTwo = context.Players
                .Where(c => c.GameId == id)
                .Select(s => s.SecondPlayer)
                .Select(x => x.HaunterId)
                .FirstOrDefaultAsync()
                .Result;

            return (playerOne, playerTwo);
        }

        private static State GetStatus(int winChain, List<int> cellList, int index)
        {
            var resGame = IsBingo(winChain, cellList, index);

            var status = State.Loss;
            if (resGame) status = State.Winning;
            if (cellList.Contains(0)) status = State.Continuation;
            if (!resGame & !cellList.Contains(0)) status = State.Draw;

            return status;
        }

        private static (int X, int Y) IndexToCell(int det, int req)
        {
            return (req / det, req - req / det * det);
        }

        private enum Direct
        {
            East, Es, South, Ws, West, Nw, North, Ne
        }

        private static bool IsConcurrency(int ticTac, int compare)
        {
            if (compare == 0) return false;
            return compare == ticTac;
        }

        private static List<int> NewHorizons(int index, int mapSize)
        {
            var row = index / mapSize;
            var col = index - index / mapSize * mapSize;

            var east = index + 1;
            var es = index + mapSize + 1;
            var south = index + mapSize;
            var ws = index + mapSize - 1;
            var west = index - 1;
            var nw = index - mapSize - 1;
            var north = index - mapSize;
            var ne = index - mapSize + 1;

            var inFirstRow = 0;
            var inLastRow = mapSize - 1;

            var inFirstCol = 0;
            var inLastCol = mapSize - 1;

            var lastInLastRow = mapSize * mapSize - 1;
            var firstInLastRow = mapSize * mapSize - mapSize;

            if (row == inFirstRow)
            {
                if (index == inFirstCol)
                    return new List<int> {east, es, south, 0, 0, 0, 0, 0};
                if (index == inLastCol)
                    return new List<int> {0, 0, south, ws, west, 0, 0, 0};

                return new List<int> {east, es, south, ws, west, 0, 0, 0};
            }

            if (col == inFirstCol)
            {
                if (index == inFirstRow)
                    return new List<int> {east, es, south, 0, 0, 0, 0, 0};
                if (index == firstInLastRow)
                    return new List<int> {east, 0, 0, 0, 0, 0, north, ne};
                return new List<int> {east, es, south, 0, 0, 0, north, ne};
            }

            if (col == inLastCol)
            {
                if (index == inFirstRow)
                    return new List<int> {east, es, south, 0, 0, 0, 0, 0};
                if (index == lastInLastRow)
                    return new List<int> {0, 0, 0, 0, west, nw, north, 0};
                return new List<int> {0, 0, south, ws, west, nw, north, 0};
            }

            if (row != inLastRow)
                return new List<int> {east, es, south, ws, west, nw, north, ne};

            if (index == lastInLastRow)
                return new List<int> {0, 0, 0, 0, west, nw, north, 0};

            if (index == firstInLastRow)
                return new List<int> {east, 0, 0, 0, 0, 0, north, ne};

            return new List<int> {east, 0, 0, 0, west, nw, north, ne};
        }

        public static bool IsBingo(int currentWinChain,
                                   IReadOnlyList<int> cells, int index)
        {
            var mapSize = (int) Math.Sqrt(cells.Count);
            var directWestEast = 1;
            var directNorthSouth = 1;
            var directEsNw = 1;
            var directWsNe = 1;

            var neighborhood = NewHorizons(index, mapSize);

            foreach (var idx in neighborhood)
            {
                var compare = IsConcurrency(cells[index], cells[idx]);
                if (!compare) continue;

                if (neighborhood.IndexOf(idx) == (int) Direct.East)
                {
                    var id = index;
                    while (compare)
                    {
                        ++id;
                        if (id == cells.Count - 1) IsConcurrency(cells[index], cells[id]);
                        else
                            compare = id + 1 < cells.Count &&
                                      IsConcurrency(cells[index], cells[id]);
                        if (compare) directWestEast++;
                    }
                }
                else if (neighborhood.IndexOf(idx) == (int) Direct.West)
                {
                    var id = index;
                    while (compare)
                    {
                        --id;
                        if (id == 0) IsConcurrency(cells[index], cells[id]);
                        else
                            compare = id - 1 >= 0 &&
                                      IsConcurrency(cells[index], cells[id]);
                        if (compare) directWestEast++;
                    }
                }
                else if (neighborhood.IndexOf(idx) == (int) Direct.Es)
                {
                    var id = index;
                    while (compare)
                    {
                        id = id + mapSize + 1;
                        if (id == cells.Count - 1) IsConcurrency(cells[index], cells[id]);
                        else
                            compare = id + 1 < cells.Count &&
                                      IsConcurrency(cells[index], cells[id]);
                        if (compare) directEsNw++;
                    }
                }
                else if (neighborhood.IndexOf(idx) == (int) Direct.Nw)
                {
                    var id = index;
                    while (compare)
                    {
                        id = id - mapSize - 1;
                        if (id == 0) IsConcurrency(cells[index], cells[id]);
                        else
                            compare = id - 1 >= 0 &&
                                      IsConcurrency(cells[index], cells[id]);
                        if (compare) directEsNw++;
                    }
                }
                else if (neighborhood.IndexOf(idx) == (int) Direct.South)
                {
                    var id = index;
                    while (compare)
                    {
                        id += mapSize;
                        if (id == cells.Count - 1) IsConcurrency(cells[index], cells[id]);
                        else
                            compare = id < cells.Count &&
                                      IsConcurrency(cells[index], cells[id]);
                        if (compare) directNorthSouth++;
                    }
                }
                else if (neighborhood.IndexOf(idx) == (int) Direct.North)
                {
                    var id = index;
                    while (compare)
                    {
                        id -= mapSize;
                        if (id == 0) IsConcurrency(cells[index], cells[id]);
                        else
                            compare = id - 1 >= 0 &&
                                      IsConcurrency(cells[index], cells[id]);
                        if (compare) directNorthSouth++;
                    }
                }
                else if (neighborhood.IndexOf(idx) == (int) Direct.Ws)
                {
                    var id = index;
                    while (compare)
                    {
                        id = id + mapSize - 1;
                        compare = id + 1 < cells.Count &&
                                  IsConcurrency(cells[index], cells[id]);
                        if (compare) directWsNe++;
                    }
                }
                else if (neighborhood.IndexOf(idx) == (int) Direct.Ne)
                {
                    var id = index;
                    while (compare)
                    {
                        id = id - mapSize + 1;
                        compare = id - 1 >= 0 &&
                                  IsConcurrency(cells[index], cells[id]);
                        if (compare) directWsNe++;
                    }
                }
            }

            if (directWestEast >= currentWinChain) return true;
            if (directNorthSouth >= currentWinChain) return true;
            if (directWsNe >= currentWinChain) return true;
            if (directEsNw >= currentWinChain) return true;

            return false;
        }
    }
}