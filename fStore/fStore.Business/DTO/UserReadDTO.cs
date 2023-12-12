using fStore.Core;

namespace fStore.Business;

    public class UserReadDTO
    {
        public string Name {get;set;}
        public string Email {get;set;}
        public string Avatar {get;set;} 
        public Role Role {get;set;}
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
