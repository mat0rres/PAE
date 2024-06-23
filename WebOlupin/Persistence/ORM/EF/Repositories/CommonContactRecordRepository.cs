namespace MOROPO.PremierAutomotiveExport.WebOlupin.Persistence.ORM.EF.Repositories
{
    public class CommonContactRecordRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract.AbstractEntityRepository<PremierAutomotiveExport.Persistence.Domain.Entities.CommonContactRecord, long>
    {
        public CommonContactRecordRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<PremierAutomotiveExport.Persistence.Domain.Entities.CommonContactRecord, object> GetPagingOrderSelector()
        {
            return entity => entity.CreatedAt;
        }

        public override bool IsPagingOrderDescending()
        {
            return false;
        }
    }
}
