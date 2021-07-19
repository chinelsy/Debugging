using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Day5.Interfaces;

namespace Day5
{
    class UserMenu : IMenu
    {
        private readonly ILogger _logger;
        public UserMenu(ILogger logger)
        {
            _logger = logger;
        }
        public void Display(string text)
        {


            _logger.LogLine(text);
        }
    }
}
