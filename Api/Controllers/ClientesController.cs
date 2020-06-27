using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{
    public class ClientesController : ApiController
    {
        // GET: api/clientess
        public IHttpActionResult Get()
        {
            return Ok(Clientes.GetCliente());
        }

        // GET: api/clientess/5
        public IHttpActionResult Get(int id)
        {
            return Ok(Clientes.GetCliente(id));
        }

        // POST: api/clientess
        public IHttpActionResult Post(Clientes clientes)
        {
            return Ok(Clientes.Guardar(clientes));
        }

        // PUT: api/clientess/5
        public IHttpActionResult Put(Clientes clientes)
        {
            return Ok(Clientes.Modificar(clientes));
        }

        // DELETE: api/clientess/5
        public IHttpActionResult Delete(int id)
        {
            return Ok(Clientes.Eliminar(id));
        }
    }
}
