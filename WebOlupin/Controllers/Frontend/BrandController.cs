using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Enumerations;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View;
using System.Linq;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Controllers.Frontend
{
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class BrandController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IEntityRepository<Brand, System.Guid> brandRepository;
        private readonly IEntityRepository<Product, System.Guid> productRepository;
        private readonly IEntityRepository<PageHeader, PageId> pageHeaderRepository;
        private readonly IEntityRepository<PageVisit, long> pageVisitRepository;

        public BrandController(IEntityRepository<PageVisit, long> pageVisitRepository, IEntityRepository<PageHeader, PageId> pageHeaderRepository, IEntityRepository<Product, System.Guid> productRepository, IEntityRepository<Brand, System.Guid> brandRepository)
        {
            this.brandRepository = brandRepository;
            this.productRepository = productRepository;
            this.pageHeaderRepository = pageHeaderRepository;
            this.pageVisitRepository = pageVisitRepository;
        }

        [Microsoft.AspNetCore.Mvc.Route("{key}")]
        public Microsoft.AspNetCore.Mvc.IActionResult Index(System.Guid key)
        {
            pageVisitRepository.Add(new PageVisit
            {
                PageId = PageId.Brand,
                Context = Request.Path,
                Carrier = Request.Headers.UserAgent,
                NetworkIdentifier = Request.Headers.Host
            }, "system");

            PageHeaderDisplayViewModel header = null;

            var headerEntity = pageHeaderRepository.FindOneBy(_entity => _entity.Id == PageId.Brand);

            if (headerEntity != null)
            {
                header = new PageHeaderDisplayViewModel();

                header.Import(headerEntity);
            }

            var entity = brandRepository.Get(key);

            var model = new Models.View.Frontend.BrandIndexViewModel
            {
                Header = header,
                Brand = new BrandDisplayViewModel(),
                Products = productRepository.FindBy(_entity => _entity.BrandId == entity.Id).ToArray().Select(__entity =>
                {
                    var __model = new ProductDisplayViewModel();

                    __model.Import(__entity);

                    return __model;
                }).ToArray()
            };

            model.Brand.Import(entity);

            return View(model);
        }
    }
}
