using CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OcjenskaVjezbaKolekcije
{
    public partial class Form1 : Form
    {
        List<Osoba> listaOsoba = new List<Osoba>();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnUnesi_Click(object sender, EventArgs e)
        {
            try
            {
                Osoba osoba = new Osoba(txtBox1.Text, txtBox2.Text, Convert.ToInt32(txtBox3.Text), comboBox1.Text);
                listaOsoba.Add(osoba);
                txtBox1.Clear();
                txtBox2.Clear();
                txtBox3.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnIspisi_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.Text = ("Ime\t\t" + "Prezime\t\t" + "Godina rodenja\t\t" + "Spol\t\t" + "Dodatak");
            textBox1.AppendText(Environment.NewLine);
            foreach (Osoba osoba in listaOsoba)
            {
                textBox1.AppendText(osoba.ToString());
                textBox1.AppendText(Environment.NewLine);
            }
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            foreach (Osoba osoba in listaOsoba)
            {
                if (osoba.Spol == "M")
                {
                    osoba.Dodatak = "Ima brkove";
                }
                else
                {
                    osoba.Dodatak = "Nema brkove";
                }
            }
        }

        private void btnSprei_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    
                    myStream.Close();
                }
            }
            using (var stream = File.Open("osobeFile.csv", FileMode.Append))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(listaOsoba);
            }
        }
    }

    class Osoba
    {
        string ime, prezime;
        int godRod;
        string spol, dodatak;

        public Osoba(string ime, string prezime, int godRod, string spol)
        {
            this.Ime = ime;
            this.Prezime = prezime;
            this.GodRod = godRod;
            this.Spol = spol;
        }

        public string Ime { get => ime; set => ime = value; }
        public string Prezime { get => prezime; set => prezime = value; }
        public int GodRod { get => godRod; set => godRod = value; }
        public string Spol { get => spol; set => spol = value; }
        public string Dodatak { get => dodatak; set => dodatak = value; }

        public override string ToString()
        {
            string ispis = (this.ime + "\t\t" + this.prezime + "\t\t" + this.godRod + "\t\t\t" + this.spol + "\t\t" + this.dodatak);
            return ispis;
        }
    }
}
