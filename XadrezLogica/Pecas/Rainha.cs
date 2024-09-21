
namespace XadrezLogica.Pecas
{
    public class Rainha : Peca
    {
        public override Jogador Cor { get; }
        public override PecaTipo Tipo => PecaTipo.Rainha;

        public static readonly Direcao[] dirs = new Direcao[]
        {
            Direcao.Norte,
            Direcao.Sul,
            Direcao.Leste,
            Direcao.Oeste,
            Direcao.Nordeste,
            Direcao.Noroeste,
            Direcao.Sudeste,
            Direcao.Sudoeste
        };

        public Rainha(Jogador cor)
        {
            this.Cor = cor;
        }
        public override Peca Copiar()
        {
            Rainha copia = new Rainha(Cor);
            copia.JaMoveu = JaMoveu;
            return copia;
        }
        public override IEnumerable<Movimento> GetMovimentos(Posicao De, Tabuleiro tabuleiro)
        {
            return MoverPosicoesEmDirs(De, tabuleiro, dirs).Select(Para => new MovimentoNormal(De, Para));
        }
    }
}
