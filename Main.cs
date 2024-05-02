using MySql.Data;
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

namespace PizzériaAPP
{
    public partial class Main : Form
    {
        public int UserID;
        
        string connStr = "server=localhost;user=root;database=pizzeriaapp;port=3306;password=";
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt32(textBox1.Text);
                if (comboBox1.SelectedItem == null || textBox1.Text == "")
                {
                    MessageBox.Show("Tölts ki minden mezőt!");
                }
                else
                {
                    if (Convert.ToInt32(textBox1.Text) <= 0)
                    {
                        MessageBox.Show("A mennyiség nem lehet 0 vagy negatív");
                    }
                    else
                    {
                        string pizza = Convert.ToString(comboBox1.SelectedValue);
                        int db = Convert.ToInt32(textBox1.Text);
                        string updateQuery = $"INSERT INTO `kosár`(`PizzaID`, `Mennyiség`, `UserID`) VALUES ('{pizza}','{db}',{UserID})";
                        MySqlConnection databaseConnection = new MySqlConnection(connStr);
                        MySqlCommand commandDatabase = new MySqlCommand(updateQuery, databaseConnection);
                        commandDatabase.CommandTimeout = 60;
                        MySqlDataReader reader;
                        try
                        {
                            databaseConnection.Open();
                            reader = commandDatabase.ExecuteReader();



                            databaseConnection.Close();
                            MessageBox.Show("Sikeres Beillesztés!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Nem számot adtál meg");
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 kosar = new Form2();
            kosar.UserID = UserID;
            kosar.ShowDialog();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            string sql = $"SELECT * FROM `pizza`";
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
                    adatok = rdr[1].ToString() + " " + rdr[2].ToString() + " Ft";
                    if (!listBox1.Items.Contains(adatok.ToString()))
                    {
                        listBox1.Items.Add(adatok.ToString());
                    }
                    comboSource.Add(Convert.ToString(rdr[0]), Convert.ToString(rdr[1]));
                    comboBox1.DataSource = new BindingSource(comboSource, null);
                    comboBox1.DisplayMember = "Value";
                    comboBox1.ValueMember = "Key";
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 rendelés = new Form3();
            rendelés.UserID = UserID;
            rendelés.ShowDialog();
        }
    }
}
