using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace bevasarlolista
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        private double osszesen = 0;
        public MainWindow()
        {
            InitializeComponent();
            if (Termekvalaszto.Items.Count > 0)
            {
                (Termekvalaszto.Items[0] as ComboBoxItem)!.IsSelected = true;
            }
        }

        private void Hozzaad_Click(object sender, RoutedEventArgs e)
        {
            if (Termekvalaszto.SelectedItem is ComboBoxItem item)
            {
                var nev = item.Content.ToString();
                var egysegar = double.Parse((item.Tag.ToString() ?? "0"));

                if (double.TryParse(MennyisegMezo.Text, out double menny) && menny > 0)
                {
                    double sorOsszeg = egysegar * menny;
                    Lista.Items.Add($"{nev} - {menny} db - {sorOsszeg:F0}");

                    osszesen = sorOsszeg;
                    Frissit_Osszesen();

				}
                else
                {
                    MessageBox.Show("Érvénytelen mennyiség", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        private void CE_Click(object sender, RoutedEventArgs e)
        {
            MennyisegMezo.Text = "1";
            MennyisegMezo.Focus();
            MennyisegMezo.SelectAll();
            Afajelolo.IsEnabled = true;
            Afajelolo.IsChecked = false;
		}

        private void Frissit_Osszesen()
        {
            double vegosszeg = osszesen;
            if (Afajelolo.IsChecked == true)
            {
                vegosszeg *= 1.27;
            }
            
                OsszesenCimke.Text = $"{vegosszeg:F0} Ft";
		}

        private void Afaajelolo_Checked(object sender, RoutedEventArgs e)
        {
                Frissit_Osszesen();
            Afajelolo.IsEnabled = false;
		}

		private void Afaajelolo_Unchecked(object sender, RoutedEventArgs e)
		{
			OsszesenCimke.Text = $"{osszesen:F0} Ft";
		}

		private void Kijelolttorles_Click(object sender, RoutedEventArgs e)
		{
            for (int i = Lista.SelectedItems.Count - 1; i >= 0; i--)
			{
                var sor = Lista.SelectedItems[i]?.ToString();
                if (sor != null)
                {
                    var parts = sor.Split(' ');
                    if (double.TryParse(parts[^2], out double ertek))
                    {
                        osszesen -= ertek;
					}
                    Lista.Items.Remove(sor);
				}
			}
            Frissit_Osszesen();
		}

		private void CListatorles_Click(object sender, RoutedEventArgs e)
		{
            Lista.Items.Clear();
            osszesen = 0;
            Frissit_Osszesen();
		}

		
	}
}
