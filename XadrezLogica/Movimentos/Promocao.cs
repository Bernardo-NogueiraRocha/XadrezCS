using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XadrezLogica.Pecas;

namespace XadrezLogica
{
    public class Promocao : Movimento
    {
        public override TiposMovimentos Tipo => TiposMovimentos.Promocao;
        public override Posicao DePos { get; }
        public override Posicao ParaPos { get; }

        private readonly PecaTipo novoTipo;

        public Promocao(Posicao dePos,Posicao paraPos, PecaTipo Novo) 
        {
            DePos= dePos;
            ParaPos= paraPos;
            this.novoTipo = Novo;
        }

        public Peca criaPecaPromocao(Jogador Cor)
        {
            return novoTipo switch
            {
                PecaTipo.Cavalo => new Cavalo(Cor),
                PecaTipo.Torre => new Torre(Cor),
                PecaTipo.Bispo => new Bispo(Cor),
                _ => new Rainha(Cor)
            };

        }

        public override bool MovimentoLegal(Tabuleiro tabuleiro)
        {
            return base.MovimentoLegal(tabuleiro);
        }

        public override void Executar(Tabuleiro tabuleiro)
        {
            Peca peao = tabuleiro[DePos];
            tabuleiro[DePos] = null;

            Peca nova = criaPecaPromocao(peao.Cor);
            nova.JaMoveu = true;
            tabuleiro[ParaPos] = nova;
        }
    }
}
