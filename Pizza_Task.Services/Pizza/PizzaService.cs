using Pizza_Task.Data;
using Pizza_Task.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Task.Services
{
	public class PizzaService : IPizzaService
	{
		private readonly IPizzaRepository _pizzaRepository;

		public PizzaService(IPizzaRepository pizzaRepository)
		{
			_pizzaRepository= pizzaRepository;
		}

		public async Task<bool> AddPizza(IList<Toppings> toppings, Sizes size)
		{
			return await _pizzaRepository.AddPizza(toppings, size);
		}

		public decimal CalculateCost(IList<Toppings> toppings, Sizes size)
		{
			return _pizzaRepository.CalculateCost(toppings, size);
		}

		public async Task<IEnumerable<Pizza>> GetPizzas()
		{
			return await _pizzaRepository.GetPizzas();
		}
	}
}
