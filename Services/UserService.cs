using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Day5.DataAccess;
using Day5.Helpers;
using Day5.Interfaces;

namespace Day5
{
    class UserService : IService<User>
    {
        private readonly DataStore _dataStore;
        private readonly ILogger _logger = new Logger();

        public UserService()
        {
            _dataStore = new DataStore();
        }


        public void Add(string key, User entity)
        {
            if (_dataStore.Users.ContainsKey(key))
            {
                _dataStore.Users[key].Add(entity);
            }
            else
            {
                _logger.Log("Operation Failed!, Key does not exist!");
            }
        }

        public void Update(string key, int index, User entity)
        {
            if (_dataStore.Users.ContainsKey(key))
            {
                _dataStore.Users[key][index] = entity;
            }
            else
            {
                _logger.Log("Operation Failed!, Key does not exist!");
            }
        }

        public User Get(string key, int index)
        {
            return _dataStore.Users.ContainsKey(key) ? _dataStore.Users[key][index] : null;
        }

        public Dictionary<string, IList<User>> GetAll()
        {
            return _dataStore.Users;
        }

        public IEnumerable<User> GetAll(string key)
        {
            return _dataStore.Users.ContainsKey(key) ? _dataStore.Users[key] : null;
        }
    }
}

