using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using Xunit.Sdk;
using TinyUrlNintexAssignment.Services;
using TinyUrlNintexAssignment.Data;
using Microsoft.Extensions.Caching.Memory;
using TinyUrlNintexAssignment.Tests.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TinyUrlNintexAssignment.Controllers;

namespace TinyUrlNintexAssignment.Tests.Services
{
    public class TinyUrlServiceTest
    {
        public Mock<ITinyUrlService> _service;
        private Mock<IDbContextOptions> context;
        private Mock<IMemoryCache> memCached;
        
        TinyUrlRepositoryMock _mockData;

        public TinyUrlServiceTest()
        {
            Initialize();
        }
        private void Initialize()
        {
            context = new Mock<IDbContextOptions>() ;
            memCached = new Mock<IMemoryCache>();
            _mockData = new TinyUrlRepositoryMock();
            _service = new Mock<ITinyUrlService>();
            _service.Setup(p => p.GetAll()).Returns(_mockData.GetListOfUrls());           

        }

      
    }
}
