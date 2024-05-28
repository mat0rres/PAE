using System.Collections.Generic;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Frontend
{
    public class CategoryIndexViewModel
    {
        public CategoryDisplayViewModel Category {  get; set; }

        public List<KeyValuePair<BrandDisplayViewModel, ProductDisplayViewModel[]>> Favorites { get; private set; } = new List<KeyValuePair<BrandDisplayViewModel, ProductDisplayViewModel[]>>();

        public List<KeyValuePair<BrandDisplayViewModel, ProductDisplayViewModel[]>> Regulars { get; private set; } = new List<KeyValuePair<BrandDisplayViewModel, ProductDisplayViewModel[]>>();
    }
}
