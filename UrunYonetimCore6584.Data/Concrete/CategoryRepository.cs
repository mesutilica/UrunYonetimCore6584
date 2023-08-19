using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UrunYonetimCore6584.Core.Entities;
using UrunYonetimCore6584.Data.Abstract;

namespace UrunYonetimCore6584.Data.Concrete
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<Category> GetCategoryByIncludeAsync(Expression<Func<Category, bool>> expression)
        {
            return await _context.Categories.Where(expression).Include(p => p.Products).FirstOrDefaultAsync();
        }
    }
}
