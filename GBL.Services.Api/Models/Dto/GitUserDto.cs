﻿using System;
using System.Runtime.Serialization;

namespace GBL.Service.Api.Models.Dto
{
    [DataContract]
    public class UserDto
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string AvatarUrl { get; set; }

        [DataMember]
        public string Location { get; set; }

    }
}
