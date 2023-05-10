using Eczane.Core.Entities;
using Eczane.Core.Repositories;

namespace Eczane.Data.Repositories
{
    public  class Repository<T>:IRepository<T> where T:Entity
    {
        private readonly DataContext _context;
        public Repository(DataContext context)
        {

        }
    }
}
