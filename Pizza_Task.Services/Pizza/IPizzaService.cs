using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizza_Task.Data;

namespace Pizza_Task.Services
{
	public interface IPizzaService
	{
		Task<IEnumerable<Pizza>> GetPizzas();
		Task<bool> AddPizza(IList<Toppings> toppings, Sizes size);

		decimal CalculateCost(IList<Toppings> toppings, Sizes size);
	}
}
