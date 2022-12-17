using System.Diagnostics.Metrics;
using System.Linq;

namespace pract1;
using System;
using System.Collections;
class Program
{
    static void Main(string[] args)
    {
        string ubicacionArchivo = "Violations_2019.csv";
        System.IO.StreamReader archivo = new System.IO.StreamReader(ubicacionArchivo);
        string separador = ",";
        string linea;
        int i = 0;
        int j = 0;
        archivo.ReadLine();
        String StreetName = "";
        ArrayList Streets = new ArrayList();
        ArrayList AllStreets = new ArrayList();
        ArrayList needed_values_licence = new ArrayList();
        int[] numeroderepes = new int[1000];

        // Create a dictionary to store the number of violations for each hour for each street
        Dictionary<string, Dictionary<int, int>> violationsByHour = new Dictionary<string, Dictionary<int, int>>();

        while ((linea = archivo.ReadLine()) != null)
        {
            String[] fila = linea.Split(separador);
            StreetName = fila[5];
            int hour = -1;
            if (fila[1].Length >= 2)
            {
                hour = int.Parse(fila[1].Substring(0, 2));
            }


            if (Streets.Contains(StreetName))
            {
                numeroderepes[i]++;
            }
            else
            {
                Streets.Add(StreetName);
                i++;
                numeroderepes[i]++;
            }

            // Update the number of violations for the current hour for the current street
            if (!violationsByHour.ContainsKey(StreetName))
            {
                violationsByHour[StreetName] = new Dictionary<int, int>();
            }
            if (!violationsByHour[StreetName].ContainsKey(hour))
            {
                violationsByHour[StreetName][hour] = 1;
            }
            else
            {
                violationsByHour[StreetName][hour]++;
            }
        }

        int[] top5 = new int[5];
        for (i = 0; i < 5; i++)
        {
            int max = numeroderepes.Max();
            int top1 = Array.IndexOf(numeroderepes, max);
            top5[i] = top1;
            Array.Clear(numeroderepes, top1, 1);
        }

        for (i = 0; i < 5; i++)
        {
            Console.WriteLine("Street: {0}", (string)Streets[i]);
            Dictionary<int, int> hours = violationsByHour[(string)Streets[i]];
            foreach (KeyValuePair<int, int> entry in hours)
            {
                Console.WriteLine("Hour: {0}, Violations: {1}", entry.Key, entry.Value);
            }
        }


    }
}