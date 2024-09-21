using System.Xml;
using System.Linq; // Adicione esta linha para usar LINQ

namespace XadrezLogica
{
    public class Xadrezbot
    {
        public Jogador Cor;

        public int alpha = int.MinValue;

        public int beta = int.MaxValue;

        public List<Movimento> movimentosOponentes;

        public List<Movimento> movimentosBot;

        private readonly Dictionary<PecaTipo, int> ValorPecas = new Dictionary<PecaTipo, int> {
            { PecaTipo.Rainha, 900},
            { PecaTipo.Torre, 500},
            { PecaTipo.Bispo, 300},
            { PecaTipo.Cavalo, 300},
            { PecaTipo.Rei, 10000},
            { PecaTipo.Peao, 100}
        };

        public Xadrezbot(Jogador jogador) { 
            Cor = jogador;
            movimentosOponentes = new List<Movimento>();
            movimentosBot = new List<Movimento>();
        }

        public int Evaluation(Tabuleiro tabuleiro, Jogador jogador)
        {
            int output = 0;
            int mobilidadePecas = 0 ;
            
            for (int i = 0; i < 8; i++)
            {
                for(int j = 0;j  < 8; j++)
                {
                    Posicao aux = new Posicao(i, j);
                    
                    if (!tabuleiro.EstaVazio(aux)){
                        int count = tabuleiro[aux].GetMovimentos(aux, tabuleiro).Count();
                        mobilidadePecas += count * (jogador == tabuleiro[aux].Cor ? 1 : -1);
                        output += ValorPecas[tabuleiro[aux].Tipo] * (jogador == tabuleiro[aux].Cor ? 1 : -1);
                    }
                }
            }

            return output + mobilidadePecas;
        }

        public (Movimento, int) Minimax(EstadoJogo game, int depth, bool maximizingPlayer, int alpha, int beta, int count)
        {
            if (depth == 0 || game.Result != Resultado.None)
            {
                return (null, Evaluation(game.Tabuleiro, Cor));
            }

            IEnumerable<Movimento> Legais = game.GetMovimentosLegais(maximizingPlayer ? Cor : Cor.Oponente());

            if (!Legais.Any())
            {
                return (null, Evaluation(game.Tabuleiro, Cor));
            }

            Movimento melhorMovimento = null;

            if (maximizingPlayer)
            {
                int maxEval = int.MinValue;

                foreach (Movimento legal in Legais)
                {
                    EstadoJogo Copia = game.Copiar();
                    legal.Executar(Copia.Tabuleiro);
                    var result = Minimax(Copia, depth - 1, false, alpha, beta, count + 1);
                    int eval = result.Item2;
                    Console.WriteLine("Eval Posicao " + count + ":" + eval + " Alpha = " + alpha + " Beta = " + beta);
                    if (eval > maxEval)
                    {
                        maxEval = eval;
                        melhorMovimento = legal;
                    }

                    alpha = Math.Max(alpha, eval);
                    if (beta <= alpha)
                    {
                        break; // Beta cutoff
                    }
                }

                return (melhorMovimento, maxEval);
            }
            else
            {
                int minEval = int.MaxValue;

                foreach (Movimento legal in Legais)
                {
                    EstadoJogo Copia = game.Copiar();
                    legal.Executar(Copia.Tabuleiro);
                    var result = Minimax(Copia, depth - 1, true, alpha, beta,count+1);
                    int eval = result.Item2;
                    Console.WriteLine("Eval Posicao "+count + ":" + eval + " Alpha = " + alpha + " Beta = " + beta);
                    if (eval < minEval)
                    {
                        minEval = eval;
                        melhorMovimento = legal;
                    }

                    beta = Math.Min(beta, eval);
                    if (beta <= alpha)
                    {
                        break; // Beta cutoff
                    }
                }

                return (melhorMovimento, minEval);
            }
        }


        public Movimento FazerMovimentoBot(EstadoJogo game, Movimento movimentoOponente)
        {
            movimentosOponentes.Add(movimentoOponente);
            var result = Minimax(game, 5, true, alpha,beta,0);
            movimentosBot.Add(result.Item1);
            return result.Item1;
        }


    }
}
