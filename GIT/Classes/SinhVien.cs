using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace GIT
{
    class SinhVien
    {
        private string _MaSV;
        private string _HoDem;

        public string MaSV
        {
            get { return this._MaSV; }

            set
            {
                this._MaSV = value;
                connectionSQL.ExecuteUpdate("SinhVien", "MaSV", "MaSV", new object[] { MaSV, MaSV });
            }
        }
        public string HoDem
        {
            get { return this._HoDem; }
            set
            {
                this._HoDem = value;
                connectionSQL.ExecuteUpdate("SinhVien", "MaSV", "HoDem", new object[] { MaSV, HoDem });
            }
        }

        public SinhVien(string MaSV)
        {
            DataTable dt = connectionSQL.ExecuteSelect("SinhVien", "MaSV", new object[] { MaSV });
            this._MaSV = dt.Rows[0]["MaSV"].ToString();
            this._HoDem = dt.Rows[0]["HoDem"].ToString();
        }

        //public DataTable Select()
        //{
        //    return connectionSQL.ExecuteSelect ("SinhVien",)
        //}
    }
}
