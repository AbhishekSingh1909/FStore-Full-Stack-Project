using System.ComponentModel.DataAnnotations;

namespace fStore.Core;
    public class User : BaseEntity
    {
        [Required]
        public string Name{get;set;}
        [Required]
        public string Email {get;set;}
        [Required]
        public  string Password {get;set;}
        public string? Avatar { get; set; }
        [Required]
        public Role Role {get;set;}
    }
