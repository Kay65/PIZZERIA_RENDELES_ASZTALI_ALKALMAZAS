using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PizzériaAPP
{
    public partial class Form2 : Form
    {
        public int UserID;
        string connStr = "server=localhost;user=root;database=pizzeriaapp;port=3306;password=";
        public int osszar()
        {
            int osszar = 0;
            string sql = $"SELECT kosár.ID,pizza.Name,kosár.Mennyiség,pizza.Price FROM kosár,pizza WHERE kosár.PizzaId=pizza.ID and kosár.UserID={UserID};";
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr;
            try
            {

                conn.Open();
                rdr = cmd.ExecuteReader();
                
                while (rdr.Read())
                {
                    osszar += Convert.ToInt32(rdr[2]) * Convert.ToInt32(rdr[3]);
                }
                conn.Close();
             }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message); 
            }
            return osszar;
        }
        public void kiiras()
        {
            
            int osszar = 0;
            string sql = $"SELECT kosár.ID,pizza.Name,kosár.Mennyiség,pizza.Price FROM kosár,pizza WHERE kosár.PizzaId=pizza.ID and kosár.UserID={UserID};";
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr;
            Dictionary<string, string> comboSource = new Dictionary<string, string>();

            try
            {
                conn.Open();
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    comboSource.Add(Convert.ToString(rdr[0]), $"{Convert.ToString(rdr[1])} {Convert.ToString(rdr[2])} db {Convert.ToString(rdr[3])} Ft/db");
                    listBox1.DataSource = new BindingSource(comboSource, null);
                    listBox1.DisplayMember = "Value";
                    listBox1.ValueMember = "Key";
                    osszar += Convert.ToInt32(rdr[2]) * Convert.ToInt32(rdr[3]);
                }
                label1.Text = "összesen: "+osszar+" Ft"; 
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        public void torles()
        {
            string updateQuery = $"DELETE FROM `kosár` where `UserID`={UserID}";
            MySqlConnection databaseConnection = new MySqlConnection(connStr);
            MySqlCommand commandDatabase = new MySqlCommand(updateQuery, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;
            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();



                databaseConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            kiiras();
        }
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            kiiras();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            torles();
            MessageBox.Show("Sikeres Törlés!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = Convert.ToString(listBox1.SelectedValue);
            string updateQuery = $"DELETE FROM `kosár` where ID={id}";
            MySqlConnection databaseConnection = new MySqlConnection(connStr);
            MySqlCommand commandDatabase = new MySqlCommand(updateQuery, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;
            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();



                databaseConnection.Close();
                MessageBox.Show("Sikeres Törlés!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            kiiras();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);


            string kiszállítás = DateTime.Now.AddHours(2).ToString("yyyy-MM-dd hh:mm:ss");
            int oszar = osszar();
            MessageBox.Show(Convert.ToString(oszar));
            string updateQuery = $"INSERT INTO `rendelés`(`Állapot`, `Várható kiszállitás`,`UserID`, `Osszár`, `kód`) VALUES ('felvéve','{kiszállítás}',{UserID},{oszar},'{finalString}'); ";
            MySqlConnection databaseConnection = new MySqlConnection(connStr);
            MySqlCommand commandDatabase = new MySqlCommand(updateQuery, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;
            try
            {
                
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();
                updateQuery = $"INSERT INTO `rendeleselem` (`RendelésID`, `PizzaID`, `Menyiség`) SELECT rendelés.ID, kosár.PizzaID, kosár.Mennyiség FROM rendelés JOIN kosár ON rendelés.kód = '{finalString}' AND kosár.UserID = '{UserID}'; ";
                commandDatabase = new MySqlCommand(updateQuery, databaseConnection);
                databaseConnection.Close();
                try
                {
                    databaseConnection.Open();
                    reader = commandDatabase.ExecuteReader();
                    try
                    {
                        torles();
                        MessageBox.Show("Rendelés sikeresen rögzítve!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                   


                    databaseConnection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


                databaseConnection.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
