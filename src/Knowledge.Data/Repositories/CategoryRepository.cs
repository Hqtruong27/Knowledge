using Knowledge.Data.EF;
using Knowledge.Data.Models;
using Knowledge.Data.Repositories.Generic;

namespace Knowledge.Data.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {

    }
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
