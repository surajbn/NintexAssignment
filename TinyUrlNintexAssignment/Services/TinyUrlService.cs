using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using TinyUrlNintexAssignment.Data;
using TinyUrlNintexAssignment.Models;
using TinyUrlNintexAssignment.Operations;

namespace TinyUrlNintexAssignment.Services
{
    /// <summary>
    /// This class implements the ITinyUrlService interface to process repository CRUD operations.
    /// </summary>
    public class TinyUrlService: ITinyUrlService
    {
        private readonly TinyUrlContext _context;
        private readonly IMemoryCache _MemoryCache;
        private const string _cacheKey = "nintex-tiny-url";

        /// <summary>
        /// Holds the list of all urls created so far.
        /// </summary>
        public List<TinyUrl> AllTinyUrls = new List<TinyUrl>();

        /// <summary>
        /// TinyUrlService Constructor initializes current context and memory cache object.
        /// </summary>
        public TinyUrlService(TinyUrlContext context, IMemoryCache memCache)
        {
            _context = context;
            _MemoryCache = memCache;
            GetOrSetCache();
        }

        /// <summary>
        /// The method caches all the url objects.
        /// </summary>
        ///  <param name="refreshCache">Refreshes the cache based on the value - true/false.</param>
        public void GetOrSetCache(bool refreshCache = false)
        {
            if (!_MemoryCache.TryGetValue(_cacheKey, out AllTinyUrls) || refreshCache)
            {
                AllTinyUrls = _context.TinyUrls.ToList();
                _MemoryCache.Set(_cacheKey, AllTinyUrls,
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));

            }
        }

        /// <summary>
        /// Gets all urls as a list of tinyUrl objects.
        /// </summary>
        public List<TinyUrl> GetAll()
        {
            return AllTinyUrls;
        }

        /// <summary>
        /// Gets TinyUrl object by its hash.
        /// </summary>
        /// <param name="encodedHash">encoded hash of the url.</param>
        ///<returns>Returns the tiny url object.</returns>
        public TinyUrl GetByPath(string encodedHash)
        {
            return _context.TinyUrls.Find(TinyUrlHelper.Decode((encodedHash)));
        }

        /// <summary>
        /// Saves the new url into the database, if already present then returns its id.
        /// </summary>
        /// <param name="shortUrl">TinyUrl object which needs to be inserted into the DB.</param>
        ///<returns>Returns id of the newly added url object.</returns>
        public int Save(TinyUrl shortUrl)
        {
            var CheckForDuplicates = AllTinyUrls.FirstOrDefault(x => x.OriginalUrl == shortUrl.OriginalUrl);
            if (CheckForDuplicates != null)
            {
                return CheckForDuplicates.Id;
            }
            else
            {
                _context.TinyUrls.Add(shortUrl);
                _context.SaveChanges();
                GetOrSetCache(true);

                return shortUrl.Id;
            }
            
        }
    }
}
