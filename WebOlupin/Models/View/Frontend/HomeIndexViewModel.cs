using System.Collections.Generic;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Frontend
{
    public class HomeIndexViewModel : Contract.AbstractFrontendPageViewModel
    {
        public BrandPromotionDisplayViewModel[] BrandPromotions { get; set; }

        public KeyValuePair<BrandDisplayViewModel, BrandSupportOfferDisplayViewModel[]>[] BrandSupportOffers { get; set; }

        public BrandDisplayViewModel[] Brands { get; set; }

        public CampaignDisplayViewModel[] Campaigns { get; set; }
    }
}
