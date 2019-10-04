using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyUrlNintexAssignment.Data;
using TinyUrlNintexAssignment.Models;
using TinyUrlNintexAssignment.Operations;

namespace TinyUrlNintexAssignment.Services
{
    public class TinyUrlService: ITinyUrlService
    {
        private readonly TinyUrlContext _context;

        public TinyUrlService(TinyUrlContext context)
        {
            _context = context;
        }

        public TinyUrl GetById(int id)
        {
            return _context.TinyUrls.Find(id);
        }

        public TinyUrl GetByPath(string path)
        {
            return _context.TinyUrls.Find(TinyUrlHelper.Decode((path)));
        }

        public TinyUrl GetByOriginalUrl(string originalUrl)
        {
            foreach (var shortUrl in _context.TinyUrls)
            {
                if (shortUrl.OriginalUrl == originalUrl)
                {
                    return shortUrl;
                }
            }

            return null;
        }

        public int Save(TinyUrl shortUrl)
        {
            _context.TinyUrls.Add(shortUrl);
            _context.SaveChanges();

            return shortUrl.Id;
        }
    }
}
