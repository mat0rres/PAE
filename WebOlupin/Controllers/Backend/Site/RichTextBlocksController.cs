using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Enumerations;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Backend;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Controllers.Backend.Site
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(Framework.Guama.Core.Security.Presentation.Constants.Permissions.ShellManager)]
    [Area("site")]
    public class RichTextBlocksController : Framework.Guama.Core.Presentation.Web.Backend.Controllers.AbstractEntityManagementController<RichTextBlock, RichTextBlockId, RichTextBlockInputViewModel, RichTextBlockDisplayViewModel>
    {
        public RichTextBlocksController(Framework.Guama.Core.Persistence.Repositories.IEntityRepository<RichTextBlock, RichTextBlockId> repository, Microsoft.Extensions.Localization.IStringLocalizer<RichTextBlocksController> localizer, ILogger<RichTextBlocksController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Constants.Authorization.RichTextBlockViewerPermission)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        public override IActionResult Create()
        {
            return NotFound();
        }

        public override IActionResult Create(RichTextBlockInputViewModel modelInput)
        {
            return NotFound();
        }

        [RequirePermissions(Constants.Authorization.RichTextBlockViewerPermission)]
        public override IActionResult Edit(RichTextBlockId key)
        {
            return base.Edit(key);
        }

        [RequirePermissions(Constants.Authorization.RichTextBlockViewerPermission)]
        public override IActionResult Edit(RichTextBlockInputViewModel modelInput)
        {
            return base.Edit(modelInput);
        }

        public override IActionResult Delete(RichTextBlockId key)
        {
            return NotFound();
        }

        public override IActionResult Delete(RichTextBlockDisplayViewModel displayModel)
        {
            return NotFound();
        }
    }
}
