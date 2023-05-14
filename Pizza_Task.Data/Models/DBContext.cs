using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_Task.Data.Models
{
	public partial class DBContext : DbContext
	{
		public DBContext(DbContextOptions options): base(options) { }

		public DbSet<BasePizza> Pizzas { get; set; }

		public DbSet<ToppingsOnPizza> ToppingsOnPizzas { get; set; }

	}
}
