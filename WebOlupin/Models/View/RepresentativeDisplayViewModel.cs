namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View
{
    public class RepresentativeDisplayViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<PremierAutomotiveExport.Persistence.Domain.Entities.Representative, System.Guid>
    {
        public System.Guid Id { get; set; }

        public bool Enabled { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public ImageDisplayViewModel Image { get; set; }

        public string About { get; set; }

        public int Priority { get; set; }

        public string Position { get; set; }

        public string Fullname => string.IsNullOrEmpty(LastName) ? Name : string.Format("{0} {1}", Name, LastName);

        public void Import(PremierAutomotiveExport.Persistence.Domain.Entities.Representative entity)
        {
            Id = entity.Id;
            Enabled = entity.Enabled;
            Priority = entity.Priority;
            Position = entity.Position;
            Name = entity.Identity.Name;
            LastName = entity.Identity.LastName;
            About = entity.Identity.About;
            Image = new ImageDisplayViewModel();

            Image.Import(entity.Identity.Image);
        }
    }
}
