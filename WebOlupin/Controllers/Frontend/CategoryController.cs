using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Frontend;
using System.Linq;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Controllers.Frontend
{
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class CategoryController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IEntityRepository<Category, int> categoryRepository;
        private readonly IEntityRepository<Brand, System.Guid> brandRepository;
        private readonly IEntityRepository<Product, System.Guid> productRepository;

        public CategoryController(IEntityRepository<Product, System.Guid> productRepository, IEntityRepository<Brand, System.Guid> brandRepository, IEntityRepository<Category, int> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
            this.brandRepository = brandRepository;
            this.productRepository = productRepository;
        }

        [Microsoft.AspNetCore.Mvc.Route("{key}")]
        public Microsoft.AspNetCore.Mvc.IActionResult Index(int key)
        {
            try
            {
                var entity = categoryRepository.Get(key);

                var model = new CategoryIndexViewModel
                {
                    Category = new CategoryDisplayViewModel()
                };

                var favoriteBrands = brandRepository.FindBy(_entity => _entity.IsFavorite).ToArray();

                foreach (var item in favoriteBrands)
                {
                    var favoriteBrandProducts = productRepository.FindBy(_entity => _entity.BrandId == item.Id && _entity.CategoryId == entity.Id).ToArray();

                    if (favoriteBrandProducts.Length <= 0) continue;

                    var _model = new BrandDisplayViewModel();

                    _model.Import(item);

                    model.Favorites.Add(new System.Collections.Generic.KeyValuePair<BrandDisplayViewModel, ProductDisplayViewModel[]>(_model, favoriteBrandProducts.Select(__entity =>
                    {
                        var __model = new ProductDisplayViewModel();

                        __model.Import(__entity);

                        return __model;
                    }).ToArray()));
                }

                var otherBrands = brandRepository.FindBy(_entity => !_entity.IsFavorite).ToArray();

                foreach (var item in otherBrands)
                {
                    var otherBrandProducts = productRepository.FindBy(_entity => _entity.BrandId == item.Id && _entity.CategoryId == entity.Id).ToArray();

                    if (otherBrandProducts.Length <= 0) continue;

                    var _model = new BrandDisplayViewModel();

                    _model.Import(item);

                    model.Regulars.Add(new System.Collections.Generic.KeyValuePair<BrandDisplayViewModel, ProductDisplayViewModel[]>(_model, otherBrandProducts.Select(__entity =>
                    {
                        var __model = new ProductDisplayViewModel();

                        __model.Import(__entity);

                        return __model;
                    }).ToArray()));
                }

                model.Category.Import(entity);

                return View(model);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
