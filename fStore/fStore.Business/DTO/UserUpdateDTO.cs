using System.ComponentModel;

namespace fStore.Business;
    public class UserUpdateDTO
    {
        public string Name {get;set;}
        public string Avatar {get;set;}
        public DateTime? UpdatedAt { get; set; } 
    }
