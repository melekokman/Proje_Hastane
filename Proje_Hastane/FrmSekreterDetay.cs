﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proje_Hastane
{
    public partial class FrmSekreterDetay: Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }
        public string TCno;

        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = TCno;

            //Ad Soyad

            SqlCommand komut = new SqlCommand("Select SekreterAdSoyad from Tbl_Sekreter where SekreterTC=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", LblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();
           while (dr.Read())
            {
                LblAdSoyad.Text = dr[0].ToString();
            }
           bgl.baglanti().Close();

            //Branşları Datagride Aktarma
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Branslar ", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //Doktorları Listeye Aktarma 
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select (DoktorAd+ ' '+DoktorSoyad) as Doktorlar,DoktorBrans as Branşlar from Tbl_Doktorlar ", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;


            //Branşı Comboboxa Aktarma
            SqlCommand komut2 = new SqlCommand("select BransAd from Tbl_Branslar ", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);

            }
            bgl.baglanti().Close();
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutkaydet = new SqlCommand("insert into Tbl_Randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values (@r1,@r2,@r3,@r4)", bgl.baglanti());
            komutkaydet.Parameters.AddWithValue("@r1",MskTarih.Text);
            komutkaydet.Parameters.AddWithValue("@r2",MskSaat.Text);
            komutkaydet.Parameters.AddWithValue("@r3",CmbBrans.Text);   
            komutkaydet.Parameters.AddWithValue("@r4",CmbDoktor.Text);
            komutkaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Oluşturuldu");
        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();

            SqlCommand komut = new SqlCommand("Select DoktorAd,DoktorSoyad from Tbl_Doktorlar where DoktorBrans=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", CmbBrans.Text);
            SqlDataReader dr=komut.ExecuteReader();
            while (dr.Read())
            {
                CmbDoktor.Items.Add(dr[0]+" " + dr[1]);
            }
            bgl.baglanti().Close();
        }

        private void BtnOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Duyurular (duyuru) values (@d1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@d1",RchDuyuruOlustur.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close() ;
            MessageBox.Show("Duyuru Oluşturuldu");
        }

        private void BtnDoktorPaneli_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli drp = new FrmDoktorPaneli();
            drp.Show();
            this.Hide();
        }

        private void BtnBransPaneli_Click(object sender, EventArgs e)
        {
            FrmBransPaneli frb = new FrmBransPaneli();
            frb.Show();
            this.Hide();
        }

        private void BtnRandevuListe_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi fr = new FrmRandevuListesi();
            fr.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmDuyurular fr = new FrmDuyurular();
            fr.Show();
            this.Hide();
        }
    }
}
