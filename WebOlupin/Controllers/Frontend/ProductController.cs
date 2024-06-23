using Microsoft.AspNetCore.Mvc;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Models.View;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Enumerations;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View;
using System.Linq;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Controllers.Frontend
{
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IEntityRepository<Product, System.Guid> productRepository;
        private readonly IEntityRepository<PageHeader, PageId> pageHeaderRepository;
        private readonly IEntityRepository<PageVisit, long> pageVisitRepository;
        private readonly IEntityRepository<ProductForContactRecord, long> productForContactRecordRepository;

        public ProductController(IEntityRepository<ProductForContactRecord, long> productForContactRecordRepository, IEntityRepository<PageVisit, long> pageVisitRepository, IEntityRepository<PageHeader, PageId> pageHeaderRepository, IEntityRepository<Product, System.Guid> productRepository)
        {
            this.productRepository = productRepository;
            this.pageHeaderRepository = pageHeaderRepository;
            this.pageVisitRepository = pageVisitRepository;
            this.productForContactRecordRepository = productForContactRecordRepository;
        }

        [Route("{key}")]
        public IActionResult Index(System.Guid key)
        {
            pageVisitRepository.Add(new PageVisit
            {
                PageId = PageId.Product,
                Context = Request.Path,
                Carrier = Request.Headers.UserAgent,
                NetworkIdentifier = Request.Headers.Host
            }, "system");

            PageHeaderDisplayViewModel header = null;

            var headerEntity = pageHeaderRepository.FindOneBy(_entity => _entity.Id == PageId.Product);

            if (headerEntity != null)
            {
                header = new PageHeaderDisplayViewModel();

                header.Import(headerEntity);
            }

            var entity = productRepository.Get(key);

            var model = new Models.View.Frontend.ProductDetailViewModel
            {
                Header = header,
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
                }).ToArray(),
                Colors = entity.Colors.Select(_entity => new ProductColorDisplayViewModel
                {
                    Id = _entity.Id,
                    Label = _entity.Label,
                    Color = _entity.Color
                }).ToArray()
            };

            model.Product.Import(entity);

            return View(model);
        }

        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        public virtual IActionResult Contact(ProductForContactRecordInputViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = model.Export();

                productForContactRecordRepository.Add(entity, User.Identity.Name ?? "guest");

                SetDefaultNotification(NotificationType.Success, "Intención de compra registrada con éxito", "Nuestro equipo de ventas le contactará lo antes posible para asistirlo en el propósito de adquirir este producto.");

                return RedirectToAction("Index", new { key = model.ProductId });
            }

            SetDefaultNotification(NotificationType.Danger, "Los campos especificados no son correctos.", "Los datos suministrados debe estar en el formáto correcto (Correo Electrónico, Número de Teléfono y Número de Identidad) y solo es opcional las Especificaciones. Por favor, inténtelo nuevamente, asegurándose de cumplir estos sencillos requisitos.");

            return RedirectToAction("Index", new { key = model.ProductId });
        }

        protected void SetDefaultNotification(NotificationType notificationType, string title, string message)
        {
            var notification = new NotificationViewModel
            {
                Type = notificationType,
                Title = title,
                Message = message
            };

            TempData[NotificationViewModel.DEFAULT_NOTIFICATION_MAP_KEY] = Newtonsoft.Json.JsonConvert.SerializeObject(notification);
        }
    }
}
