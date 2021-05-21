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
            Console.WriteLine("Welcome to the student's system");

            Console.WriteLine("Send option please:");

            Console.WriteLine(@"
                1-Consult students in data base 
                2-Register student 
                3-Update student data 
                4-Consult student by Id
                5-Delete student 
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
                default:
                    Console.WriteLine("Option entered does not exist");
                    break;
            }
            
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
    }
}
