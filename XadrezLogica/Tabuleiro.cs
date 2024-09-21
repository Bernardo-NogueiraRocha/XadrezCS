using System.Runtime.CompilerServices;
using XadrezLogica.Pecas;

namespace XadrezLogica
{
    public class Tabuleiro
    {
        private readonly Peca[,] pecas = new Peca[8, 8];

        private readonly Dictionary<Jogador, Posicao> PeoesPassadasDuplas = new Dictionary<Jogador, Posicao>
        {
            { Jogador.Brancas, null },
            {Jogador.Pretas, null }
        };

        public Peca this[int linha, int coluna] 
        {
            get { return pecas[linha, coluna];}
            set { pecas[linha, coluna] = value;}
        }
        public Peca this[Posicao pos]
        {
            get { return pecas[pos.linha, pos.coluna]; }
            set { this[pos.linha,pos.coluna] = value;}
        }
        public Posicao GetPoesDuplos(Jogador Cor)
        {
            return PeoesPassadasDuplas[Cor];
        }

        public void SetPeoesDuplos(Jogador Cor, Posicao pos)
        {
            PeoesPassadasDuplas[Cor] = pos;
        }

        public static Tabuleiro Inicio()
        {
            Tabuleiro tabuleiro = new Tabuleiro();
            tabuleiro.AdicionarPecasIniciais();
            return tabuleiro;
        }
        private void AdicionarPecasIniciais()
        {
            this[0, 0] = new Torre(Jogador.Pretas);
            this[0, 1] = new Cavalo(Jogador.Pretas);
            this[0, 2] = new Bispo(Jogador.Pretas);
            this[0, 3] = new Rainha(Jogador.Pretas);
            this[0, 4] = new Rei(Jogador.Pretas);
            this[0, 5] = new Bispo(Jogador.Pretas);
            this[0, 6] = new Cavalo(Jogador.Pretas);
            this[0, 7] = new Torre(Jogador.Pretas);

            this[7, 0] = new Torre(Jogador.Brancas);
            this[7, 1] = new Cavalo(Jogador.Brancas);
            this[7, 2] = new Bispo(Jogador.Brancas);
            this[7, 3] = new Rainha(Jogador.Brancas);
            this[7, 4] = new Rei(Jogador.Brancas);
            this[7, 5] = new Bispo(Jogador.Brancas);
            this[7, 6] = new Cavalo(Jogador.Brancas);
            this[7, 7] = new Torre(Jogador.Brancas);
            for (int i = 0; i < 8; i++)
            {
                this[1, i] = new Peao(Jogador.Pretas);
                this[6, i] = new Peao(Jogador.Brancas);
            }
        }
        public static bool DentroTabuleiro(Posicao pos)
        {
            return pos.linha >= 0 && pos.linha < 8 && pos.coluna >= 0 && pos.coluna < 8;
        }
        public bool EstaVazio(Posicao pos)
        {
            return this[pos] == null;
        }

        public IEnumerable<Posicao> PosicaoPecas()
        {
            for(int i = 0;i < 8;i++)
            {
                for(int j = 0;j < 8; j++)
                {
                    Posicao nova = new Posicao(i, j);
                    if (!EstaVazio(nova))
                    {
                        yield return nova;
                    }
                }
            }
        }

        public IEnumerable<Posicao> PosicaoPecasPorCor(Jogador Cor)
        {
            return PosicaoPecas().Where(pos => this[pos].Cor == Cor); 
        }

        public bool Xeque(Jogador Cor)
        {
            return PosicaoPecasPorCor(Cor.Oponente()).Any(pos =>
            {
                Peca peca = this[pos];
                return peca.PodeCapturarReiInimigo(pos, this);
            });
        }

        public Tabuleiro Copiar()
        {
            Tabuleiro copia = new Tabuleiro();
            foreach(Posicao pos in PosicaoPecas())
            {
                copia[pos] = this[pos].Copiar();
            }
            return copia;
        }

    }

}
