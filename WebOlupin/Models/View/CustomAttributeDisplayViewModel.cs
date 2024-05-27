namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View
{
    public class CustomAttributeDisplayViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<PremierAutomotiveExport.Persistence.Domain.Entities.CustomAttribute, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public void Import(PremierAutomotiveExport.Persistence.Domain.Entities.CustomAttribute entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }
    }
}
