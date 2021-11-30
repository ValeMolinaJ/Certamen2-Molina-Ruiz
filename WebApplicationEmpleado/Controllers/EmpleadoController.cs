using EmpleadoLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplicationEmpleado.Models;

namespace WebApplicationEmpleado.Controllers
{
    public class EmpleadoController : ApiController
    {
        //Método para traer datos.
        [HttpGet]
        [Route("api/v1/empleado")]
        public respuesta listar(string rut = "")
        {
            respuesta resp = new respuesta();
            try
            {
                List<empleados> listado = new List<empleados>();
                empleadoEntity clienteData = new empleadoEntity();
                DataSet data = rut == "" ? clienteData.listadoEmpleados() : clienteData.listadoEmpleados(rut);
                for (int i = 0; i < data.Tables[0].Rows.Count; i++)
                {
                    empleados item = new empleados();
                    item.rut = data.Tables[0].Rows[i].ItemArray[0].ToString();
                    item.nombre = data.Tables[0].Rows[i].ItemArray[1].ToString();
                    item.apellido = data.Tables[0].Rows[i].ItemArray[2].ToString();
                    item.mail = data.Tables[0].Rows[i].ItemArray[3].ToString();
                    item.telefono = data.Tables[0].Rows[i].ItemArray[4].ToString();
                    listado.Add(item);
                }
                resp.error = false;
                resp.mensaje = "OK";
                if (listado.Count > 0)
                {
                    resp.data = listado;
                }
                else

                    resp.data = "No se encontro empleado";
                return resp;
            }
            catch (Exception e)
            {
                resp.error = true;
                resp.mensaje = "Error:" + e.Message;
                resp.data = null;
                return resp;

            }
        }
        //Método para insertar datos.
        [HttpPost]
        [Route("api/v1/setEmpleado")]
        public respuesta guardar(empleados empleado)
        {
            respuesta resp = new respuesta();
            try
            {
                empleadoEntity emp = new empleadoEntity(empleado.rut, empleado.nombre, empleado.apellido, empleado.mail, empleado.telefono);
                int estado = emp.guardar();
                if (estado == 1)
                {
                    resp.error = false;
                    resp.mensaje = "Empleado ingresado";
                    resp.data = empleado;
                }
                else
                {
                    resp.error = true;
                    resp.mensaje = "No se realizo el ingreso";
                    resp.data = null;
                }
                return resp;
            }
            catch (Exception e)
            {
                resp.error = true;
                resp.mensaje = "Error:" + e.Message;
                resp.data = null;
                return resp;
            }
        }
        //Método para eliminar datos.
        [HttpDelete]
        [Route("api/v1/deleteEmpleado")]
        public respuesta eliminar(string rut)
        {
            respuesta resp = new respuesta();
            try
            {
                empleadoEntity emp = new empleadoEntity();
                emp.Rut = rut;
                int estado = emp.eliminar();
                if (estado == 1)
                {
                    resp.error = false;
                    resp.mensaje = "Empleado eliminado";
                    resp.data = null;
                }
                else
                {
                    resp.error = true;
                    resp.mensaje = "No se realizó la eliminación";
                    resp.data = null;
                }
                return resp;
            }
            catch (Exception e)
            {
                resp.error = true;
                resp.mensaje = "Error:" + e.Message;
                resp.data = null;
                return resp;
            }
        }

        //Método para actualizar datos.
        [HttpPut]
        [Route("api/v1/updateEmpleado")]
        public respuesta actualizar(empleados empleado)
        {
            respuesta resp = new respuesta();
            try
            {
                empleadoEntity emp = new empleadoEntity(empleado.rut, empleado.nombre, empleado.apellido, empleado.mail, empleado.telefono);
                int estado = emp.actualizar(empleado.rut);
                if (estado == 1)
                {
                    resp.error = false;
                    resp.mensaje = "Empleado Modificado";
                    resp.data = empleado;
                }
                else
                {
                    resp.error = true;
                    resp.mensaje = "No se realizó la modificación";
                    resp.data = null;
                }
                return resp;
            }
            catch (Exception e)
            {
                resp.error = true;
                resp.mensaje = "Error:" + e.Message;
                resp.data = null;
                return resp;
            }
        }
    }
}
