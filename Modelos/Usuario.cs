using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Modelos
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public int Dni { get; set; }
        public string Password { get; set; }
        public DateTime Borrado { get; set; }
        public List<Rol> ListaRoles { get; set; }
        public string Roles
        {
            get 
            {
                List<string> devolver = new List<string>();

                foreach (Rol oRol in ListaRoles)
                {
                    devolver.Add(oRol.Mostrar);
                }

                return string.Join(", ", devolver);
            }
        }

        public override string ToString()
        {
            return this.Nombre;
        }

        public Usuario(string nombre, int dni, string email)
        {
            Nombre = nombre;
            Dni = dni;
            Email = email;
            Password = Dni.ToString();
        }

        public Usuario(string nombre, int dni, string email, int id, DateTime borrado)
        {
            Nombre = nombre;
            Dni = dni;
            Email = email;
            Id = id;
            Borrado = borrado;
            Password = Dni.ToString();
        }

        public Usuario(string nombre, int dni, string email, int id, DateTime borrado, List<Rol> roles)
        {
            Nombre = nombre;
            Dni = dni;
            Email = email;
            Id = id;
            Borrado = borrado;
            Password = Dni.ToString();
            ListaRoles = roles;
        }
    }
}
