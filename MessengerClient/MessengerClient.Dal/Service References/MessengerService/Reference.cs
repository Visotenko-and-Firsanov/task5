﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MessengerClient.Dal.MessengerService
{
    using System.Runtime.Serialization;
    using System;


    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "Profile", Namespace = "http://schemas.datacontract.org/2004/07/MessengerServer")]
    [System.SerializableAttribute()]
    public partial class Profile : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged
    {

        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MessengerClient.Dal.MessengerService.Contact[] ContactsField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool OnlineField;

        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public MessengerClient.Dal.MessengerService.Contact[] Contacts
        {
            get
            {
                return this.ContactsField;
            }
            set
            {
                if ((object.ReferenceEquals(this.ContactsField, value) != true))
                {
                    this.ContactsField = value;
                    this.RaisePropertyChanged("Contacts");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                if ((object.ReferenceEquals(this.NameField, value) != true))
                {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Online
        {
            get
            {
                return this.OnlineField;
            }
            set
            {
                if ((this.OnlineField.Equals(value) != true))
                {
                    this.OnlineField = value;
                    this.RaisePropertyChanged("Online");
                }
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "Contact", Namespace = "http://schemas.datacontract.org/2004/07/MessengerServer")]
    [System.SerializableAttribute()]
    public partial class Contact : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged
    {

        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool OnlineField;

        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool StateField;

        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message
        {
            get
            {
                return this.MessageField;
            }
            set
            {
                if ((object.ReferenceEquals(this.MessageField, value) != true))
                {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                if ((object.ReferenceEquals(this.NameField, value) != true))
                {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Online
        {
            get
            {
                return this.OnlineField;
            }
            set
            {
                if ((this.OnlineField.Equals(value) != true))
                {
                    this.OnlineField = value;
                    this.RaisePropertyChanged("Online");
                }
            }
        }

        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool State
        {
            get
            {
                return this.StateField;
            }
            set
            {
                if ((this.StateField.Equals(value) != true))
                {
                    this.StateField = value;
                    this.RaisePropertyChanged("State");
                }
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null))
            {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "MessengerService.IMessengerServerService", CallbackContract = typeof(MessengerClient.Dal.MessengerService.IMessengerServerServiceCallback))]
    public interface IMessengerServerService
    {

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IMessengerServerService/UploadUserData", ReplyAction = "http://tempuri.org/IMessengerServerService/UploadUserDataResponse")]
        MessengerClient.Dal.MessengerService.Profile UploadUserData(string username);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IMessengerServerService/UploadUserData", ReplyAction = "http://tempuri.org/IMessengerServerService/UploadUserDataResponse")]
        System.Threading.Tasks.Task<MessengerClient.Dal.MessengerService.Profile> UploadUserDataAsync(string username);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IMessengerServerService/RefreshUserData", ReplyAction = "http://tempuri.org/IMessengerServerService/RefreshUserDataResponse")]
        void RefreshUserData(string username);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IMessengerServerService/RefreshUserData", ReplyAction = "http://tempuri.org/IMessengerServerService/RefreshUserDataResponse")]
        System.Threading.Tasks.Task RefreshUserDataAsync(string username);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IMessengerServerService/SendMessage", ReplyAction = "http://tempuri.org/IMessengerServerService/SendMessageResponse")]
        void SendMessage(string usernameSenders, string usernameReceiver, string message);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IMessengerServerService/SendMessage", ReplyAction = "http://tempuri.org/IMessengerServerService/SendMessageResponse")]
        System.Threading.Tasks.Task SendMessageAsync(string usernameSenders, string usernameReceiver, string message);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IMessengerServerService/FindUser", ReplyAction = "http://tempuri.org/IMessengerServerService/FindUserResponse")]
        void FindUser(string requiredUsername);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IMessengerServerService/FindUser", ReplyAction = "http://tempuri.org/IMessengerServerService/FindUserResponse")]
        System.Threading.Tasks.Task FindUserAsync(string requiredUsername);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IMessengerServerService/UserStatusCheck", ReplyAction = "http://tempuri.org/IMessengerServerService/UserStatusCheckResponse")]
        void UserStatusCheck(string requiredUsername);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IMessengerServerService/UserStatusCheck", ReplyAction = "http://tempuri.org/IMessengerServerService/UserStatusCheckResponse")]
        System.Threading.Tasks.Task UserStatusCheckAsync(string requiredUsername);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IMessengerServerService/UploadingUserData", ReplyAction = "http://tempuri.org/IMessengerServerService/UploadingUserDataResponse")]
        void UploadingUserData(string username);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IMessengerServerService/UploadingUserData", ReplyAction = "http://tempuri.org/IMessengerServerService/UploadingUserDataResponse")]
        System.Threading.Tasks.Task UploadingUserDataAsync(string username);
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMessengerServerServiceCallback
    {

        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://tempuri.org/IMessengerServerService/LoadMessage")]
        void LoadMessage(string usernameReceiver, string message);
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMessengerServerServiceChannel : MessengerClient.Dal.MessengerService.IMessengerServerService, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MessengerServerServiceClient : System.ServiceModel.DuplexClientBase<MessengerClient.Dal.MessengerService.IMessengerServerService>, MessengerClient.Dal.MessengerService.IMessengerServerService
    {

        public MessengerServerServiceClient(System.ServiceModel.InstanceContext callbackInstance) :
                base(callbackInstance)
        {
        }

        public MessengerServerServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) :
                base(callbackInstance, endpointConfigurationName)
        {
        }

        public MessengerServerServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) :
                base(callbackInstance, endpointConfigurationName, remoteAddress)
        {
        }

        public MessengerServerServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
                base(callbackInstance, endpointConfigurationName, remoteAddress)
        {
        }

        public MessengerServerServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
                base(callbackInstance, binding, remoteAddress)
        {
        }

        public MessengerClient.Dal.MessengerService.Profile UploadUserData(string username)
        {
            return base.Channel.UploadUserData(username);
        }

        public System.Threading.Tasks.Task<MessengerClient.Dal.MessengerService.Profile> UploadUserDataAsync(string username)
        {
            return base.Channel.UploadUserDataAsync(username);
        }

        public void RefreshUserData(string username)
        {
            base.Channel.RefreshUserData(username);
        }

        public System.Threading.Tasks.Task RefreshUserDataAsync(string username)
        {
            return base.Channel.RefreshUserDataAsync(username);
        }

        public void SendMessage(string usernameSenders, string usernameReceiver, string message)
        {
            base.Channel.SendMessage(usernameSenders, usernameReceiver, message);
        }

        public System.Threading.Tasks.Task SendMessageAsync(string usernameSenders, string usernameReceiver, string message)
        {
            return base.Channel.SendMessageAsync(usernameSenders, usernameReceiver, message);
        }

        public void FindUser(string requiredUsername)
        {
            base.Channel.FindUser(requiredUsername);
        }

        public System.Threading.Tasks.Task FindUserAsync(string requiredUsername)
        {
            return base.Channel.FindUserAsync(requiredUsername);
        }

        public void UserStatusCheck(string requiredUsername)
        {
            base.Channel.UserStatusCheck(requiredUsername);
        }

        public System.Threading.Tasks.Task UserStatusCheckAsync(string requiredUsername)
        {
            return base.Channel.UserStatusCheckAsync(requiredUsername);
        }

        public void UploadingUserData(string username)
        {
            base.Channel.UploadingUserData(username);
        }

        public System.Threading.Tasks.Task UploadingUserDataAsync(string username)
        {
            return base.Channel.UploadingUserDataAsync(username);
        }
    }
}
