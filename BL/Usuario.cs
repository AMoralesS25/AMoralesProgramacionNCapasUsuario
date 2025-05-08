using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result AddEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AMoralesProgramacionNCapasUsuariosEntities context = new
                    DL.AMoralesProgramacionNCapasUsuariosEntities())
                {
                    int rowsAffect = context.UsuarioAdd(usuario.UserName, usuario.Nombre, usuario.ApellidoPaterno,
                        usuario.ApellidoMaterno, usuario.Email, usuario.Password, DateTime.Parse(usuario.FechaNacimiento.ToString()),
                        usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.Estatus, usuario.CURP, usuario.Imagen,
                        usuario.Rol.IdRol, usuario.Direccion.Calle, usuario.Direccion.NumeroExterior, usuario.Direccion.NumeroInterior, usuario.Direccion.Colonia.IdColonia);
                    if (rowsAffect > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo insertar el registro";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result UpdateEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AMoralesProgramacionNCapasUsuariosEntities context = new DL.AMoralesProgramacionNCapasUsuariosEntities())
                {
                    int rowsAffect = context.UsuarioUpdate(usuario.IdUsuario, usuario.UserName, usuario.Nombre, usuario.ApellidoPaterno,
                        usuario.ApellidoMaterno, usuario.Email, usuario.Password, DateTime.Parse(usuario.FechaNacimiento.ToString()),
                        usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.Estatus, usuario.CURP, usuario.Imagen,
                        usuario.Rol.IdRol, usuario.Direccion.Calle, usuario.Direccion.NumeroExterior, usuario.Direccion.NumeroInterior, usuario.Direccion.Colonia.IdColonia);
                    if (rowsAffect > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo actualizar el registro";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result DeleteEF(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AMoralesProgramacionNCapasUsuariosEntities context = new
                    DL.AMoralesProgramacionNCapasUsuariosEntities())
                {
                    int rowsAffect = context.UsuarioDelete(idUsuario);
                    if (rowsAffect > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo eliminar el registro";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result GetAllEF(ML.Usuario usuarioObj)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AMoralesProgramacionNCapasUsuariosEntities context =
                    new DL.AMoralesProgramacionNCapasUsuariosEntities())
                {
                    var query = context.UsuarioGetAll(usuarioObj.Nombre, usuarioObj.ApellidoPaterno, usuarioObj.ApellidoMaterno, usuarioObj.Rol.IdRol).ToList();
                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var objBD in query)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.Rol = new ML.Rol(); //abrir la puerta para ir a ROL
                            usuario.Direccion = new ML.Direccion(); //SE ABRIO LA PUERTA A DIRECCION
                            usuario.Direccion.Colonia = new ML.Colonia(); //SE ABRIO LA PUERTA A COLONIA
                            usuario.Direccion.Colonia.Municipio = new ML.Municipio(); //SE ABRIO LA PUERTA A MUNICIPIO
                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado(); //SE ABRIO LA PUERTA A ESTADO
                            usuario.IdUsuario = objBD.IdUsuario;
                            usuario.UserName = objBD.UserName;
                            usuario.Nombre = objBD.NombreUsuario;
                            usuario.ApellidoPaterno = objBD.ApellidoPaterno;
                            usuario.ApellidoMaterno = objBD.ApellidoMaterno;
                            usuario.Email = objBD.Email;
                            usuario.Password = objBD.Password;
                            usuario.FechaNacimiento = objBD.FechaNacimiento;
                            usuario.Sexo = objBD.Sexo;
                            usuario.Telefono = objBD.Telefono;
                            usuario.Celular = objBD.Celular;
                            usuario.Estatus = objBD.Estatus;
                            usuario.CURP = objBD.CURP;
                            usuario.Imagen = objBD.Imagen;
                            if (objBD.Imagen != null)
                            {
                                usuario.ImagenBase64 = Convert.ToBase64String(objBD.Imagen);
                            }
                            else
                            {
                                usuario.ImagenBase64 = null;
                            }
                            usuario.Imagen = objBD.Imagen;
                            usuario.Rol.Nombre = objBD.NombreRol;
                            usuario.Direccion.Calle = objBD.Calle;
                            usuario.Direccion.NumeroExterior = objBD.NumeroExterior;
                            usuario.Direccion.NumeroInterior = objBD.NumeroInterior;
                            usuario.Direccion.Colonia.Nombre = objBD.NombreColonia;
                            usuario.Direccion.Colonia.CodigoPostal = objBD.CodigoPostal;
                            usuario.Direccion.Colonia.Municipio.Nombre = objBD.NombreMunicipio;
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre = objBD.NombreEstado;

                            result.Objects.Add(usuario);
                            result.Correct = true;
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;

        }

        public static ML.Result GetByIdEF(int idUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AMoralesProgramacionNCapasUsuariosEntities context =
                    new DL.AMoralesProgramacionNCapasUsuariosEntities())
                {
                    var query = context.UsuarioGetById(idUsuario).SingleOrDefault();
                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.Rol = new ML.Rol(); //abrir la puerta para ir a ROL
                        usuario.Direccion = new ML.Direccion(); //SE ABRIO LA PUERTA A DIRECCION
                        usuario.Direccion.Colonia = new ML.Colonia(); //SE ABRIO LA PUERTA A COLONIA
                        usuario.Direccion.Colonia.Municipio = new ML.Municipio(); //SE ABRIO LA PUERTA A MUNICIPIO
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado(); //SE ABRIO LA PUERTA A ESTADO

                        usuario.IdUsuario = query.IdUsuario;
                        usuario.UserName = query.UserName;
                        usuario.Nombre = query.NombreUsuario;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Email = query.Email;
                        usuario.Password = query.Password;
                        usuario.FechaNacimiento = query.FechaNacimiento;
                        usuario.Sexo = query.Sexo;
                        usuario.Telefono = query.Telefono;
                        usuario.Celular = query.Celular;
                        usuario.Estatus = query.Estatus;
                        usuario.CURP = query.CURP;
                        usuario.Imagen = query.Imagen;
                        if (query.IdRol != null)
                        {
                            //trae un id 
                            usuario.Rol.IdRol = query.IdRol.Value;
                        }
                        else
                        {
                            usuario.Rol.IdRol = 0;

                        }
                        if (query.IdDireccion != null)
                        {
                            usuario.Direccion.IdDireccion = query.IdDireccion.Value;
                        }
                        else
                        {
                            usuario.Direccion.Colonia.IdColonia = 0;
                        }
                        usuario.Direccion.Calle = query.Calle;
                        usuario.Direccion.NumeroExterior = query.NumeroExterior;
                        usuario.Direccion.NumeroInterior = query.NumeroInterior;
                        if (query.IdColonia != null)
                        {
                            usuario.Direccion.Colonia.IdColonia = query.IdColonia.Value;
                        }
                        else
                        {
                            usuario.Direccion.Colonia.IdColonia = 0;
                        }
                        usuario.Direccion.Colonia.CodigoPostal = query.CodigoPostal;
                        if (query.IdMunicipio != null)
                        {
                            usuario.Direccion.Colonia.Municipio.IdMunicipio = query.IdMunicipio.Value;
                        }
                        else
                        {
                            usuario.Direccion.Colonia.Municipio.IdMunicipio = 0;
                        }
                        if (query.IdEstado != null)
                        {
                            usuario.Direccion.Colonia.Municipio.Estado.IdEstado = query.IdEstado.Value;

                        }
                        else
                        {
                            usuario.Direccion.Colonia.Municipio.Estado.IdEstado = 0;
                        }


                        result.Object = usuario; //BOXING
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No existe el registro";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result CambiarEstatus(int Usuario, bool Estatus)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AMoralesProgramacionNCapasUsuariosEntities context = new
                    DL.AMoralesProgramacionNCapasUsuariosEntities())
                {
                    int rowsAffect = context.CambioEstatus(Usuario, Estatus);
                    if (rowsAffect > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo actualizar el estatus";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result CargaMasiva()
        {
            ML.Result result = new ML.Result();
            string ruta = @"C:\Users\digis\Downloads\CargaMasiva.txt";
            try
            {
                StreamReader streamReader = new StreamReader(ruta);
                string fila = "";

                streamReader.ReadLine();

                while ((fila = streamReader.ReadLine()) != null)
                {
                    string[] valores = fila.Split('|');

                    ML.Usuario usuario = new ML.Usuario();
                    usuario.UserName = valores[0];
                    usuario.Nombre = valores[1];
                    usuario.ApellidoPaterno = valores[2];
                    usuario.ApellidoMaterno = valores[3];
                    usuario.Nombre = valores[4];


                    BL.Usuario.AddEF(usuario);
                    result.Correct = true;
                }


            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result LeerExcel(string cadenaConexion)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (OleDbConnection context = new OleDbConnection(cadenaConexion))
                {
                    string query = "SELECT * FROM [Sheet1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;

                        OleDbDataAdapter adapter = new OleDbDataAdapter();
                        adapter.SelectCommand = cmd;

                        DataTable tablaUsuario = new DataTable();
                        adapter.Fill(tablaUsuario);

                        if (tablaUsuario.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in tablaUsuario.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario();
                                usuario.Rol = new ML.Rol();
                                usuario.Direccion = new ML.Direccion();
                                usuario.Direccion.Colonia = new ML.Colonia();
                                usuario.UserName = row[0].ToString();
                                usuario.Nombre = row[1].ToString();
                                usuario.ApellidoPaterno = row[2].ToString();
                                usuario.ApellidoMaterno = row[3].ToString();
                                usuario.Email = row[4].ToString();
                                usuario.Password = row[5].ToString();
                                usuario.FechaNacimiento = row[6].ToString();
                                usuario.Sexo = row[7].ToString();
                                usuario.Telefono = row[8].ToString();
                                usuario.Celular = row[9].ToString();
                                //usuario.Estatus = Convert.ToBoolean(row[10].ToString());
                                usuario.CURP = row[10].ToString();
                                usuario.Imagen = null;
                                if (row[11] == DBNull.Value || string.IsNullOrEmpty(row[11].ToString()))
                                {
                                    usuario.Rol.IdRol = 0;
                                }
                                else
                                {
                                    usuario.Rol.IdRol = Convert.ToInt16(row[11]);
                                }
                                usuario.Direccion.Calle = row[12].ToString();
                                usuario.Direccion.NumeroExterior = row[13].ToString();
                                usuario.Direccion.NumeroInterior = row[14].ToString();
                                if (row[15] == DBNull.Value || string.IsNullOrEmpty(row[15].ToString()))
                                {
                                    usuario.Direccion.Colonia.IdColonia = 0;
                                }
                                else
                                {
                                    usuario.Direccion.Colonia.IdColonia = Convert.ToInt16(row[15]);
                                }

                                result.Objects.Add(usuario);

                            }
                            result.Correct = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;

            }
            return result;
        }

        public static ML.ResultExcel ValidarExcel(List<object> registros)
        {
            ML.ResultExcel result = new ML.ResultExcel();
            result.Errores = new List<object>();

            int contador = 1;

            foreach (ML.Usuario usuario in registros)
            {
                ML.ResultExcel resultValidacion = new ML.ResultExcel();

                resultValidacion.NumeroRegistro = contador;

                if (usuario.UserName.Length > 50 || usuario.UserName == "" || usuario.UserName == null)
                {
                    resultValidacion.ErrorMessage += "La columna username (A" + (contador + 1) + ") tiene un error porque excede el número de caracteres permitidos o es vacía. ";
                }

                if (usuario.Nombre.Length > 50 || usuario.Nombre == "" || usuario.Nombre == null)
                {
                    resultValidacion.ErrorMessage += "La columna nombre (B" + (contador + 1) + ") tiene un error porque excede el número de caracteres permitidos o es vacía. ";
                }

                if (usuario.ApellidoPaterno.Length > 50 || usuario.ApellidoPaterno == "" || usuario.ApellidoPaterno == null)
                {
                    resultValidacion.ErrorMessage += "La columna apellido paterno (C" + (contador + 1) + ") tiene un error porque excede el número de caracteres permitidos o es vacía. ";
                }

                if (usuario.ApellidoMaterno.Length > 50 || usuario.ApellidoMaterno == "" || usuario.ApellidoMaterno == null)
                {
                    resultValidacion.ErrorMessage += "La columna apellido materno (D" + (contador + 1) + ") tiene un error porque excede el número de caracteres permitidos o es vacía. ";
                }

                if (usuario.Email.Length > 254 || usuario.Email == "" || usuario.Email == null)
                {
                    resultValidacion.ErrorMessage += "La columna email (E" + (contador + 1) + ") tiene un error porque excede el número de caracteres permitidos o es vacía. ";
                }

                if (usuario.Password.Length > 50 || usuario.Password == "" || usuario.Password == null)
                {
                    resultValidacion.ErrorMessage += "La columna password (F" + (contador + 1) + ") tiene un error porque excede el número de caracteres permitidos o es vacía. ";
                }

                if (usuario.FechaNacimiento.Length > 50 || usuario.FechaNacimiento == "" || usuario.FechaNacimiento == null)
                {
                    resultValidacion.ErrorMessage += "La columna fecha de nacimiento (G" + (contador + 1) + ") tiene un error porque excede el número de caracteres permitidos o es vacía. ";
                }

                if (usuario.Sexo.Length > 2 || usuario.Sexo == "" || usuario.Sexo == null)
                {
                    resultValidacion.ErrorMessage += "La columna sexo (H" + (contador + 1) + ") tiene un error porque excede el número de caracteres permitidos o es vacía. ";
                }

                if (usuario.Telefono.Length > 20 || usuario.Telefono == "" || usuario.Telefono == null)
                {
                    resultValidacion.ErrorMessage += " \n La columna telefono (I" + (contador + 1) + ") tiene un error porque excede el número de caracteres permitidos o es vacía. ";
                }

                if (usuario.Celular.Length > 20 || usuario.Celular == "" || usuario.Celular == null)
                {
                    resultValidacion.ErrorMessage += "La columna celular (J" + (contador + 1) + ") tiene un error porque excede el número de caracteres permitidos o es vacía. ";
                }

                if (usuario.CURP.Length > 50 || usuario.CURP == "" || usuario.CURP == null)
                {
                    resultValidacion.ErrorMessage += "La columna CURP (K" + (contador + 1) + ") tiene un error porque excede el número de caracteres permitidos o es vacía. ";
                }

                if (usuario.Rol.IdRol == 0 || usuario.Rol.IdRol == null)
                {
                    resultValidacion.ErrorMessage += " La columna Id rol (L" + (contador + 1) + ") tiene un error porque es vacía o es igual a cero. ";
                }

                if (usuario.Direccion.Calle.Length > 50 || usuario.Direccion.Calle == "" || usuario.Direccion.Calle == null)
                {
                    resultValidacion.ErrorMessage += "La columna calle (M" + (contador + 1) + ") tiene un error porque excede el número de caracteres permitidos o es vacía. ";
                }

                if (usuario.Direccion.NumeroInterior.Length > 20 || usuario.Direccion.NumeroInterior == "" || usuario.Direccion.NumeroInterior == null)
                {
                    resultValidacion.ErrorMessage += "La columna numero interior (N" + (contador + 1) + ") tiene un error porque excede el número de caracteres permitidos o es vacía. ";
                }

                if (usuario.Direccion.NumeroExterior.Length > 20 || usuario.Direccion.NumeroExterior == "" || usuario.Direccion.NumeroExterior == null)
                {
                    resultValidacion.ErrorMessage += "La columna numero exterior (O" + (contador + 1) + ") tiene un error porque excede el número de caracteres permitidos o es vacía. ";
                }

                if (usuario.Direccion.Colonia.IdColonia == 0 || usuario.Direccion.Colonia.IdColonia == null)
                {
                    resultValidacion.ErrorMessage += "La columna Id colonia (P" + (contador + 1) + ") tiene un error porque es vacía o es igual a cero. ";
                }

                if (resultValidacion.ErrorMessage != null)
                {
                    result.Errores.Add(resultValidacion);
                }

                contador++;
            }

            return result;
        }
    }

}
