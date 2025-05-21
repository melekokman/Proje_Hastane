using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Proje_Hastane
{
    class sqlbaglantisi
    {
        public SqlConnection baglanti() //oluşturduğumuz methodun adı
        {
            SqlConnection baglan = new SqlConnection("Data Source=ANGEL\\SQL;Initial Catalog=HastaneProje;Integrated Security=True");
            baglan.Open();
            return baglan;
           
        
        
        }
    }
}
