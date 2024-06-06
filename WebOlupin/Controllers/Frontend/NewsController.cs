using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Enumerations;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View;
using System.Linq;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Controllers.Frontend
{
    public class NewsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IEntityRepository<Post, long> postRepository;
        private readonly IEntityRepository<PageHeader, PageId> pageHeaderRepository;
        private readonly IEntityRepository<PageVisit, long> pageVisitRepository;

        public NewsController(IEntityRepository<PageVisit, long> pageVisitRepository, IEntityRepository<PageHeader, PageId> pageHeaderRepository, IEntityRepository<Post, long> postRepository)
        {
            this.postRepository = postRepository;
            this.pageHeaderRepository = pageHeaderRepository;
            this.pageVisitRepository = pageVisitRepository;
        }

        public Microsoft.AspNetCore.Mvc.IActionResult Index()
        {
            pageVisitRepository.Add(new PageVisit
            {
                PageId = PageId.News,
                Context = Request.Path,
                Carrier = Request.Headers.UserAgent,
                NetworkIdentifier = Request.Headers.Host
            }, "system");

            PageHeaderDisplayViewModel header = null;

            var headerEntity = pageHeaderRepository.FindOneBy(_entity => _entity.Id == PageId.News);

            if (headerEntity != null)
            {
                header = new PageHeaderDisplayViewModel();

                header.Import(headerEntity);
            }

            return View(new Models.View.Frontend.NewsIndexViewModel
            {
                Header = header,
                Items = postRepository.FindBy(entity => entity.Enabled).OrderByDescending(entity => entity.CreatedAt).ToArray().Select(entity =>
                {
                    var _model = new PostDisplayViewModel();

                    _model.Import(entity);

                    return _model;
                }).ToArray()
            });
        }
    }
}
