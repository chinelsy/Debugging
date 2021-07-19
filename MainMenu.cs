using Day5.Interfaces;

namespace Day5
{
    public class MainMenu : IMenu
    {
        private readonly ILogger _logger;
        public MainMenu(ILogger logger)
        {
            _logger = logger;
        }
        public void Display(string text)
        {


            _logger.LogLine(text);
        }
    }
}