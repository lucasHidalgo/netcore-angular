using System.ComponentModel.DataAnnotations;

namespace MeetingApp.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string NombreUsuario { get; set; }      

        [Required]
        public string Password { get; set; }
    }
}