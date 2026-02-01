using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    public class UsuarioRepository : EfRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AppDbContext context) : base(context)
        {
        }
    }
}
