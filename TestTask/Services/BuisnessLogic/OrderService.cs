using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.BuisnessLogic
{
	public class OrderService : IOrderService
	{
		private readonly ApplicationDbContext db;

		public OrderService(ApplicationDbContext context)
		{
			db = context;
		}

		public async Task<Order> GetOrder()
		{
			try
			{
				var orderWithMaxTotalPrice = await db.Orders
											.OrderByDescending(m => m.Price * m.Quantity)
											.FirstOrDefaultAsync();
				return orderWithMaxTotalPrice;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
		}

		public async Task<List<Order>> GetOrders()
		{
			try
			{
				var orderWithProductQuantityAbove10 = await db.Orders
													.Where(m => m.Quantity > 10)
													.ToListAsync();
				return orderWithProductQuantityAbove10;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
		}
	}
}