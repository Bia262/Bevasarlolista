using System.Windows;
using static wpf1217.MainWindow;

namespace wpf1217
{

    public partial class Hozzáadás : Window
    {

        public ItemModel ujtermek;
        public Hozzáadás()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ujtermek = new ItemModel(nev.Text, int.Parse(mennyiseg.Text), int.Parse(ar.Text), kategoria.Text);
            DialogResult = true;
        }
    }
}