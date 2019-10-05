using System;
using Microsoft.AspNetCore.Mvc;
using TinyUrlNintexAssignment.Services;
using TinyUrlNintexAssignment.Models;
using TinyUrlNintexAssignment.Operations;
using System.Collections.Generic;

namespace TinyUrlNintexAssignment.Controllers
{
    /// <summary>
    /// TinyUrls Controller holds the actions for performing url services.
    /// </summary>
    public class TinyUrlsController : Controller
    {

        private readonly ITinyUrlService _service;

        /// <summary>
        /// TinyUrls Controller's constructor is used to initialize the service call.
        /// </summary>
        /// <param name="service">Pass in service object.</param>
        /// TODO: Logging and caching can be initialized here.
        public TinyUrlsController(ITinyUrlService service)
        {
            _service = service;
            
        }

        /// <summary>
        /// Create action is used to create tiny urls by accepting original urls.
        /// </summary>
        /// <param name="OriginalUrl">Pass in Original url.</param>
        [HttpPost]
        public JsonResult Create(string OriginalUrl)
        {
            try
            {
                if (TinyUrlHelper.ValidateUrl(OriginalUrl))
                {
                    var tinyUrl = new TinyUrl()
                    {
                        OriginalUrl = OriginalUrl
                    };

                    TryValidateModel(tinyUrl);
                    if (ModelState.IsValid)
                    {
                        tinyUrl.Id = _service.Save(tinyUrl);

                        var JsonResults = new TinyUrlViewModel(tinyUrl, TinyUrlHelper.GetFullUrl(tinyUrl.Id, Request));
                        return Json(JsonResults);

                    }
                }

                throw new UriFormatException("URL format is invalid!");
            }

            catch(Exception ex)
            {
                Response.StatusCode = 500;
                return Json("Exception was thrown - "+ex.Message);
            }
        }

        /// <summary>
        /// ViewOriginalUrl action is used to view the original Url.
        /// </summary>
        /// <param name="encodedHash">Pass in encoded hash.</param>
        public JsonResult ViewOriginalUrl(string encodedHash)
        {
            if (string.IsNullOrEmpty(encodedHash) || string.IsNullOrWhiteSpace(encodedHash))
            {
                throw new ArgumentNullException("encodedUrl", "Encoded URL cannot be empty!");
            }

            var tinyUrlObject = _service.GetByPath(encodedHash);
            if (tinyUrlObject == null)
            {
                Response.StatusCode = 404;
                return Json("Cannot find the URL, Please check the Encoded URL and try again!");
            }

           
            return Json(tinyUrlObject);
        }

        /// <summary>
        /// RedirectTo action is used to redirect user to original url provided by the user.
        /// </summary>
        /// <param name="encodedHash">Pass in encoded hash.</param>
        public IActionResult RedirectTo(string encodedHash)
        {
            if (encodedHash == null)
            {
                return NotFound();
            }

            var tinyUrlObject = _service.GetByPath(encodedHash);
            if (tinyUrlObject == null)
            {
                return NotFound();
            }

            return Redirect(tinyUrlObject.OriginalUrl);
        }

        /// <summary>
        /// Gets all the url objects stored .
        /// </summary>
        public JsonResult GetAll()
        {
            return Json(_service.GetAll());
        }

    }
}