﻿using OdevApi.Base;
using System.ComponentModel.DataAnnotations;

namespace OdevApi.Dto;

public class TokenRequest
{
    [Required]
    [MaxLength(125)]
    [UserNameAttribute]
    [Display(Name = "User Name")]
    public string UserName { get; set; }

    [Required]
    [PasswordAttribute]
    public string Password { get; set; }
}
