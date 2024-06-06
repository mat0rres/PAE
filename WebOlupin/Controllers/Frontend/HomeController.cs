using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Enumerations;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View;
using System.Collections.Generic;
using System.Linq;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Controllers.Frontend
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IEntityRepository<Brand, System.Guid> brandRepository;
        private readonly IEntityRepository<BrandSupportOffer, System.Guid> brandSupportOfferRepository;
        private readonly IEntityRepository<BrandPromotion, System.Guid> brandPromotionRepository;
        private readonly IEntityRepository<PageHeader, PageId> pageHeaderRepository;
        private readonly IEntityRepository<PageVisit, long> pageVisitRepository;

        public HomeController(IEntityRepository<PageVisit, long> pageVisitRepository, IEntityRepository<PageHeader, PageId> pageHeaderRepository, IEntityRepository<BrandPromotion, System.Guid> brandPromotionRepository, IEntityRepository<BrandSupportOffer, System.Guid> brandSupportOfferRepository, IEntityRepository<Brand, System.Guid> brandRepository)
        {
            this.brandRepository = brandRepository;
            this.brandSupportOfferRepository = brandSupportOfferRepository;
            this.brandPromotionRepository = brandPromotionRepository;
            this.pageHeaderRepository = pageHeaderRepository;
            this.pageVisitRepository = pageVisitRepository;
        }

        public Microsoft.AspNetCore.Mvc.IActionResult Index()
        {
            pageVisitRepository.Add(new PageVisit
            {
                PageId = PageId.Home,
                Context = Request.Path,
                Carrier = Request.Headers.UserAgent,
                NetworkIdentifier = Request.Headers.Host
            }, "system");

            PageHeaderDisplayViewModel header = null;

            var headerEntity = pageHeaderRepository.FindOneBy(_entity => _entity.Id == PageId.Home);

            if (headerEntity != null)
            {
                header = new PageHeaderDisplayViewModel();

                header.Import(headerEntity);
            }

            var model = new Models.View.Frontend.HomeIndexViewModel
            {
                Header = header,
                BrandPromotions = brandPromotionRepository.GetAll().ToArray().Select(_entity =>
                {
                    var _model = new BrandPromotionDisplayViewModel();

                    _model.Import(_entity);

                    return _model;
                }).ToArray(),
                BrandSupportOffers = brandSupportOfferRepository.FindBy(_entity => _entity.Enabled).GroupBy(_grouping => _grouping.Brand).ToArray().Select(_grouping =>
                {
                    var _item = new KeyValuePair<BrandDisplayViewModel, BrandSupportOfferDisplayViewModel[]>(new BrandDisplayViewModel(), _grouping.Select(__entity =>
                    {
                        var __model = new BrandSupportOfferDisplayViewModel
                        {
                            Id = __entity.Id,
                            Title = __entity.Title,
                            Description = __entity.Description,
                            FeatureImage = new ImageDisplayViewModel()
                        };

                        __model.FeatureImage.Import(__entity.FeatureImage);

                        return __model;
                    }).ToArray());

                    _item.Key.Import(_grouping.Key);

                    return _item;
                }).ToArray(),
                Brands = brandRepository.GetAll().ToArray().Select(_entity =>
                {
                    var _model = new BrandDisplayViewModel();

                    _model.Import(_entity);

                    return _model;
                }).ToArray()
            };

            return View(model);
        }
    }
}
