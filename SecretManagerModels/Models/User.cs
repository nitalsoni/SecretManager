using System;
using System.Collections.Generic;

namespace SecretManagerModels.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Secret { get; set; } = null!;
        public bool? IsActive { get; set; }
    }
}
