namespace MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities
{
    public class Brand : Framework.Guama.Core.Persistence.Entities.Contract.IEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; } = System.Guid.NewGuid();

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(IdentityId))]
        public Framework.Guama.Identity.Persistence.Entities.OrganizationIdentity Identity { get; set; } = new Framework.Guama.Identity.Persistence.Entities.OrganizationIdentity();
        public System.Guid IdentityId { get; set; }

        public bool IsFavorite { get; set; }

        public string PromotionalSlogan { get; set; }
    }
}
