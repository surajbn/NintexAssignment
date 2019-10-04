using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TinyUrlNintexAssignment.Services;
using TinyUrlNintexAssignment.Models;
using TinyUrlNintexAssignment.Operations;

namespace TinyUrlNintexAssignment.Controllers
{
    public class TinyUrlsController : Controller
    {

        private readonly ITinyUrlService _service;

        public TinyUrlsController(ITinyUrlService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            //TODO
            return RedirectToAction(actionName: nameof(Create));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string OriginalUrl)
        {
            var TinyUrl = new TinyUrl()
            {
                OriginalUrl = OriginalUrl
            };

            TryValidateModel(TinyUrl);
            if (ModelState.IsValid)
            {
                _service.Save(TinyUrl);
                return RedirectToAction(actionName: nameof(ViewTinyUrl), routeValues: new { id = TinyUrl.Id });
            }
            return View(TinyUrl);
        }

        public IActionResult ViewTinyUrl(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var shortUrl = _service.GetById(id.Value);
            if (shortUrl == null)
            {
                return NotFound();
            }

            ViewData["Path"] = TinyUrlHelper.Encode(shortUrl.Id);

            return View(shortUrl);
        }

        [HttpGet("/ShortUrls/RedirectTo/{path:required}", Name = "ShortUrls_RedirectTo")]
        public IActionResult RedirectTo(string path)
        {
            if (path == null)
            {
                return NotFound();
            }

            var shortUrl = _service.GetByPath(path);
            if (shortUrl == null)
            {
                return NotFound();
            }

            return Redirect(shortUrl.OriginalUrl);
        }
    }
}