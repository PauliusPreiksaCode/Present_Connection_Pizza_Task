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

        [HttpPost("api/pizza-task/calculate-cost")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<decimal> CalculateCost([FromBody] Pizza pizzaOrder)
        {
			Enum.TryParse(pizzaOrder.BasePizza.Sizes, out Sizes sizeEnum);
            List<Toppings> toppingEnums = new List<Toppings>();

            foreach (string topping in pizzaOrder.Toppings)
            {
				Enum.TryParse(topping, out Toppings toppingEnum);
				toppingEnums.Add(toppingEnum);
            }


            var result = _service.CalculateCost(toppingEnums, sizeEnum);

			if (result == 0) return BadRequest(result);
			return Ok(result);
        }


        [HttpPost("api/pizza-task/add-pizza")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> AddPizza([FromBody] Pizza pizzaOrder)
		{

            Enum.TryParse(pizzaOrder.BasePizza.Sizes, out Sizes sizeEnum);
            List<Toppings> toppingEnums = new List<Toppings>();

            foreach (string topping in pizzaOrder.Toppings)
            {
                Enum.TryParse(topping, out Toppings toppingEnum);
                toppingEnums.Add(toppingEnum);
            }

            try
			{
				bool successful = await _service.AddPizza(toppingEnums, sizeEnum);
				return successful ? Ok(true) : BadRequest(false);
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
