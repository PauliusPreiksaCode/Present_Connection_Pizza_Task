using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Task.Data
{
	public class Pizza
	{
		public BasePizza BasePizza { get; set; }
		public List<string> Toppings { get; set; } = new List<string>();

	}
}
