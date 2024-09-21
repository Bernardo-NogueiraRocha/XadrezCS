using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XadrezLogica
{
    public enum Jogador
    {
        None,
        Pretas,
        Brancas
    }

    public static class JogadorExtensoes
    {
        public static Jogador Oponente (this Jogador jogador)
        {
            return jogador switch
            {
                Jogador.Brancas => Jogador.Pretas,
                Jogador.Pretas => Jogador.Brancas,
                _ => Jogador.None,
            };
        }
    }
}
