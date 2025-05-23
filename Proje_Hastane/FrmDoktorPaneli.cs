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
    public partial class FrmDoktorPaneli: Form
    {
        public FrmDoktorPaneli()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmDoktorPaneli_Load(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from Tbl_Doktorlar ", bgl.baglanti());
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;


            //Branşları Comboboxa Aktarma 
            SqlCommand komut2 = new SqlCommand("select BransAd from Tbl_Branslar ", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);

            }
            bgl.baglanti().Close();
        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Doktorlar (DoktorAd,DoktorSoyad,DoktorBrans,DoktorTC,DoktorSifre) values (@d1,@d2,@d3,@d4,@d5)",bgl.baglanti());
            komut.Parameters.AddWithValue("@d1",TxtAd.Text);
            komut.Parameters.AddWithValue("@d2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@d3", CmbBrans.Text);
            komut.Parameters.AddWithValue("@d4", MskTC.Text);
            komut.Parameters.AddWithValue("@d5", TxtSifre.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);




        }

       

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            CmbBrans.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            MskTC.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtSifre.Text = dataGridView1.Rows[secilen ].Cells[5].Value.ToString();

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut1 = new SqlCommand("delete from Tbl_Doktorlar where DoktorTC = @p1", bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1",MskTC.Text);
            komut1.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("Update Tbl_Doktorlar set DoktorAd=@d1,DoktorSoyad=@d2,DoktorBrans=@d3,DoktorSifre=@d5 where DoktorTC=@d4", bgl.baglanti());
            komut2.Parameters.AddWithValue("@d1", TxtAd.Text);
            komut2.Parameters.AddWithValue("@d2", TxtSoyad.Text);
            komut2.Parameters.AddWithValue("@d3", CmbBrans.Text);
            komut2.Parameters.AddWithValue("@d4", MskTC.Text);
            komut2.Parameters.AddWithValue("@d5", TxtSifre.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Güncellendi ","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
        }
    }
}
