
//using SPFM.Core.Util;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using Microsoft.SharePoint;



namespace BTS.SP.STC.Layouts
{
    public class UploadFile : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if(context.Request.HttpMethod == "POST")
            {
                List<Message> lstmsg = new List<Message>();
                HttpFileCollection filecollection = context.Request.Files;
                if (filecollection != null && filecollection.Count > 0)
                {
                    for (int i = 0; i < filecollection.Count; i++)
                    {
                       Message msg = new Message();
                        try
                        {

                            string file = filecollection.AllKeys[i];
                            HttpPostedFile hpf = context.Request.Files[file] as HttpPostedFile;
                            if (hpf.ContentLength > 10485760)
                            {
                                msg.Error = true;
                                msg.Title = "Dung lượng file vượt quá mức cho phép là 10Mb !";
                                msg.ID = i;
                            }
                            else
                            {
                                file = file.Substring(0, file.IndexOf('-'));
                                UploadedFile uploadedFile = new UploadedFile();
                                string SaveLocation = HttpContext.Current.Server.MapPath("~/") + "\\UploadFile\\" + file;
                                System.IO.Directory.CreateDirectory(SaveLocation);
                                long value = DateTime.Now.Ticks;

                                string filenamefull = hpf.FileName.Replace(" ", "_");
                                string filetype = filenamefull.Substring(filenamefull.LastIndexOf('.') + 1);
                                string filename = filenamefull.Substring(0, filenamefull.LastIndexOf('.'));
                                string filenamesave = filename + "_TM" + value + "." + filetype;
                                string filesave = SaveLocation + "\\" + filenamesave;
                                hpf.SaveAs(filesave);
                                uploadedFile.FolderName = file;
                                uploadedFile.FileName = filenamesave;
                                uploadedFile.FileSize = hpf.ContentLength;
                                msg.Error = false;
                                msg.Title = "Upload thành công !";
                                msg.ID = i;
                                msg.Data = filenamesave;
                            }

                        }
                        catch (Exception ex)
                        {
                            msg.Error = true;
                            msg.Title = "Lỗi lưu file đính kèm !";
                            msg.ID = i;

                        }
                       lstmsg.Add(msg);
                    }
                }
                context.Response.ContentType = "application/json";
                System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                oSerializer.MaxJsonLength = 2147483644;
                context.Response.Write(oSerializer.Serialize(lstmsg));
            } 
            
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }




    }
}