using System;
using System.Web.Mvc;
using Legal.Ner.DataAccess.Interfaces;
using Legal.Ner.Domain.Semantic;
using Legal.Ner.Log;

namespace Legal.Ner.Web.Controllers
{
    public class SemanticController : Controller
    {
        private readonly ISparqlData _sparqlData;
        private readonly ILogger _logger;

        public SemanticController(ISparqlData sparqlData, ILogger logger)
        {
            _sparqlData = sparqlData;
            _logger = logger;
        }

        // GET: Semantic
        public ActionResult Index()
        {
            return View();
        }

        // POST: Semantic/Index
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(FormCollection collection)
        {
            try
            {
                string query = collection["Query"];
                if (!string.IsNullOrEmpty(query))
                {
                    string output = _sparqlData.Get(collection["Query"]);
                    QueryOutput queryOutput = new QueryOutput { Output = output };

                    return View("Details", queryOutput);
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
            return View();
        }

        // GET: Semantic/Details
        public ActionResult Details()
        {
            QueryOutput queryOutput = new QueryOutput { Output = string.Empty };
            return View(queryOutput);
        }
    }
}
