using System;
using System.Collections.Generic;
using System.Linq;

namespace Alura.LeilaoOnline.Core
{
    public class Leilao
    {
        public enum EstadoLeilao
        {
            LeilaoAntesDoPregao,
            LeilaoEmAndamento,
            LeilaoFinalizado
        }

        private IModalidadeAvaliacao _modalidade;

        private Interessada _ultimoLance;

        private IList<Lance> _lances;
        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }

        public EstadoLeilao Estado { get; private set; }

        public Lance Ganhador { get; private set; }


        public Leilao(string peca, IModalidadeAvaliacao modalidade)
        {
            Peca = peca;
            _lances = new List<Lance>();
            Estado = EstadoLeilao.LeilaoAntesDoPregao;
            _modalidade = modalidade;

        }

        public void RecebeLance(Interessada cliente, double valor)
        {
            if (cliente != _ultimoLance && Estado == EstadoLeilao.LeilaoEmAndamento)
            {
                _lances.Add(new Lance(cliente, valor));
                _ultimoLance = cliente;
            }

        }

        public void IniciaPregao()
        {
            Estado = EstadoLeilao.LeilaoEmAndamento;
        }

        public void TerminaPregao()
        {
            if (Estado != EstadoLeilao.LeilaoEmAndamento)
                throw new InvalidOperationException("Deve iniciar o pregão");

            Ganhador = _modalidade.Avalia(this);

            Estado = EstadoLeilao.LeilaoFinalizado;
        }
    }
}
