namespace SeguimientoGam.Modelos.Interfaces
{

	public interface IProveedorHash
	{
		void ObtenerHash(string data, out string hash, out string salt);
		bool VerificarHash(string data, string hash, string salt = "");
	}

}

