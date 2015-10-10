using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace accesoDatos
{
    public abstract class CapaDatosAbstractaSingleton<S> where S : CapaDatosAbstractaSingleton<S>, new()
    {
        private static S _instance;

        public static S Instance()
        {
            if (_instance == null)
            {
                _instance = new S();
            }

            return _instance;
        }
    }
}
