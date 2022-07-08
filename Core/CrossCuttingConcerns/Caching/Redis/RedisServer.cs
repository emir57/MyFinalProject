using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Caching.Redis
{
    public class RedisServer
    {
        private ConnectionMultiplexer _connectionMultiplexer;
        private IDatabase _database;
        private string configurationString;
        private int _currentDatabaseId = 0;
        public RedisServer(IConfiguration configuration)
        {
            createRedisConfigurationString(configuration);

            _connectionMultiplexer = ConnectionMultiplexer.Connect(configurationString);
            _database = _connectionMultiplexer.GetDatabase(_currentDatabaseId);
        }

        private void createRedisConfigurationString(IConfiguration configuration)
        {
            string host = configuration.GetSection("RedisConfiguration:host").Value;
            string port = configuration.GetSection("RedisConfiguration:port").Value;
            configurationString = String.Format($"{host}:{port}");
        }
    }
}
