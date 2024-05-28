namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View
{
    public class BrandDisplayViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<PremierAutomotiveExport.Persistence.Domain.Entities.Brand, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public ImageDisplayViewModel Image { get; set; }

        public bool IsFavorite { get; set; }

        public string PromotionalSlogan { get; set; }

        public string Description { get; set; }

        public void Import(PremierAutomotiveExport.Persistence.Domain.Entities.Brand entity)
        {
            Id = entity.Id;
            Name = entity.Identity.Name;
            IsFavorite = entity.IsFavorite;
            PromotionalSlogan = entity.PromotionalSlogan;
            Description = entity.Description;
            Image = new ImageDisplayViewModel();

            Image.Import(entity.Identity.Image);
        }
    }
}
