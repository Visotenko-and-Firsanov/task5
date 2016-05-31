using System.Collections.Generic;
using System.ServiceModel;

namespace MessengerServer
{
    [ServiceContract (CallbackContract = typeof(IMessengerServerServiceCallback))]
    public interface IMessengerServerService
    {
        [OperationContract]
        Profile UploadUserData(string username);//log in

        [OperationContract]
        List<string> RefreshUserData(string username);

        [OperationContract(IsOneWay = true)]
        void SendMessage(string usernameSenders, string usernameReceiver, string message);

        [OperationContract]
        bool FindUser(string requiredUsername);

        [OperationContract]
        bool UserStatusCheck(string requiredUsername);

        //[OperationContract(IsOneWay = true)]
        //void GetMessage(string usernameReceiver);

        [OperationContract]
        void UploadingUserData(string username);
    }

    [ServiceContract]
    public interface IMessengerServerServiceCallback
    { 
        [OperationContract(IsOneWay = true)]
        void LoadMessage(string usernameReceiver, string message);

        [OperationContract(IsOneWay = true)]
        void ContactsStatusUpdate(string usernameReceiver, bool online);
    }
}
