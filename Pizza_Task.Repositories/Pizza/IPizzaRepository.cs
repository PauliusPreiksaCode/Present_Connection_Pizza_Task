using Pizza_Task.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Task.Repositories
{
	public interface IPizzaRepository
	{
		Task<IEnumerable<Pizza>> GetPizzas();
		Task<bool> AddPizza(IList<Toppings> toppings, Sizes size);

		decimal CalculateCost(IList<Toppings> toppings, Sizes size);
	}
}
