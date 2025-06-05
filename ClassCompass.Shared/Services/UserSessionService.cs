﻿using ClassCompass.Shared.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ClassCompass.Shared.Services
{
    public class UserSessionService
    {
        public object? CurrentUser { get; set; }
        public bool IsLoggedIn => CurrentUser != null;

        public void SetCurrentUser(object user)
        {
            CurrentUser = user;
        }

        public void ClearCurrentUser()
        {
            CurrentUser = null;
        }

        public T? GetCurrentUser<T>() where T : class
        {
            return CurrentUser as T;
        }
    }
}