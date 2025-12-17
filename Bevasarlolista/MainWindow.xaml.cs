using System.Windows;
using System.Windows.Controls;
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
            if (termekDG.SelectedItem != null
                && termekDG.SelectedItem is ItemModel)
            {
                termekek.Remove((ItemModel)termekDG.SelectedItem);
                termekDG.ItemsSource = termekek;
                termekDG.Items.Refresh();
            }
        }

        private void tipus3leg(object sender, RoutedEventArgs e)
        {
            termekDG.ItemsSource = termekek.Where(x => x.Kategoria == "A")
                .OrderByDescending(x => x.Osszesen)
                .Take(3)
                .ToList();
        }

        private void top5ossz(object sender, RoutedEventArgs e)
        {
            termekDG.ItemsSource = termekek
                .OrderByDescending(x => x.Osszesen)
                .Take(5)
                .ToList();
        }

        private void kedvOssz(object sender, RoutedEventArgs e)
        {
            termekDG.ItemsSource = termekek.Where(x => x.Mennyiseg > 1)
                .Select(z => new { Nev = z.Nev, Osszeg = z.Osszesen });
        }

        private void adatBe(object sender, RoutedEventArgs e)
        {
            termekDG.ItemsSource = termekek;
            termekDG.Items.Refresh();
        }

        private void arCsokk(object sender, RoutedEventArgs e)
        {
            termekDG.ItemsSource = termekek.OrderByDescending(x => x.Ar);
        }

        private void dTipus500(object sender, RoutedEventArgs e)
        {
            termekDG.ItemsSource = termekek
                .Where(x => x.Kategoria == "D" && x.Ar < 500)
                .ToList();
        }

        private void nevOsszABC(object sender, RoutedEventArgs e)
        {
            termekDG.ItemsSource = termekek
                .OrderBy(x => x.Nev)
                .ThenByDescending(x => x.Osszesen)
                .ToList();
        }

        private void tipusOssz(object sender, RoutedEventArgs e)
        {
            termekDG.ItemsSource = termekek
                .GroupBy(x => x.Kategoria)
                .Select(g => new
                {
                    Kategoria = g.Key,
                    Osszesen = g.Sum(x => x.Osszesen)
                })
                .ToList();
        }

        private void tipusAtlag(object sender, RoutedEventArgs e)
        {
            termekDG.ItemsSource = termekek
                .GroupBy(x => x.Kategoria)
                .Select(g => new
                {
                    Kategoria = g.Key,
                    AtlagAr = g.Average(x => x.Ar)
                })
                .ToList();
        }
        private void tobbMint1(object sender, RoutedEventArgs e)
        {
            termekDG.ItemsSource = termekek.Where(t => t.Mennyiseg > 1).Select(k => new { Nev = k.Nev, Összeg = k.Osszesen });
        }
        private void reset(object sender, RoutedEventArgs e)
        {
            termekDG.ItemsSource = termekek;
        }

        private void csokken(object sender, RoutedEventArgs e)
        {
            termekDG.ItemsSource = termekek.OrderByDescending(x => x.Ar);
        }

        private void dTipus500felett(object sender, RoutedEventArgs e)
        {
            termekDG.ItemsSource = termekek.Where(t => t.Kategoria == "D" && t.Ar > 500);
        }

        
        private void darabEsOsszertek(object sender, RoutedEventArgs e)
        {
            termekDG.ItemsSource = termekek.OrderBy(x => x.Nev)
                .GroupBy(g => g.Kategoria)
                .Select(g => new { Típus = g.Key, Darab = g.Sum(t => t.Mennyiseg), Összesen = g.Sum(t => t.Osszesen) });
        }

        private void tipusAtlagar(object sender, RoutedEventArgs e)
        {
            termekDG.ItemsSource = termekek.GroupBy(t => t.Kategoria)
                .Select(g => new
                {
                    Kategória = g.Key,
                    Átlagár = Math.Round(g.Average(t => t.Ar), 2)
                });
        }

        private void highestTotalByCategoryBtn(object sender, RoutedEventArgs e)
        {
            termekDG.ItemsSource = termekek.GroupBy(t => t.Kategoria).Select(
                g => new
                {
                    Kategória = g.Key,
                    Összérték = g.Max(g => g.Osszesen)
                });
        }

        private void bcEzerKisebb(object sender, RoutedEventArgs e)
        {
            termekDG.ItemsSource = termekek.Where(t => (t.Kategoria == "B" || t.Kategoria == "C") && t.Ar < 1000);
        }
        private void felett500csoport(object sender, RoutedEventArgs e)
        {
            termekDG.ItemsSource = termekek
                .Where(c => c.Ar > 500)
                .GroupBy(x => x.Kategoria)
                .Select(v => new { Kategoria = v.Key, TermékekSzáma = v.Count() });

        }

        private void kevesebbMint10esKisebb1000(object sender, RoutedEventArgs e)
        {
            termekDG.ItemsSource = termekek
                .Where(t => t.Mennyiseg > 10 && t.Ar < 1000)
                .OrderBy(t => t.Ar);
        }

        private void OsszN2000ABC(object sender, RoutedEventArgs e)
        {
            termekDG.ItemsSource = termekek.Where(x => x.Osszesen > 2000).OrderBy(x => x.Nev);
        }

        private void termeknevTipusCsoportositas(object sender, RoutedEventArgs e)
        {
            termekDG.ItemsSource = termekek.GroupBy(t => new { Nev = t.Nev, Kategoria = t.Kategoria })
                .Select(x => new { TermekNev = x.Key.Nev, Kategoria = x.Key.Kategoria, Darab = x.Count() });
        }


        private void LegertekesebbTipusonkent(object sender, RoutedEventArgs e)
        {
            termekDG.ItemsSource = termekek.GroupBy(item => item.Kategoria).Select(x => new { Kategoria = x.Key, Ar = x.Max(s => s.Ar) });
        }

        private void OsszesitettDbTipusonkent(object sender, RoutedEventArgs e)
        {
            termekDG.ItemsSource = termekek.GroupBy(f => f.Kategoria).Select(g => new { Kategoria = g.Key, Darab = g.Sum(q => q.Mennyiseg) });
        }

        private void nullaFt(object sender, RoutedEventArgs e)
        {
            var nullaFtosTermek = termekek.Any(x => x.Ar == 0);

            if (nullaFtosTermek == false)
            {
                MessageBox.Show("Nincs nulla Ft-os termék.");
            }
            else
            {
                MessageBox.Show("Van nulla Ft-os termék.");
            }
        }

        private void kenyerek(object sender, RoutedEventArgs e)
        {
            termekDG.ItemsSource = termekek.Where(x => x.Nev.Contains("Kenyer")).OrderByDescending(c => c.Ar);

        }

        private void egyezoAr(object sender, RoutedEventArgs e)
        {
            var egyformak = termekek.GroupBy(x => x.Ar)
                .Select(g => new { darab = g.Count() })
                .Any(z => z.darab > 1);

            if (egyformak == true)
            {
                MessageBox.Show($"Van olyan adataink, amelyeknek megeggyezik az ára!");
            }
            else
            {
                MessageBox.Show($"Nincs olyan adat amely egy másik adat árával megeggyezne, mind egyedi");
            }
        }

       

        private void egyezo(object sender, RoutedEventArgs e)
        {
            termekDG.ItemsSource = termekek
                .GroupBy(t => t.Nev)
                .Where(t => t.Count() > 1)
                .SelectMany(t => t);
        }

        

        private void nemc(object sender, RoutedEventArgs e)
        {
            termekDG.ItemsSource = termekek.Where(t => t.Kategoria != "C");
        }

        private void nevHosszSzerint(object sender, RoutedEventArgs e)
        {
            termekDG.ItemsSource = termekek.OrderBy(x => x.Nev.Length);
        }

        private void aTipusOsszAr(object sender, RoutedEventArgs e)
        {
            termekDG.ItemsSource = termekek
                .Where(t => t.Kategoria == "A")
                .OrderBy(t => t.Nev)
                .Select(t => new
                {
                    Kategória = t.Kategoria,
                    Név = t.Nev,
                    Összes = t.Osszesen
                });
        }
    }
}