using System.Collections.Generic;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Backend
{
    public class HomeViewModel
    {
        public Dictionary<string, ICollection<Framework.Guama.Core.Presentation.Web.Backend.Models.View.ManagementDashboardSectionViewModel>> Sections { get; set; }

        public long[] VisitsBySection {  get; set; }

        public KeyValuePair<System.DayOfWeek, long>[] WeekUserActivity { get; set; }

        public KeyValuePair<int, long>[] MonthUserActivity { get; set; }

        public KeyValuePair<int, long>[] SemesterUserActivity { get; set; }

        public int UnreadProductForContactRecordCount { get; set; }
        public int WeekProductForContactRecordCount { get; set; }

        public int UnreadCommonContactRecordCount { get; set; }
        public int WeekCommonContactRecordCount { get; set; }
    }
}
