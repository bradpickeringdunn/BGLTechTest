﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BGL.Web.GitService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GitUserDto", Namespace="http://schemas.datacontract.org/2004/07/GBL.Service.Api.Models.DTO")]
    [System.SerializableAttribute()]
    public partial class GitUserDto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="GitService.IGitService")]
    public interface IGitService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGitService/LoadGitUser", ReplyAction="http://tempuri.org/IGitService/LoadGitUserResponse")]
        BGL.Web.GitService.GitUserDto LoadGitUser();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IGitService/LoadGitUser", ReplyAction="http://tempuri.org/IGitService/LoadGitUserResponse")]
        System.Threading.Tasks.Task<BGL.Web.GitService.GitUserDto> LoadGitUserAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IGitServiceChannel : BGL.Web.GitService.IGitService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GitServiceClient : System.ServiceModel.ClientBase<BGL.Web.GitService.IGitService>, BGL.Web.GitService.IGitService {
        
        public GitServiceClient() {
        }
        
        public GitServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public GitServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GitServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public GitServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public BGL.Web.GitService.GitUserDto LoadGitUser() {
            return base.Channel.LoadGitUser();
        }
        
        public System.Threading.Tasks.Task<BGL.Web.GitService.GitUserDto> LoadGitUserAsync() {
            return base.Channel.LoadGitUserAsync();
        }
    }
}