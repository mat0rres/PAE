namespace MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities
{
    public class Category : Framework.Guama.Core.Persistence.Entities.Contract.INamedEntity<int>, Framework.Guama.Core.Persistence.Entities.Contract.IHabilitableEntity<int>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }

        public bool Enabled { get; set; }

        public int Priority { get; set; } = 1;

        [System.ComponentModel.DataAnnotations.Required]
        public string Name { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(ParentId))]
        public Category Parent { get; set; }
        public int? ParentId { get; set; }

        public bool IsOnNavigation { get; set; }
    }
}
