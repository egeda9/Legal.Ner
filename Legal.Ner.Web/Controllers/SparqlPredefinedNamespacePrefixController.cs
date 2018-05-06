using System.Collections.Generic;
using System.Web.Mvc;
using Legal.Ner.DataAccess.Interfaces;
using Legal.Ner.Domain.Metadata;
using Legal.Ner.Log;
using PagedList;

namespace Legal.Ner.Web.Controllers
{
    public class SparqlPredefinedNamespacePrefixController : Controller
    {
        private readonly ISparqlPredifinedNamespacesPrefixesData _predifinedNamespacesPrefixesData;
        private const int PageSize = 10;

        public SparqlPredefinedNamespacePrefixController(ILogger logger, ISparqlPredifinedNamespacesPrefixesData predifinedNamespacesPrefixesData)
        {
            _predifinedNamespacesPrefixesData = predifinedNamespacesPrefixesData;
        }

        // GET: SparqlPredefinedNamespacePrefix
        public ActionResult Index(int? page)
        {
            return GetPrefixex(page);
        }

        private ActionResult GetPrefixex(int? page)
        {
            int pageNumber = page ?? 1;
            IList<SparqlPredefinedNamespacePrefix> prefixex = _predifinedNamespacesPrefixesData.Get();
            IPagedList<SparqlPredefinedNamespacePrefix> prefixesPaged = prefixex.ToPagedList(pageNumber, PageSize);
            return View(prefixesPaged);
        }
    }
}
