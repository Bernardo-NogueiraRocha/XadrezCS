namespace XadrezLogica
{
    public class Posicao
    {
        public int linha { get; }
        public int coluna { get; }

        public Posicao(int linha, int coluna)
        {
            this.linha = linha;
            this.coluna = coluna;
        }

        public Jogador CorQuadrado()
        {
            if ((linha + coluna) % 2 == 0)
            {
                return Jogador.Brancas;
            }
            else return Jogador.Pretas;
        }

        public override bool Equals(object obj)
        {
            return obj is Posicao posicao &&
                   linha == posicao.linha &&
                   coluna == posicao.coluna;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(linha, coluna);
        }

        public static bool operator ==(Posicao left, Posicao right)
        {
            return EqualityComparer<Posicao>.Default.Equals(left, right);
        }

        public static bool operator !=(Posicao left, Posicao right)
        {
            return !(left == right);
        }

        public static Posicao operator +(Posicao left, Direcao right)
        {
            return new Posicao(left.linha+right.deltaLinha, left.coluna+right.deltaColuna);
        }

    }
}
