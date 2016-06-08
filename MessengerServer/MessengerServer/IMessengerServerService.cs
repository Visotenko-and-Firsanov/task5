using System.Collections.Generic;
using System.ServiceModel;
using MessengerDal;

namespace MessengerServer
{
    [ServiceContract(CallbackContract = typeof (IMessengerServerServiceCallback))]
    public interface IMessengerServerService
    {
        /// <summary>
        /// Загрузка данных пользователя в бд
        /// </summary>
        /// <param name="username">пользователь чьи данные загружаются</param>
        /// <returns></returns>
        [OperationContract]
        User UploadUserData(string username);

        /// <summary>
        /// реализует отправку сообщения
        /// </summary>
        /// <param name="usernameSenders">отправитель</param>
        /// <param name="usernameReceiver">получатель</param>
        /// <param name="message">сообщение</param>
        [OperationContract(IsOneWay = true)]
        void SendMessage(string usernameSenders, string usernameReceiver, string message);

        /// <summary>
        /// осуществляет поиск пользователей в списке друзей
        /// </summary>
        /// <param name="requiredUsername">имя пользователя которое надо найти</param>
        /// <returns></returns>
        [OperationContract]
        List<Friend> FindUser(string requiredUsername);

        /// <summary>
        /// Выгрузка данных из бд
        /// </summary>
        /// <param name="user"></param>
        [OperationContract]
        void UploadingUserData(User user);
    }

    [ServiceContract]
    public interface IMessengerServerServiceCallback
    {
        /// <summary>
        /// реализует загрузку сообщений
        /// </summary>
        /// <param name="usernameReceiver">отправитель</param>
        /// <param name="message">сообщение</param>
        [OperationContract(IsOneWay = true)]
        void LoadMessage(string usernameReceiver, string message);

        /// <summary>
        /// обновляет состояние контактов
        /// </summary>
        /// <param name="usernameReceiver">получатель</param>
        /// <param name="online">статус</param>
        [OperationContract(IsOneWay = true)]
        void ContactsStatusUpdate(string usernameReceiver, bool online);
    }
}