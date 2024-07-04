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

            var semesterPageVisits = pageVisitRepository.FindBy(_entity => _entity.CreatedAt > now.AddMonths(-6)).ToArray();

            var monthPageVisits = semesterPageVisits.Where(_entity => _entity.CreatedAt > now.AddMonths(-1)).ToArray();

            var weekPageVisit = monthPageVisits.Where(_entity => _entity.CreatedAt > now.AddDays(-5)).ToArray();

            var model = new Models.View.Backend.HomeViewModel
            {
                Sections = managementDashboardSectionProviders.ToManagementDashboardSectionGroups(),
                VisitsBySection =
                [
                    semesterPageVisits.Count(_entity => _entity.PageId == PremierAutomotiveExport.Persistence.Domain.Enumerations.PageId.About),
                    semesterPageVisits.Count(_entity => _entity.PageId == PremierAutomotiveExport.Persistence.Domain.Enumerations.PageId.News),
                    semesterPageVisits.Count(_entity => _entity.PageId == PremierAutomotiveExport.Persistence.Domain.Enumerations.PageId.Services),
                    semesterPageVisits.Count(_entity => _entity.PageId == PremierAutomotiveExport.Persistence.Domain.Enumerations.PageId.Product),
                    semesterPageVisits.Count(_entity => _entity.PageId == PremierAutomotiveExport.Persistence.Domain.Enumerations.PageId.Category),
                    semesterPageVisits.Count(_entity => _entity.PageId == PremierAutomotiveExport.Persistence.Domain.Enumerations.PageId.Brand),
                    semesterPageVisits.Count(_entity => _entity.PageId == PremierAutomotiveExport.Persistence.Domain.Enumerations.PageId.Contact)
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
                MonthUserActivity =
                [
                    new KeyValuePair<int,long>(now.Day, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day)),
                    new KeyValuePair<int,long>(now.AddDays(-1).Day, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 1)),
                    new KeyValuePair<int,long>(now.AddDays(-2).Day, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 2)),
                    new KeyValuePair<int,long>(now.AddDays(-3).Day, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 3)),
                    new KeyValuePair<int,long>(now.AddDays(-4).Day, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 4)),
                    new KeyValuePair<int,long>(now.AddDays(-5).Day, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 5)),
                    new KeyValuePair<int,long>(now.AddDays(-6).Day, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 6)),
                    new KeyValuePair<int,long>(now.AddDays(-7).Day, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 7)),
                    new KeyValuePair<int,long>(now.AddDays(-8).Day, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 8)),
                    new KeyValuePair<int,long>(now.AddDays(-9).Day, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 9)),
                    new KeyValuePair<int,long>(now.AddDays(-10).Day, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 10)),
                    new KeyValuePair<int,long>(now.AddDays(-11).Day, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 11)),
                    new KeyValuePair<int,long>(now.AddDays(-12).Day, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 12)),
                    new KeyValuePair<int,long>(now.AddDays(-13).Day, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 13)),
                    new KeyValuePair<int,long>(now.AddDays(-14).Day, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 14)),
                    new KeyValuePair<int,long>(now.AddDays(-15).Day, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 15)),
                    new KeyValuePair<int,long>(now.AddDays(-16).Day, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 16)),
                    new KeyValuePair<int,long>(now.AddDays(-17).Day, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 17)),
                    new KeyValuePair<int,long>(now.AddDays(-18).Day, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 18)),
                    new KeyValuePair<int,long>(now.AddDays(-19).Day, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 19)),
                    new KeyValuePair<int,long>(now.AddDays(-20).Day, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 20)),
                    new KeyValuePair<int,long>(now.AddDays(-21).Day, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 21)),
                    new KeyValuePair<int,long>(now.AddDays(-22).Day, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 22)),
                    new KeyValuePair<int,long>(now.AddDays(-23).Day, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 23)),
                    new KeyValuePair<int,long>(now.AddDays(-24).Day, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 24)),
                    new KeyValuePair<int,long>(now.AddDays(-25).Day, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 25)),
                    new KeyValuePair<int,long>(now.AddDays(-26).Day, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 26)),
                    new KeyValuePair<int,long>(now.AddDays(-27).Day, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 27)),
                    new KeyValuePair<int,long>(now.AddDays(-28).Day, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 28)),
                    new KeyValuePair<int,long>(now.AddDays(-29).Day, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 29)),
                    new KeyValuePair<int,long>(now.AddDays(-30).Day, weekPageVisit.LongCount(_entity => _entity.CreatedAt.Day == now.Day - 30))
                ],
                SemesterUserActivity = 
                [
                    new KeyValuePair<int,long>(now.Month, semesterPageVisits.LongCount(_entity => _entity.CreatedAt.Month == now.Month)),
                    new KeyValuePair<int,long>(now.AddMonths(-1).Month, semesterPageVisits.LongCount(_entity => _entity.CreatedAt.Month == now.AddMonths(-1).Month)),
                    new KeyValuePair<int,long>(now.AddMonths(-2).Month, semesterPageVisits.LongCount(_entity => _entity.CreatedAt.Month == now.AddMonths(-2).Month)),
                    new KeyValuePair<int,long>(now.AddMonths(-3).Month, semesterPageVisits.LongCount(_entity => _entity.CreatedAt.Month == now.AddMonths(-3).Month)),
                    new KeyValuePair<int,long>(now.AddMonths(-4).Month, semesterPageVisits.LongCount(_entity => _entity.CreatedAt.Month == now.AddMonths(-4).Month)),
                    new KeyValuePair<int,long>(now.AddMonths(-5).Month, semesterPageVisits.LongCount(_entity => _entity.CreatedAt.Month == now.AddMonths(-5).Month))
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
