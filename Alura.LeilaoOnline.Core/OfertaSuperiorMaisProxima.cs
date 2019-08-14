using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Alura.LeilaoOnline.Core
{
    public class OfertaSuperiorMaisProxima : IModalidadeAvaliacao
    {
        public Lance Avalia(Leilao leilao)
        {
            return leilao.Lances
                .DefaultIfEmpty(new Lance(null, 0))
                .Where(l =>l.Valor > leilao.)
                .OrderBy(l => l.Valor)
                .LastOrDefault();
        }
    }
}
