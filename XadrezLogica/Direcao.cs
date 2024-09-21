using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XadrezLogica
{
    public class Direcao
    {
        public readonly static Direcao Norte= new Direcao(-1,0);
        public readonly static Direcao Sul = new Direcao(+1,0);
        public readonly static Direcao Leste = new Direcao(0,1);
        public readonly static Direcao Oeste = new Direcao(0,-1);
        public readonly static Direcao Noroeste = Norte + Oeste;
        public readonly static Direcao Nordeste = Norte + Leste;
        public readonly static Direcao Sudoeste = Sul + Oeste;
        public readonly static Direcao Sudeste = Sul + Leste;

        public int deltaLinha { get; }
        public int deltaColuna {  get; }

        public Direcao(int deltaLinha, int deltaColuna)
        {
            this.deltaLinha = deltaLinha;
            this.deltaColuna = deltaColuna;
        }

        public static Direcao operator +(Direcao dir1, Direcao dir2)
        {
           return new Direcao(dir1.deltaLinha+ dir2.deltaLinha, dir1.deltaColuna+ dir2.deltaColuna);
        }

        public static Direcao operator *(int escalar, Direcao dir)
        {
            return new Direcao(escalar*dir.deltaLinha, escalar*dir.deltaColuna);
        }
    }
}
