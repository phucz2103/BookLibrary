﻿using MediatR;

namespace BookLibrary.API.Features.Auth.Register
{
    public class RegisterCommand :IRequest<bool>
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
