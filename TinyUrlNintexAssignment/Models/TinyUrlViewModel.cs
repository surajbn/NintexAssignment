
namespace TinyUrlNintexAssignment.Models
{
    /// <summary>
    /// TinyUrls ViewModel which contains all the properties which are required for response.
    /// </summary>
    /// <param name="service">Pass in service object.</param>
    public class TinyUrlViewModel
    {
        public int Id { get; }

        public string OriginalUrl { get; }

        public string EncodedUrl { get; }

        /// <summary>
        /// TinyUrls ViewModel constructor which initializes all the properties required for the response.
        /// </summary>
        /// <param name="url">Tiny url object.</param>
        /// <param name="encodedUrl">Encoded url.</param>
        public TinyUrlViewModel(TinyUrl url, string encodedUrl)
        {
            if (url != null && !string.IsNullOrEmpty(encodedUrl))
            {
                this.Id = url.Id;
                this.OriginalUrl = url.OriginalUrl;
                this.EncodedUrl = encodedUrl;
            }
        }
    }
}
