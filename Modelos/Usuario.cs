using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelos
{
    public class Usuario
    {
        public int Id { get ; set; }
        public string Nombre { get; set; }
        public int Dni { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Usuario(string nombre, string password, string mail, int dni) 
        {
            Nombre = nombre;
            Email = mail;
            Password = password;
            Dni = dni;
        }
    }
}
