using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Legal.Ner.DataAccess.Interfaces;
using Legal.Ner.Domain;
using Legal.Ner.Log;
using PagedList;

namespace Legal.Ner.Web.Controllers
{
    public class EntityBulkController : Controller
    {
        private readonly IEntityBulkData _entityBulkData;
        private readonly ILogger _logger;
        private const int PageSize = 10;

        public EntityBulkController(IEntityBulkData entityBulkData, ILogger logger)
        {
            _entityBulkData = entityBulkData;
            _logger = logger;
        }

        // GET: EntityBulk
        public ActionResult Index(int? page, string searchString, int fileKeyId)
        {
            ViewBag.SearchString = searchString;
            ViewBag.FileKeyId = fileKeyId;

            int pageNumber = page ?? 1;
            List<EntityBulk> entitiesBulk = _entityBulkData.Get(fileKeyId, searchString);
            IPagedList<EntityBulk> entitiesPaged = entitiesBulk.ToPagedList(pageNumber, PageSize);
            return View(entitiesPaged);
        }

        // GET: EntityBulk/Details/5
        public ActionResult Details(string eid, int fileKeyId)
        {
            EntityBulk entityBulk = _entityBulkData.GetByEid(eid, fileKeyId);
            if (entityBulk == null)
                return HttpNotFound();

            return View(entityBulk);
        }

        // GET: EntityBulk/Edit/5
        public ActionResult Edit(string eid, int fileKeyId)
        {
            ViewBag.FileKeyId = fileKeyId;

            EntityBulk entityBulk = _entityBulkData.GetByEid(eid, fileKeyId);
            if (entityBulk == null)
                return HttpNotFound();

            return View(entityBulk);
        }

        // POST: EntityBulk/Edit/5
        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            try
            {
                EntityBulk entityBulk = new EntityBulk
                {
                    FileKey = new FileKey
                    {
                        Id = Convert.ToInt32(collection["FileKey.Id"])
                    },
                    EntityName = collection["EntityName"],
                    Added = Convert.ToBoolean(collection["Added"].Split(',')[0]),
                    EntityType = collection["EntityType"],
                    Eid = collection["Eid"]
                };

                _entityBulkData.Update(entityBulk);

                return RedirectToAction("Index", new { fileKeyId = entityBulk .FileKey.Id });
            }

            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return View();
            }
        }

        // GET: EntityBulk/Delete/5
        public ActionResult Delete(string eid, int fileKeyId)
        {
            EntityBulk entityBulk =_entityBulkData.GetByEid(eid, fileKeyId);
            if (entityBulk == null)
            {
                return HttpNotFound();
            }

            return View(entityBulk);
        }

        // POST: EntityBulk/Delete/5
        [HttpPost]
        public ActionResult Delete(FormCollection collection)
        {
            try
            {
                EntityBulk entityBulk = new EntityBulk
                {
                    FileKey = new FileKey
                    {
                        Id = Convert.ToInt32(collection["FileKey.Id"])
                    },
                    EntityName = collection["EntityName"],
                    Added = Convert.ToBoolean(collection["Added"].Split(',')[0]),
                    EntityType = collection["EntityType"],
                    Eid = collection["Eid"]
                };

                ViewBag.ErrorMessage = string.Empty;

                if (entityBulk.Added)
                {
                    ViewBag.ErrorMessage = $"No se puede eliminar la entidad {entityBulk.EntityName}. Ya ha sido agregada a la ontología";
                    return View(entityBulk);
                }

                _entityBulkData.Delete(entityBulk);
                return RedirectToAction("Index", new { fileKeyId = entityBulk.FileKey.Id });
            }
            catch
            {
                return View();
            }
        }
    }
}
