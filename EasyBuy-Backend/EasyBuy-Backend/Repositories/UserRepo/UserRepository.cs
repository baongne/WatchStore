using EasyBuy_Backend.Data;
using EasyBuy_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EasyBuy_Backend.Repositories.UserRepo
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly MyDbContext _context;

        public UserRepository(MyDbContext context) : base(context)
        {
            _context = context;
        }

		public User GetByEmail(string email)
		{
			return _context.Set<User>().FirstOrDefault(u => u.Email == email);
		}
    }
}
