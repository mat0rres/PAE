namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Frontend
{
    public class ProductDetailViewModel
    {
        public ProductDisplayViewModel Product {  get; set; }

        public string Referer { get; set; }

        public ProductCustomAttributeViewModel[] Attributes { get; set; }

        public ProductFeatureDisplayViewModel[] Features { get; set; }

        public ImageDisplayViewModel[] ExtraImages { get; set; }
    }
}
