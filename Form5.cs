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
    public partial class Form5 : Form
    {
        string connStr = "server=localhost;user=root;database=pizzeriaapp;port=3306;password=";
        public Form5()
        {
            InitializeComponent();
        }
        private void kiiras()
        {
            string sql = $"SELECT * FROM `pizza`";
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
                    comboSource.Add(rdr[0].ToString(), rdr[1].ToString() + " " + rdr[2].ToString() + " Ft");
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
        private void Form5_Load(object sender, EventArgs e)
        {
            kiiras();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt32(textBox2.Text);
                if (textBox2.Text == "" || textBox1.Text == "")
                {
                    MessageBox.Show("Tölts ki minden mezőt!");
                }
                else
                {
                    if (Convert.ToInt32(textBox2.Text) <= 0)
                    {
                        MessageBox.Show("A mennyiség nem lehet 0 vagy negatív");
                    }
                    else
                    {
                        string pizza = Convert.ToString(textBox1.Text);
                        int ar = Convert.ToInt32(textBox2.Text);
                        string updateQuery = $"INSERT INTO `pizza`(`Name`, `Price`) VALUES ('{pizza}','{ar}')";
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
            kiiras();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string updateQuery = $"DELETE FROM `pizza` where `ID`={listBox1.SelectedValue}";
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
    }
}
