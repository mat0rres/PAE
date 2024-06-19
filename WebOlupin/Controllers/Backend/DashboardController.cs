using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.WebOlupin.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Controllers.Backend
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Framework.Guama.Core.Presentation.Web.Security.Attributes.RequirePermissions(Framework.Guama.Core.Security.Presentation.Constants.Permissions.ShellManager)]
    [Framework.Guama.Core.Presentation.Web.Security.Attributes.ChangeUserPasswordRequiredAction]
    [Framework.Guama.Core.Presentation.Web.Security.Attributes.CatchChangeUserPasswordRequiredException]
    [Route("[area]/[controller]")]
    [Area("management")]
    public class DashboardController : Framework.Guama.Core.Presentation.Web.Common.Controllers.BaseWebController
    {
        private readonly IEnumerable<Framework.Guama.Core.Presentation.Web.Backend.Providers.Facade.IManagementDashboardSectionProviderFacade> managementDashboardSectionProviders;
        private readonly IEntityRepository<PageVisit, long> pageVisitRepository;
        private readonly IEntityRepository<ProductForContactRecord, long> productForContactRecordRepository;
        private readonly IEntityRepository<CommonContactRecord, long> commonContactRecordRepository;

        public DashboardController(IEntityRepository<CommonContactRecord, long> commonContactRecordRepository, IEntityRepository<ProductForContactRecord, long> productForContactRecordRepository, IEntityRepository<PageVisit, long> pageVisitRepository, IEnumerable<Framework.Guama.Core.Presentation.Web.Backend.Providers.Facade.IManagementDashboardSectionProviderFacade> managementDashboardSectionProviders, Microsoft.Extensions.Localization.IStringLocalizer<DashboardController> localizer, ILogger<DashboardController> logger) : base(localizer, logger)
        {
            this.managementDashboardSectionProviders = managementDashboardSectionProviders;
            this.pageVisitRepository = pageVisitRepository;
            this.productForContactRecordRepository = productForContactRecordRepository;
            this.commonContactRecordRepository = commonContactRecordRepository;
        }

        public IActionResult Index()
        {
            var now = System.DateTime.Now;

            var weekPageVisit = pageVisitRepository.FindBy(_entity => _entity.CreatedAt > now.AddDays(-5)).ToArray();

            var model = new Models.View.Backend.HomeViewModel
            {
                Sections = managementDashboardSectionProviders.ToManagementDashboardSectionGroups(),
                VisitsBySection =
                [
                    pageVisitRepository.Count(_entity => _entity.PageId == PremierAutomotiveExport.Persistence.Domain.Enumerations.PageId.About),
                    pageVisitRepository.Count(_entity => _entity.PageId == PremierAutomotiveExport.Persistence.Domain.Enumerations.PageId.News),
                    pageVisitRepository.Count(_entity => _entity.PageId == PremierAutomotiveExport.Persistence.Domain.Enumerations.PageId.Services),
                    pageVisitRepository.Count(_entity => _entity.PageId == PremierAutomotiveExport.Persistence.Domain.Enumerations.PageId.Product),
                    pageVisitRepository.Count(_entity => _entity.PageId == PremierAutomotiveExport.Persistence.Domain.Enumerations.PageId.Category),
                    pageVisitRepository.Count(_entity => _entity.PageId == PremierAutomotiveExport.Persistence.Domain.Enumerations.PageId.Brand),
                    pageVisitRepository.Count(_entity => _entity.PageId == PremierAutomotiveExport.Persistence.Domain.Enumerations.PageId.Contact)
                ],
                WeekUserActivity =
                [
                    new KeyValuePair<System.DayOfWeek,long>(now.DayOfWeek, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day)),
                    new KeyValuePair<System.DayOfWeek,long>(now.AddDays(-1).DayOfWeek, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 1)),
                    new KeyValuePair<System.DayOfWeek,long>(now.AddDays(-2).DayOfWeek, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 2)),
                    new KeyValuePair<System.DayOfWeek,long>(now.AddDays(-3).DayOfWeek, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 3)),
                    new KeyValuePair<System.DayOfWeek,long>(now.AddDays(-4).DayOfWeek, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 4)),
                    new KeyValuePair<System.DayOfWeek,long>(now.AddDays(-5).DayOfWeek, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 5)),
                    new KeyValuePair<System.DayOfWeek,long>(now.AddDays(-6).DayOfWeek, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 6))
                ],
                UnreadProductForContactRecordCount = productForContactRecordRepository.Count(_entity => _entity.ReadAt == null),
                WeekProductForContactRecordCount = productForContactRecordRepository.Count(_entity => _entity.CreatedAt > now.AddDays(-7)),
                UnreadCommonContactRecordCount = commonContactRecordRepository.Count(_entity => _entity.ReadAt == null),
                WeekCommonContactRecordCount = commonContactRecordRepository.Count(_entity => _entity.CreatedAt > now.AddDays(-7))
            };

            return View(model);
        }
    }
}
