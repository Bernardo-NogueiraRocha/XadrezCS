namespace XadrezLogica.Pecas
{
    public class Cavalo : Peca
    {
        public override Jogador Cor { get; }
        public override PecaTipo Tipo => PecaTipo.Cavalo;
        public Cavalo(Jogador cor)
        {
            this.Cor = cor;
        }
        public override Peca Copiar()
        {
            Cavalo copia = new Cavalo(Cor);
            copia.JaMoveu = JaMoveu;
            return copia;
        }

        private static IEnumerable<Posicao> PosicoesPotenciais(Posicao De)
        {
            foreach(Direcao vDir in new Direcao[] {Direcao.Norte, Direcao.Sul })
            {
                foreach(Direcao hDir in new Direcao[] { Direcao.Oeste, Direcao.Leste })
                {
                    yield return De + 2*vDir + hDir;
                    yield return De + 2*hDir + vDir;
                }
            }
        }
        private IEnumerable<Posicao> MoverPosicoes(Posicao De,Tabuleiro tabuleiro)
        {
            return PosicoesPotenciais(De).Where
                (pos => Tabuleiro.DentroTabuleiro(pos) && (tabuleiro.EstaVazio(pos) || tabuleiro[pos].Cor != Cor));
        }

        public override IEnumerable<Movimento> GetMovimentos(Posicao De, Tabuleiro tabuleiro)
        {
            return MoverPosicoes(De, tabuleiro).Select(Para => new MovimentoNormal(De,Para));
        }
    }
}
