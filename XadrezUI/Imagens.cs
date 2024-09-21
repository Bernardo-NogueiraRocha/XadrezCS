using System.Windows.Media;
using System.Windows.Media.Imaging;
using XadrezLogica;
namespace XadrezUI
{
    public static class Imagens
    {
        private static readonly Dictionary<PecaTipo, ImageSource> PecasBrancas = new()
        {
            {PecaTipo.Peao ,CarregarImagem("Assets/PawnW.png") },
            {PecaTipo.Bispo ,CarregarImagem("Assets/BishopW.png") },
            {PecaTipo.Cavalo ,CarregarImagem("Assets/KnightW.png") },
            {PecaTipo.Torre ,CarregarImagem("Assets/RookW.png") },
            {PecaTipo.Rainha ,CarregarImagem("Assets/QueenW.png") },
            {PecaTipo.Rei ,CarregarImagem("Assets/KingW.png") }
        };

        private static readonly Dictionary<PecaTipo, ImageSource> PecasPretas = new()
        {
            {PecaTipo.Peao ,CarregarImagem("Assets/PawnB.png") },
            {PecaTipo.Bispo ,CarregarImagem("Assets/BishopB.png") },
            {PecaTipo.Cavalo ,CarregarImagem("Assets/KnightB.png") },
            {PecaTipo.Torre ,CarregarImagem("Assets/RookB.png") },
            {PecaTipo.Rainha ,CarregarImagem("Assets/QueenB.png") },
            {PecaTipo.Rei ,CarregarImagem("Assets/KingB.png") }
        };

        private static ImageSource CarregarImagem(string caminhoArquivo)
        {
            return new BitmapImage(new Uri(caminhoArquivo,UriKind.Relative));
        }

        public static ImageSource GetImage(Jogador Cor,PecaTipo tipo)
        {
            return Cor switch
            {
                Jogador.Brancas => PecasBrancas[tipo],
                Jogador.Pretas => PecasPretas[tipo],
                _ => null
            };
        }

        public static ImageSource GetImage(Peca peca)
        {
            if (peca == null) return null;
            return GetImage(peca.Cor, peca.Tipo);
        }
    }
}
