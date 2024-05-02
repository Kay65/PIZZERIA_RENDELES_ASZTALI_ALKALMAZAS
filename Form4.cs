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
    public partial class Form4 : Form
    {
        public int UserID;
        string connStr = "server=localhost;user=root;database=pizzeriaapp;port=3306;password=";
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main main = new Main();

            main.UserID = UserID;

            main.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form5 pizza = new Form5();
            pizza.ShowDialog();

        }
    }
}
