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
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using Vjezba3Wpf.Modeli;
using System.IO;
using static Vjezba3Wpf.Modeli.Transakcije.Hub;

namespace Vjezba3Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<Transakcije.Hub> transakcije = new List<Transakcije.Hub>();
        public MainWindow()
        {
            InitializeComponent();
            valuta.ItemsSource = Enum.GetValues(typeof(Transakcije.Hub.Valuta)).Cast<Transakcije.Hub.Valuta>();
            valuta.SelectedItem = Transakcije.Hub.Valuta.kuna;

        }



        private void platitelj_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void primatelj_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void iznos_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {


        }

        private void iban_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {


        }
        private void model_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void save_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Transakcije.Hub uplatnica = new Transakcije.Hub();
            try
            {
                TestBox(platitelj);
                uplatnica.platitelj = platitelj.Text;
                TestBox(primatelj);
                uplatnica.primatelj = primatelj.Text;
                TestBox(iban);
                ModelBox(iban);
                uplatnica.iban = iban.Text;
                TestBox(iznos);
                uplatnica.iznos = int.Parse(iznos.Text);
                ModelBox(model);
                TestBox(model);
                uplatnica.model = model.Text;
                uplatnica.valuta = Provjera();
                uplatnica.vrijemeUplate = DateTime.Now;

                List<Transakcije.Hub> transakcije = new List<Transakcije.Hub>();
                transakcije.Add(uplatnica);
                Transakcije.NewTransaction(transakcije);
                MainWindow mainWindowObj = new MainWindow();
                this.Visibility = Visibility.Hidden;
                mainWindowObj.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private static void TestBox(TextBox textBox)
        {
            if (String.IsNullOrEmpty(textBox.Text))
                throw new Exception("Nedostaje " + textBox.Name);
        }

        private static void ModelBox(TextBox textBox)
        {
            if (textBox.Text.Substring(0,2)!="HR")
                throw new Exception("Nedostaje HR prefiks " + textBox.Name);
        }
        private void find_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string test = iban.Text.ToString();
            string test1 = platitelj.Text.ToString();
            try
            {
                using (Stream stream = File.Open("data.bin", FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    var up2 = (List<Transakcije.Hub>)bin.Deserialize(stream);
                    foreach (Transakcije.Hub uplatnica in up2)
                    {
                        if (uplatnica.iban == test || uplatnica.platitelj == test1)
                        {
                            platitelj.Text = uplatnica.platitelj;
                            primatelj.Text = uplatnica.primatelj;
                            iznos.Text = uplatnica.iznos.ToString();
                            iban.Text = uplatnica.iban;
                            model.Text = uplatnica.model;
                            vrijeme.SelectedDate = uplatnica.vrijemeUplate;
                            valuta.SelectedItem = uplatnica.valuta;
                            return;
                        }
                    }
                }
            }
            catch (IOException)
            {
            }

        }



        private Valuta Provjera()
        {
            string currency = valuta.SelectedItem.ToString();
            int brojac = 0;
            foreach (var type in Enum.GetValues(typeof(Valuta)))
            {
                ++brojac;
                if (currency == type.ToString())
                {
                    return (Valuta)brojac;
                }
            }
            throw new Exception ();
        }
    }
}
