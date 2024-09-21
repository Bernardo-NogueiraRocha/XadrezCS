
namespace XadrezLogica
{
    public class Torre : Peca
    {
        public override Jogador Cor { get; }
        public override PecaTipo Tipo => PecaTipo.Torre;

        public static readonly Direcao[] dirs =
        {
            Direcao.Norte,
            Direcao.Sul,
            Direcao.Leste,
            Direcao.Oeste
        };
        public Torre(Jogador Cor)
        {
            this.Cor = Cor;
        }
        public override Peca Copiar()
        {
            Torre copia = new Torre(Cor);
            copia.JaMoveu = JaMoveu;
            return copia;
        }

        public override IEnumerable<Movimento> GetMovimentos(Posicao De, Tabuleiro tabuleiro)
        {
            return MoverPosicoesEmDirs(De, tabuleiro, dirs).Select(Para => new MovimentoNormal(De, Para));
        }
    }
}
