using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;
using TestTask.Enums;

namespace TestTask.Services.BuisnessLogic
{
	public class UserService : IUserService
	{
		private readonly ApplicationDbContext db;

		public UserService(ApplicationDbContext context)
		{
			db = context;
		}

		public async Task<User> GetUser()
		{
			try
			{
				var userWithTheMostOrders = await db.Users
											.OrderByDescending(m => m.Orders.Count)
											.FirstOrDefaultAsync(); 

				return userWithTheMostOrders;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}	
		}

		public async Task<List<User>> GetUsers()
		{
			try
			{
				var inactiveUsers = await db.Users
									.Where(m => m.Status == UserStatus.Inactive)
									.ToListAsync();
				return inactiveUsers;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
		}
	}
}