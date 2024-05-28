using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Backend
{
    public class ProductCustomAttributesManagementViewModel
    {
        public ProductDisplayViewModel Product {  get; set; }

        public ProductCustomAttributeViewModel[] Items {  get; set; }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectListItem[] AvailableCustomAttributeSelectListItems { get; set; }
    }
}
