using MySql.Data;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X500;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PizzériaAPP
{
    public partial class Form1 : Form
    {
        
        string connStr = "server=localhost;user=root;database=pizzeriaapp;port=3306;password=";
        public Form1()
        {
            InitializeComponent();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            if(EmailReg.Text!="" && PassReg.Text != "" && PassRegRep.Text != "")
            {
                if (PassReg.Text == PassRegRep.Text)
                {
                    string email = Convert.ToString(EmailReg.Text);
                    string password = Convert.ToString(PassReg.Text);
                    string updateQuery = $"INSERT INTO `felhasználó`(`Email`, `Jelszó`, `Admin`) VALUES ('{email}','{password}','0')";
                    MySqlConnection databaseConnection = new MySqlConnection(connStr);
                    MySqlCommand commandDatabase = new MySqlCommand(updateQuery, databaseConnection);
                    commandDatabase.CommandTimeout = 60;
                    MySqlDataReader reader;
                    try
                    {
                        databaseConnection.Open();
                        reader = commandDatabase.ExecuteReader();



                        databaseConnection.Close();
                        MessageBox.Show("Sikeres Regisztráció!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("A két jelszó nem egyezik!");
                }
            }
            else
            {
                MessageBox.Show("Tölts ki minden mezőt!");
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (Email.Text != "" && Password.Text != "")
            {
                if (PassReg.Text == PassRegRep.Text)
                {
                    string email = Convert.ToString(Email.Text);
                    string password = Convert.ToString(Password.Text);
                    string sql = $"SELECT `ID`,`Admin` FROM `felhasználó` WHERE `Email`='{email}' AND `Jelszó`='{password}'";
                    MySqlConnection conn = new MySqlConnection(connStr);
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rdr;
                    Dictionary<string, string> comboSource = new Dictionary<string, string>();
                    bool admin;
                    
                    try
                    {
                        

                        conn.Open();
                        
                        object obj = cmd.ExecuteScalar();
                        if (Convert.ToInt32(obj) != 0)
                        {
                            
                            rdr = cmd.ExecuteReader();
                            rdr.Read();
                            admin =Convert.ToBoolean(rdr[1]);
                            if (admin == true)
                            {
                                Form4 Admin = new Form4();
                                Admin.UserID = Convert.ToInt32(rdr[0]);
                                Admin.ShowDialog();
                            }
                            else
                            {
                                Main main = new Main();

                                main.UserID = Convert.ToInt32(rdr[0]);

                                main.ShowDialog();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Nincs ilyen felhasználó");
                        }    
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("A két jelszó nem egyezik!");
                }
            }
            else
            {
                MessageBox.Show("Tölts ki minden mezőt!");
            }
        }
    }
}
