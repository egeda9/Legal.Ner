using System;
using System.Web.Mvc;
using Legal.Ner.DataAccess.Interfaces;
using Legal.Ner.Domain.Graph;
using Legal.Ner.Log;

namespace Legal.Ner.Web.Controllers
{
    public class LegislativeActController : Controller
    {
        private readonly IGraphData _graphData;
        private readonly ILogger _logger;

        public LegislativeActController(IGraphData graphData, ILogger logger)
        {
            _graphData = graphData;
            _logger = logger;
        }

        // GET: LegislativeAct
        public ActionResult Index()
        {
            return View();
        }

        // POST: LegislativeAct/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                LegislativeAct legislativeAct = new LegislativeAct
                {
                    Label = collection["Label"],
                    SourceClasses = collection["SourceClasses"],
                    Uri = collection["Uri"],
                    HasApproval = collection["HasApproval"],
                    HasBibliographicData = collection["HasBibliographicData"],
                    HasBook = collection["HasBook"],
                    HasDebate = collection["HasDebate"],
                    HasDiscussion = collection["HasDiscussion"],
                    HasExhibitionReasons = collection["HasExhibitionReasons"],
                    HasHypothesis = collection["HasHypothesis"],
                    HasInitiative = collection["HasInitiative"],
                    HasMandate = collection["HasMandate"],
                    HasNumber = Convert.ToInt32(collection["HasNumber"]),
                    HasPublicationDate = string.IsNullOrEmpty(collection["HasPublicationDate"]) ? DateTime.MinValue : Convert.ToDateTime(collection["HasPublicationDate"]),
                    HasSaction = collection["HasSaction"],
                    HasSubject = collection["HasSubject"],
                    HasTitle = collection["HasTitle"]
                };

                _graphData.CreateNode(legislativeAct);
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
            return RedirectToAction("Index");
        }
    }
}
