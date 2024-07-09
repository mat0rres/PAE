namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View
{
    public class CampaignDisplayViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<PremierAutomotiveExport.Persistence.Domain.Entities.Campaign, System.Guid>
    {
        public System.Guid Id { get; set; }

        public string Name { get; set; }

        public bool Enabled { get; set; }

        public int Priority { get; set; }

        public string Url { get; set; }

        public ImageDisplayViewModel Image { get; set; }

        public void Import(PremierAutomotiveExport.Persistence.Domain.Entities.Campaign entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Enabled = entity.Enabled;
            Priority = entity.Priority;
            Url = entity.Url;
            Image = new ImageDisplayViewModel();

            Image.Import(entity.Image);
        }
    }
}
