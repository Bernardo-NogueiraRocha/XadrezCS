using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XadrezLogica
{
    public abstract class Peca
    {
        public abstract PecaTipo Tipo { get; }
        public abstract Jogador Cor { get; }
        public bool JaMoveu { get; set; } = false;
        public abstract Peca Copiar();

        public abstract IEnumerable<Movimento> GetMovimentos(Posicao De,Tabuleiro tabuleiro);

        protected IEnumerable<Posicao> MoverPosicoesEmDir(Posicao De, Tabuleiro tabuleiro, Direcao dir)
        {
            for(Posicao pos = De + dir; Tabuleiro.DentroTabuleiro(pos); pos += dir)
            {
                if (tabuleiro.EstaVazio(pos))
                {
                    yield return pos;
                    continue;
                }
                Peca peca = tabuleiro[pos];
                if(peca.Cor != Cor)
                {
                    yield return pos;
                }

                yield break;
            }
        }
        protected IEnumerable<Posicao> MoverPosicoesEmDirs(Posicao De, Tabuleiro tabuleiro, Direcao[] dirs)
        {
            return dirs.SelectMany(dir => MoverPosicoesEmDir(De, tabuleiro, dir));
        }

        public virtual bool PodeCapturarReiInimigo(Posicao De, Tabuleiro tabuleiro)
        {
            return GetMovimentos(De, tabuleiro).Any(movimento =>
            {
                Peca peca = tabuleiro[movimento.ParaPos];
                return peca != null && peca.Tipo == PecaTipo.Rei;
            });
        }
    }
}
