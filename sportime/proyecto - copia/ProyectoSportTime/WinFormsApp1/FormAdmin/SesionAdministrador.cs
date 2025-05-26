using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForm.FormAdmin
{
    public static class SesionAdministrador
    {
        public static int? Admin_ID { get; private set; }

        public static void IniciarSesion(int adminId)
        {
            Admin_ID = adminId;
        }

        public static void CerrarSesion()
        {
            Admin_ID = null;
        }
    }
}
