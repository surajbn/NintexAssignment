using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyUrlNintexAssignment.Operations
{
    /// <summary>
    /// Unitilities Class which holds the operations for url encoding, decoding and formatting.
    /// </summary>
    public class TinyUrlHelper
    {
        private const string Alphabet = "23456789bcdfghjkmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ-_";
        private static readonly int Base = Alphabet.Length;
        //Counter which is set to 7 digit by default. This will be substracted by the id of the url object. This will allow us to have 3 trillion combinations of hash's.
        private static readonly int Counter = 10000000;

        /// <summary>
        /// B62 encode which is used for shortening of url.
        /// </summary>
        /// <param name="urlId">id of the tiny url object, which is used for decremental purpose.</param>
        ///<returns>Returns encoded hash as string for the tiny url object.</returns>
        public static string Encode(int urlId)
        {
            var hash = new StringBuilder();
            urlId = Counter - urlId;
            while (urlId > 0)
            {
                hash.Insert(0, Alphabet.ElementAt(urlId % Base));
                urlId = urlId / Base;
            }
            return hash.ToString();
        }

        /// <summary>
        /// B62 decode which is used for decoding the hash.
        /// </summary>
        /// <param name="encodedHash">id of the tiny url object, which is used for decremental purpose.</param>
        ///<returns>Returns id for the tiny url object.</returns>
        public static int Decode(string encodedHash)
        {
            var urlId = 0;
            for (var i = 0; i < encodedHash.Length; i++)
            {
                urlId = urlId * Base + Alphabet.IndexOf(encodedHash.ElementAt(i));
            }
            urlId = Counter - urlId;
            return urlId;
        }

        /// <summary>
        /// Used to get the full url for the encoded hash.
        /// </summary>
        /// <param name="id">id of the tiny url object, which is used for decremental purpose.</param>
        /// <param name="request">Current request object.</param>
        ///<returns>Returns encoded URL for the tiny url object.</returns>
        public static string GetFullUrl(int id, HttpRequest request)
        {
            string encodedURL = TinyUrlHelper.Encode(id);
            encodedURL = $"{request.Scheme}://{request.Host}/{encodedURL}";
            return encodedURL;
        }

        /// <summary>
        /// Used to validate the original url entered by the user.
        /// </summary>
        /// <param name="url">Original url.</param>
        ///<returns>Returns bool value whether the url is valid or not.</returns>
        public static bool ValidateUrl(string url)
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out Uri uri) || null == uri)
                return false;

            return true;
        }
    }

}
