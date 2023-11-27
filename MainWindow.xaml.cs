using System;
using System.Collections.Generic;
using System.IO;
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

namespace Nobel_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Dijazas> dijazottak;
        public MainWindow()
        {
            InitializeComponent();
            for (int ev = 1950; ev <= 2020; ev++)
            {
                cbEvek.Items.Add(ev);
            }

            //2. feladat
            dijazottak = File.ReadAllLines("nobel.csv").Skip(1).Select(sor => new Dijazas(sor)).ToList();
            dgDijazottak.ItemsSource = dijazottak;

            //3. feladat: Arthur B. McDonalds milyen típusú díjat kapott?

            string dijNeve = dijazottak.Find(x => x.KeresztNev == "Arthur B." && x.VezetekNev == "McDonald").Dij;
            txtEredmeny.Text = $"3. feladat: {dijNeve}";

            //4. feladat:Ki kapott 2017-ben irodalmi Nobel-díjat?

            Dijazas kiKapta = dijazottak.Find(x => x.Evszam == 2017 && x.Dij == "irodalmi");
            txtEredmeny.Text += $"\n4. feladat: {kiKapta.KeresztNev} {kiKapta.VezetekNev}";

            //5. feladat:Mely szervezetek kaptak béke Nobel-díjat 1990-től napjainkig?

            txtEredmeny.Text += $"\n5. feladat:";
            var szervezetek = dijazottak.Where(x => x.Evszam >= 1990 && x.Dij == "béke" && x.VezetekNev == "");
            foreach (var szervezet in szervezetek)
            {
                txtEredmeny.Text += $"\n\t{szervezet.Evszam}: {szervezet.KeresztNev}";
            }

            //6. feladat: A Curie család több tagja is kapott díjat.
            //            A család tagjai melyik évben, milyen díjat kapott?

            txtEredmeny.Text += $"\n6. feladat:";

            //var csaladtagok = dijazottak.Where(x => x.VezetekNev.Contains("Curie"));
            //foreach (var tag in csaladtagok)
            //{
            //    txtEredmeny.Text += $"\n\t{tag.Evszam}: {tag.KeresztNev} {tag.VezetekNev}({tag.Dij})";
            //}

            dijazottak.Where(x => x.VezetekNev.Contains("Curie"))
                .ToList()
                .ForEach(elem => txtEredmeny.Text += $"\n\t{elem.Evszam}: {elem.KeresztNev} {elem.VezetekNev}({elem.Dij})");

            //7. feladat: Melyik típusú díjból hányat osztottak ki ? 

            txtEredmeny.Text += $"\n7. feladat:";
            dijazottak.GroupBy(x => x.Dij)
                .ToList()
                .ForEach(csoport => txtEredmeny.Text += $"\n\t{csoport.Key,-20}{csoport.Count(),5} db");


            //"sas".PadRight(20)

            //8. feladat

            File.WriteAllLines("orvosi.txt", dijazottak
                .Where(x => x.Dij == "orvosi")
                .Select(obj => $"{obj.Evszam}:{obj.KeresztNev} {obj.VezetekNev}"));

        }

        // Szűrés névszerint:
        private void txtNev_TextChanged(object sender, TextChangedEventArgs e)
        {
            var szurtLista = dijazottak.Where(x => x.VezetekNev.StartsWith(txtNev.Text));
            dgDijazottak.ItemsSource = szurtLista;
        }

        // Szűrés évszám szerint:
        private void cbEvek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var szurtLista = dijazottak.Where(x => Convert.ToString(x.Evszam) == cbEvek.SelectedItem.ToString());
            dgDijazottak.ItemsSource = szurtLista;
        }
    }
}

