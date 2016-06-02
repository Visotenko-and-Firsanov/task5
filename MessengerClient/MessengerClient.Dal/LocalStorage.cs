using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using MessengerClient.Model;
using MessengerClient.Presentation;

namespace MessengerClient.Dal
{
    public class LocalStorage : IStorage
    {
        private string _sourcePath;

        public LocalStorage(string name)
        {
            _sourcePath = GeneretaPath(name);
        }
        public LocalStorage()
        { }

        public void Save(MyProfile profile)
        {
            IFormatter formatter = new BinaryFormatter();

            var stream = new FileStream(_sourcePath, FileMode.Create);

            formatter.Serialize(stream, profile);

            stream.Close();
        }

        public MyProfile Load(string name)
        {
            _sourcePath = GeneretaPath(name);

            IFormatter formatter = new BinaryFormatter();


            var stream = new FileStream(_sourcePath, FileMode.OpenOrCreate);

            MyProfile profile = new MyProfile();

            try
            {
                profile = (MyProfile)formatter.Deserialize(stream);
            }
            catch (SerializationException e)
            {

                profile.MyName = name;
            }

            stream.Close();

            return profile;
        }

        private string GeneretaPath(string name)
        {
            StringBuilder path = new StringBuilder();

            path.Append("../../../users/");
            path.Append(name);

            return path.ToString();
        }
    }
}