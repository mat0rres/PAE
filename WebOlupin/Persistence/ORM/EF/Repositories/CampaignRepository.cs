using Microsoft.EntityFrameworkCore;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Persistence.ORM.EF.Repositories
{
    public class CampaignRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract.AbstractEntityRepository<PremierAutomotiveExport.Persistence.Domain.Entities.Campaign, System.Guid>
    {
        public CampaignRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<PremierAutomotiveExport.Persistence.Domain.Entities.Campaign> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(entity => entity.Image);
        }
    }
}
