using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PizzériaAPP
{
    public partial class Form3 : Form
    {
        public int UserID;
        string connStr = "server=localhost;user=root;database=pizzeriaapp;port=3306;password=";
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            string sql = $"SELECT `ID`,`kód`,`Osszár`,`Állapot`,`Várható kiszállitás` FROM `rendelés` WHERE `UserID`={UserID};";
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
                    comboSource.Add(Convert.ToString(rdr[0]), $"Kód: {rdr[1]} Összár: {rdr[2]} Ft Állapot: {rdr[3]} Várható Kiszállítás: {rdr[4]}");
                    listBox1.DataSource = new BindingSource(comboSource, null);
                    listBox1.DisplayMember = "Value";
                    listBox1.ValueMember = "Key";
                }
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            string id = Convert.ToString(listBox1.SelectedValue);
            string sql = $"SELECT pizza.Name,pizza.Price,rendeleselem.Menyiség,rendeleselem.Menyiség * pizza.Price FROM `rendeleselem`,pizza WHERE pizza.ID = rendeleselem.PizzaID AND rendeleselem.RendelésID = {id}; ";
            MySqlConnection conn = new MySqlConnection(connStr);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr;
            Dictionary<string, string> comboSource = new Dictionary<string, string>();

            try
            {
                string adatok;

                conn.Open();
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    adatok = "Pizza: " + rdr[0].ToString() + " Egységár:" + rdr[1].ToString() + " Ft/db" + " Menyiség: " + rdr[2].ToString() + " Összár: " + rdr[3].ToString() + " Ft";
                    listBox2.Items.Add(adatok.ToString());
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
