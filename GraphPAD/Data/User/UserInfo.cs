using System;
using System.ComponentModel;

namespace GraphPAD.Data.User
{
    public class UserInfo
    {
        private static string _email;
        public static string Email {
            get => _email; 
            
            set 
            {        
                _email = value;
            } 
        }

        public static string Token { get; set; }

        public static string Message { get; set; }

        public static string Role { get; set; }


    }
}
