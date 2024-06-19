using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Controllers.Backend.Contacts
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(Framework.Guama.Core.Security.Presentation.Constants.Permissions.ShellManager)]
    [Area("contacts")]
    public class CommonContactRecordsController : Framework.Guama.Core.Presentation.Web.Backend.Controllers.AbstractEntityManagementController<CommonContactRecord, long, CommonContactRecordInputViewModel, CommonContactRecordDisplayViewModel>
    {
        public CommonContactRecordsController(IEntityRepository<CommonContactRecord, long> repository, Microsoft.Extensions.Localization.IStringLocalizer<CommonContactRecordsController> localizer, ILogger<CommonContactRecordsController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Constants.Authorization.CommonContactRecordViewerPermission)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        public override IActionResult Create()
        {
            return NotFound();
        }

        public override IActionResult Create(CommonContactRecordInputViewModel modelInput)
        {
            return NotFound();
        }

        public override IActionResult Edit(long key)
        {
            return NotFound();
        }

        public override IActionResult Edit(CommonContactRecordInputViewModel modelInput)
        {
            return NotFound();
        }

        public override IActionResult Delete(long key)
        {
            return NotFound();
        }

        [RequirePermissions(Constants.Authorization.CommonContactRecordRemoverPermission)]
        public override IActionResult Delete(CommonContactRecordDisplayViewModel displayModel)
        {
            return base.Delete(displayModel);
        }

        [RequirePermissions(Constants.Authorization.CommonContactRecordViewerPermission)]
        [Route("[action]/{key}")]
        public IActionResult Detail(long key)
        {
            var entity = Repository.Get(key);

            if (entity.ReadAt == null)
            {
                entity.ReadAt = System.DateTime.Now;

                Repository.Update(entity, User.Identity.Name);
            }

            var model = new CommonContactRecordDisplayViewModel();

            model.Import(entity);

            return View(model);
        }
    }
}
