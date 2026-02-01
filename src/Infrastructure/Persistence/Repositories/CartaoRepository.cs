using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class CartaoRepository : EfRepository<Cartao>, ICartaoRepository
    {
        public CartaoRepository(AppDbContext context) : base(context)
        {
        }
    }
}
