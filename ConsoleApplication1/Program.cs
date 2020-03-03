using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warc;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var warc = new WarcFile(@"D:\Projects\WarcSharp-master\warcFiles\net-domino.com-2020-02-29-ae999b8a-00000.warc.gz");
           
            var dirPath = AppDomain.CurrentDomain.BaseDirectory +
                @"site";
            Directory.CreateDirectory(dirPath);
            foreach(var f in warc.FilesystemEntries)
            {
                var bytes = f.ExtractResponse();
                var path = dirPath + f.Filename;
                path = path.Replace('?', '_');
                var fileInfo = new FileInfo(path);
                var subDirPath = fileInfo.DirectoryName;
                if(fileInfo.Extension == "")
                {
                    path = path + ".html";
                }
                if (!Directory.Exists(subDirPath))
                    Directory.CreateDirectory(subDirPath);
                File.WriteAllBytes(path, bytes);
            }
           
        }
    }
}
