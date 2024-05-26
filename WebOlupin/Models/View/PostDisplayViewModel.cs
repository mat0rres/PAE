namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View
{
    public class PostDisplayViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<PremierAutomotiveExport.Persistence.Domain.Entities.Post, long>
    {
        public long Id { get; set; }

        public System.DateTime CreatedAt { get; set; }

        public bool Enabled { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public string Content { get; set; }

        public ImageDisplayViewModel FeatureImage { get; set; }

        public void Import(PremierAutomotiveExport.Persistence.Domain.Entities.Post entity)
        {
            Id = entity.Id;
            CreatedAt = entity.CreatedAt;
            Enabled = entity.Enabled;
            Title = entity.Title;
            Summary = entity.Summary;
            Content = entity.Content;

            if (entity.FeatureImage == null) return;

            FeatureImage = new ImageDisplayViewModel();

            FeatureImage.Import(entity.FeatureImage);
        }
    }
}
