using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.Mongo.DomainModel.Settings
{
    public class MongoSettings
    {
        public string Host { get; set; } = string.Empty;
        public string Port { get; set; } = string.Empty;
        public string ConnectionString => $"mongodb://{Host}:{Port}";
    }
}
