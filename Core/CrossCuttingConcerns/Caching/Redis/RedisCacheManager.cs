using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching.Redis
{
    public class RedisCacheManager : ICacheManager
    {
        private readonly RedisServer _redisServer;

        public RedisCacheManager(RedisServer redisServer)
        {
            _redisServer = redisServer;
        }

        public void Add(string key, object value, int duration)
        {
            var jsonData = JsonConvert.SerializeObject(value);
            _redisServer.Database.StringSet(key, jsonData, TimeSpan.FromMinutes(duration));
        }

        public T Get<T>(string key)
        {
            var redisData = _redisServer.Database.StringGet(key);
            return JsonConvert.DeserializeObject<T>(redisData);
        }

        public object Get(string key)
        {
            var redisData = _redisServer.Database.StringGet(key);
            return JsonConvert.DeserializeObject(redisData);
        }

        public bool IsAdd(string key)
        {
            return _redisServer.Database.KeyExists(key);
        }

        public void Remove(string key)
        {
            _redisServer.Database.KeyDelete(key);
        }

        public void RemoveByPattern(string pattern)
        {
            throw new NotImplementedException();
        }
    }
}
