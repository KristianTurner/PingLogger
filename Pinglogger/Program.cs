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
            string ip = "8.8.8.8";
            Console.Write("Ping IP [8.8.8.8]: ");
            string input = Console.ReadLine();
            if (input != "")
            {
                ip = input;
            } 
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/k ping {ip} -t",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
            if (!Directory.Exists("C:/temp/"))
            {
                Directory.CreateDirectory("C:/temp/");
            }
            using (StreamWriter w = File.AppendText("C:/temp/pinglog.txt"))
            {
                w.WriteLine("-------New Session-------");
            }
            proc.Start();
            while (!proc.StandardOutput.EndOfStream)
            {
                string line = proc.StandardOutput.ReadLine();
                var now = DateTime.Now;
                var outputString = "" + now + ": " + line;
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
