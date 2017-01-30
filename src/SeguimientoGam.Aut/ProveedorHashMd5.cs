using SeguimientoGam.Modelos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeguimientoGam.Aut
{
    public class ProveedorHashMd5 : IProveedorHash
    {
        public void ObtenerHash(string data, out string hash, out string salt)
        {
            var md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes(data);

            byte[] hashBytes = md5.ComputeHash(inputBytes);


            // step 2, convert byte array to hex string

            var sb = new StringBuilder();

            for (int i = 0; i < hashBytes.Length; i++)

            {

                sb.Append(hashBytes[i].ToString("x2"));
            }

            hash = sb.ToString();
            salt = "";
        }

        public bool VerificarHash(string data, string hash, string salt = "")
        {
            if (string.IsNullOrEmpty(data) || string.IsNullOrEmpty(hash)) return false;
            string hashVerificar;
            string saltVerificar;
            ObtenerHash(data, out hashVerificar, out saltVerificar);
            return hash == hashVerificar;
        }
    }
}
