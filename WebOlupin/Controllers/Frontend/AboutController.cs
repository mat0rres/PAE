using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Enumerations;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Controllers.Frontend
{
    public class AboutController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IEntityRepository<RichTextBlock, RichTextBlockKind> richTextBlockRepository;

        public AboutController(IEntityRepository<RichTextBlock, RichTextBlockKind> richTextBlockRepository)
        {
            this.richTextBlockRepository = richTextBlockRepository;
        }

        public Microsoft.AspNetCore.Mvc.IActionResult Index()
        {
            var model = new Models.View.Frontend.AboutIndexViewModel
            {
                MainTextBlock = new Models.View.RichTextBlockDisplayViewModel()
            };

            model.MainTextBlock.Import(richTextBlockRepository.Get(RichTextBlockKind.About));

            return View(model);
        }
    }
}
