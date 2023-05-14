using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pizza_Task.Data;
using Pizza_Task.Services;

namespace Pizza_Task.Controllers
{
	public class Pizza_TaskController : Controller
	{
		private readonly IPizzaService _service;

		public Pizza_TaskController(IPizzaService service)
		{
			_service = service;
		}

		[HttpGet("api/pizza-task/get-pizzas")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<ActionResult<IEnumerable<Pizza>>> GetPizzasDB()
		{
			var result = await _service.GetPizzas();

			if (!result.Any()) return NotFound();
			return Ok(result);
		}

		[HttpGet("api/pizza-task/calculate-cost")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<decimal> CalculateCost(IList<Toppings> toppings, Sizes size)
		{
			var result = _service.CalculateCost(toppings, size);

			if (result == 0) return BadRequest();
			return Ok(result);
		}


		[HttpPost("api/pizza-task/add-pizza")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> AddPizza(IList<Toppings> toppings, Sizes size)
		{
			try
			{
				bool successful = await _service.AddPizza(toppings, size);
				return successful ? Ok() : BadRequest();
			}
			catch (ArgumentException ex)
			{ 
				return BadRequest(ex.Message); 
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
	}
}
