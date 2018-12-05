using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IRepository<TEntity, TPK> where TEntity : class
    {
        IEnumerable<TEntity> Get();
        TEntity Get(TPK id);
        TEntity Create(TEntity entity);
        bool Update(TPK id, TEntity entity);
        bool Update(string email, TEntity entity);
        bool Delete(TPK id);
        void Dispose();
    }
}
