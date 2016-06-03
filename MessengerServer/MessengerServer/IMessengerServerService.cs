using System.ServiceModel;
using MessengerDal;
using User = MessengerDal.User;

namespace MessengerServer
{
    [ServiceContract(CallbackContract = typeof(IMessengerServerServiceCallback))]
    public interface IMessengerServerService
    {
        [OperationContract]
        User UploadUserData(string username);//log in

       // [OperationContract]
       // List<string> RefreshUserData(string username);

        [OperationContract(IsOneWay = true)]
        void SendMessage(string usernameSenders, string usernameReceiver, string message);

        [OperationContract]
        Friend FindUser(string requiredUsername);

        //[OperationContract]
       // bool UserStatusCheck(string requiredUsername);

        //[OperationContract(IsOneWay = true)]
        //void GetMessage(string usernameReceiver);

        [OperationContract]
        void UploadingUserData(User user);
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
