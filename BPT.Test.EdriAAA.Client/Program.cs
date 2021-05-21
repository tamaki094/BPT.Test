using System;
using BPT.Test.EdriAAA.Client.Models;
using BPT.Test.EdriAAA.Client.Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BPT.Test.EdriAAA.Client
{
    class Program
    {
        static EstudianteAsignacionesApi api = new EstudianteAsignacionesApi();
        static string url_base = "https://localhost:44344/";
        static string action = "";

        static void Main(string[] args)
        {
            AbrirMenu();
            
        }

        private static void AbrirMenu()
        {
            Console.WriteLine("Welcome to the student's system");

            Console.WriteLine("Send option please:");

            Console.WriteLine(@"
                1 - Consult students in data base 
                2 - Register student 
                3 - Update student data 
                4 - Consult student by Id
                5 - Delete student 
                6 - Reguster Asignation 
                7 - Consult Asignations
                8 - Consult Asignations by student
                9 - Enroll student to a course
                10 - Salir
            ");

            Console.Write("Option:");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    ObtenerEstudiantes();
                    break;
                case "2":
                    GuardarEstudiantes();
                    break;
                case "3":
                    ActualizarEstudiante();
                    break;
                case "4":
                    ObtenerEstudiantePorId();
                    break;
                case "5":
                    BorrarEstudiante();
                    break;
                case "6":
                    GuardarAsignacion();
                    break;
                case "7":
                    ObtenerAsignaciones();
                    break;
                case "8":
                    ObtenerAsignacionesPorEstudiante();
                    break;
                case "9":
                    AsignarEstudiante();
                    break;
                case "10":
                    return;
                default:
                    Console.WriteLine("Option entered does not exist");
                    break;
            }
            AbrirMenu();
        }

        private static void ObtenerEstudiantes()
        {
            try
            {
                action = "api/Estudiante";
                string url = string.Format("{0}{1}", url_base, action);
                Console.WriteLine("Please wait...");
                dynamic response = api.Get(url);
                List<Estudiantes> lst_estudiantes = JsonConvert.DeserializeObject<List<Estudiantes>>(Convert.ToString(response));
                Console.WriteLine("\r \t");
                foreach (Estudiantes estudiante in lst_estudiantes)
                {
                    Console.WriteLine(string.Format("Student Name: {0}",estudiante.Nombre));
                }

                //Console.WriteLine(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private static void ObtenerEstudiantePorId()
        {
            try
            {
                Console.WriteLine("Write student id:");
                int id = Convert.ToInt32(Console.ReadLine());
                Estudiantes estudiante = GetEstudianteById(id);

                Console.WriteLine(string.Format("Se encontro a : {0}", estudiante.Nombre));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private static void GuardarEstudiantes()
        {
            try
            {
                string nombre = "";
                DateTime fecha_nacimiento;

                Console.WriteLine("Write  student name: ");
                nombre = Console.ReadLine();

                Console.WriteLine("Write birth date: ");
                fecha_nacimiento = DateTime.Parse(Console.ReadLine());

                action = "api/Estudiante";

                Estudiantes estudiante = new Estudiantes();
                estudiante.Nombre = nombre;
                estudiante.FechaNacimiento = fecha_nacimiento;

                string json = JsonConvert.SerializeObject(estudiante);
                Console.WriteLine("Plase wait...");
                dynamic respuesta = api.Post((string.Format("{0}{1}", url_base, action)), json);


                Console.WriteLine(respuesta);
            }
            catch (Exception ex )
            {
                Console.WriteLine(ex.Message);
            }
            

        }

        private static void GuardarAsignacion()
        {
            try
            {
                string nombre = "";

                Console.WriteLine("Write  asignation name: ");
                nombre = Console.ReadLine();

                action = "api/Asignaciones/GuardarAsignacion";

                Asignaciones asignacion = new Asignaciones();
                asignacion.Nombre = nombre;

                string json = JsonConvert.SerializeObject(asignacion);
                Console.WriteLine("Plase wait...");
                dynamic respuesta = api.Post((string.Format("{0}{1}", url_base, action)), json);


                Console.WriteLine(respuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        private static void AsignarEstudiante()
        {
            try
            {
                int idestudiante;
                int idasignacion;

                Console.WriteLine("Write  student id: ");
                idestudiante = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Write asignation id: ");
                idasignacion = Convert.ToInt32(Console.ReadLine());

                action = "api/AsignacionesEstudiante/AsignarEstudiante";

                AsignacionesEstudiante ae = new AsignacionesEstudiante();
                ae.IdEstudiante = idestudiante;
                ae.IdAsignacion = idasignacion;

                string json = JsonConvert.SerializeObject(ae);
                Console.WriteLine("Plase wait...");
                dynamic respuesta = api.Post((string.Format("{0}{1}", url_base, action)), json);


                Console.WriteLine(respuesta);
            }
            catch (Exception ex )
            {
                Console.WriteLine(ex.Message);
            }
            

        }


        private static void ActualizarEstudiante()
        {

            try
            {
                Console.WriteLine("Write student id to update:");
                int id = Convert.ToInt32(Console.ReadLine());
                Estudiantes estudiante = GetEstudianteById(id);

                Console.WriteLine(string.Format("Se encontro con el Id {0}: Nombre. {1} Fecha. {2}", estudiante.Id, estudiante.Nombre, estudiante.FechaNacimiento));


                string nombre = estudiante.Nombre;
                DateTime fecha_nacimiento = estudiante.FechaNacimiento;

                Console.WriteLine("Escribe un nombre:");
                nombre = Console.ReadLine();

                Console.WriteLine("Escribe una fecha de nacimiento");
                fecha_nacimiento = DateTime.Parse(Console.ReadLine());

                action = "api/Estudiante";

                estudiante.Id = id;
                estudiante.Nombre = nombre;
                estudiante.FechaNacimiento = fecha_nacimiento;

                string json = JsonConvert.SerializeObject(estudiante);

                dynamic respuesta = api.Put((string.Format("{0}{1}", url_base, action)), json);

                Console.WriteLine(respuesta);
            }
            catch (Exception ex )
            {
                Console.Write(ex.Message);
            }
        }

        private static Estudiantes GetEstudianteById(int id)
        {
            Estudiantes estudiante = new Estudiantes();
            try
            {
                action = "api/Estudiante/";
                string id_estudiante = id.ToString();
                string url = string.Format("{0}{1}{2}", url_base, action, id_estudiante);
                Console.WriteLine("Please wait...");
                dynamic response = api.Get(url);
                estudiante = JsonConvert.DeserializeObject<Estudiantes>(Convert.ToString(response));
                
                return estudiante;
            }
            catch (Exception ex)
            {
                return estudiante;
            }

        }

        private static void BorrarEstudiante()
        {
            try
            {
                Console.WriteLine("Write student Id: ");
                string id = Console.ReadLine();

                action = "api/Estudiante";

                Estudiantes estudiante = new Estudiantes();
                estudiante.Id = Convert.ToInt32(id);

                string json = JsonConvert.SerializeObject(estudiante);
                Console.WriteLine("Plase wait...");
                dynamic respuesta = api.Delete((string.Format("{0}{1}", url_base, action)), json);


                Console.WriteLine(respuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ObtenerAsignaciones()
        {
            try
            {
                action = "api/Asignaciones/ConsultarAsignaciones";
                string url = string.Format("{0}{1}", url_base, action);
                Console.WriteLine("Please wait...");
                dynamic response = api.Get(url);
                List<Asignaciones> lst_asignaciones = JsonConvert.DeserializeObject<List<Asignaciones>>(Convert.ToString(response));
                Console.WriteLine("\r \t");
                foreach (Asignaciones asignacion in lst_asignaciones)
                {
                    Console.WriteLine(string.Format("Asignation Name: {0}", asignacion.Nombre));
                }

                //Console.WriteLine(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ObtenerAsignacionesPorEstudiante()
        {
            try
            {
                Console.WriteLine("Entered id student: ");
                string id_estudiante = Console.ReadLine(); 
                action = "api/AsignacionesEstudiante/ConsultarAsignacionesByEstudiante/";
                string url = string.Format("{0}{1}{2}", url_base, action, id_estudiante);
                Console.WriteLine("Please wait...");
                dynamic response = api.Get(url);
                List<AsignacionByEstudiante> lst_asignaciones = JsonConvert.DeserializeObject<List<AsignacionByEstudiante>>(Convert.ToString(response));
                Console.WriteLine("\r \t");
                foreach (AsignacionByEstudiante asignacion in lst_asignaciones)
                {
                    Console.WriteLine(string.Format("student ''{0}'' is enrolled in the ''{1}'' course", asignacion.estudiante, asignacion.asignacion));
                  
                }

                //Console.WriteLine(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
