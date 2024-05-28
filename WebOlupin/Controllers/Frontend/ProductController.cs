using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View;
using System.Linq;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Controllers.Frontend
{
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class ProductController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IEntityRepository<Product, System.Guid> productRepository;

        public ProductController(IEntityRepository<Product, System.Guid> productRepository)
        {
            this.productRepository = productRepository;
        }

        [Microsoft.AspNetCore.Mvc.Route("{key}")]
        public Microsoft.AspNetCore.Mvc.IActionResult Index(System.Guid key)
        {
            var entity = productRepository.Get(key);

            var model = new Models.View.Frontend.ProductDetailViewModel
            {
                Referer = Request.Headers.Referer,
                Product = new ProductDisplayViewModel(),
                Attributes = entity.CustomAttributeBridges.Select(bridge =>  new ProductCustomAttributeViewModel
                {
                    CustomAttributeId = bridge.SternId,
                    CustomAttributeName = bridge.Stern.Name,
                    Value = bridge.Value
                }).ToArray(),
                Features = entity.Features.Select(_entity =>
                {
                    var _model = new ProductFeatureDisplayViewModel();

                    _model.Import(_entity);

                    return _model;
                }).ToArray(),
                ExtraImages = entity.ExtraImageBridges.Select(bridge =>
                {
                    var _model = new ImageDisplayViewModel();

                    _model.Import(bridge.Stern);

                    return _model;
                }).ToArray()
            };

            model.Product.Import(entity);

            return View(model);
        }
    }
}
