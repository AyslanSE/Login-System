using System;
using System.IO;
using System.Xml.Serialization;

namespace Login
{
    [XmlRoot("dados")]
    public class Dados
    {
        public string usuario;
        public string senha;

        [XmlElement(typeof(String))]
        public string Usuario
        {
            get { return this.usuario; }
            set { this.usuario = value; }
        }
        [XmlElement(typeof(String))]
        public string Senha
        {
            get { return this.senha; }
            set { this.senha = value; }
        }
    }

    class Program
    {
        static string name, key;
        static string path = @"C:\Users\Usuário\AppData\LocalLow\savestests\usukey.dat";

        static void Main(string[] args)
        {
            switch (File.Exists(path))
            {
                case true: Login();
                    break;
                case false:
                    Console.WriteLine("deseja criar uma nova conta? \n \n1-Sim\n2-Nao");
                    int seletion;
                    seletion = int.Parse(Console.ReadLine());

                    switch (seletion)
                    {
                        case 1:
                            NovosDados();
                            break;
                        case 2:
                            Login();
                            break;
                    }
                    break;
            }
        }
        static void NovosDados()
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            XmlSerializer x = new XmlSerializer(typeof(Dados));
            StreamWriter writer = new StreamWriter(path, true);

            Dados dados = new Dados();

            Console.WriteLine("Insira seu nome de usuario");
            name = Console.ReadLine();
            dados.usuario = name;

            Console.WriteLine("Insira sua senha");
            key = Console.ReadLine();

            for (int i = 1; i > 0; i++)
            {
                string keyconfirm;
                Console.WriteLine("confirme sua senha");
                keyconfirm = Console.ReadLine();

                if (keyconfirm != key)
                    Console.WriteLine("senhas diferentes");
                else
                {
                    dados.senha = key;
                    i = -10;
                }
            }

            x.Serialize(writer, dados);
            writer.Close();

            Login();
        }
        static void Login()
        {
            Dados dados = new Dados();

            XmlSerializer x = new XmlSerializer(typeof(Dados));
            StreamReader reader = new StreamReader(path);

            Dados d = (Dados)x.Deserialize(reader);
            reader.Close();

            dados.usuario = d.Usuario;
            dados.senha = d.Senha;

            Console.WriteLine("====Login====");
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Insira seu nome de usuario");
                name = Console.ReadLine();
                if (name == dados.usuario)
                {
                    Console.WriteLine("Insira sua senha");
                    key = Console.ReadLine();
                    if (key == dados.Senha)
                    {
                        Account();
                    }
                    else
                        Console.WriteLine("voce errou " + (i + 1) + " vezes \n \nse errar 3 vezes o programa sera fechado");
                }
                else
                    Console.WriteLine("voce errou " + (i + 1) + " vezes \n \nse errar 3 vezes o programa sera fechado");
            }
            Console.ReadKey();
        }
        static void Account()
        {
            Console.WriteLine("\nBem-Vindo(a) " + name + "\n \nOque deseja fazer?\n \n1-deletar usuario\n2-sair da conta");
            int seletion;
            seletion = int.Parse(Console.ReadLine());
            switch (seletion)
            {
                case 1:
                    File.Delete(path);
                    break;
                case 2:
                    Login();
                    break;
            }
            Console.ReadKey();
        }
    }
}