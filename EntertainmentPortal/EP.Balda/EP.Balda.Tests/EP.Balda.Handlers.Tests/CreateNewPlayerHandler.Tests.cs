using AutoMapper;
using CSharpFunctionalExtensions;
using EP.Balda.Data.Context;
using EP.Balda.Logic.Commands;
using EP.Balda.Logic.Handlers;
using EP.Balda.Logic.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EP.Balda.Tests.EP.Balda.Handlers.Tests
{
    [TestFixture]
    public class CreateNewPlayerHandler_Tests
    {
        IMapper _mapper;
        Mock<IValidator<CreateNewPlayerCommand>> _validator;

        [SetUp]
        public void Setup()
        {
            _mapper = new Mock<IMapper>().Object;
            IEnumerable<ValidationFailure> failures = new List<ValidationFailure>();
            var validationResult = new ValidationResult(failures);
            _validator = new Mock<IValidator<CreateNewPlayerCommand>>();
            _validator.Setup(x => x.ValidateAsync(It.IsAny<FluentValidation.ValidationContext>(), It.IsAny<CancellationToken>())).ReturnsAsync(validationResult);

        }

        [Test]
        public async Task TestCreateNewPlayer_Handle_NormalData()
        {
            var options = new DbContextOptionsBuilder<BaldaGameDbContext>()
                .UseInMemoryDatabase(databaseName: "TestCreateNewPlayer_Handle_NotValidData")
                .Options;

            var request = new CreateNewPlayerCommand()
            {
                Login = "Login",
                NickName = "NickName",
                Password = "Password"
            };

            Result<Player> result;

            using (var context = new BaldaGameDbContext(options))
            {
                var service = new CreateNewPlayerHandler(context, _mapper, _validator.Object);
                result = await service.Handle(request, CancellationToken.None);
            }

            using (var context = new BaldaGameDbContext(options))
            {
                Assert.IsTrue(result.IsSuccess);
            }
        }
    }
}
