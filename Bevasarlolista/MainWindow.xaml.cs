using System.Windows;
using static wpf1217.MainWindow;

namespace wpf1217
{
    public partial class MainWindow : Window
    {
        public class ItemModel
        {
            public String Nev { get; set; }
            public int Mennyiseg { get; set; }
            public int Ar { get; set; }
            public string Kategoria { get; set; }
            public int Osszesen { get; set; }

            public ItemModel(string nev, int mennyiseg, int ar, string kategoria)
            {
                Nev = nev;
                Mennyiseg = mennyiseg;
                Ar = ar;
                Kategoria = kategoria;
                Osszesen = mennyiseg * ar;
            }
        }

        List<ItemModel> termekek = new List<ItemModel>();
        public MainWindow()
        {
            InitializeComponent();

            termekek.Add(new ItemModel("Tej", 5, 450, "A"));
            termekek.Add(new ItemModel("Kenyer", 10, 350, "B"));
            termekek.Add(new ItemModel("Sajt", 2, 1200, "A"));
            termekek.Add(new ItemModel("Alma", 20, 200, "C"));
            termekek.Add(new ItemModel("Narancs", 15, 300, "C"));
            termekek.Add(new ItemModel("Hús", 3, 2500, "D"));
            termekek.Add(new ItemModel("Csokoládé", 7, 900, "B"));
            termekek.Add(new ItemModel("Kenyér", 1, 450, "B"));
            termekek.Add(new ItemModel("Tej", 12, 400, "A"));
            termekek.Add(new ItemModel("Sajt", 5, 1500, "D"));
            termekDG.ItemsSource = termekek;
        }

        private void hozzaadas(object sender, RoutedEventArgs e)
        {
            var ujtermek = new Hozzáadás();
            if (ujtermek.ShowDialog() == true)
            {
                termekek.Add(ujtermek.ujtermek);
                termekDG.ItemsSource = termekek;
                termekDG.Items.Refresh();
            }

        }

        private void torles(object sender, RoutedEventArgs e)
        {
            if (termekDG.SelectedItem != null)
            {
                termekek.Remove((ItemModel)termekDG.SelectedItem);
                termekDG.Items.Refresh();                
            }
        }
    }
}