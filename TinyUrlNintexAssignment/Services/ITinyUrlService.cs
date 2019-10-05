using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyUrlNintexAssignment.Models;

namespace TinyUrlNintexAssignment.Services
{
    public interface ITinyUrlService
    {
        /// <summary>
        /// Gets TinyUrl object by its hash.
        /// </summary>
        /// <param name="encodedHash">encoded hash of the url.</param>
        ///<returns>Returns the tiny url object.</returns>
        TinyUrl GetByPath(string path);

        /// <summary>
        /// Saves the new url into the database, if already present then returns its id.
        /// </summary>
        /// <param name="shortUrl">TinyUrl object which needs to be inserted into the DB.</param>
        ///<returns>Returns id of the newly added url object.</returns>
        int Save(TinyUrl shortUrl);

        /// <summary>
        /// Gets all urls as a list of tinyUrl objects.
        /// </summary>
        List<TinyUrl> GetAll();
    }
}
