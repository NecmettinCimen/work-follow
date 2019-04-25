using OdevTakip.Entities;
using OdevTakip.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace OdevTakip.Test
{
    public class GrupServiceTest
    {
        readonly GrupService _grupService;

        public GrupServiceTest()
        {
            _grupService = new GrupService(new GenericRepository());
        }

        [Fact]
        public void Test()
        {
            bool result = _grupService.Insert(new Grup { ad = "Test", aciklama = "Test", Olusturankisi = 1, yoneticiid = 1 });

            Assert.True(result);

            List<Grup> grups = _grupService.Select(new Grup { Olusturankisi = 1 });

            Assert.NotEmpty(grups);

            Grup grup = _grupService.First(new Grup { Id = 1 });

            Assert.NotNull(grup);


            
        }
    }
}
