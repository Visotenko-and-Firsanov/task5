﻿using System;
using System.Collections.Generic;
using System.Text;
using MessengerClient.Model;

namespace MessengerClient.Presentation
{
    public static class MyProfileEdditor
    {
        public static MyProfile DeleteContact(MyProfile profile, string contactName)
        {
            var i = FindIndex(profile.MyContacts, contactName);

            if (i == -1)
                return profile;

            profile.MyContacts.RemoveAt(i);

            return profile;
        }

        public static MyProfile AddMessage(MyProfile profile, string contactName, string message)
        {
            var i = FindIndex(profile.MyContacts, contactName);

            if (i == -1)
                throw new Exception("Нет нужного контакта для отправки сообщения");

            StringBuilder newMessage = new StringBuilder();

            newMessage.Append(message);

            profile.MyContacts[i].MessageHistory += ReformatMessage.Reformat(profile.MyName, message);

            return profile;
        }

        public static int FindIndex(List<Contact> list, string contactName)
        {
            int index = -1;

            for (var i = 0; i < list.Count; ++i)
            {
                if (list[i].Name != contactName) continue;
                index = i;
                break;
            }

            return index;
        }

        public static MyProfile ChangeStatus(MyProfile profile, string contactName, bool status)
        {

            var i = FindIndex(profile.MyContacts, contactName);

            if (i == -1)
                return profile;

            profile.MyContacts[i].Online = status;

            return profile;
        }
    }
}