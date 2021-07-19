using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Day5.Interfaces;

namespace Day5.Helpers
{
   public  class Logger : ILogger
    {
        public void LogLine(string message)
        {
            Console.WriteLine(message);
        }

        public void Log(string message)
        {
            Console.Write(message);
        }
    }
}
