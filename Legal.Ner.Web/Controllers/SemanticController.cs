using System.Web.Mvc;
using Legal.Ner.DataAccess.Interfaces;

namespace Legal.Ner.Web.Controllers
{
    public class SemanticController : Controller
    {
        private readonly IGraphData _graphData;
        private readonly ISparqlData _sparqlData;

        public SemanticController(IGraphData graphData, ISparqlData sparqlData)
        {
            _graphData = graphData;
            _sparqlData = sparqlData;
        }

        // GET: Semantic
        public ActionResult Index()
        {
            // _sparqlData.Get(items[0]);
            //IList<BaseSemanticGraph> items = _graphData.GetItems("ACTO_REFORMATORIO_DE_LA_CONSTITUCION_POLITICA").ToList();
            return View();
        }

        // GET: Semantic/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Semantic/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Semantic/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Semantic/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Semantic/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Semantic/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Semantic/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
