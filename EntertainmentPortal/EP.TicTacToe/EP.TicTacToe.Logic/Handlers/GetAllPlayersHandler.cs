namespace EP.TicTacToe.Logic.Handlers
{
    //public class GetAllPlayersHandler: IRequestHandler<GetAllPlayers, Maybe<IEnumerable<PlayerBL>>>
    //{
    //    private readonly IMapper _mapper;
    //    private readonly PlayerDbContext _context;

    //    public GetAllPlayersHandler(IMapper mapper, PlayerDbContext context)
    //    {
    //        _mapper = mapper;
    //        _context = context;
    //    }

    //    public async Task<Maybe<IEnumerable<PlayerBL>>> Handle(GetAllPlayers request, CancellationToken cancellationToken)
    //    {
    //        var result = await _context.Players
    //            .AsNoTracking()
    //            .ToArrayAsync(cancellationToken)
    //            .ConfigureAwait(false);

    //        return result.Any() ?
    //            Maybe<IEnumerable<PlayerBL>>.None :
    //            Maybe<IEnumerable<PlayerBL>>.From(result);
    //    }
    //}
}