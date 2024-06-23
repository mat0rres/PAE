using Microsoft.AspNetCore.Mvc;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Models.View;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Enumerations;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Controllers.Frontend
{
    public class ContactController : Controller
    {
        private readonly IEntityRepository<RichTextBlock, RichTextBlockId> richTextBlockRepository;
        private readonly IEntityRepository<PageHeader, PageId> pageHeaderRepository;
        private readonly IEntityRepository<PageVisit, long> pageVisitRepository;
        private readonly IEntityRepository<CommonContactRecord, long> commonContactRecordRepository;

        public ContactController(IEntityRepository<CommonContactRecord, long> commonContactRecordRepository, IEntityRepository<PageVisit, long> pageVisitRepository, IEntityRepository<PageHeader, PageId> pageHeaderRepository, IEntityRepository<RichTextBlock, RichTextBlockId> richTextBlockRepository)
        {
            this.richTextBlockRepository = richTextBlockRepository;
            this.pageHeaderRepository = pageHeaderRepository;
            this.pageVisitRepository = pageVisitRepository;
            this.commonContactRecordRepository = commonContactRecordRepository;
        }

        public IActionResult Index()
        {
            pageVisitRepository.Add(new PageVisit
            {
                PageId = PageId.Contact,
                Context = Request.Path,
                Carrier = Request.Headers.UserAgent,
                NetworkIdentifier = Request.Headers.Host
            }, "system");

            PageHeaderDisplayViewModel header = null;

            var headerEntity = pageHeaderRepository.FindOneBy(_entity => _entity.Id == PageId.Contact);

            if (headerEntity != null)
            {
                header = new PageHeaderDisplayViewModel();

                header.Import(headerEntity);
            }

            var model = new Models.View.Frontend.ContactIndexViewModel
            {
                Header = header,
                RichTextBlock = new RichTextBlockDisplayViewModel()
            };

            model.RichTextBlock.Import(richTextBlockRepository.Get(RichTextBlockId.Contact));

            return View(model);
        }

        [HttpPost]
        [Route("[action]")]
        [ValidateAntiForgeryToken]
        public virtual IActionResult Contact(CommonContactRecordInputViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = model.Export();

                commonContactRecordRepository.Add(entity, User.Identity.Name ?? "guest");

                SetDefaultNotification(NotificationType.Success, "Mensaje de contacto registrado con éxito.", "Gracias por contactarnos y vistar nuestro sitio. Le prestaremos la atención adecuada a su mensaje.");

                return RedirectToAction("Index");
            }

            SetDefaultNotification(NotificationType.Danger, "Los campos especificados no son correctos.", "Los datos suministrados debe estar en el formáto correcto (Correo Electrónico y Número de Teléfono) y solo es opcional las Mensaje. Por favor, inténtelo nuevamente, asegurándose de cumplir estos sencillos requisitos.");

            return RedirectToAction("Index");
        }

        private void SetDefaultNotification(NotificationType notificationType, string message)
        {
            var notification = new NotificationViewModel
            {
                Type = notificationType,
                Message = message
            };

            TempData[NotificationViewModel.DEFAULT_NOTIFICATION_MAP_KEY] = Newtonsoft.Json.JsonConvert.SerializeObject(notification);
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
