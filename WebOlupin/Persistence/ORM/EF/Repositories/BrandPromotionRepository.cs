using Microsoft.EntityFrameworkCore;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using System.Linq;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Persistence.ORM.EF.Repositories
{
    public class BrandPromotionRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract.AbstractEntityRepository<BrandPromotion, System.Guid>
    {
        public BrandPromotionRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<BrandPromotion> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.Brand).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.Image)
                .Include(entity => entity.FeatureImage);
        }
    }
}
