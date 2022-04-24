using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;


namespace GenericCodeUML;

public class Program
{
    public static void Main(string[] args)
    {
        string umlFile = args[0];
        string pathSave = args[1];
        StringBuilder builder = new StringBuilder();
        try
        {
            using (var sr = new StreamReader(umlFile))
            {
                builder.AppendLine(sr.ReadToEnd());
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }

        GenerateCode generate = new GenerateCode(builder.ToString());
        generate.GeneratePath();
        generate.GenerateMethods();
        generate.GenerateProperty();
        generate.GenerateEvents();
        generate.GenerateLink();
        generate.CreateDecloration(pathSave);

    }

}