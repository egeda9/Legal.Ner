using System;
using System.Web.Mvc;
using Legal.Ner.DataAccess.Interfaces;
using Legal.Ner.Domain.Graph;
using Legal.Ner.Log;

namespace Legal.Ner.Web.Controllers
{
    public class JurisprudenceController : Controller
    {
        private readonly IGraphData _graphData;
        private readonly ILogger _logger;

        public JurisprudenceController(IGraphData graphData, ILogger logger)
        {
            _graphData = graphData;
            _logger = logger;
        }

        // GET: Jurisprudence
        public ActionResult Index()
        {
            return View();
        }

        // POST: Jurisprudence/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Jurisprudence jurisprudence = new Jurisprudence
                {
                    Label = collection["Label"],
                    Namespace = collection["Namespace"],
                    Uri = collection["Uri"],
                    HasNumber = Convert.ToInt32(collection["HasNumber"]),
                    HasAnalyzedLegalAspect = collection["HasAnalyzedLegalAspect"],
                    HasBibliographicData = collection["HasBibliographicData"],
                    HasDispatchName = collection["HasDispatchName"],
                    HasExplanationThesis = collection["HasExplanationThesis"],
                    HasExtract = collection["HasExtract"],
                    HasLegalProblem = collection["HasLegalProblem"],
                    HasMagistrateSpeakerName = collection["HasMagistrateSpeakerName"],
                    HasMethod = collection["HasMethod"],
                    HasPart = collection["HasPart"],
                    HasPublicationDate = string.IsNullOrEmpty(collection["HasPublicationDate"]) ? DateTime.MinValue : Convert.ToDateTime(collection["HasPublicationDate"]),
                    HasSubject = collection["HasSubject"],
                    HasThesis = collection["HasThesis"],
                    HasTitle = collection["HasTitle"],
                    HasVoteClarification = collection["HasVoteClarification"],
                    HasVoteSalvage = collection["HasVoteSalvage"],
                    Comment = collection["Comment"]
                };

                _graphData.CreateNode(jurisprudence);
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
            return RedirectToAction("Index");
        }
    }
}
