﻿using System.Runtime.Serialization;

namespace GBL.Service.Api.Models.Request
{
    [DataContract]
    public class GetUserRequest
    {
        [DataMember]
        public string Username { get; set; }

        public GetUserRequest(string username)
        {
            this.Username = username;
        }
    }
}
