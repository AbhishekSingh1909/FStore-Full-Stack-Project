using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fStore.Business;

    public class UserCreateDTO
    {
        public string Name {get;set;}
        public string Email {get;set;}
        public  string Password {get;set;}
        public string Avatar {get;set;} 
        
    }