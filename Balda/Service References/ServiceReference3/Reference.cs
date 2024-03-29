﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Balda.ServiceReference3 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference3.IService1", CallbackContract=typeof(Balda.ServiceReference3.IService1Callback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/CreateGame", ReplyAction="http://tempuri.org/IService1/CreateGameResponse")]
        void CreateGame(string id, string word);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/CreateGame", ReplyAction="http://tempuri.org/IService1/CreateGameResponse")]
        System.Threading.Tasks.Task CreateGameAsync(string id, string word);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ConnectToGame", ReplyAction="http://tempuri.org/IService1/ConnectToGameResponse")]
        string ConnectToGame(string id, string nickname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ConnectToGame", ReplyAction="http://tempuri.org/IService1/ConnectToGameResponse")]
        System.Threading.Tasks.Task<string> ConnectToGameAsync(string id, string nickname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/FindAllGame", ReplyAction="http://tempuri.org/IService1/FindAllGameResponse")]
        string[] FindAllGame();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/FindAllGame", ReplyAction="http://tempuri.org/IService1/FindAllGameResponse")]
        System.Threading.Tasks.Task<string[]> FindAllGameAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/LeaveGame", ReplyAction="http://tempuri.org/IService1/LeaveGameResponse")]
        void LeaveGame();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/LeaveGame", ReplyAction="http://tempuri.org/IService1/LeaveGameResponse")]
        System.Threading.Tasks.Task LeaveGameAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetLetter", ReplyAction="http://tempuri.org/IService1/SetLetterResponse")]
        void SetLetter(string[][] list, string word, string nickname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetLetter", ReplyAction="http://tempuri.org/IService1/SetLetterResponse")]
        System.Threading.Tasks.Task SetLetterAsync(string[][] list, string word, string nickname);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/Miss", ReplyAction="http://tempuri.org/IService1/MissResponse")]
        void Miss();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/Miss", ReplyAction="http://tempuri.org/IService1/MissResponse")]
        System.Threading.Tasks.Task MissAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Callback {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/Turn", ReplyAction="http://tempuri.org/IService1/TurnResponse")]
        void Turn(string[][] list, string word);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ConnectionNewPlayer", ReplyAction="http://tempuri.org/IService1/ConnectionNewPlayerResponse")]
        void ConnectionNewPlayer(string nickname);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : Balda.ServiceReference3.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.DuplexClientBase<Balda.ServiceReference3.IService1>, Balda.ServiceReference3.IService1 {
        
        public Service1Client(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public Service1Client(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public Service1Client(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void CreateGame(string id, string word) {
            base.Channel.CreateGame(id, word);
        }
        
        public System.Threading.Tasks.Task CreateGameAsync(string id, string word) {
            return base.Channel.CreateGameAsync(id, word);
        }
        
        public string ConnectToGame(string id, string nickname) {
            return base.Channel.ConnectToGame(id, nickname);
        }
        
        public System.Threading.Tasks.Task<string> ConnectToGameAsync(string id, string nickname) {
            return base.Channel.ConnectToGameAsync(id, nickname);
        }
        
        public string[] FindAllGame() {
            return base.Channel.FindAllGame();
        }
        
        public System.Threading.Tasks.Task<string[]> FindAllGameAsync() {
            return base.Channel.FindAllGameAsync();
        }
        
        public void LeaveGame() {
            base.Channel.LeaveGame();
        }
        
        public System.Threading.Tasks.Task LeaveGameAsync() {
            return base.Channel.LeaveGameAsync();
        }
        
        public void SetLetter(string[][] list, string word, string nickname) {
            base.Channel.SetLetter(list, word, nickname);
        }
        
        public System.Threading.Tasks.Task SetLetterAsync(string[][] list, string word, string nickname) {
            return base.Channel.SetLetterAsync(list, word, nickname);
        }
        
        public void Miss() {
            base.Channel.Miss();
        }
        
        public System.Threading.Tasks.Task MissAsync() {
            return base.Channel.MissAsync();
        }
    }
}
