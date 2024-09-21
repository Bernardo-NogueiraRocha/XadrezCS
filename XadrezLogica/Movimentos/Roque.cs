namespace XadrezLogica
{
    public class Roque : Movimento
    {
        public override TiposMovimentos Tipo { get; }
        public override Posicao DePos { get; }

        public override Posicao ParaPos { get; }

        private readonly Direcao DirecaoRei;

        private readonly Posicao TorreDePos;

        private readonly Posicao TorreParaPos;

        public Roque(TiposMovimentos TipoMove, Posicao PosicaoRei)
        {
            Tipo = TipoMove;
            DePos= PosicaoRei;
            if(Tipo == TiposMovimentos.RoqueK)
            {
                DirecaoRei = Direcao.Leste;
                ParaPos = new Posicao(PosicaoRei.linha,6);
                TorreDePos = new Posicao(PosicaoRei.linha, 7);
                TorreParaPos = new Posicao(PosicaoRei.linha, 5);
            }
            else if(Tipo == TiposMovimentos.RoqueQ)
            {
                DirecaoRei = Direcao.Oeste;
                ParaPos = new Posicao(PosicaoRei.linha, 2);
                TorreDePos = new Posicao(PosicaoRei.linha, 0);
                TorreParaPos = new Posicao(PosicaoRei.linha, 3);
            }

        }

        public override void Executar(Tabuleiro tabuleiro)
        {
            new MovimentoNormal(DePos,ParaPos).Executar(tabuleiro);
            new MovimentoNormal(TorreDePos,TorreParaPos).Executar(tabuleiro);
        }

        public override bool MovimentoLegal(Tabuleiro tabuleiro)
        {
            return base.MovimentoLegal(tabuleiro);
        }
    }
}
