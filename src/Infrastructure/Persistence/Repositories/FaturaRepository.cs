using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class FaturaRepository : EfRepository<Fatura>, IFaturaRepository
    {
        public FaturaRepository(AppDbContext context) : base(context)
        {
        }
    }
}
