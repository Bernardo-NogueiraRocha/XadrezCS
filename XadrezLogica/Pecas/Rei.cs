namespace XadrezLogica.Pecas
{
    public class Rei :Peca
    {
        public override Jogador Cor { get; }
        public override PecaTipo Tipo => PecaTipo.Rei;

        private static readonly Direcao[] dirs = new Direcao[] {
            Direcao.Norte,
            Direcao.Sul,
            Direcao.Leste,
            Direcao.Oeste,
            Direcao.Sudeste,
            Direcao.Sudoeste,
            Direcao.Nordeste,
            Direcao.Noroeste
        };


        public Rei(Jogador cor)
        {
            this.Cor = cor;
        }

        private static bool TorreMoveu(Posicao pos, Tabuleiro tabuleiro)
        {
            if (tabuleiro.EstaVazio(pos))
            {
                return false;
            }
            Peca peca = tabuleiro[pos];
            return peca.Tipo == PecaTipo.Torre && !peca.JaMoveu;
        }

        private static bool CasasVaziasRoque(IEnumerable<Posicao> posicoes, Tabuleiro tabuleiro)
        {
            return posicoes.All(pos => tabuleiro.EstaVazio(pos));
        }

        private bool PodeRoqueK(Posicao DePos, Tabuleiro tabuleiro)
        {
            if(JaMoveu == true)
            {
                return false;
            }
            Posicao Torre = new Posicao(DePos.linha,7);
            Posicao[] EntrePosicoes = new Posicao[] {new(DePos.linha,5), new(DePos.linha,6)};
            return CasasVaziasRoque(EntrePosicoes, tabuleiro) && TorreMoveu(Torre, tabuleiro); 
        }

        private bool PodeRoqueQ(Posicao DePos, Tabuleiro tabuleiro)
        {
            if (JaMoveu == true)
            {
                return false;
            }

            Posicao Torre = new Posicao(DePos.linha, 0);
            Posicao[] EntrePosicoes = new Posicao[] {new(DePos.linha,3), new(DePos.linha,2), new(DePos.linha, 1) };

            return CasasVaziasRoque(EntrePosicoes, tabuleiro) && TorreMoveu(Torre, tabuleiro);
        }

        public override Peca Copiar()
        {
            Rei copia = new Rei(Cor);
            copia.JaMoveu = JaMoveu;
            return copia;
        }

        private IEnumerable<Posicao> MoverPosicoes(Posicao De, Tabuleiro tabuleiro)
        {
            foreach(Direcao dir in dirs)
            {
                Posicao Para = De + dir;
                if (!Tabuleiro.DentroTabuleiro(Para))
                {
                    continue;
                }
                if (tabuleiro.EstaVazio(Para) || tabuleiro[Para].Cor != Cor )
                {
                    yield return Para; 
                }
            }
        }

        public override IEnumerable<Movimento> GetMovimentos(Posicao De, Tabuleiro tabuleiro)
        {
            foreach(Posicao Para in MoverPosicoes(De, tabuleiro))
            {
                yield return new MovimentoNormal(De, Para);
            }
            if(PodeRoqueK(De, tabuleiro))
            {
                yield return new Roque(TiposMovimentos.RoqueK, De);
            }
            if(PodeRoqueQ(De, tabuleiro))
            {
                yield return new Roque(TiposMovimentos.RoqueQ, De);
            }
        }

        public override bool PodeCapturarReiInimigo(Posicao De, Tabuleiro tabuleiro)
        {
            return MoverPosicoes(De, tabuleiro).Any(para =>
            {
                Peca peca = tabuleiro[para];
                return peca != null && peca.Tipo == PecaTipo.Rei;
            });
        }
    }
}
