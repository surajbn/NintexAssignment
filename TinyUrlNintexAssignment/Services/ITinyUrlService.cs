using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyUrlNintexAssignment.Models;

namespace TinyUrlNintexAssignment.Services
{
    public interface ITinyUrlService
    {
        TinyUrl GetById(int id);

        TinyUrl GetByPath(string path);

        TinyUrl GetByOriginalUrl(string originalUrl);

        int Save(TinyUrl shortUrl);
    }
}
