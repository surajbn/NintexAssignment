using System;
using System.Collections.Generic;
using System.Text;
using TinyUrlNintexAssignment.Models;
using Xunit;

namespace TinyUrlNintexAssignment.Tests.Models
{
    public class TinyUrlViewModelTests
    {
        private TinyUrlRepositoryMock mockData;
        private TinyUrl tinyUrlModel;
        private string hash;
        public TinyUrlViewModelTests()
        {
            mockData = new TinyUrlRepositoryMock();
            tinyUrlModel = mockData.GetTinyUrl();
            hash = mockData.hash;
        }
       
        [Fact]
        public void Constructor()
        {
            TinyUrlViewModel tinyUrlVM = new TinyUrlViewModel(tinyUrlModel, hash);
            Assert.Equal(tinyUrlVM.Id, tinyUrlModel.Id);
            Assert.Equal(tinyUrlVM.OriginalUrl, tinyUrlModel.OriginalUrl);
            Assert.Equal(tinyUrlVM.EncodedUrl, hash);
        }

        [Fact]
        public void ConstructorNullValues()
        {
            TinyUrlViewModel tinyUrlVM = new TinyUrlViewModel(null, "");
            Assert.Equal(0, tinyUrlVM.Id);
            Assert.Null(tinyUrlVM.OriginalUrl);
            Assert.Null(tinyUrlVM.EncodedUrl);
        }
    }
}
