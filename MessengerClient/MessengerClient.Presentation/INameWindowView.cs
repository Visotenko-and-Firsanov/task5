using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerClient.Presentation
{
    public interface INameWindowView
    {
        event EventHandler LoadProfile;
        string Name { get; set; }
    }
}
