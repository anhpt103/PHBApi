﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.ENTITY.Rp.BIEU2CP1
{
    public class PHB_BIEU2CP1Vm
    {

        public class BaoCaoDetail
        {
            public BaoCaoDetail()
            {
                lstCOL = new List<ColObject>();
            }
            public string TEN_CHI_TIEU { get; set; }
            public string STT_CHI_TIEU { get; set; }
            public int SAP_XEP { get; set; }
            public decimal? TONG { get; set; }
            public List<ColObject> lstCOL { get; set; }
        }


        public class LoaiKhoanItem
        {
            public LoaiKhoanItem()
            {
                KhoanItem = new List<string>();
            }
            public string LoaiItem { get; set; }
            public List<string> KhoanItem { get; set; }
        }
        public class DataRes
        {
            public DataRes()
            {
                Body = new List<BaoCaoDetail>();
                Header = new List<LoaiKhoanItem>();
            }
            public List<BaoCaoDetail> Body { get; set; }
            public List<LoaiKhoanItem> Header { get; set; }
            public int NAM_BC { get; set; }
        }
        public class TemplateReport
        {
            public string MA_SO { get; set; }
            public string TEN_CHI_TIEU { get; set; }
            public string STT_CHI_TIEU { get; set; }
            public int SAP_XEP { get; set; }
        }

       public class InputPara
        {
            public string REFID { get; set; }           
            public string MA_CHUONG { get; set; }
            public string MA_QHNS { get; set; }
            public string TEN_QHNS { get; set; }
            public int NAM_BC { get; set; }
            public int KY_BC { get; set; }
          
        }

        public class ColObject : IEquatable<ColObject>
        {
            public ColObject(string _khoan, decimal _GiaTri)
            {
                KHOAN = _khoan;
                GIA_TRI = _GiaTri;
               
            }

            public string KHOAN { get; set; }
            public decimal GIA_TRI { get; set; }
           

            public bool Equals(ColObject other)
            {
                return !ReferenceEquals(null, other) && KHOAN.Equals(other);
            }            
            public override int GetHashCode()
            {
                return KHOAN.GetHashCode();
            }
        }

        public class EditModel
        {
            public string REFID { get; set; }
            public List<PHB_BIEU2CP1_DETAIL> LstAdd { get; set; }
            public List<PHB_BIEU2CP1_DETAIL> LstEdit { get; set; }
            public List<string> LstKhoanDelete { get; set; }
            public List<string> LstLoaiDelete { get; set; }
        }

        public class ContentData
        {
            public ContentData()
            {
                lstDetail = new List<PHB_BIEU2CP1_DETAIL>();
            }
            public string MA_LOAI { get; set; }
            public string MA_KHOAN { get; set; }
            public List<PHB_BIEU2CP1_DETAIL> lstDetail { get; set; }
        }
    }
}