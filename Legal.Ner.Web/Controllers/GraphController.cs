using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Legal.Ner.DataAccess.Interfaces;
using Legal.Ner.Domain.Graph;
using Legal.Ner.Log;
using Legal.Ner.Web.Models;

namespace Legal.Ner.Web.Controllers
{
    public class GraphController : Controller
    {
        private readonly IGraphData _graphData;
        private readonly ILogger _logger;

        public GraphController(IGraphData graphData, ILogger logger)
        {
            _graphData = graphData;
            _logger = logger;
        }

        // GET: Graph/Index
        public ActionResult Index()
        {
            List<BaseSemanticGraph> individuals = new List<BaseSemanticGraph>();

            individuals.AddRange(_graphData.GetItems("DOCUMENTO_LEGAL"));
            individuals.AddRange(_graphData.GetItems("ORGANIZACION_ESTADO"));
            individuals.AddRange(_graphData.GetItems("PARTE_DOCUMENTO"));
            individuals.AddRange(_graphData.GetItems("PODER_PUBLICO"));

            var objectProperties = _graphData.GetItems("ObjectProperty").OrderBy(x => x.Label);

            individuals = individuals.OrderBy(x => x.Label).ToList();

            var individualsList = individuals.Select(x => new
            {
                Id = x.Namespace,
                Text = x.Label + "$" + x.Uri
            });

            var individualSelectList = new SelectList(individualsList, "Id", "Text");

            GraphRelationViewModel graphRelation = new GraphRelationViewModel
            {
                Sources = individualSelectList,
                Targets = individualSelectList,
                ObjectProperties = objectProperties
            };

            return View(graphRelation);
        }

        // POST: Graph/Index
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            try
            {
                _graphData.RelateNodes(collection["SelectedSource"], collection["SelectedTarget"], collection["SelectedObjectProperty"], collection["SourceLabel"], collection["TargetLabel"]);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return View();
            }
        }

        // GET: Graph/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Graph/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                BaseSemanticGraph graph = new BaseSemanticGraph
                {
                    Label = collection["Label"],
                    Uri = collection["Uri"],
                    Namespace = collection["Namespace"],
                    Comment = collection["Comment"]
                };

                _graphData.CreateNode(graph);

                return RedirectToAction("Create");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return View();
            }
        }

        // GET: Graph/Edit/5
        public ActionResult Edit()
        {
            return View();
        }

        // POST: Graph/Edit/5
        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            try
            {
                BaseSemanticGraph graph = new BaseSemanticGraph
                {
                    Label = collection["Label"],
                    Uri = collection["Uri"],
                    Filter = collection["Filter"],
                    Namespace = collection["Namespace"]
                };

                _graphData.UpdateUriNode(graph);

                return RedirectToAction("Edit");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return View();
            }
        }

        // GET: Graph/Delete/5
        public ActionResult Delete()
        {
            return View();
        }

        // POST: Graph/Delete/5
        [HttpPost]
        public ActionResult Delete(FormCollection collection)
        {
            try
            {
                BaseSemanticGraph graph = new BaseSemanticGraph
                {
                    Label = collection["Label"],
                    Namespace = collection["Namespace"]
                };

                _graphData.DeleteNode(graph);

                return RedirectToAction("Delete");
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return View();
            }
        }
    }
}
