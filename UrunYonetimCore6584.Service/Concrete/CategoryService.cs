using UrunYonetimCore6584.Data;
using UrunYonetimCore6584.Data.Concrete;
using UrunYonetimCore6584.Service.Abstract;

namespace UrunYonetimCore6584.Service.Concrete
{
    public class CategoryService : CategoryRepository, ICategoryService
    {
        public CategoryService(DatabaseContext context) : base(context)
        {
        }
    }
}
