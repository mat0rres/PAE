using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Attributes;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Backend;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View;
using System.Linq;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Controllers.Backend.Contacts
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [RequirePermissions(Framework.Guama.Core.Security.Presentation.Constants.Permissions.ShellManager)]
    [Area("contacts")]
    public class ProductForContactRecordsController : Framework.Guama.Core.Presentation.Web.Backend.Controllers.AbstractEntityManagementController<ProductForContactRecord, long, ProductForContactRecordInputViewModel, ProductForContactRecordDisplayViewModel>
    {
        public ProductForContactRecordsController(IEntityRepository<ProductForContactRecord, long> repository, Microsoft.Extensions.Localization.IStringLocalizer<ProductForContactRecordsController> localizer, ILogger<ProductForContactRecordsController> logger) : base(repository, localizer, logger)
        {
        }

        [RequirePermissions(Constants.Authorization.ProductForContactRecordViewerPermission)]
        public override IActionResult Index(int sheet = 1, int limit = 25)
        {
            return base.Index(sheet, limit);
        }

        public override IActionResult Create()
        {
            return NotFound();
        }

        public override IActionResult Create(ProductForContactRecordInputViewModel modelInput)
        {
            return NotFound();
        }

        public override IActionResult Edit(long key)
        {
            return NotFound();
        }

        public override IActionResult Edit(ProductForContactRecordInputViewModel modelInput)
        {
            return NotFound();
        }

        public override IActionResult Delete(long key)
        {
            return NotFound();
        }

        [RequirePermissions(Constants.Authorization.ProductForContactRecordRemoverPermission)]
        public override IActionResult Delete(ProductForContactRecordDisplayViewModel model)
        {
            return base.Delete(model);
        }

        [RequirePermissions(Constants.Authorization.ProductForContactRecordViewerPermission)]
        [Route("[action]/{key}")]
        public IActionResult Details(long key)
        {
            var entity = Repository.Get(key);

            if (entity.ReadAt == null)
            {
                entity.ReadAt = System.DateTime.Now;

                Repository.Update(entity, User.Identity.Name);
            }

            var model = new ProductForContactRecordDisplayViewModel();

            model.Import(entity);

            return View(model);
        }
    }
}
