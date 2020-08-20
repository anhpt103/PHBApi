using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.PHB.SERVICE.Helper
{
    public static class ReadNumberToText
    {
        private static int CapTi
        {
            get;
            set;
        }

        private static string t
        {
            get;
            set;
        }

        public static string ByWords(this decimal so)
        {
            string str = so.ToString("########################0");
            string str1 = "";
            try
            {
                if (str != "")
                {
                    str1 = (!(so == new decimal(0)) ? ReadNumberToText.DocSo(ReadNumberToText.TaoMangSo(str)) : "không");
                }
            }
            catch (Exception ex)
            {
                try
                {
                    str1 = ReadNumberToText.DocSo(ReadNumberToText.TaoMangSo(str));
                }
                catch (Exception ex2)
                {

                }
            }
            return ReadNumberToText.Viethoa(str1);
        }

        private static string Chuc(string chuc)
        {
            string str = "";
            char chr = chuc[1];
            int num = Convert.ToInt32(chr.ToString());
            int num1 = Convert.ToInt32(chuc);
            if (num1 < 20)
            {
                if (num1 == 10)
                {
                    str = "mười";
                }
                else if (num1 == 11)
                {
                    str = "mười một";
                }
                else if (num1 != 15)
                {
                    chr = chuc[1];
                    str = string.Concat("mười ", ReadNumberToText.DonVi(chr.ToString()));
                }
                else
                {
                    str = "mười lăm";
                }
            }
            else if (num == 1)
            {
                chr = chuc[0];
                str = string.Concat(ReadNumberToText.DonVi(chr.ToString()), " mươi mốt");
            }
            else if (num == 5)
            {
                chr = chuc[0];
                str = string.Concat(ReadNumberToText.DonVi(chr.ToString()), " mươi lăm");
            }
            else if (num != 0)
            {
                chr = chuc[0];
                string str1 = ReadNumberToText.DonVi(chr.ToString());
                chr = chuc[1];
                str = string.Format("{0} mươi {1}", str1, ReadNumberToText.DonVi(chr.ToString()));
            }
            else
            {
                chr = chuc[0];
                str = string.Concat(ReadNumberToText.DonVi(chr.ToString()), " mươi ");
            }
            if (chuc[0].ToString() == "0")
            {
                str = str.Replace(" mươi ", "");
                str = str.Replace("mười ", "");
            }
            return str;
        }

        private static string DocSo(string[] arr)
        {
            string str = "";
            string str1 = str;
            string str2 = str;
            string str3 = str;
            string str4 = str;
            string str5 = str;
            string str6 = str;
            string str7 = str;
            int length = (int)arr.Length;
            while (length > 0)
            {
                if ((ReadNumberToText.CapTi <= 0 ? false : length < (int)arr.Length))
                {
                    str3 = string.Concat(str3, " tỉ ");
                }
                string[] strArrays = arr[length - 1].Split(new char[] { ',' });
                string str8 = ReadNumberToText.Tram(strArrays[0].ToString());
                switch ((int)strArrays.Length)
                {
                    case 1:
                        {
                            str6 = string.Concat(str8, str3);
                            break;
                        }
                    case 2:
                        {
                            str5 = string.Format("{0} nghìn {1}{2}", str8, ReadNumberToText.Tram(strArrays[1].ToString()), str3);
                            break;
                        }
                    case 3:
                        {
                            int num = Convert.ToInt32(strArrays[0]);
                            str2 = str8;
                            if (num <= 0)
                            {
                                str2 = str8;
                            }
                            else
                            {
                                str2 = (num >= 100 ? string.Concat(str2, " triệu ") : string.Concat(str2.Replace(" lẻ ", ""), " triệu "));
                            }
                            str1 = ReadNumberToText.Tram(strArrays[1]);
                            if (Convert.ToInt32(strArrays[1]) > 0)
                            {
                                str1 = string.Concat(str1, " nghìn ");
                            }
                            str4 = string.Concat(str2, str1, ReadNumberToText.Tram(strArrays[2]), str3);
                            break;
                        }
                }
                length--;
                str7 = string.Concat(str6, str5, str4, str7);
                string str9 = "";
                str4 = str9;
                str5 = str9;
                str6 = str9;
                str1 = str9;
                str2 = str9;
            }
            ReadNumberToText.CapTi = 0;
            return str7;
        }

        private static string DonVi(string dv)
        {
            string str = "";
            string str1 = dv;
            if (str1 != null)
            {
                switch (str1)
                {
                    case "0":
                        {
                            str = "";
                            break;
                        }
                    case "1":
                        {
                            str = "một";
                            break;
                        }
                    case "2":
                        {
                            str = "hai";
                            break;
                        }
                    case "3":
                        {
                            str = "ba";
                            break;
                        }
                    case "4":
                        {
                            str = "bốn";
                            break;
                        }
                    case "5":
                        {
                            str = "năm";
                            break;
                        }
                    case "6":
                        {
                            str = "sáu";
                            break;
                        }
                    case "7":
                        {
                            str = "bảy";
                            break;
                        }
                    case "8":
                        {
                            str = "tám";
                            break;
                        }
                    case "9":
                        {
                            str = "chín";
                            break;
                        }
                }
            }
            return str;
        }

        private static string[] TaoMangSo(string nghin)
        {
            char chr;
            bool str;
            try
            {
                nghin = Convert.ToInt32(nghin).ToString();
            }
            catch (Exception exception)
            {
            }
            int num = 0;
            int num1 = num;
            int num2 = num;
            int num3 = num;
            int num4 = num;
            int length = nghin.Length;
            num2 = (length % 3 != 0 ? length + length / 3 : length + length / 3 - 1);
            string[] strArrays = new string[num2];
            num2--;
            while (length > 0)
            {
                if (num4 == 3)
                {
                    num4 = 1;
                    num3++;
                    if (num3 != 3)
                    {
                        strArrays[num2 - num1] = ",";
                    }
                    else
                    {
                        num3 = 0;
                        ReadNumberToText.CapTi = ReadNumberToText.CapTi + 1;
                        strArrays[num2 - num1] = "_";
                    }
                    num1++;
                    chr = nghin[length - 1];
                    strArrays[num2 - num1] = chr.ToString();
                }
                else
                {
                    num4++;
                    if (nghin[length - 1].ToString() == " ")
                    {
                        str = true;
                    }
                    else
                    {
                        chr = nghin[length - 1];
                        str = !(chr.ToString() != "\n");
                    }
                    if (!str)
                    {
                        chr = nghin[length - 1];
                        strArrays[num2 - num1] = chr.ToString();
                    }
                }
                num1++;
                length--;
            }
            ReadNumberToText.t = string.Join("", strArrays);
            return string.Join("", strArrays).Split(new char[] { '\u005F' });
        }

        private static string Tram(string tram)
        {
            char chr;
            string str = "";
            int num = Convert.ToInt32(tram);
            if (num == 0)
            {
                str = "";
            }
            else if (tram.Length == 1)
            {
                str = ReadNumberToText.DonVi(tram);
            }
            else if (tram.Length == 2)
            {
                str = ReadNumberToText.Chuc(tram);
            }
            else if (num % 100 == 0)
            {
                chr = tram[0];
                str = string.Concat(ReadNumberToText.DonVi(chr.ToString()), " trăm");
            }
            else if (num < 100)
            {
                str = string.Concat("lẻ ", ReadNumberToText.Chuc(tram.Substring(1).ToString()));
            }
            else if (!(tram[1].ToString() != "0"))
            {
                chr = tram[0];
                string str1 = ReadNumberToText.DonVi(chr.ToString());
                chr = tram[2];
                str = string.Format("{0} trăm lẻ {1}", str1, ReadNumberToText.DonVi(chr.ToString()));
            }
            else
            {
                chr = tram[0];
                str = string.Format("{0} trăm {1}", ReadNumberToText.DonVi(chr.ToString()), ReadNumberToText.Chuc(tram.Substring(1).ToString()));
            }
            return str;
        }

        private static string Viethoa(string str)
        {
            string str1;
            if (!string.IsNullOrEmpty(str))
            {
                char[] charArray = str.ToCharArray();
                charArray[0] = char.ToUpper(charArray[0]);
                str1 = new string(charArray);
            }
            else
            {
                str1 = str;
            }
            return str1;
        }
    }
}
