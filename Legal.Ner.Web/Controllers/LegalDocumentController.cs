using System;
using System.Web.Mvc;
using Legal.Ner.DataAccess.Interfaces;
using Legal.Ner.Domain.Graph;
using Legal.Ner.Log;

namespace Legal.Ner.Web.Controllers
{
    public class LegalDocumentController : Controller
    {
        private readonly IGraphData _graphData;
        private readonly ILogger _logger;

        public LegalDocumentController(IGraphData graphData, ILogger logger)
        {
            _graphData = graphData;
            _logger = logger;
        }

        // GET: LegalDocument
        public ActionResult Index()
        {
            return View();
        }

        // POST: LegalDocument/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                LegalDocument legalDocument = new LegalDocument
                {
                    Label = collection["Label"],
                    SourceClasses = collection["SourceClasses"],
                    Uri = collection["Uri"],
                    HasBibliographicData = collection["HasBibliographicData"],
                    HasNumber = Convert.ToInt32(collection["HasNumber"]),
                    HasPublicationDate = string.IsNullOrEmpty(collection["HasPublicationDate"]) ? DateTime.MinValue : Convert.ToDateTime(collection["HasPublicationDate"]),
                    HasSubject = collection["HasSubject"],
                    HasTitle = collection["HasTitle"]
                };

                _graphData.CreateNode(legalDocument);
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
            return RedirectToAction("Index");
        }
    }
}
