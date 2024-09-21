namespace XadrezLogica
{
    public abstract class Movimento
    {
        public abstract TiposMovimentos Tipo { get; }
        public abstract Posicao DePos {  get; }
        public abstract Posicao ParaPos { get; }

        public abstract void Executar(Tabuleiro tabuleiro);

        public virtual bool MovimentoLegal(Tabuleiro tabuleiro)
        {
            Jogador jogador = tabuleiro[DePos].Cor;
            Tabuleiro copia = tabuleiro.Copiar();
            Executar(copia);
            return !copia.Xeque(jogador);
        }

        public string toString()
        {
            return "("+ DePos.linha + "," + DePos.coluna + ")" + ", " + "(" + ParaPos.linha+ "," + ParaPos.coluna + ")";
        }
    }
}
