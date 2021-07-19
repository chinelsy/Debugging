using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Day5.DataAccess;
using Day5.Helpers;
using Day5.Interfaces;

namespace Day5.Services
{
   public class BankService : IService<Bank>
   {
       private readonly DataStore _dataStore;
       private readonly ILogger _logger = new Logger();
       
       public BankService()
       {
           _dataStore = new DataStore();
       }

       
        public void Add(string key, Bank entity)
        {
            if (_dataStore.Banks.ContainsKey(key))
            {
                _dataStore.Banks[key].Add(entity);
            }
            else
            {
                _logger.Log("Operation Failed!, Key does not exist!");
            }   
        }

        public void Update(string key, int index, Bank entity)
        {
            if (_dataStore.Banks.ContainsKey(key))
            {
                _dataStore.Banks[key][index] = entity;
            }
            else
            {
                _logger.Log("Operation Failed!, Key does not exist!");
            }
        }

        public Bank Get(string key, int index)
        {
            return _dataStore.Banks.ContainsKey(key) ? _dataStore.Banks[key][index] : null;
        }

        public Dictionary<string, IList<Bank>> GetAll()
        {
            return _dataStore.Banks;
        }

        public IEnumerable<Bank> GetAll(string key)
        {
            return _dataStore.Banks.ContainsKey(key) ? _dataStore.Banks[key] : null;
        }
    }
}
