namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View
{
    public class ShowcaseItemDisplayViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<PremierAutomotiveExport.Persistence.Domain.Entities.ShowcaseItem, System.Guid>
    {
        public System.Guid Id { get; set; }

        public ImageDisplayViewModel Image { get; set; }

        public void Import(PremierAutomotiveExport.Persistence.Domain.Entities.ShowcaseItem entity)
        {
            Id = entity.Id;
            Image = new ImageDisplayViewModel();

            Image.Import(entity.Image);
        }
    }
}
