namespace XadrezLogica
{
    public class PeoesDuplos : Movimento
    {
        public override TiposMovimentos Tipo => TiposMovimentos.PassadaDupla;
        public override Posicao DePos { get; }
        public override Posicao ParaPos { get; }

        private readonly Posicao posicaoSkip;

        public PeoesDuplos(Posicao dePos, Posicao paraPos)
        {
            DePos = dePos;
            ParaPos = paraPos;
            this.posicaoSkip = new Posicao((DePos.linha + ParaPos.linha)/2, ParaPos.coluna );
        }

        public override void Executar(Tabuleiro tabuleiro)
        {
            Jogador jogador = tabuleiro[DePos].Cor;
            tabuleiro.SetPeoesDuplos(jogador, posicaoSkip);
            new MovimentoNormal(DePos, ParaPos).Executar(tabuleiro);
        }

    }
}
