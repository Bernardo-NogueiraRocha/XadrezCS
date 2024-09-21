namespace XadrezLogica
{
    public class MovimentoNormal : Movimento
    {
        public override TiposMovimentos Tipo => TiposMovimentos.Normal;
        public override Posicao DePos { get; }
        public override Posicao ParaPos { get; }
        public MovimentoNormal(Posicao de, Posicao para)
        {
            DePos = de;
            ParaPos = para;
        }
        public override void Executar(Tabuleiro tabuleiro)
        {
            Peca peca = tabuleiro[DePos];
            tabuleiro[ParaPos]=peca;
            tabuleiro[DePos] = null;
            peca.JaMoveu = true;
        }
    }
}
