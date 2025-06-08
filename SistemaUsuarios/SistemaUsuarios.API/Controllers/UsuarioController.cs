using System.Collections.Generic;
using System.Web.Http;
using SistemaUsuarios.DAL;

namespace SistemaUsuarios.API.Controllers
{
    [RoutePrefix("api/usuario")]
    public class UsuarioController : ApiController
    {
        private readonly UsuarioRepository _repo = new UsuarioRepository();

        [HttpGet]
        [Route("consultar")]
        public IHttpActionResult Consultar()
        {
            var lista = _repo.ObtenerUsuarios();
            return Ok(lista);
        }

        [HttpPost]
        [Route("agregar")]
        public IHttpActionResult Agregar(Usuario usuario)
        {
            _repo.AgregarUsuario(usuario);
            return Ok("Usuario agregado correctamente.");
        }

        [HttpPut]
        [Route("modificar")]
        public IHttpActionResult Modificar(Usuario usuario)
        {
            _repo.ModificarUsuario(usuario);
            return Ok("Usuario modificado correctamente.");
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IHttpActionResult Eliminar(int id)
        {
            _repo.EliminarUsuario(id);
            return Ok("Usuario eliminado correctamente.");
        }
    }
}
