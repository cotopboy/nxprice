using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Utilities.DataTypes.ExtensionMethods;
using Utilities.FileFormats.Zip;
using Utilities.IO;
using Utilities.IO.ExtensionMethods;
using Utilities.Math.ExtensionMethods;
using Utilities.Reflection.ExtensionMethods;
using Utilities.Serialization;



namespace nxprice_data
{
    public class FileDbEngine<T> where T : class, new()
    {
        private T db = new T ();
        private string fileDbPath;

        public FileDbEngine()
        {
            this.fileDbPath = DirectoryHelper.CombineWithCurrentExeDir("db.xml");
        }

        public FileDbEngine(string fileName,string extension)
        {
            this.fileDbPath = DirectoryHelper.CombineWithCurrentExeDir(fileName + extension);
        }

        public FileDbEngine(string fileDbPath)
        {
            this.fileDbPath = fileDbPath;
        }

        public void SetDB(T db)
        {
            this.db = db;
        }

        public T LoadFileDB()
        {

            FileInfo fileInfo = new FileInfo(this.fileDbPath);

            var bytes = fileInfo.ReadBinary();

            db = FormatterMg.XMLDerObjectFromBytes(typeof(T), bytes) as T;
            

            return db;
        }

        public void Save()
        {
            Byte[] bytes = this.db.ObjectToBlob();

            string x = FormatterMg.XMLSerObjectToString(this.db);

            FileInfo xf = new FileInfo(this.fileDbPath);

            xf.Save(x, Encoding.UTF8);

            string basePath = Path.GetDirectoryName(this.fileDbPath);
            string backupPath = Path.Combine(basePath,"db_backup");

            string zipFileName = "db_" + DateTime.Now.ToString("yyyy_MMdd_HHmm") + "_.zip";
            string zipFileFullpath = Path.Combine(backupPath, zipFileName);

            if (!Directory.Exists(backupPath)) Directory.CreateDirectory(backupPath);
            
            ZipFile zip = new ZipFile(zipFileFullpath, true);
            zip.AddFile(this.fileDbPath); 
            zip.Dispose();
           
        }


    }

    [Serializable]
    public class FileDb
    {
        public int TimerInterval { get; set; }

        public EmailConfig EmailConfig { get; set; }

        public WebProxyConfig WebProxy { get; set; }

        private List<TargetRecord> targetRecords;

        public List<TargetRecord> TargetRecords
        {
            get { return targetRecords; }
            set { targetRecords = value; }
        }
    }


    [Serializable]
    public class WebProxyConfig
    {        
        public bool IsEnablded{ get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    [Serializable]
    public class EmailConfig
    {
        public string GmailSmtp { get; set; }
        public int GmailPort { get; set; }
        public string GmailAccount { get; set; }
        public string GmailPassword { get; set; }
    }

}
