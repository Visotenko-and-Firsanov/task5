using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessengerClient.Model;

namespace MessengerClient.Presentation
{
    public interface IStorage
    {
        void Save(MyProfile profile);

        MyProfile Load(string name);
    }
}
