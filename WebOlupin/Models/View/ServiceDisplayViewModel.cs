namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View
{
    public class ServiceDisplayViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<PremierAutomotiveExport.Persistence.Domain.Entities.Service, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ImageDisplayViewModel FeatureImage { get; set; }

        public void Import(PremierAutomotiveExport.Persistence.Domain.Entities.Service entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
            FeatureImage = new ImageDisplayViewModel();

            FeatureImage.Import(entity.FeatureImage);
        }
    }
}
