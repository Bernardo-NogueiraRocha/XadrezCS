namespace XadrezLogica
{
    public class EstadoJogo
    {
        public Tabuleiro Tabuleiro { get; set; }
        public Jogador JogadorAtual { get; private set; }

        public Resultado Result { get; set; }

        public Xadrezbot bot ;

        public bool ativarBot = true;

        public EstadoJogo(Jogador jogador,Tabuleiro tabuleiro)
        {
            JogadorAtual = jogador;
            Tabuleiro = tabuleiro;
            Result = Resultado.None;
            if(ativarBot)
            {
                bot = new Xadrezbot(jogador.Oponente());
            }
            
        }

        public IEnumerable<Movimento> MovimentosPorPeca(Posicao pos) {
            if (Tabuleiro.EstaVazio(pos) || Tabuleiro[pos].Cor != JogadorAtual)
            {
                return Enumerable.Empty<Movimento>();
            }
            Peca peca = Tabuleiro[pos];

            IEnumerable<Movimento> candidatos =  peca.GetMovimentos(pos,Tabuleiro);
            return candidatos.Where(movimento => movimento.MovimentoLegal(Tabuleiro));
        }

        public void FazerMovimento(Movimento movimento)
        {
            Tabuleiro.SetPeoesDuplos(JogadorAtual, null);
            movimento.Executar(Tabuleiro);
            JogadorAtual = JogadorAtual.Oponente();
            ChecagemGameOver();
            if (ativarBot == true)
            {
                Tabuleiro.SetPeoesDuplos(JogadorAtual, null);
                bot.FazerMovimentoBot(this,movimento).Executar(Tabuleiro);
                JogadorAtual = JogadorAtual.Oponente();
                ChecagemGameOver();
            }
            if(Result != Resultado.None)
            {
                
            }
        }

        public IEnumerable<Movimento> GetMovimentosLegais(Jogador Cor)
        {
            IEnumerable<Movimento> candidatos = Tabuleiro.PosicaoPecasPorCor(Cor).SelectMany(pos =>
            {
                Peca peca = Tabuleiro[pos];
                return peca.GetMovimentos(pos, Tabuleiro);
            });

            return candidatos.Where(movimento => movimento.MovimentoLegal(Tabuleiro));
        }

        public void ChecagemGameOver()
        {
            if (!GetMovimentosLegais(JogadorAtual).Any()) {
                if (Tabuleiro.Xeque(JogadorAtual))
                {
                    Result = Resultado.XequeMate;
                }
                else
                {
                    Result = Resultado.StealMate;
                }
            }   
        }

        public EstadoJogo Copiar()
        {
            Tabuleiro tabuleiroCopiado = this.Tabuleiro.Copiar();
            EstadoJogo copia = new EstadoJogo(this.JogadorAtual, tabuleiroCopiado)
            {
                Result = this.Result,
                ativarBot = this.ativarBot
            };
            return copia;
        }


    }
}
