namespace MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities
{
    public class Representative : Framework.Guama.Core.Persistence.Entities.Contract.IHabilitableEntity<System.Guid>
    {
        public Representative()
        {
            Id = System.Guid.NewGuid();
        }

        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; }

        public bool Enabled { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(IdentityId))]
        public Framework.Guama.Identity.Persistence.Entities.PersonIdentity Identity { get; set; }
        public System.Guid IdentityId { get; set; }

        public int Priority { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Position { get; set; }
    }
}
