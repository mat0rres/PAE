using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Backend;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Controllers.Backend.Contacts
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(Framework.Guama.Core.Security.Presentation.Constants.Permissions.ShellManager)]
    [Area("contacts")]
    public class CommercialsController : Framework.Guama.Core.Presentation.Web.Backend.Controllers.AbstractEntityManagementController<Commercial, System.Guid, CommercialInputViewModel, CommercialDisplayViewModel>
    {
        public CommercialsController(IEntityRepository<Commercial, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<CommercialsController> localizer, ILogger<CommercialsController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Constants.Authorization.CommercialViewerPermission)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequirePermissions(Constants.Authorization.CommercialCreatorPermission)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Constants.Authorization.CommercialCreatorPermission)]
        public override IActionResult Create(CommercialInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        [RequirePermissions(Constants.Authorization.CommercialEditorPermission)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Constants.Authorization.CommercialEditorPermission)]
        public override IActionResult Edit(CommercialInputViewModel modelInput)
        {
            return base.Edit(modelInput);
        }

        public override IActionResult Delete(System.Guid key)
        {
            return NotFound();
        }

        [RequirePermissions(Constants.Authorization.CommercialRemoverPermission)]
        public override IActionResult Delete(CommercialDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
