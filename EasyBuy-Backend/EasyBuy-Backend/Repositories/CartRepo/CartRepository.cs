using EasyBuy_Backend.Data;
using EasyBuy_Backend.Models;
using EasyBuy_Backend.Repositories.CartRepo;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EasyBuy_Backend.Repositories.CartRepo
{
	public class CartRepository : Repository<Cart>, ICartRepository
	{
		public CartRepository(MyDbContext context) : base(context)
		{
		}

		public async Task<bool> DeleteCartByUserIdAsync(int userId)
		{

			var carts = await _context.Carts.Where(c => c.UserId == userId).ToListAsync();

			if (carts == null || carts.Count == 0)
			{
				return false; 
			}

			_context.Carts.RemoveRange(carts);

			await _context.SaveChangesAsync();

			return true; 
		}
	}
}
