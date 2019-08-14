using System;
using Xunit;
using Alura.LeilaoOnline.Core;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
    {
        [Theory]
        [InlineData(1200, 1250, new double[] { 800, 1150, 1400, 1250 })]
        public void RetornaValorSuperiorMaisProximoDadoLeilaoNessaModalidade(
            double valorDestino, double valorEsperado, double[] ofertas)
        {
            //arrange
            IModalidadeAvaliacao modalidade = new OfertaSuperiorMaisProxima(valorDestino);
            var leilao = new Leilao("Van", modalidade);
            var fulano = new Interessada("fulano", leilao);
            var maria = new Interessada("fulano", leilao);

            for (int i = 0; i < ofertas.Length; i++)
            {
                if (i % 2 == 0)
                    leilao.RecebeLance(fulano, ofertas[1]);
                else
                    leilao.RecebeLance(maria, ofertas[i]);
                 
            }

            //act
            leilao.TerminaPregao();

            //Assert
            Assert.Equal(valorEsperado, leilao.Ganhador.Valor);

        }

        [Fact]
        public void LancaInvalidOperationExceptionDadoPregaoNaoIniciado()
        {
            //Arrange
            var leilao = new Leilao("Van Gogh");

            //Act
            Action act = (() => leilao.TerminaPregao());

            var exception = Assert.Throws<InvalidOperationException>(act);
            var mensagemEsperada = "Deve iniciar o pregão";
            Assert.Equal(mensagemEsperada, exception.Message);
        }
        [Fact]
        public void RetornaZeroDadoLeilaoSemLance()
        {
            //Arrange
            var leilao = new Leilao("Van Gogh");
            leilao.IniciaPregao();

            //Act
            leilao.TerminaPregao();

            //Assert
            var valorEsperado = 0;

            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }


        [Theory]
        [InlineData(1000, new double[] { 800, 900, 1000, 990 })]
        [InlineData(1200, new double[] { 800, 900, 1000, 1200 })]
        [InlineData(800, new double[] { 800 })]
        public void RetornaMaiorValorComPeloMenosUmLance(double valorEsperado, double[] ofertas)
        {
            //Arrange
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessada("Fulano", leilao);

            leilao.IniciaPregao();

            foreach (var item in ofertas)
            {
                leilao.RecebeLance(new Interessada($"clie{item.ToString()}", leilao), item);
            }

            //Act
            leilao.TerminaPregao();

            //Assert
            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
        }


    }

}
