using Microsoft.AspNetCore.Mvc;
using MOROPO.Framework.Guama.Core.Persistence.ORM.EF.Entities;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Controllers.Backend.Security
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(Framework.Guama.Core.Security.Presentation.Constants.Permissions.ShellManager)]
    [Area("security")]
    public class TracesController : Framework.Guama.Core.Presentation.Web.Backend.Controllers.AbstractEntityConsultFilteredController<Trace, long, Models.View.Backend.TraceDisplayViewModel, Models.View.Backend.Filter.TracesFilterViewModel>
    {
        public TracesController(Framework.Guama.Core.Persistence.Repositories.IEntityRepository<Trace, long> repository, Microsoft.Extensions.Localization.IStringLocalizer<TracesController> localizer, Microsoft.Extensions.Logging.ILogger<TracesController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Constants.Authorization.TraceViewerPermission)]
        public IActionResult Index()
        {
            return View();
        }

        [RequirePermissions(Constants.Authorization.TraceViewerPermission)]
        public override IActionResult Filter(Models.View.Backend.Filter.TracesFilterViewModel filterModel)
        {
            return base.Filter(filterModel);
        }

        protected override string GetFilterSessionID()
        {
            return "traces-management-filter";
        }
    }
}
