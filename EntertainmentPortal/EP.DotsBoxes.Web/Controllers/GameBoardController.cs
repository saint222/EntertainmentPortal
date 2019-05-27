using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EP.DotsBoxes.Logic.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EP.DotsBoxes.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameBoardController : ControllerBase
    {
        private readonly IMediator _mediator;

         public GameBoardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/gameboard
        [HttpGet]
        public async Task<IActionResult> GetGameBoardAsync()
        {
            var result = await _mediator.Send<int[,]>(new GetGameBoard());
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }

        // POST api/gameboard
        [HttpPost]
        public async Task<IActionResult> SetSizeAsync()
        {
            string rowString = Request.Form.FirstOrDefault(p => p.Key == "rows").Value;
            int rows = Int32.Parse(rowString);

            string columnString = Request.Form.FirstOrDefault(p => p.Key == "columns").Value;
            int columns = Int32.Parse(columnString);

            var result = await _mediator.Send<int[,]>(new SetSize(rows, columns));
            return result != null ? (IActionResult)Ok(result) : NotFound();
        }     
    }
}