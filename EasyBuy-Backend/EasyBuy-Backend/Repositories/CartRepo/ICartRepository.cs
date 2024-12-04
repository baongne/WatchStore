using EasyBuy_Backend.Models;
﻿using EasyBuy_Backend.Repositories.CartRepo;
using EasyBuy_Backend.Models;

namespace EasyBuy_Backend.Repositories.CartRepo
{
	public interface ICartRepository : IRepository<Cart>
	{
		Task<bool> DeleteCartByUserIdAsync(int userId);

	}
}
