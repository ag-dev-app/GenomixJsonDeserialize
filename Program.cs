using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenomixJsonDeserialize
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Json File Path : ");
            //string path = Console.ReadLine();
            string path = @"C:\Users\rcmd\Desktop\Aternum DEV\json\ag-SB20879-file all.json";
            StreamReader stream = new StreamReader(path);
            string JsonString = stream.ReadToEnd();

            var json = JToken.Parse(JsonString);
            var fieldsCollector = new JsonFieldsCollector(json);
            var fields = fieldsCollector.GetAllFields();

            fieldsCollector.SaveFieldsKeys(JsonString);
            List<string> keys = fieldsCollector.GetFieldsKeys();

            Console.WriteLine("Listes des clés :\n");

            foreach (string key in keys)
            {
                Console.WriteLine(key);
            }

            Translate Traducteur = new Translate();

            string rep = "";
            do
            {
                Console.WriteLine("\nEntrer la clé de la valeur à afficher :");
                rep = Console.ReadLine();
                string result = fieldsCollector.DisplayValue(rep).ToString();
                Console.WriteLine("\n" + result);
                string resultTrad;

                Console.WriteLine("\nTraduire ? oui / non");
                rep = Console.ReadLine();

                if (rep == "oui")
                {
                    resultTrad = Traducteur.TranslateText(result);
                    Console.WriteLine("\n" + resultTrad);
                }

            } while (rep !="stop");

        }
    }
}
