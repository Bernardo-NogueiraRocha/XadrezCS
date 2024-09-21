namespace XadrezLogica
{
    public class Peao : Peca
    {
        public override PecaTipo Tipo => PecaTipo.Peao;
        public override Jogador Cor { get; }

        public readonly Direcao frente;

        public Peao(Jogador Cor)
        {
            this.Cor = Cor;
            if(Cor == Jogador.Brancas)
            {
                frente = Direcao.Norte ;
            }
            else if(Cor == Jogador.Pretas)
            {
                frente = Direcao.Sul;
            }
        }
        public override Peca Copiar()
        {
            Peao Copia = new Peao(Cor);
            Copia.JaMoveu = JaMoveu;
            return Copia;
        }

        private static bool PodeMoverPara(Posicao pos, Tabuleiro tabuleiro)
        {
            return Tabuleiro.DentroTabuleiro(pos) && tabuleiro.EstaVazio(pos);
        }

        private bool PodeCapturar(Posicao pos, Tabuleiro tabuleiro)
        {
            if(!Tabuleiro.DentroTabuleiro(pos) || tabuleiro.EstaVazio(pos))
            {
                return false;
            }

            return tabuleiro[pos].Cor != this.Cor ;
        }

        private static IEnumerable<Movimento> MovimentosPromocao(Posicao De, Posicao Para)
        {
            yield return new Promocao(De, Para, PecaTipo.Cavalo);
            yield return new Promocao(De, Para, PecaTipo.Bispo);
            yield return new Promocao(De, Para, PecaTipo.Torre);
            yield return new Promocao(De, Para, PecaTipo.Rainha);
        }

        private IEnumerable<Movimento> MovimentosFrente(Tabuleiro tabuleiro,Posicao De)
        {
            Posicao UmPasso = De + frente;

            if(PodeMoverPara(UmPasso, tabuleiro))
            {
                if(UmPasso.linha == 0 || UmPasso.linha == 7)
                {
                    foreach(Movimento aux in MovimentosPromocao(De, UmPasso))
                    {
                        yield return aux;
                    }
                }
                else
                {
                    yield return new MovimentoNormal(De, UmPasso);
                }

                Posicao DoisPassos = UmPasso + frente;
                if(!JaMoveu && PodeMoverPara(DoisPassos, tabuleiro))
                {
                    yield return new PeoesDuplos(De, DoisPassos);
                }
            }
        }

        private IEnumerable<Movimento> MovimentosDiagonais(Tabuleiro tabuleiro,Posicao De)
        {
            foreach(Direcao dir in new Direcao[] { Direcao.Oeste, Direcao.Leste })
            {
                Posicao Para = De + frente + dir;
                if (Para == tabuleiro.GetPoesDuplos(Cor.Oponente()))
                {
                    yield return new EnPassant(De, Para);
                }

                else if(PodeCapturar(Para, tabuleiro))
                {
                    if (Para.linha == 0 || Para.linha == 7)
                    {
                        foreach (Movimento aux in MovimentosPromocao(De, Para))
                        {
                            yield return aux;
                        }
                    }
                    else
                    {
                        yield return new MovimentoNormal(De, Para);
                    }
                }
            }
        }

        public override IEnumerable<Movimento> GetMovimentos(Posicao De, Tabuleiro tabuleiro)
        {
            return MovimentosFrente(tabuleiro, De).Concat(MovimentosDiagonais(tabuleiro,De));
        }


        public override bool PodeCapturarReiInimigo(Posicao De, Tabuleiro tabuleiro)
        {
            return MovimentosDiagonais(tabuleiro,De).Any(movimento =>
            {
                Peca peca = tabuleiro[movimento.ParaPos];
                return peca != null && peca.Tipo == PecaTipo.Rei;
            });
        }
    }
}
