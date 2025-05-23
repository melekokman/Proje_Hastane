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
    public partial class FrmDoktorGiris: Form
    {
        public FrmDoktorGiris()
        {
            InitializeComponent();
        }

        private void LnkUyeOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmDoktorBilgiDuzenle fr = new FrmDoktorBilgiDuzenle();
            fr.Show();
            this.Hide();
        }

        private void FrmDoktorGiris_Load(object sender, EventArgs e)
        {
            

        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select * from Tbl_Doktorlar where DoktorTC=@p1 and DoktorSifre=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",MskTC.Text);
            komut.Parameters.AddWithValue("@p2",TxtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                FrmDoktorDetay fr = new FrmDoktorDetay();
                fr.TCno=MskTC.Text;
                fr.Show();
                this.Hide();
            }
            else { 
            
                MessageBox.Show("Hatalı TC veya Şifre","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);

            }
            bgl.baglanti().Close();
        }
    }
}
