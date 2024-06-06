using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Enumerations;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Controllers.Frontend
{
    public class ContactController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IEntityRepository<RichTextBlock, RichTextBlockId> richTextBlockRepository;
        private readonly IEntityRepository<PageHeader, PageId> pageHeaderRepository;
        private readonly IEntityRepository<PageVisit, long> pageVisitRepository;

        public ContactController(IEntityRepository<PageVisit, long> pageVisitRepository, IEntityRepository<PageHeader, PageId> pageHeaderRepository, IEntityRepository<RichTextBlock, RichTextBlockId> richTextBlockRepository)
        {
            this.richTextBlockRepository = richTextBlockRepository;
            this.pageHeaderRepository = pageHeaderRepository;
            this.pageVisitRepository = pageVisitRepository;
        }

        public Microsoft.AspNetCore.Mvc.IActionResult Index()
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
    }
}
