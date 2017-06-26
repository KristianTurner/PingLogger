using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pinglogger
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime now = new DateTime();
            string outputString = String.Empty;
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/k ping 8.8.8.8 -t",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
            using (StreamWriter w = File.AppendText("C:/temp/pinglog.txt"))
            {
                w.WriteLine("-------New Session-------");
            }
            proc.Start();
            while (!proc.StandardOutput.EndOfStream)
            {
                string line = proc.StandardOutput.ReadLine();
                now = DateTime.Now;
                outputString = "" + now + ": " + line;
                Console.WriteLine(outputString);
                using (StreamWriter w = File.AppendText("C:/temp/pinglog.txt"))
                {
                    w.WriteLine(outputString);
                }
                // do something with line
            }
        }
    }
}
