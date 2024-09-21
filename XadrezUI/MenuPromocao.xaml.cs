using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using XadrezLogica;
using XadrezLogica.Pecas;

namespace XadrezUI
{
    /// <summary>
    /// Interação lógica para MenuPromocao.xam
    /// </summary>
    public partial class MenuPromocao : UserControl
    {
        public event Action<PecaTipo> PecaSelecionada;

        public MenuPromocao(Jogador Cor)
        {
            InitializeComponent();

            ImagemBispo.Source = Imagens.GetImage(Cor, PecaTipo.Bispo);
            ImagemCavalo.Source = Imagens.GetImage(Cor,PecaTipo.Cavalo);
            ImagemRainha.Source = Imagens.GetImage(Cor, PecaTipo.Rainha);
            ImagemTorre.Source = Imagens.GetImage(Cor, PecaTipo.Torre);
        }

        private void ImagemRainha_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PecaSelecionada?.Invoke(PecaTipo.Rainha);
        }

        private void ImagemBispo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PecaSelecionada?.Invoke(PecaTipo.Bispo);
        }

        private void ImagemCavalo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PecaSelecionada?.Invoke(PecaTipo.Cavalo);
        }

        private void ImagemTorre_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PecaSelecionada?.Invoke(PecaTipo.Torre);
        }
    }
}
