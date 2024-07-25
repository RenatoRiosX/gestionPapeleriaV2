using LogicaAplicacion.DTOs.UsuarioDTOs;
using LogicaNegocio.Entidades;
using LogicaNegocio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.DTOsMappers.UsuarioMappers
{
	public class UsuarioConRolDtoMapper
	{
		public static UsuarioConRolDto toDto(Usuario usuario)
		{
			string rol;
			if (usuario is Administrador)
			{
				rol = "Administrador";
			}
			else
			{
				rol = "Encargado";
			}
			return new UsuarioConRolDto
			{

				Email = usuario.Email,
				Rol = rol,
				Id = usuario.Id
			};
		}

	}
}
