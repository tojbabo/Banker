using Banker.DATA;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banker.UTIL
{
    public class FileMaster
    {
        public async static Task<string> Read(string filename)
        {
            string data = null;

            var task = Task.Run(() =>
            {
                try
                {
                    data = File.ReadAllText(Static.PATH + filename);
                }
                catch (IOException e)
                {
                    Debug.WriteLine("there is no file");
                }
            });
            task.Wait();

            return data;
        }
        public async static void Write(string filename, string data, bool append = true)
        {
            _ = Task.Run(() =>
             {
                 DirectoryInfo dir = new DirectoryInfo(Static.PATH);
                 if (!dir.Exists) dir.Create();

                 if (append)
                 {
                     File.AppendAllText(Static.PATH + filename, $"{data}\n");
                 }
                 else
                 {
                     File.WriteAllText(Static.PATH + filename, data);
                 }
             });
        }
    }
}
