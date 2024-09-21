using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XadrezLogica
{
    public class Bispo : Peca
    {
        public override Jogador Cor { get; }
        public override PecaTipo Tipo => PecaTipo.Bispo;

        public static readonly Direcao[] dirs =
        [
            Direcao.Nordeste,
            Direcao.Sudeste,
            Direcao.Noroeste,
            Direcao.Sudoeste
        ];

        public Bispo(Jogador Cor)
        {
            this.Cor = Cor;
        }

        public override Peca Copiar()
        {
            Bispo copia = new Bispo(Cor);
            copia.JaMoveu = JaMoveu;
            return copia;
        }

        public override IEnumerable<Movimento> GetMovimentos(Posicao De, Tabuleiro tabuleiro)
        {
            return MoverPosicoesEmDirs(De, tabuleiro, dirs).Select(Para => new MovimentoNormal(De,Para));
        }
    }
}
