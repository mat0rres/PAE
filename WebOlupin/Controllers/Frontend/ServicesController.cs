using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Enumerations;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View;
using System.Linq;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Controllers.Frontend
{
    public class ServicesController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IEntityRepository<Service, System.Guid> serviceRepository;
        private readonly IEntityRepository<PageHeader, PageId> pageHeaderRepository;
        private readonly IEntityRepository<PageVisit, long> pageVisitRepository;

        public ServicesController(IEntityRepository<PageVisit, long> pageVisitRepository, IEntityRepository<PageHeader, PageId> pageHeaderRepository, IEntityRepository<Service, System.Guid> serviceRepository)
        {
            this.serviceRepository = serviceRepository;
            this.pageHeaderRepository = pageHeaderRepository;
            this.pageVisitRepository = pageVisitRepository;
        }

        public Microsoft.AspNetCore.Mvc.IActionResult Index()
        {
            pageVisitRepository.Add(new PageVisit
            {
                PageId = PageId.Services,
                Context = Request.Path,
                Carrier = Request.Headers.UserAgent,
                NetworkIdentifier = Request.Headers.Host
            }, "system");

            PageHeaderDisplayViewModel header = null;

            var headerEntity = pageHeaderRepository.FindOneBy(_entity => _entity.Id == PageId.Services);

            if (headerEntity != null)
            {
                header = new PageHeaderDisplayViewModel();

                header.Import(headerEntity);
            }

            return View(new Models.View.Frontend.ServicesIndexViewModel
            {
                Header = header,
                Items = serviceRepository.FindBy(entity => entity.Enabled).OrderBy(entity => entity.Priority).ToArray().Select(entity =>
                {
                    var _model = new Models.View.ServiceDisplayViewModel();

                    _model.Import(entity);

                    return _model;
                }).ToArray()
            });
        }
    }
}
