using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Models.View;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Enumerations;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View;
using System.Linq;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Controllers.Frontend
{
    public class ContactController : Framework.Guama.Core.Presentation.Web.Common.Controllers.BaseWebController
    {
        private readonly IEntityRepository<RichTextBlock, RichTextBlockId> richTextBlockRepository;
        private readonly IEntityRepository<PageHeader, PageId> pageHeaderRepository;
        private readonly IEntityRepository<PageVisit, long> pageVisitRepository;
        private readonly IEntityRepository<CommonContactRecord, long> commonContactRecordRepository;
        private readonly IEntityRepository<Commercial, System.Guid> commercialRepository;
        private readonly Helpers.EmailNotificationHelper emailNotificationHelper;

        public ContactController(Helpers.EmailNotificationHelper emailNotificationHelper, IEntityRepository<Commercial, System.Guid> commercialRepository, IEntityRepository<CommonContactRecord, long> commonContactRecordRepository, IEntityRepository<PageVisit, long> pageVisitRepository, IEntityRepository<PageHeader, PageId> pageHeaderRepository, IEntityRepository<RichTextBlock, RichTextBlockId> richTextBlockRepository, Microsoft.Extensions.Localization.IStringLocalizer<ContactController> localizer, ILogger<ContactController> logger) : base(localizer, logger)
        {
            this.richTextBlockRepository = richTextBlockRepository;
            this.pageHeaderRepository = pageHeaderRepository;
            this.pageVisitRepository = pageVisitRepository;
            this.commonContactRecordRepository = commonContactRecordRepository;
            this.commercialRepository = commercialRepository;
            this.emailNotificationHelper = emailNotificationHelper;
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

                var commercials = commercialRepository.FindBy(_entity => _entity.NotifyProductForContact).ToArray();

                if (commercials.Length > 0)
                {
                    try
                    {
                        emailNotificationHelper.NotifyByEmailForCommonContactToCommercials(Request, entity, commercials);
                    }
                    catch (System.Exception exeption)
                    {
                        logger.LogError(exeption, "Error sending notification email message.");
                    }
                }

                SetDefaultNotification(NotificationType.Success, "Mensaje de contacto registrado con éxito.", "Gracias por contactarnos y vistar nuestro sitio. Le prestaremos la atención adecuada a su mensaje.");

                return RedirectToAction("Index");
            }

            SetDefaultNotification(NotificationType.Danger, "Los campos especificados no son correctos.", "Los datos suministrados debe estar en el formáto correcto (Correo Electrónico y Número de Teléfono) y solo es opcional las Mensaje. Por favor, inténtelo nuevamente, asegurándose de cumplir estos sencillos requisitos.");

            return RedirectToAction("Index");
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
