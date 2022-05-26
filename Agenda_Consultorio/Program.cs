using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Agenda_Consultorio
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool continuar = true;
            Administrapaciente adm = new Administrapaciente();

            try
            {
                IFormatter lformatter = new BinaryFormatter();
                Stream lstream = new FileStream("pacient.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
                adm = (Administrapaciente)lformatter.Deserialize(lstream);  
                lstream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("    Menu Principal");
                Console.WriteLine("    ==============");
                Console.WriteLine("1-Cadastro de pacientes");
                Console.WriteLine("2-Listar");
                Console.WriteLine("3-Fim");
                Console.Write("Elija su Opcion: ");
                string opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":
                        Paciente persona = new Paciente();
                        Console.Write("Nombre del Paciente: ");
                        persona.nombre = Console.ReadLine();
                        Console.Write("CPF: ");
                        persona.cpf = Console.ReadLine();
                        Console.Write("fecha de nacimiento: ");
                        persona.fec_nac = Console.ReadLine();
                        adm.AgregarPaciente(persona);
                        break;
                    case "2":
                        List<Paciente> lista = adm.ObtenerPaciente();
                        Console.WriteLine("nombre \t\t\t cpf \t\t\t fecha nacimiento");
                        for (int i = 0; i < lista.Count; i++)
                        {
                            Console.WriteLine("{0} \t\t {1} \t\t {2}", lista[i].nombre, lista[i].cpf, lista[i].fec_nac);
                        }
                        Console.ReadKey();
                        break;
                    case "3":
                        IFormatter formatter = new BinaryFormatter();
                        Stream lstream = new FileStream("pacient.bin", FileMode.Create, FileAccess.Write, FileShare.None);
                        formatter.Serialize(lstream, adm);
                        lstream.Close();
                        Console.Clear();
                        Console.WriteLine(" \n\ngracias por participar en este proyecto");
                        Console.ReadLine();
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine(" \n\n   elija una opcion valida");
                        Console.ReadLine();
                        break;


                }
            }

           
        }
    }
    
    [Serializable]
    class Administrapaciente
    {
        List<Paciente> pacientes = new List<Paciente>();

        public void AgregarPaciente(Paciente persona)
        {
            pacientes.Add(persona);
        }

        public List<Paciente> ObtenerPaciente()
        {
            return pacientes;
        }
    }
    [Serializable]
    class Paciente
    {
        public string nombre;
        public string cpf;
        public string fec_nac;
    }
    
}
