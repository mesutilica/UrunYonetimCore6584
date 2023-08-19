using System.Linq.Expressions;
using UrunYonetimCore6584.Core.Entities;

namespace UrunYonetimCore6584.Data.Abstract
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryByIncludeAsync(Expression<Func<Category, bool>> expression);
    }
}
