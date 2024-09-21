using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XadrezLogica
{
    public class EnPassant : Movimento
    {
        public override TiposMovimentos Tipo => TiposMovimentos.EnPassant;
        public override Posicao DePos { get; }
        public override Posicao ParaPos { get; }

        private readonly Posicao Captura;

        public EnPassant(Posicao De, Posicao Para)
        {
            DePos = De;
            ParaPos = Para;
            Captura = new Posicao(De.linha, Para.coluna);

        }

        public override void Executar(Tabuleiro tabuleiro)
        {
            new MovimentoNormal(DePos, ParaPos).Executar(tabuleiro);
            tabuleiro[Captura] = null;
        }

        public override bool MovimentoLegal(Tabuleiro tabuleiro)
        {
            return base.MovimentoLegal(tabuleiro);
        }
    }
}
