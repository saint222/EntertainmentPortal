using MediatR;
using Moq;
using NUnit.Framework;

namespace EP.Sudoku.Tests
{
    [TestFixture]
    public class PlayersControllerTests
    {        
        IMediator _mediator;
       
        [SetUp] 
        public void MockInitialize()
        {            
            var mock = new Mock<IMediator>();
            mock.Setup(o => o);           
            _mediator = mock.Object;
        }

        
    }
}
