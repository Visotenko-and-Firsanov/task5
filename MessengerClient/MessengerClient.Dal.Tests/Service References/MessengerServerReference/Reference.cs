﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MessengerClient.Dal.Tests.MessengerServerReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="User", Namespace="http://schemas.datacontract.org/2004/07/MessengerDal")]
    [System.SerializableAttribute()]
    public partial class User : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private MessengerClient.Dal.Tests.MessengerServerReference.Friend[] ContactsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.KeyValuePair<string, string>[] MessageBySenderField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public MessengerClient.Dal.Tests.MessengerServerReference.Friend[] Contacts {
            get {
                return this.ContactsField;
            }
            set {
                if ((object.ReferenceEquals(this.ContactsField, value) != true)) {
                    this.ContactsField = value;
                    this.RaisePropertyChanged("Contacts");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.KeyValuePair<string, string>[] MessageBySender {
            get {
                return this.MessageBySenderField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageBySenderField, value) != true)) {
                    this.MessageBySenderField = value;
                    this.RaisePropertyChanged("MessageBySender");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Friend", Namespace="http://schemas.datacontract.org/2004/07/MessengerDal")]
    [System.SerializableAttribute()]
    public partial class Friend : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool OnlineField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Online {
            get {
                return this.OnlineField;
            }
            set {
                if ((this.OnlineField.Equals(value) != true)) {
                    this.OnlineField = value;
                    this.RaisePropertyChanged("Online");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MessengerServerReference.IMessengerServerService", CallbackContract=typeof(MessengerClient.Dal.Tests.MessengerServerReference.IMessengerServerServiceCallback))]
    public interface IMessengerServerService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessengerServerService/UploadUserData", ReplyAction="http://tempuri.org/IMessengerServerService/UploadUserDataResponse")]
        MessengerClient.Dal.Tests.MessengerServerReference.User UploadUserData(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessengerServerService/UploadUserData", ReplyAction="http://tempuri.org/IMessengerServerService/UploadUserDataResponse")]
        System.Threading.Tasks.Task<MessengerClient.Dal.Tests.MessengerServerReference.User> UploadUserDataAsync(string username);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IMessengerServerService/SendMessage")]
        void SendMessage(string usernameSenders, string usernameReceiver, string message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IMessengerServerService/SendMessage")]
        System.Threading.Tasks.Task SendMessageAsync(string usernameSenders, string usernameReceiver, string message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessengerServerService/FindUser", ReplyAction="http://tempuri.org/IMessengerServerService/FindUserResponse")]
        MessengerClient.Dal.Tests.MessengerServerReference.Friend FindUser(string requiredUsername);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessengerServerService/FindUser", ReplyAction="http://tempuri.org/IMessengerServerService/FindUserResponse")]
        System.Threading.Tasks.Task<MessengerClient.Dal.Tests.MessengerServerReference.Friend> FindUserAsync(string requiredUsername);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessengerServerService/UploadingUserData", ReplyAction="http://tempuri.org/IMessengerServerService/UploadingUserDataResponse")]
        void UploadingUserData(MessengerClient.Dal.Tests.MessengerServerReference.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMessengerServerService/UploadingUserData", ReplyAction="http://tempuri.org/IMessengerServerService/UploadingUserDataResponse")]
        System.Threading.Tasks.Task UploadingUserDataAsync(MessengerClient.Dal.Tests.MessengerServerReference.User user);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMessengerServerServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IMessengerServerService/LoadMessage")]
        void LoadMessage(string usernameReceiver, string message);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IMessengerServerService/ContactsStatusUpdate")]
        void ContactsStatusUpdate(string usernameReceiver, bool online);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMessengerServerServiceChannel : MessengerClient.Dal.Tests.MessengerServerReference.IMessengerServerService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MessengerServerServiceClient : System.ServiceModel.DuplexClientBase<MessengerClient.Dal.Tests.MessengerServerReference.IMessengerServerService>, MessengerClient.Dal.Tests.MessengerServerReference.IMessengerServerService {
        
        public MessengerServerServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public MessengerServerServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public MessengerServerServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public MessengerServerServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public MessengerServerServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public MessengerClient.Dal.Tests.MessengerServerReference.User UploadUserData(string username) {
            return base.Channel.UploadUserData(username);
        }
        
        public System.Threading.Tasks.Task<MessengerClient.Dal.Tests.MessengerServerReference.User> UploadUserDataAsync(string username) {
            return base.Channel.UploadUserDataAsync(username);
        }
        
        public void SendMessage(string usernameSenders, string usernameReceiver, string message) {
            base.Channel.SendMessage(usernameSenders, usernameReceiver, message);
        }
        
        public System.Threading.Tasks.Task SendMessageAsync(string usernameSenders, string usernameReceiver, string message) {
            return base.Channel.SendMessageAsync(usernameSenders, usernameReceiver, message);
        }
        
        public MessengerClient.Dal.Tests.MessengerServerReference.Friend FindUser(string requiredUsername) {
            return base.Channel.FindUser(requiredUsername);
        }
        
        public System.Threading.Tasks.Task<MessengerClient.Dal.Tests.MessengerServerReference.Friend> FindUserAsync(string requiredUsername) {
            return base.Channel.FindUserAsync(requiredUsername);
        }
        
        public void UploadingUserData(MessengerClient.Dal.Tests.MessengerServerReference.User user) {
            base.Channel.UploadingUserData(user);
        }
        
        public System.Threading.Tasks.Task UploadingUserDataAsync(MessengerClient.Dal.Tests.MessengerServerReference.User user) {
            return base.Channel.UploadingUserDataAsync(user);
        }
    }
}
