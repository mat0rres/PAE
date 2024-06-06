namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Frontend
{
    public class BrandIndexViewModel : Contract.AbstractFrontendPageViewModel
    {
        public BrandDisplayViewModel Brand {  get; set; }

        public ProductDisplayViewModel[] Products { get; set; }
    }
}
