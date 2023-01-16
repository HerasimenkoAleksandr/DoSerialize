using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;

namespace dz14
{
    public struct Fraction
    {
        public int Numerator { get; set; }
        public int Denominator { get; set; }
        public Fraction Creat_Fraction()
        {
            Console.Write("Введите числитель: ");
            Numerator = Int32.Parse(Console.ReadLine());
            Console.Write("Введите знаменатель: ");
            Denominator = Int32.Parse(Console.ReadLine());
            return this;
        }
    }

    public class ArrayFraction
    {
        public List<Fraction> fraction;


        public ArrayFraction()
        {
            fraction=new List<Fraction>();
        }

    public void Add()
    {

        Fraction temp = new Fraction();
            Console.Write($"Для создания {fraction.Count+1}-й дроби->"); ;
            temp.Creat_Fraction();
        fraction.Add(temp);
    }

    public override string ToString()
    {
        string a = "Массив дробей: ";

        foreach (var i in fraction)
        {
            a += " " + i.Numerator + "/" + i.Denominator.ToString();
        }
        return a;
    }
        public void Serialize()
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Fraction>));
            try
            {
                using (Stream fStream = File.Create("test.xml"))
                {
                    xmlFormat.Serialize(fStream, fraction);
                }
              
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        public void DeSerialize()
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Fraction>));
            
                
                using (Stream fStream = File.OpenRead("test.xml"))
                {
                if (fraction != null)
                   {
                    fraction.Clear();
                    fraction = (List<Fraction>)xmlFormat.Deserialize(fStream);
                   }
                else
                   {
                    fraction = (List<Fraction>)xmlFormat.Deserialize(fStream);
                   }
                }
            
        }
        
}

    [Serializable]
    public class Articles
    {
        public string Name { get; set; }
        public int Characters { get; set; }
        public string Announcement { get; set; }
       

        public void AddArticles()
        {
            
            Console.WriteLine("Ввдите название: ");
            Name = Console.ReadLine();
            Console.WriteLine("Укажите количество символов: ");
            Characters = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Запишите краткий анонс: ");
            Announcement = Console.ReadLine();
           
            
        }
        public void PrintArticles(int c)
        {
   
         
            Console.WriteLine($"Название {c} статьи: {Name} ");
            Console.WriteLine($"Rоличество символов: {Characters}");
            Console.WriteLine($"Анонс: {Announcement}");
        }
       

    }
   
    [Serializable]
    public class Magazine
    {
        public string Name { get; set; }
        public string Publisher_name { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        List<Articles > articles;
      
        public Magazine() 
        {
            Date = DateTime.Now;
            Publisher_name = "IT STEP Academy";
            articles=new List<Articles>();
          
        }
        
        public void AddInformation()
        {
            Console.WriteLine($"Введите недостающую информаци о журнале издательства {Publisher_name} " +
                $"дата создания которого {Date.ToShortDateString()}");
            Console.WriteLine("Добавьте название журнала: ");
            Name= Console.ReadLine();
            Console.WriteLine("Укажите количество страниц: ");
            Amount = Int32.Parse(Console.ReadLine());
            AddArticles();
        }
       
            public void AddArticles()
        {
            int AmountArticles;
            Articles temp = new Articles();
           
            Console.WriteLine($"Укажите информацию о статьях в журнале {Name}");
            Console.WriteLine("Первое - введите количество статей ");
            AmountArticles = Int32.Parse(Console.ReadLine());
            for(int i=0; i < AmountArticles; i++)
            {
                Console.Write($"Для {i+1} статьи - ");
                temp.AddArticles();
                articles.Add(temp);
               
            }
            
        }
        public void ReadInformation()
        {
          
            Console.WriteLine($"Название журнала:      {Name}");
            Console.WriteLine($"Название издательства: {Publisher_name}");
            Console.WriteLine($"Дата создания:         {Date.ToShortDateString()}");
            Console.WriteLine($"Количество страниц:    {Amount}");
            Console.WriteLine($"Также ознакомьтесь о статьях данного журнала: ");
            int c = 0;
          foreach(var art in articles)
            {
                c++;
                art.PrintArticles(c);
            }
                
           
        }

        public void DoSerialize()
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(Magazine));
            try
            {
                using (Stream fStream = File.Create("magazine.xml"))
                {
                    xmlFormat.Serialize(fStream, this);

                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            ArticlsesSerialize();
        }
        public void DoDeserialize()
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(Magazine));
            
            Magazine temp = new Magazine();
            using (Stream fStream = File.OpenRead("magazine.xml"))
                temp = (Magazine)xmlFormat.Deserialize(fStream);
            Name = temp.Name;
            Publisher_name = temp.Publisher_name;
            Date = temp.Date;
            Amount = temp.Amount;
            //articles =temp.articles;
            // temp.articles.PrintArticles(0);
            //articles.DeSerialize();
            ArticlsesDeSerialize();
        }

        public void ArticlsesSerialize()
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Articles>));
            try
            {
                using (Stream fStream = File.Create("articles.xml"))
                {
                    xmlFormat.Serialize(fStream, articles);
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public void ArticlsesDeSerialize()
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<Articles>));
            try
            {
                using (Stream fStream = File.OpenRead("articles.xml"))
                {
                    if (articles != null)
                    {
                        articles.Clear();
                        articles = (List<Articles>)xmlFormat.Deserialize(fStream);
                    }
                    else
                    {
                        articles = (List<Articles>)xmlFormat.Deserialize(fStream);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                Console.WriteLine($"Что за хрень?");
            }
        }
    }

    [Serializable]
    public class ArrayMagazne
    {
        public List<Magazine> magazine;
        public ArrayMagazne()
        {
            magazine = new List<Magazine>();

        }
    
    public void AddMagazineToArray(Magazine obj)
    {
        magazine.Add(obj);
    }
        public void ReadMagazineArray()
        {
            foreach(var i in magazine)
            {
                i.ReadInformation();
            }
        }

        public void ArraySerialize()
        {
            BinaryFormatter BinFormat = new BinaryFormatter();
            try
            {
                using (Stream fStream = File.Create("BinaryMagazineArray.bin"))
                {
                    BinFormat.Serialize(fStream, magazine);
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }
        public void  ArrayDeSerialize()
        {
            BinaryFormatter BinFormat = new BinaryFormatter();
            List<Magazine> p = null;
            using (Stream fStream = File.OpenRead("BinaryMagazineArray.bin"))
            {
                p = (List<Magazine>)BinFormat.Deserialize(fStream);
            }
            foreach (var i in p)
            {
                i.ReadInformation();
            }

        }
        
       
       

    }

    class Program
    {
        

    
        static void Main(string[] args)
        {
            Console.WriteLine("Задание 1");
            ArrayFraction A = new ArrayFraction();
            A.Add();
            A.Add();
            A.Add();
            A.Serialize();
            Console.WriteLine("сериализованая информация записана в файл");
            Console.WriteLine("\nзагрузка и десериализация информации информации из файла ->\n");
            A.DeSerialize();
            Console.WriteLine(A);

            Console.WriteLine("перейти к заданию 2 - нажмите любую кнопку");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Задание 2");
            Magazine magazine = new Magazine();
            magazine.AddInformation();
            //  magazine.AddArticles();
           // magazine.ReadInformation();
            magazine.DoSerialize();
            Console.WriteLine("сериализованая информация записана в файл");
         
            Console.WriteLine("\nзагрузка и десериализация информации информации из файла ->\n");
            magazine.DoDeserialize();
            magazine.ReadInformation();
            Console.WriteLine("\nперейти к заданию 3 - нажмите любую кнопку");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Задание 3");
            Console.WriteLine("В предидущем задании информация о журнале и статьях\n" +
                " сохранялась в разных файлах. В задании 3  обьект журнал\n " +
                "(который включает в себя информацию о себе и о статьях) записан в один файл! ");
            BinaryFormatter BinFormat = new BinaryFormatter();
          try
            {
                using (Stream fStream = File.Create("BinaryMagazineNEW.bin"))
                {
                    BinFormat.Serialize(fStream, magazine);
                }
                Console.WriteLine("сериализованая информация записана в файл");
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
              Magazine p = null;
              using (Stream fStream =File.OpenRead("BinaryMagazineNEW.bin"))
              {
                  p = (Magazine)BinFormat.Deserialize(fStream);
              }
            Console.WriteLine("\nзагрузка и десериализация информации информации из файла ->\n");
              p.ReadInformation();
            Console.WriteLine("\nперейти к заданию 4 - нажмите любую кнопку");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("задание 4");
            Console.WriteLine("Сериализация и десиарилизация масива журналов " +
                "(масив создан из обьекта предидущего задания)\n");
            ArrayMagazne array = new ArrayMagazne();
            array.AddMagazineToArray(p);
            array.AddMagazineToArray(p);
            array.AddMagazineToArray(p);
            array.AddMagazineToArray(p);

            array.ArraySerialize();
            Console.WriteLine("сериализованая информация записана в файл");
            array.ArrayDeSerialize();
            Console.WriteLine("\nзагрузка и десериализация информации информации из файла ->\n");
            array.ReadMagazineArray();



        }
    }
}
