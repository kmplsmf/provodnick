using Newtonsoft.Json;
using System.Text;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    public class nikotin
    {
        public string kokoin;
        public string anesteziafetamin;
        public string protein;
    }
    class baza
    {
        static List<nikotin> narkota = new List<nikotin>();
        public static void deser(string sslka)
        {
            Console.Clear();
            if (sslka.Contains(".txt"))
            {
                narkota.Clear();
                string[] a = File.ReadAllLines(sslka);
                foreach (string s in a)
                {
                    Console.WriteLine(s);
                }
                Thread.Sleep(1000);
                nikotin b = new nikotin();
                b.kokoin = a[0];
                b.anesteziafetamin = a[1];
                b.protein = a[2];
                narkota.Add(b);
                vivod_texta("f1 - json, f2 - xml");
            }
            if (sslka.Contains(".json"))
            {
                narkota.Clear();
                narkota = JsonConvert.DeserializeObject<List<nikotin>>(File.ReadAllText(sslka));
                vivod_texta("f3 - txt, f2 - xml");
            }
            if (sslka.Contains(".xml"))
            {
                narkota.Clear();
                XmlSerializer xml = new XmlSerializer(typeof(nikotin));
                using (FileStream fs = new FileStream(sslka, FileMode.Open))
                {
                    narkota.Add((nikotin)xml.Deserialize(fs));
                }
                vivod_texta("f3 - txt, f1 - json");
            }

        }
        private static void vivod_texta(string txt)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Clear();
            foreach (nikotin n in narkota)
            {
                Console.WriteLine($"{n.kokoin}\n{n.anesteziafetamin}\n{n.protein}");
            }
            Console.WriteLine($"Выберите в какой формат перевести: {txt}");
            ser(Console.ReadKey());

        }
        private static void ser(ConsoleKeyInfo key)
        {
            nikotin fa = new nikotin();
            Thread.Sleep(1000);
            if (key.Key == ConsoleKey.F1)
            {
                File.WriteAllText("C:\\Users\\Ксандр\\Desktop\\prod.json", JsonConvert.SerializeObject(narkota));
            }
            if (key.Key == ConsoleKey.F2)
            {
                XmlSerializer xml = new XmlSerializer(typeof(nikotin));
                using (FileStream fs = new FileStream("C:\\Users\\Ксандр\\Desktop\\xml.xml", FileMode.OpenOrCreate))
                {
                    xml.Serialize(fs, narkota[0]);
                }
            }
            if (key.Key == ConsoleKey.F2)
            {
                string text = "";
                foreach (nikotin a in narkota)
                {
                    text += $"{a.kokoin}\n{a.anesteziafetamin}\n{a.protein}";
                }
                File.WriteAllText("C:\\Users\\Ксандр\\Desktop\\adf.xml", text);
            }
        }
    }
    class Program : baza
    {
        static void Main()
        {

            while (true)
            {

                Console.Clear();
                Console.WriteLine("Введите ссылку");
                deser(Console.ReadLine());
            }
        }
    }
}