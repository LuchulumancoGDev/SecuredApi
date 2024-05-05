﻿using Microsoft.AspNetCore.Identity;

namespace SecureApiWithJwtToken.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
