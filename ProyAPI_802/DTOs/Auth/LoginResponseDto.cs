using System.Collections.Generic;

namespace ProyAPI_802.DTOs.Auth
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string Nombre { get; set; }
        public List<string> Roles { get; set; }
    }
}
