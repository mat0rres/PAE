using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View;
using System.Linq;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Controllers.Frontend
{
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class BrandController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IEntityRepository<Brand, System.Guid> brandRepository;
        private readonly IEntityRepository<Product, System.Guid> productRepository;

        public BrandController(IEntityRepository<Product, System.Guid> productRepository, IEntityRepository<Brand, System.Guid> brandRepository)
        {
            this.brandRepository = brandRepository;
            this.productRepository = productRepository;
        }

        [Microsoft.AspNetCore.Mvc.Route("{key}")]
        public Microsoft.AspNetCore.Mvc.IActionResult Index(System.Guid key)
        {
            var entity = brandRepository.Get(key);

            var model = new Models.View.Frontend.BrandIndexViewModel
            {
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
