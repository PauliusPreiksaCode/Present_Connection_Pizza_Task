using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Task.Data
{
	public class BasePizza
	{
		[Key]
		public int Id { get; set; }

		public string Sizes { get; set; }

		public decimal Price { get; set; }

	}
}
