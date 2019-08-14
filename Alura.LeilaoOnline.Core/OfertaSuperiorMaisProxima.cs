using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Alura.LeilaoOnline.Core
{
    public class OfertaSuperiorMaisProxima : IModalidadeAvaliacao
    {
        private double ValorDestino;

        public OfertaSuperiorMaisProxima(double valorDestino)
        {
            ValorDestino = valorDestino;
        }

        public Lance Avalia(Leilao leilao)
        {
            return leilao.Lances
                .DefaultIfEmpty(new Lance(null, 0))
                .Where(l =>l.Valor > ValorDestino)
                .OrderBy(l => l.Valor)
                .LastOrDefault();
        }
    }
}
