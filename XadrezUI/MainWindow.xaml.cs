using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using XadrezLogica;
namespace XadrezUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Image[,] imagensPecas = new Image[8,8];

        private readonly Rectangle[,] Highlights = new Rectangle[8,8];

        private readonly Dictionary<Posicao,Movimento> cacheMovimentos = new Dictionary<Posicao,Movimento>();

        private EstadoJogo EstadoJogo;

        private Posicao PosicaoSelecionada = null;

        [DllImport("Kernel32")]
        public static extern void AllocConsole();
        public MainWindow()
        {
            InitializeComponent();
            InicializarTabuleiro();
            EstadoJogo = new EstadoJogo(Jogador.Brancas,Tabuleiro.Inicio());
            AllocConsole();
            DesenharTabuleiro(EstadoJogo.Tabuleiro);
        }

        private void InicializarTabuleiro()
        {
            for(int i = 0; i< 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    Image imagem = new Image();
                    imagensPecas[i,j] = imagem;
                    
                    GridPecas.Children.Add(imagem);
                    Rectangle Highlight = new Rectangle();
                    Highlights[i,j] = Highlight;
                    GridHighlights.Children.Add(Highlight);
                }
            }
        }

        private void DesenharTabuleiro(Tabuleiro tabuleiro)
        {
            for(int i = 0; i< 8; i++)
            {
                for(int j = 0;j < 8; j++)
                {
                    Peca peca = tabuleiro[i, j];
                    imagensPecas[i,j].Source= Imagens.GetImage(peca);
                }
            }
        }

        private void GridTabuleiro_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point ponto = e.GetPosition(GridTabuleiro);
            Posicao pos = PosicaoParaQuadrado(ponto);
            if(PosicaoSelecionada == null)
            {
                PosicaoDeSelecionada(pos);
            }
            else
            {
                PosicaoParaSelecionada(pos);
            }
        }

        private void PosicaoDeSelecionada(Posicao pos)
        {
            IEnumerable<Movimento> movimentos = EstadoJogo.MovimentosPorPeca(pos);
            if (movimentos.Any())
            {
                PosicaoSelecionada = pos;
                CacheMovimentos(movimentos);
                MostrarHightlights();
            }
        }

        private void PosicaoParaSelecionada(Posicao pos)
        {
            PosicaoSelecionada = null;
            EsconderHighlights();
            if (cacheMovimentos.TryGetValue(pos, out Movimento movimento))
            {
                if(movimento.Tipo == TiposMovimentos.Promocao)
                {
                    FazerPromocao(movimento.DePos, movimento.ParaPos);
                }
                FazerMovimentos(movimento);
            }
        }

        private void FazerPromocao(Posicao De, Posicao Para)
        {

        }

        private void FazerMovimentos(Movimento movimento)
        {
            EstadoJogo.FazerMovimento(movimento);
            DesenharTabuleiro(EstadoJogo.Tabuleiro);
            if(EstadoJogo.Result == Resultado.XequeMate)
            {
                XequeMateWindow janela = new XequeMateWindow();
                if(EstadoJogo.JogadorAtual.Oponente() == Jogador.Brancas)
                {
                    janela.LabelJogador.Content = "Jogador ganhador: Brancas!";
                }
                else
                {
                    janela.LabelJogador.Content = "Jogador ganhador : Pretas";
                }
                janela.Show();
            }else if (EstadoJogo.Result == Resultado.StealMate)
            {
                XequeMateWindow janela = new XequeMateWindow();
                janela.LabelJogador.Content = "Empate por Afogamento";
            }
        }

        private Posicao PosicaoParaQuadrado(Point ponto)
        {
            double tamanhoQuadrado = GridTabuleiro.ActualWidth / 8;
            int linha = (int)(ponto.Y / tamanhoQuadrado);
            int coluna = (int)(ponto.X / tamanhoQuadrado);
            return new Posicao(linha, coluna);
        }



        private void CacheMovimentos(IEnumerable<Movimento> movimentos)
        {
            cacheMovimentos.Clear();
            foreach(Movimento movimento in movimentos)
            {
                cacheMovimentos[movimento.ParaPos] = movimento;
            }
        }

        private void MostrarHightlights()
        {
            Color cor = Color.FromArgb(150, 125, 255, 125);
            foreach(Posicao Para in cacheMovimentos.Keys)
            {
                Highlights[Para.linha, Para.coluna].Fill = new SolidColorBrush(cor);
            }
        }

        private void EsconderHighlights()
        {
            foreach(Posicao Para in cacheMovimentos.Keys)
            {
                Highlights[Para.linha, Para.coluna].Fill = Brushes.Transparent; 
            }
        }

    }
}