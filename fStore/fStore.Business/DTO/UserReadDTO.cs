using fStore.Core;

namespace fStore.Business;

    public class UserReadDTO
    {
        public string Name {get;set;}
        public string Email {get;set;}
        public string Avatar {get;set;} 
        public Role Role {get;set;}

        public UserReadDTO Convert (User user)
        {
            return new  UserReadDTO 
            {
                Name = user.Name,
                Email = user.Email,
                Avatar = user.Avatar,
                Role = user.Role
            };
        }
    }
