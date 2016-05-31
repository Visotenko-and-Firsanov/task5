using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using MySql.Data.MySqlClient;

namespace MessengerServer
{
    public class MessengerServerService : IMessengerServerService
    {
        private readonly IStorage _dbBase;

        public MessengerServerService()
        {
            using (var mySqlConnection = new MySqlConnection("server=localhost;port=3306;database=messageserver;uid=root;password=1234;"))
            {
                using (_dbBase = new StorageFromDbBase(mySqlConnection, false))
                {
                }
                mySqlConnection.Open();
                //try
                //{
                //    // DbConnection that is already opened
                //    using (_dbBase = new StorageFromDbBase(mySqlConnection, false))
                //    {


                //        // Passing an existing transaction to the context
                //        context.Database.UseTransaction(transaction);

                //        // DbSet.AddRange
                //        List<Car> cars = new List<Car>();

                //        cars.Add(new Car { Manufacturer = "Nissan", Model = "370Z", Year = 2012 });
                //        cars.Add(new Car { Manufacturer = "Ford", Model = "Mustang", Year = 2013 });
                //        cars.Add(new Car { Manufacturer = "Chevrolet", Model = "Camaro", Year = 2012 });
                //        cars.Add(new Car { Manufacturer = "Dodge", Model = "Charger", Year = 2013 });

                //        context.Cars.AddRange(cars);

                //        context.SaveChanges();
                //    }

                //}
                _dbBase.Save(new Profile{Name = "Roma", Online = true, ProifileId = 1});
                var user = _dbBase.Load("Roma");

            }
        }

        public Profile UploadUserData(string username)
        {
            var result = _dbBase.LoadOneProfile(username);
            if (result != null)
                return result;
            UserRegistration(username);
            return new Profile();
        }

        private void UserRegistration(string username)
        {
           // var prof  = new Profile {Name = username, Online = true, Contacts = null};
            //_dbBase.Save(prof);
        }

        public List<string> RefreshUserData(string username)
        {
			return  _dbBase.GetOnlineMembers(username);
        }

        public void SendMessage(string usernameSenders, string usernameReceiver, string message)
        {
			if(_dbBase.CheckStatus(usernameReceiver))
                OperationContext.Current.GetCallbackChannel<IMessengerServerServiceCallback>().LoadMessage(usernameReceiver, message);
            _dbBase.SendMessage(usernameSenders, usernameReceiver, message);
        }

        public bool FindUser(string requiredUsername)
        {
            return _dbBase.LoadOneProfile(requiredUsername) != null;
        }

        public bool UserStatusCheck(string requiredUsername)
        {
            return _dbBase.CheckStatus(requiredUsername);
        }

        public void UploadingUserData(string username)
        {
            throw new NotImplementedException();
        }
    }
}
