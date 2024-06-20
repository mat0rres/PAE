namespace MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities
{
    public class CommonContactRecord : Contract.AbstractContactRecord
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string Subject { get; set; }
    }
}
