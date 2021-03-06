﻿using System.ComponentModel.DataAnnotations;

namespace BGL.Web.ViewModels
{
    public class UserRepositorySearchViewModel : BaseViewModel
    {
        [Required(ErrorMessage ="You must enter a username in order to make a search.")]
        public string Username { get; set; }
    }
}