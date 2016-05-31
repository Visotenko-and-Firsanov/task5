using System.ServiceModel;

namespace MessengerServer
{
    [ServiceContract (CallbackContract = typeof(IMessengerServerServiceCallback))]
    public interface IMessengerServerService
    {
        [OperationContract]
        void UploadUserData(string username);

        [OperationContract]
        void RefreshUserData(string username);

        [OperationContract]
        void SendMessage(string usernameSenders, string usernameReceiver, string message);

        [OperationContract]
        void FindUser(string requiredUsername);

        [OperationContract]
        void UserStatusCheck(string requiredUsername);

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
    }
}
