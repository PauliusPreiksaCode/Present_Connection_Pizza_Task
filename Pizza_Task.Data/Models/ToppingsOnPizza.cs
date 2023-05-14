using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Task.Data
{
	public class ToppingsOnPizza
	{
		[Key]
		public Guid Id { get; set; }
		public string Name { get; set; }
		public int Fk_PizzaId { get; set; }
	}
}
