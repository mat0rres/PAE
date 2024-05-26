using System.Collections.Generic;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Frontend
{
    public class HomeIndexViewModel
    {
        public BrandPromotionDisplayViewModel[] BrandPromotions { get; set; }

        public KeyValuePair<BrandDisplayViewModel, BrandSupportOfferDisplayViewModel[]>[] BrandSupportOffers { get; set; }

        public BrandDisplayViewModel[] Brands { get; set; }
    }
}
