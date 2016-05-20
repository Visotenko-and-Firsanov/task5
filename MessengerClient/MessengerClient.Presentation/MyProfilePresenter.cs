using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessengerClient.Model;

namespace MessengerClient.Presentation
{
    public class MyProfilePresenter
    {
        private readonly IStorage _storage;
        private readonly IConection _conection;
        private readonly INameWindowView _nameWindow;
        private readonly IMainWindowView _mainWindow;


        public MyProfilePresenter(IStorage storage, IConection conection, INameWindowView nameWindow, IMainWindowView mainWindow)
        {
            _storage = storage;
            _conection = conection;
            _nameWindow = nameWindow;
            _mainWindow = mainWindow;
        }

        public void Initialize()
        {
            _nameWindow.LoadProfile += LoadProfile;
        }

        private void LoadProfile(object sender, EventArgs eventArgs)
        {
            MyProfile profile = _storage.Load(_nameWindow.Name) ?? new MyProfile { MyName = _nameWindow.Name, };

            _mainWindow.ContaktsSource = ContaсtsListPresenter.GetContaсtsViewModel(profile.MyContacts) as Collection<string>;
        }
    }
}
