using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _010100627701_TTKTS_DT01
{
    public class TIN : IComparable<TIN>
    {
        int iHeCoSo;
        double dXacSuatTin, dLuongTinRieng;
        string sTenTin;

        public double DXacSuatTin { get => dXacSuatTin; set => dXacSuatTin = value; }
        public string STenTin { get => sTenTin; set => sTenTin = value; }
        public double DLuongTinRieng { get => dLuongTinRieng; set => dLuongTinRieng = value; }
        public int IHeCoSo { get => iHeCoSo; set => iHeCoSo = value; }

        public int CompareTo(TIN other)
        {
            return this.DXacSuatTin.CompareTo(other.DXacSuatTin);
        }
    }
}
