using System;

namespace MeetingApp.API.Dtos
{
    public class UserForListDto
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }        
        public string Gender { get; set; }
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActivity { get; set; }        
        public string City { get; set; }
        public string Country { get; set; }
        public string PhotoUrl { get; set; }
        
    }
}