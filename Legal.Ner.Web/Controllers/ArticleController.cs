using System;
using System.Web.Mvc;
using Legal.Ner.DataAccess.Interfaces;
using Legal.Ner.Domain.Graph;
using Legal.Ner.Log;

namespace Legal.Ner.Web.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IGraphData _graphData;
        private readonly ILogger _logger;

        public ArticleController(IGraphData graphData, ILogger logger)
        {
            _graphData = graphData;
            _logger = logger;
        }

        // GET: Article
        public ActionResult Index()
        {
            return View();
        }

        // POST: Article/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Article article = new Article
                {
                    Label = collection["Label"],
                    Namespace = collection["Namespace"],
                    Uri = collection["Uri"],
                    HasDescription = collection["HasDescription"],
                    HasIncese = collection["HasInceseValue"].Split('%'),
                    HasLiteral = collection["HasLiteralValue"].Split('%'),
                    HasNumeral = collection["HasNumeralValue"].Split('%'),
                    HasNumber = Convert.ToInt32(collection["HasNumber"]),
                    HasParagraph = collection["HasParagraph"],
                    HasSection = collection["HasSection"],
                    Comment = collection["Comment"],
                    HasSubject = collection["HasSubject"]
                };

                _graphData.CreateNode(article);
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
            return RedirectToAction("Index");
        }
    }
}
