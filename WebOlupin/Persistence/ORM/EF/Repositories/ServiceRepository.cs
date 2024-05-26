using Microsoft.EntityFrameworkCore;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using System.Linq;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Persistence.ORM.EF.Repositories
{
    public class ServiceRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract.AbstractEntityRepository<Service, System.Guid>
    {
        public ServiceRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Service> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(entity => entity.FeatureImage);
        }
    }
}
