using ROASApp.Data.IO;
using System.Text.Json;

namespace ROASApp.Domain
{
    public class ROASService
    {
        private static List<ROAS> liste = new List<ROAS>();
        public static ROAS SaveROAS(string reklamKanali, double reklamMaliyeti, double birimFiyat, int satisAdedi)
        {
            ROAS roas = new ROAS();
            roas.reklamKanali = reklamKanali;
            roas.reklamMaliyeti = reklamMaliyeti;
            roas.birimFiyat = birimFiyat;
            roas.satisAdedi = satisAdedi;

            liste.Add(roas);
            //Todo:Dosya sistemine yazma operasyonu gerçekleşecek

            //JSON Convert
            // JsonSerializerOptions serializerOptions = new JsonSerializerOptions();
            // serializerOptions.IncludeFields = true;
            //string json= JsonSerializer.Serialize(liste,serializerOptions);


            string json = JsonSerializer.Serialize(liste,
                new JsonSerializerOptions { IncludeFields = true });

            FileOperation.Write(json);

            return roas;
        }
        public static IReadOnlyCollection<ROAS> GetAllROAS()
        {
            LoadListFromFile();
            return liste.AsReadOnly();
        }
        public static IReadOnlyCollection<ROAS> FilterByChannelName(string channelName)
        {
            LoadListFromFile();
            List<ROAS> filteredROAS = new List<ROAS>();
            foreach (ROAS r in liste)
            {
                if (r.reklamKanali.ToLower().Contains(channelName.ToLower()))
                    filteredROAS.Add(r);
            }
            return filteredROAS.AsReadOnly();
        }
        public static void LoadListFromFile()
        {
            string json = FileOperation.Read();
            liste = JsonSerializer.Deserialize<List<ROAS>>(json,
                new JsonSerializerOptions { IncludeFields = true });
        }
        public static List<ROAS> RemoveROAS(string reklamKanali)
        {
            LoadListFromFile();
            foreach (ROAS l in liste)
            {
                if (l.reklamKanali.ToLower() == reklamKanali.ToLower())
                {
                    Console.WriteLine($"{l.reklamKanali} silindi");
                    liste.Remove(l);
                    break;
                }
                else
                {
                    Console.WriteLine($"{reklamKanali} listede bulunamadi ya da onceden silindi");
                    break;
                }

            }
            string json = JsonSerializer.Serialize(liste,
                new JsonSerializerOptions { IncludeFields = true });

            FileOperation.Write(json);
            return liste;


        }
        //Todo:ROAS Silme işlevselliğini projeye ekleyin!
        //Todo:ROAS Güncelleme işlevselliğini projeye ekleyin!

        public static List<ROAS> UpdateROAS(string reklamKanali)
        {
            LoadListFromFile();
            foreach (ROAS l in liste)
            {
                if (l.reklamKanali.ToLower() == reklamKanali.ToLower())
                {
                    Console.WriteLine($"{l.reklamKanali} secildi");
                    Console.WriteLine("Lutfen guncellemek istediginiz ozelligi seciniz: " +
                        "\n1.Kanal Adi\n2. Reklam Maliyeti\n3.Satis Adedi\n4.Birim Fiyat");
                    string opt = Console.ReadLine();
                    switch (opt)
                    {
                        case "1":
                            Console.WriteLine("Lütfen yeni kanal adı giriniz: ");
                            l.reklamKanali = Console.ReadLine();
                            break;
                        case "2":
                            Console.WriteLine("Lütfen reklam maliyetini giriniz: ");
                            l.reklamMaliyeti = Convert.ToDouble(Console.ReadLine());
                            break;
                        case "3":
                            Console.WriteLine("Lütfen satis adedini giriniz: ");
                            l.satisAdedi = Convert.ToDouble(Console.ReadLine());
                            break;
                        case "4":
                            Console.WriteLine("Lutfen yeni birim fiyatini giriniz");
                            l.birimFiyat = Convert.ToDouble(Console.ReadLine());
                            break;
                        default:
                            Console.WriteLine("Gecersiz Eylem");
                            break;

                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Kanal bulunamadi");
                }

            }
            string json = JsonSerializer.Serialize(liste,
               new JsonSerializerOptions { IncludeFields = true });

            FileOperation.Write(json);
            return liste;
        }
    }
}