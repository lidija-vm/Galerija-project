using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galerija
{
    class Program
    {
        static void Main(string[] args)
        {
            Album al = new Album();
            al.otvoriStranicu();
            al.nasumicneFotografije();

            string konekcioniString;

            string server = "Server=localhost;";
            string database = "Database=galerija;";
            string username = "Uid=lidija;";
            string password = "Pwd=wnnzP3Smxlg3A7rs;";

            konekcioniString = server + database + username + password;

            MySqlConnection konekcija = new MySqlConnection(konekcioniString);

            konekcija.Open();

            for (int j = 0; j < al.Imena.Count; j++)
            {
                string insertUpit = "INSERT INTO fotografije values ('','" + al.Imena.ElementAt(j) + "','" + al.Putanje.ElementAt(j) + "')";
                MySqlCommand cmdInsertUpit = new MySqlCommand(insertUpit, konekcija);

                try
                {
                    cmdInsertUpit.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Dogodila se greska");
                    Console.WriteLine(ex);
                }
            }

            string upit = "SELECT Path from fotografije";
            MySqlCommand cmdUpit = new MySqlCommand(upit, konekcija);

            MySqlDataReader citac = cmdUpit.ExecuteReader();

            int i = 0;

            while (citac.Read())
            {
                
                string path= citac["Path"].ToString();
                if (path != al.Putanje.ElementAt(i))
                {
                    Console.WriteLine("Greska");
                }

                i++;
            }

          
          
            Console.ReadLine();
        }
    }
}
