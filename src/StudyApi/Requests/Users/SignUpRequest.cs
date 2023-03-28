using System;

namespace StudyApi.Requests.Users
{
    public class SignUpRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool ConsentRegistration { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}