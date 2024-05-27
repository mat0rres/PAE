using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using System.Linq;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Controllers.Frontend
{
    public class ServicesController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IEntityRepository<Service, System.Guid> serviceRepository;

        public ServicesController(IEntityRepository<Service, System.Guid> serviceRepository)
        {
            this.serviceRepository = serviceRepository;
        }

        public Microsoft.AspNetCore.Mvc.IActionResult Index()
        {
            return View(serviceRepository.FindBy(entity => entity.Enabled).OrderBy(entity => entity.Priority).ToArray().Select(entity =>
            {
                var _model = new Models.View.ServiceDisplayViewModel();

                _model.Import(entity);

                return _model;
            }).ToArray());
        }
    }
}
