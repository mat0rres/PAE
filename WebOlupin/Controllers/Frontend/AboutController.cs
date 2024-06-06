using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Enumerations;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View;
using System.Linq;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Controllers.Frontend
{
    public class AboutController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IEntityRepository<RichTextBlock, RichTextBlockId> richTextBlockRepository;
        private readonly IEntityRepository<Representative, System.Guid> representativeRepository;
        private readonly IEntityRepository<PageHeader, PageId> pageHeaderRepository;
        private readonly IEntityRepository<PageVisit, long> pageVisitRepository;

        public AboutController(IEntityRepository<PageVisit, long> pageVisitRepository, IEntityRepository<PageHeader, PageId> pageHeaderRepository, IEntityRepository<Representative, System.Guid> representativeRepository, IEntityRepository<RichTextBlock, RichTextBlockId> richTextBlockRepository)
        {
            this.richTextBlockRepository = richTextBlockRepository;
            this.representativeRepository = representativeRepository;
            this.pageHeaderRepository = pageHeaderRepository;
            this.pageVisitRepository = pageVisitRepository;
        }

        public Microsoft.AspNetCore.Mvc.IActionResult Index()
        {
            pageVisitRepository.Add(new PageVisit
            {
                PageId = PageId.About,
                Context = Request.Path,
                Carrier = Request.Headers.UserAgent,
                NetworkIdentifier = Request.Headers.Host
            }, "system");

            PageHeaderDisplayViewModel header = null;

            var headerEntity = pageHeaderRepository.FindOneBy(_entity => _entity.Id == PageId.About);

            if (headerEntity != null)
            {
                header = new PageHeaderDisplayViewModel();

                header.Import(headerEntity);
            }

            var model = new Models.View.Frontend.AboutIndexViewModel
            {
                Header = header,
                MainTextBlock = new RichTextBlockDisplayViewModel(),
                Representatives = representativeRepository.FindBy(entity => entity.Enabled).OrderBy(entity => entity.Priority).ToArray().Select(entity =>
                {
                    var _model = new RepresentativeDisplayViewModel();

                    _model.Import(entity);

                    return _model;
                }).ToArray()
            };

            model.MainTextBlock.Import(richTextBlockRepository.Get(RichTextBlockId.About));

            return View(model);
        }
    }
}
