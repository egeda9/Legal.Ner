using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Legal.Ner.DataAccess.Interfaces;
using Legal.Ner.Domain;
using PagedList;
using Legal.Ner.Business.Interfaces;
using Legal.Ner.Log;

namespace Legal.Ner.Web.Controllers
{
    public class FileKeyController : Controller
    {
        private readonly IFileKeyData _fileKeyData;
        private readonly IProcessFile _processFile;
        private readonly ILogger _logger;
        private const int PageSize = 10;

        public FileKeyController(IFileKeyData fileKeyData, IProcessFile processFile, ILogger logger)
        {
            _fileKeyData = fileKeyData;
            _processFile = processFile;
            _logger = logger;
        }

        // GET: FileKey
        public ActionResult Index(int? page, string searchString)
        {
            return GetDocuments(page, searchString);
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file, int? page, string searchString)
        {
            try
            {
                if (file != null && file.ContentLength > 0 && Path.GetExtension(file.FileName) == ".kaf")
                {
                    string fileName = Path.GetFileName(file.FileName);
                    _processFile.MapData(file.InputStream, fileName);
                }
                return GetDocuments(page, searchString);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
            }
            return View();
        }

        // GET: FileKey/Details/5
        public ActionResult Details(int id)
        {
            FileKey fileKey = _fileKeyData.Get(id);
            if (fileKey == null)
                return HttpNotFound();

            return View(fileKey);
        }

        // GET: FileKey/Edit/5
        public ActionResult Edit(int id)
        {
            FileKey fileKey = _fileKeyData.Get(id);
            if (fileKey == null)
                return HttpNotFound();

            return View(fileKey);
        }

        // POST: FileKey/Edit/5
        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            try
            {
                FileKey fileKey = new FileKey
                {
                    Id = Convert.ToInt32(collection["Id"]),
                    DocumentTitle = collection["DocumentTitle"],
                    ReleaseDate = Convert.ToDateTime(collection["ReleaseDate"]),
                    Number = Convert.ToInt32(collection["Number"]),
                    Description = collection["Description"]
                };

                _fileKeyData.Update(fileKey);

                return RedirectToAction("Index");
            }

            catch (Exception ex)
            {
                _logger.Error(ex.Message, ex);
                return View();
            }
        }

        // GET: FileKey/Delete/5
        public ActionResult Delete(int id)
        {
            FileKey fileKey = _fileKeyData.Get(id);
            if (fileKey == null)
                return HttpNotFound();

            return View(fileKey);
        }

        // POST: FileKey/Delete/5
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

        private ActionResult GetDocuments(int? page, string searchString)
        {
            ViewBag.SearchString = searchString;
            int pageNumber = page ?? 1;
            List<FileKey> files = _fileKeyData.Get(searchString);
            IPagedList<FileKey> filesPaged = files.ToPagedList(pageNumber, PageSize);
            return View(filesPaged);
        }
    }
}
