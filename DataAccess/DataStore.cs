using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day5.DataAccess
{
     public class DataStore
    {

        private readonly IList<Bank> _africanBanks = new List<Bank>{
            new Bank{Id = 1, Name = "UBA", Region = "Western"},
            new Bank{Id = 1, Name = "KCB", Region = "Central"}
        };

        private readonly IList<Bank> _europeanBanks = new List<Bank>{
            new Bank{Id = 1, Name = "JKC", Region = "Eastern"},
            new Bank{Id = 1, Name = "OKC", Region = "Northern"}
        };

        private readonly IList<Bank> _asianBanks = new List<Bank>{
            new Bank{Id = 1, Name = "UBA", Region = "Western"},
            new Bank{Id = 1, Name = "KCB", Region = "Central"}
        };


        private readonly IList<User> _africans = new List<User>{
            new User{Id = 1, Name = "Ada Adekole", Country = "Nigeria"},
            new User{Id = 1, Name = "Kofi Menssa", Country = "Ghana"}
           
        };

        private readonly IList<User> _asians = new List<User> { 
            new User{Id = 1, Name = "Chang Ming", Country = "China"},
            new User{Id = 1, Name = "Chung Li", Country = "South Korea"}
           
        };
        
        private readonly IList<User> _europeans = new List<User>{
            new User{Id = 1, Name = "John Doe", Country = "Austria"},
            new User{Id = 1, Name = "Garry Woods", Country = "FinLand"}
           
        };
       

        public DataStore()
        {
            Banks = new Dictionary<string, IList<Bank>> {{"Africa", _africanBanks}, {"Europe", _europeanBanks}, {"Asia", _asianBanks}};
            Users = new Dictionary<string, IList<User>> {{"Africa", _africans}, {"Europe", _europeans}, {"Asia", _asians}};
        }

        public Dictionary<string, IList<Bank>> Banks { get; }
        public Dictionary<string, IList<User>> Users { get; }
    }
}
