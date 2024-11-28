using EasyBuy_Backend.Data;
using EasyBuy_Backend.Models;
using EasyBuy_Backend.Repositories.CategoryRepo;

namespace EasyBuy_Backend.Repositories.PaymentRepo
{
	public class PaymentRepository : Repository<Payment>, IPaymentRepository
	{
		public PaymentRepository(MyDbContext context) : base(context)
		{
			//
		}
	}
}
