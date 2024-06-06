using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Enumerations;
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
        private readonly IEntityRepository<PageHeader, PageId> pageHeaderRepository;
        private readonly IEntityRepository<PageVisit, long> pageVisitRepository;

        public CategoryController(IEntityRepository<PageVisit, long> pageVisitRepository, IEntityRepository<PageHeader, PageId> pageHeaderRepository, IEntityRepository<Product, System.Guid> productRepository, IEntityRepository<Brand, System.Guid> brandRepository, IEntityRepository<Category, int> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
            this.brandRepository = brandRepository;
            this.productRepository = productRepository;
            this.pageHeaderRepository = pageHeaderRepository;
            this.pageVisitRepository = pageVisitRepository;
        }

        [Microsoft.AspNetCore.Mvc.Route("{key}")]
        public Microsoft.AspNetCore.Mvc.IActionResult Index(int key)
        {
            pageVisitRepository.Add(new PageVisit
            {
                PageId = PageId.Category,
                Context = Request.Path,
                Carrier = Request.Headers.UserAgent,
                NetworkIdentifier = Request.Headers.Host
            }, "system");

            try
            {
                PageHeaderDisplayViewModel header = null;

                var headerEntity = pageHeaderRepository.FindOneBy(_entity => _entity.Id == PageId.Category);

                if (headerEntity != null)
                {
                    header = new PageHeaderDisplayViewModel();

                    header.Import(headerEntity);
                }

                var entity = categoryRepository.Get(key);

                var model = new CategoryIndexViewModel
                {
                    Header = header,
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
