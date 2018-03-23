using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TFTPClient
{
    public partial class Form1 : Form
    {
        string SunucuIP,SDosyaAdi,DosyaYolu, DosyaAdi;
        string tempDir = "C:\\Temp\\dir.txt";




        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text (*.txt)|*.txt|Word (*.doc)|*.doc|Html (*.htm)|*.htm";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)  //dialog kutusunda tamam tıklandıysa
            {
                foreach (string i in saveFileDialog1.FileName.Split('\\'))
                {
                    if (i.Contains(".txt")) { DosyaAdi = i; } //.jpg den önceki ifadeyi al.Yani dosya adını DosyaAdi değişkenine ata
                    else if (i.Contains(".htm")) { DosyaAdi = i; }
                    else if (i.Contains(".doc")) { DosyaAdi = i; }
                    else {  }
                }
                DosyaYolu = saveFileDialog1.FileName;//Tam Adress
                textBox1.Text = DosyaYolu;
            }
            else
            {

                MessageBox.Show("Herhangi bir Dosya Seçilmedi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(SunucuIP==null)
            {
                MessageBox.Show("Sunucu IP boş olamaz!!");
            }
            else if (SDosyaAdi == null)
            {
                MessageBox.Show("Yandaki listeden bir dosya seçmelisiniz");
            }
            else
            {
                TFTPClient tftp = new TFTPClient(SunucuIP);
                tftp.Get(SDosyaAdi, DosyaYolu);
                textBox1.Text = "";
                MessageBox.Show("Dosya başarıyla kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (SunucuIP != null)
            {
                TFTPClient tftp = new TFTPClient(SunucuIP);
            tftp.Put(DosyaAdi, DosyaYolu);
                    if (!listView1.Items.ContainsKey(DosyaAdi))
                {


                    StreamWriter Dosya = File.AppendText(tempDir);//dosyayı açtık.
                    Dosya.WriteLine(DosyaAdi);
                    Dosya.Close();

                    tftp.Put("dir.txt", tempDir);
                }
                listView1.Clear();
                tftp.Get("dir.txt", tempDir);

                StreamReader dosya = File.OpenText(tempDir);//dosyayı açtık.
                string siradaki_satir = dosya.ReadLine();// ilk satırı okutuyoruz ve alt satıra geçiyoruz.
                while (siradaki_satir != null) //satır boş olana kadar okutuyoruz
                {
                    listView1.Items.Add(siradaki_satir);//okunan veriyi listboxa eklettik.
                    siradaki_satir = dosya.ReadLine(); //satırı bidaha okuttuk.
                }
                dosya.Close();
                textBox2.Text = "";
            MessageBox.Show("Dosya başarıyla sunucuya gönderildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
            else
            { MessageBox.Show("Sunucu IP boş olamaz!!"); }
}



        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            SDosyaAdi = listView1.SelectedItems[0].Text.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox3.Enabled == true)
            {
                SunucuIP = textBox3.Text;
                textBox3.Enabled = false;
            }
            else
            { textBox3.Enabled = true; }

            listView1.Clear();
            TFTPClient tftp = new TFTPClient(SunucuIP);
            tftp.Get("dir.txt", tempDir);

            StreamReader dosya = File.OpenText(tempDir);//dosyayı açtık.
            string siradaki_satir = dosya.ReadLine();// ilk satırı okutuyoruz ve alt satıra geçiyoruz.
            while (siradaki_satir != null) //satır boş olana kadar okutuyoruz
            {
                listView1.Items.Add(siradaki_satir);//okunan veriyi listboxa eklettik.
                siradaki_satir = dosya.ReadLine(); //satırı bidaha okuttuk.
            }
            dosya.Close();


        }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text (*.txt)|*.txt|Word (*.doc)|*.doc|Html (*.htm)|*.htm";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)  //dialog kutusunda tamam tıklandıysa
            {
                foreach (string i in openFileDialog1.FileName.Split('\\'))
                {
                    {
                        if (i.Contains(".txt")) { DosyaAdi = i; } //.jpg den önceki ifadeyi al.Yani dosya adını DosyaAdi değişkenine ata
                        else if (i.Contains(".htm")) { DosyaAdi = i; }
                        else if (i.Contains(".doc")) { DosyaAdi = i; }
                        else { }
                    }
                    DosyaYolu = openFileDialog1.FileName;
                    textBox2.Text = DosyaYolu;
                }
            }
            else
            {

                MessageBox.Show("Herhangi bir Dosya Seçilmedi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }


    }
}

