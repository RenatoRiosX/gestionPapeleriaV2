namespace ClientePapeleria.Models
{
	public class LoginUsuario
	{
		public string Email { get; set; }
		public string Token { get; set; }
		public string Rol { get; set; }
		public int EncargadoId { get; set; }
	}
}
