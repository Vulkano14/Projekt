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
using System.Data.SqlClient;  //biblioteka dotyczaca polaczenia z baza
using System.Data;

namespace Projekt_Programowaniev2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            komendySQL();                   //uruchomienie funkcji komendySQL
        }

        SqlConnection polaczenie = new SqlConnection(@"Data Source=DESKTOP-AT7GMHK;Initial Catalog=ProjektBaza;Integrated Security=True");  //polaczenie z baza danych

        public void wyczysc() //funkcja usuwajace elementy wpisane w pole tekstowe
        {
            markapole.Clear();
            procesorpole.Clear();
            kartapole.Clear();
            pamiecpole.Clear();
            wagapole.Clear();
            szukaniepole.Clear();
        }

        private void komendySQL()
        {
            SqlCommand szukanieSQL = new SqlCommand("SELECT * FROM Laptop", polaczenie); //komenda szukania z SQL
            DataTable dt = new DataTable();                             //polaczenie z baza 
            polaczenie.Open();
            SqlDataReader rea = szukanieSQL.ExecuteReader();
            dt.Load(rea);
            polaczenie.Close();
            datagrid.ItemsSource = dt.DefaultView;                  //polaczenie z polem 
        }

        private void wyczyscprz_Click(object sender, RoutedEventArgs e)
        {
            wyczysc();  // uruchomienie funkcji po kliknieciu w przycisk 
        }

        private void dodajprz_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand komenda = new SqlCommand("INSERT INTO Laptop VALUES (@Marka, @Procesor, @Karta, @Ram, @Waga)", polaczenie);
            komenda.CommandType = CommandType.Text;
            komenda.Parameters.AddWithValue("@Marka", markapole.Text);
            komenda.Parameters.AddWithValue("@Procesor", procesorpole.Text);
            komenda.Parameters.AddWithValue("@Karta", kartapole.Text);
            komenda.Parameters.AddWithValue("@Ram", pamiecpole.Text);
            komenda.Parameters.AddWithValue("@Waga", wagapole.Text);
            polaczenie.Open();
            komenda.ExecuteNonQuery();
            polaczenie.Close();
            komendySQL();
            wyczysc();
        }

        private void usunprz_Click(object sender, RoutedEventArgs e)
        {
            polaczenie.Open();
            SqlCommand com = new SqlCommand("DELETE FROM Laptop WHERE ID = " + szukaniepole.Text + " ", polaczenie);
            com.ExecuteNonQuery();
            polaczenie.Close();
            wyczysc();
            komendySQL();
            polaczenie.Close();

        }

        private void aktualizujprz_Click(object sender, RoutedEventArgs e)
        {
            polaczenie.Open();
            SqlCommand com = new SqlCommand("UPDATE Laptop set Marka = '"+markapole.Text+ "', Procesor = '" +procesorpole.Text + "', Karta = '" +kartapole.Text + "', Ram = '" +pamiecpole.Text + "', Waga = '" +wagapole.Text + "' WHERE ID = '"+szukaniepole.Text+"' ", polaczenie);
            com.ExecuteNonQuery();
            polaczenie.Close();
            wyczysc();
            komendySQL();
        }
    }
}
