using Microsoft.EntityFrameworkCore;
using Pizza_Task.Data;
using Pizza_Task.Data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Task.Repositories
{
	public class PizzaRepository : IPizzaRepository
	{
		private readonly DBContext _dbContext;
		public PizzaRepository(DBContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<bool> AddPizza(IList<Toppings> toppings, Sizes size)
		{
			var pizzaId = await _dbContext.Pizzas.CountAsync() + 1;

			var pizza = new BasePizza
			{
				Id = pizzaId,
				Sizes = size.ToString(),
				Price = CalculateCost(toppings, size),
			};

			foreach (var t in toppings)
			{

				var topping = new ToppingsOnPizza
				{
					Id = Guid.NewGuid(),
					Name = t.ToString(),
					Fk_PizzaId = pizza.Id,
				};
				await _dbContext.ToppingsOnPizzas.AddAsync(topping);
			}

			await _dbContext.Pizzas.AddAsync(pizza);
			return await _dbContext.SaveChangesAsync() > 0;
		}

		public decimal CalculateCost(IList<Toppings> toppings, Sizes size)
		{
			decimal price;

			switch (size)
			{
				case Sizes.Small:
					price = 8;
					break;
				case Sizes.Medium:
					price = 10;
					break;
				case Sizes.Large:
					price = 12;
					break;
				default:
					return 0;
			}

			price += toppings.Count;

			if (toppings.Count > 3)
				price = price * 0.9m;

			return price;
		}

		public async Task<IEnumerable<Pizza>> GetPizzas()
		{
			var pizzas = new List<Pizza>();

			var basePizzas = await _dbContext.Pizzas.ToListAsync();

			foreach (var bPizza in basePizzas)
			{
				Pizza pizza = new Pizza();
				pizza.BasePizza = bPizza;

				var toppings = await _dbContext.ToppingsOnPizzas.Where(t => t.Fk_PizzaId == bPizza.Id).ToListAsync();
				foreach (var topping in toppings)
				{
					pizza.Toppings.Add(topping.Name);
				}
				pizzas.Add(pizza);
			}

			return pizzas;
		}

	}
}
