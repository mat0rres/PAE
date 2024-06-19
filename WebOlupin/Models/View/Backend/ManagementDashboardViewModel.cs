using System.Collections.Generic;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Backend
{
    public class ManagementDashboardViewModel
    {
        public ManagementDashboardViewModel()
        {
            UsersList = [];
            TracesbyUsers = [];
            WeekStatisticsColumns = [];
            WeekTracesStatisticsValues = [];
        }

        public Dictionary<string, ICollection<Framework.Guama.Core.Presentation.Web.Backend.Models.View.ManagementDashboardSectionViewModel>> Sections { get; set; }

        public List<string> UsersList { get; set; }
        public List<long> TracesbyUsers { get; set; }
        public List<string> WeekStatisticsColumns { get; set; }
        public List<int> WeekTracesStatisticsValues { get; set; }
    }
}
