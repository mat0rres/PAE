namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View
{
    public class ProductFeatureDisplayViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<PremierAutomotiveExport.Persistence.Domain.Entities.ProductFeature, long>
    {
        public long Id { get; set; }

        public string Description { get; set; }

        public void Import(PremierAutomotiveExport.Persistence.Domain.Entities.ProductFeature entity)
        {
            Id = entity.Id;
            Description = entity.Description;
        }
    }
}
