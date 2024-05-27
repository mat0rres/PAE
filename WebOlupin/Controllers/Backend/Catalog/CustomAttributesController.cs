using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Backend;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Controllers.Backend.Catalog
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(Framework.Guama.Core.Security.Presentation.Constants.Permissions.ShellManager)]
    [Area("catalog")]
    public class CustomAttributesController : Framework.Guama.Core.Presentation.Web.Backend.Controllers.AbstractEntityManagementController<CustomAttribute, System.Guid, CustomAttributeInputViewModel, CustomAttributeDisplayViewModel>
    {
        public CustomAttributesController(IEntityRepository<CustomAttribute, System.Guid> repository, Microsoft.Extensions.Localization.IStringLocalizer<CustomAttributesController> localizer, ILogger<CustomAttributesController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Constants.Authorization.CustomAttributeViewerPermission)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        [RequirePermissions(Constants.Authorization.CustomAttributeCreatorPermission)]
        public override IActionResult Create()
        {
            return base.Create();
        }

        [RequirePermissions(Constants.Authorization.CustomAttributeCreatorPermission)]
        public override IActionResult Create(CustomAttributeInputViewModel modelInput)
        {
            return base.Create(modelInput);
        }

        [RequirePermissions(Constants.Authorization.CustomAttributeEditorPermission)]
        public override IActionResult Edit(System.Guid key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Constants.Authorization.CustomAttributeEditorPermission)]
        public override IActionResult Edit(CustomAttributeInputViewModel modelInput)
        {
            return base.Edit(modelInput);
        }

        public override IActionResult Delete(System.Guid key)
        {
            return NotFound();
        }

        [RequirePermissions(Constants.Authorization.CustomAttributeRemoverPermission)]
        public override IActionResult Delete(CustomAttributeDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }
    }
}
