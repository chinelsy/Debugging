using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Day5.Helpers;
using Day5.Interfaces;

namespace Day5
{
   public class BankMenu : IMenu
   {
       private readonly ILogger _logger;
       public BankMenu(ILogger logger)
       {
           _logger = logger;
       }
       public  void Display(string text)
       {
           

           _logger.LogLine(text);
       }
   }
}
