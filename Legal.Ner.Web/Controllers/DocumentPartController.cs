using System;
using System.Web.Mvc;
using Legal.Ner.DataAccess.Interfaces;
using Legal.Ner.Domain.Graph;
using Legal.Ner.Log;

namespace Legal.Ner.Web.Controllers
{
    public class DocumentPartController : Controller
    {
        private readonly IGraphData _graphData;
        private readonly ILogger _logger;

        public DocumentPartController(IGraphData graphData, ILogger logger)
        {
            _graphData = graphData;
            _logger = logger;
        }

        // GET: DocumentPart
        public ActionResult Index()
        {
            return View();
        }

        // POST: DocumentPart/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                DocumentPart documentPart = new DocumentPart
                {
                    Label = collection["Label"],
                    Namespace = collection["Namespace"],
                    Uri = collection["Uri"],
                    HasNumber = Convert.ToInt32(collection["HasNumber"]),
                    HasDescription = collection["HasDescription"],
                    Comment = collection["Comment"]
                };

                _graphData.CreateNode(documentPart);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
            return RedirectToAction("Index");
        }
    }
}
