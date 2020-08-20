using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BTS.SP.STC.Layouts
{
   
    public class UploadedFile
    {
       
        public string FolderName { get; set; }

        
        public int FileSize { get; set; }

        
        public string FileName { get; set; }
    }
}
