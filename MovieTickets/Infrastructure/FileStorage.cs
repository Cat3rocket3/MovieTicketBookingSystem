using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Text.Json;
using System.IO;

namespace MovieTickets.Infrastructure
{   

    internal class FileStorage
    {
        private readonly string path;

        public FileStorage(string path)
        {
            this.path = path;
        }

        public TheaterStorage Load()
        {
            if (!File.Exists(path))
            {
                return new TheaterStorage();
            }

            var json = File.ReadAllText(path);

            var storage = JsonSerializer.Deserialize<TheaterStorage>(json);
            if (storage == null)
                throw new Exception("Deserialization return null.");

            return storage;

        }
        public void Save(TheaterStorage storage)
        {
            var json = JsonSerializer.Serialize(storage);
            File.WriteAllText(path, json);

        }
    }
}
