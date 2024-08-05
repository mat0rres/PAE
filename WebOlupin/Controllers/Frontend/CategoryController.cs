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

                var products = productRepository.FindBy(_entity => _entity.Enabled && _entity.CategoryId == entity.Id).OrderBy(_entity => _entity.Priority).ToArray().GroupBy(_entity => _entity.Brand);

                var model = new CategoryIndexViewModel
                {
                    Header = header,
                    Category = new CategoryDisplayViewModel()
                };

                var favoritesByBrand = products.Where(_grouping => _grouping.Key.IsFavorite).ToArray();

                foreach (var grouping in favoritesByBrand)
                {
                    var _model = new BrandDisplayViewModel();

                    _model.Import(grouping.Key);

                    model.Favorites.Add(new System.Collections.Generic.KeyValuePair<BrandDisplayViewModel, ProductDisplayViewModel[]>(_model, grouping.Select(__entity =>
                    {
                        var __model = new ProductDisplayViewModel();

                        __model.Import(__entity);

                        return __model;
                    }).ToArray()));
                }

                var othersByBrands = products.Where(_grouping => !_grouping.Key.IsFavorite).ToArray();

                foreach (var grouping in othersByBrands)
                {
                    var _model = new BrandDisplayViewModel();

                    _model.Import(grouping.Key);

                    model.Regulars.Add(new System.Collections.Generic.KeyValuePair<BrandDisplayViewModel, ProductDisplayViewModel[]>(_model, grouping.Select(__entity =>
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
