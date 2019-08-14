
using Xunit;
using Alura.LeilaoOnline.Core;
using System.Linq;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoRecebeOferta
    {
        [Theory]
        [InlineData(4, new double[] { 800, 900, 1000, 1200 })]
        [InlineData(2, new double[] {800, 900})]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(int qtdEsperada, double[] ofertas)
        {
            //Arrange
            var leilao = new Leilao("Van Gogh");

            var fulano = new Interessada("Fulano", leilao);

            leilao.IniciaPregao();

            foreach (var item in ofertas)
            {
                leilao.RecebeLance(new Interessada($"clie{ item.ToString()}", leilao), item);
            }

            leilao.TerminaPregao();

            //Act  
            leilao.RecebeLance(fulano, 4000);

            //Assert
            var valorObtido = leilao.Lances.Count();
            Assert.Equal(qtdEsperada, valorObtido);
        }

        [Fact]
        public void NaoPermiteLanceConsecutivoMesmoCliente()
        {
            //Arrange
            var leilao = new Leilao("Van Gogh");
            leilao.IniciaPregao();

            var fulano = new Interessada("Fulano", leilao);

            leilao.RecebeLance(fulano, 4000);
            leilao.RecebeLance(fulano, 5000);

            leilao.TerminaPregao();

            
            //Act
            var qtd = leilao.Lances.Count();

            //Assert
            Assert.Equal(1, qtd);
        }
    }
}
