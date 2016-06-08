using System;
using System.Collections.Generic;

namespace MessengerDal
{
    public interface IStorage : IDisposable
    {
        /// <summary>
        /// сохранят пользователя в базе данных
        /// </summary>
        /// <param name="user">пользователь</param>
        void Save(User user);

        /// <summary>
        /// Обновляет статус пользователя в базе данных
        /// </summary>
        /// <param name="userName">имя пользователя</param>
        /// <param name="status">его статус на данный момент</param>
        /// <returns></returns>
        List<string> UpdateStatus(string userName, bool status);

        /// <summary>
        /// отправляет сообщение в базу данных,которое было написано,когда пользователь был ofline
        /// </summary>
        /// <param name="usernameSenders">имя отправителя</param>
        /// <param name="usernameReceiver">имя получателя</param>
        /// <param name="message">сообщение</param>
        void SendMessage(string usernameSenders, string usernameReceiver, string message);

        /// <summary>
        /// Смотрит статус пользователя
        /// </summary>
        /// <param name="userName">имя пользователя</param>
        /// <returns></returns>
        bool CheckStatus(string userName);

        /// <summary>
        /// Загружает пользователей из базы данных
        /// </summary>
        /// <param name="userName">имя пользоателя</param>
        /// <returns></returns>
        User Load(string userName);

        /// <summary>
        /// Осуществляет поск пользователей в базе данных
        /// </summary>
        /// <param name="userName">имя пользователя</param>
        /// <returns></returns>
        List<Friend> FindUser(string userName);
    }
}
