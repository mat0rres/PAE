using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Enumerations;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View;
using System.Linq;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Controllers.Frontend
{
    public class AboutController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IEntityRepository<RichTextBlock, RichTextBlockKind> richTextBlockRepository;
        private readonly IEntityRepository<Representative, System.Guid> representativeRepository;

        public AboutController(IEntityRepository<Representative, System.Guid> representativeRepository, IEntityRepository<RichTextBlock, RichTextBlockKind> richTextBlockRepository)
        {
            this.richTextBlockRepository = richTextBlockRepository;
            this.representativeRepository = representativeRepository;
        }

        public Microsoft.AspNetCore.Mvc.IActionResult Index()
        {
            var model = new Models.View.Frontend.AboutIndexViewModel
            {
                MainTextBlock = new Models.View.RichTextBlockDisplayViewModel(),
                Representatives = representativeRepository.FindBy(entity => entity.Enabled).OrderBy(entity => entity.Priority).ToArray().Select(entity =>
                {
                    var _model = new RepresentativeDisplayViewModel();

                    _model.Import(entity);

                    return _model;
                }).ToArray()
            };

            model.MainTextBlock.Import(richTextBlockRepository.Get(RichTextBlockKind.About));

            return View(model);
        }
    }
}
