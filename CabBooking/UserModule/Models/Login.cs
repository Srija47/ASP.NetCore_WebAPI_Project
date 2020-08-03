using System.ComponentModel.DataAnnotations;

namespace UserModule.Models
{
    public class Login
    {
        
        public int Id{get;set;}
        [Required]
        public string Name{get;set;}
        [Required]
        public string Password{get;set;}
    }
}
