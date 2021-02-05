using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knowledge.Infrastructure.Models
{
    /// <summary>
    /// UserModel Request
    /// </summary>
    public class UserRequest
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Dob { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
    /// <summary>
    /// UserModel Response
    /// </summary>
    public class UserResponse
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Dob { get; set; }
        public string Phone { get; set; }
    }
}
