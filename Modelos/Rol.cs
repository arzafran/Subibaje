using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelos
{
    public class Rol
    {
        public int Id { get; set; }
        public TipoRol Tipo { get; set; }
        public Usuario Usuario { get; set; }
        public Establecimiento Establecimiento { get; set; }
        public NivelEducativo Nivel { get; set; }
        public DateTime Borrado { get; set; }
        public string Mostrar
        {
            get
            { 
                return this.Tipo.Nombre + (Establecimiento == null ? "" : " (" + this.Establecimiento.Nombre + " " + this.Nivel.Nombre + ")");
            }
        }

        public Rol(TipoRol tipo, Usuario usuario, Establecimiento establecimiento, NivelEducativo nivel)
        {
            Tipo = tipo;
            Usuario = usuario;
            Establecimiento = establecimiento;
            Nivel = nivel;
        }

        public Rol(int id, TipoRol tipo, Usuario usuario, Establecimiento establecimiento, NivelEducativo nivel, DateTime borrado)
        {
            Id = id;
            Tipo = tipo;
            Usuario = usuario;
            Establecimiento = establecimiento;
            Nivel = nivel;
            Borrado = borrado;
        }
    }
}
