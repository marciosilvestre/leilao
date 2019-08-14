using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Alura.LeilaoOnline.Core;

namespace Alura.LeilaoOnline.Tests
{
    public class LanceCtor
    {
        [Fact]
        public void LancaArgumentExceptionDadoValoNegativo()
        {
            //arrange
            var valorNegativo = -1;

            //Assert

            Assert.Throws<ArgumentException>(
                //act
                () => new Lance(null, valorNegativo)
            );
        }
    }
}
