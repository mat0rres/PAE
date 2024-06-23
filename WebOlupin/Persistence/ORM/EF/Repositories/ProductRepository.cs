using Microsoft.EntityFrameworkCore;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using System.Linq;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Persistence.ORM.EF.Repositories
{
    public class ProductRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract.AbstractEntityRepository<Product, System.Guid>
    {
        public ProductRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Product> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet()
                .Include(entity => entity.Brand).ThenInclude(entity => entity.Identity).ThenInclude(entity => entity.Image)
                .Include(entity => entity.FeatureImage)
                .Include(entity => entity.Video)
                .Include(entity => entity.ExtraImageBridges).ThenInclude(bridge => bridge.Stern)
                .Include(entity => entity.CustomAttributeBridges).ThenInclude(bridge => bridge.Stern)
                .Include(entity => entity.Features)
                .Include(entity => entity.Colors);
        }
    }
}
