using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Enumerations;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View
{
    public class PageHeaderDisplayViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<PageHeader, PageId>
    {
        public PageId Id { get; set; }

        public ImageDisplayViewModel Image { get; set; }

        public void Import(PageHeader entity)
        {
            Id = entity.Id;
            Image = new ImageDisplayViewModel();

            Image.Import(entity.Image);
        }
    }
}
