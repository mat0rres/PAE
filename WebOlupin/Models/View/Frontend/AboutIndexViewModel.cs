namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Frontend
{
    public class AboutIndexViewModel : Contract.AbstractFrontendPageViewModel
    {
        public RichTextBlockDisplayViewModel MainTextBlock {  get; set; }

        public RepresentativeDisplayViewModel[] Representatives { get; set; }
    }
}
