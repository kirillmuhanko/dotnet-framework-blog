using System.Threading.Tasks;

namespace BlogApplication.Domain.Interfaces.Data.Entity
{
    public interface IDatabaseContext
    {
        Task<int> SaveChangesAsync();

        void BeginTransaction();

        void Commit();

        void Rollback();
    }
}