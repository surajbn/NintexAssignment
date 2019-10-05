using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyUrlNintexAssignment.Models;

namespace TinyUrlNintexAssignment.Tests.Models
{
    public class TinyUrlRepositoryMock
    {
        private List<TinyUrl> TinyUrlsDto;
        public string hash = "FtG4S";

        public TinyUrlRepositoryMock()
        {
            BuildTinyUrlObjects();
        }
        public TinyUrl GetTinyUrl()
        {
            return TinyUrlsDto.First();
        }

        public List<TinyUrl> GetListOfUrls()
        {
            return TinyUrlsDto;
        }

        public void BuildTinyUrlObjects()
        {
            TinyUrlsDto = new List<TinyUrl>()
            {
                new TinyUrl()
                {
                    Id= 1,
                    OriginalUrl="https://google.com"
                },
                new TinyUrl()
                {
                    Id= 2,
                    OriginalUrl="https://gmail.com"
                },
                new TinyUrl()
                {
                    Id= 3,
                    OriginalUrl="https://facebook.com"
                },
            };
        }
    }
}
