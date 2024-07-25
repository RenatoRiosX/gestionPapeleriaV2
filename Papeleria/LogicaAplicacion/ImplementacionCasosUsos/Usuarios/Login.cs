using LogicaAplicacion.InterfacesCasosUsos.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaAplicacion.DTOs.UsuarioDTOs;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using LogicaAplicacion.DTOsMappers.UsuarioMappers;
using System.Collections;
using LogicaNegocio.InterfacesEntidades;
using LogicaNegocio.Excepciones;

namespace LogicaAplicacion.ImplementacionCasosUsos.Usuarios
{
    public class Login : ILogin, IVerificarContrasenia
    {
        private IRepositorioUsuarios _repositorioUsuarios;
        public Login(IRepositorioUsuarios repo)
        {
            _repositorioUsuarios = repo;
        }

		public UsuarioConRolDto Ejecutar(string email, string psw )
		{

			var usr= _repositorioUsuarios.LoginUsuario(email);
            if(usr==null || !VerificarContrasenia(psw, usr.Contrasenia))
            {
                return null;
            }
            else
            {
                return UsuarioConRolDtoMapper.toDto(usr);
			}
		}


		public bool VerificarContrasenia(string contrasenia, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(contrasenia, hash);
        }

    }
}
