using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WeatherApp
{
    class Program
    {
        public static string? nameCity;
        public static string? ResponseAnswer;

        static void Main(string[] args)
        {
            Console.Write("Введите Ваш город: ");
            nameCity = Console.ReadLine();
            try
            {
                ConnectAsync().Wait();
            }
            catch(Exception)
            {
                Console.WriteLine("Не найденн город / нет подключения к url адресу");
            }
        }

        public static async Task ConnectAsync()
        {
            string textToFile = "";
            string path = "";

            DateTime dateTime(double dateTimes)
            {
                DateTime date = new DateTime(1970, 1, 1, 0, 0, 0);
                return date.AddSeconds(dateTimes);
            }

            WebRequest request = WebRequest.Create("https://api.openweathermap.org/data/2.5/weather?q=" + nameCity + "&units=metric&&appid=1744b9b0a51e3efef446ca399d37fbfa");
            request.Method = "POST";
            WebResponse webResponse = await request.GetResponseAsync();
            using(Stream stream = webResponse.GetResponseStream())
            {
                using(StreamReader reader = new StreamReader(stream))
                {
                    ResponseAnswer = await reader.ReadToEndAsync();
                }
            }
            webResponse.Close();

#pragma warning disable CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.
            Weather_App.Classes.WeatherResponse weatherResponse = JsonConvert.DeserializeObject<Weather_App.Classes.WeatherResponse>(ResponseAnswer);
#pragma warning restore CS8600 // Преобразование литерала, допускающего значение NULL или возможного значения NULL в тип, не допускающий значение NULL.
            Console.WriteLine("Средняя температура: " + weatherResponse.main.temp + " C");
            Console.WriteLine("Влажность: " + weatherResponse.main.humidity + " %");
            Console.WriteLine("Восход солнца: " + dateTime(weatherResponse.sys.sunrise + weatherResponse.timezone).ToString("t"));
            Console.WriteLine("Закат солнца: " + dateTime(weatherResponse.sys.sunset + weatherResponse.timezone).ToString("t"));

            textToFile += weatherResponse.name +
                "\n" + "Средняя температура: " + weatherResponse.main.temp + " C\n" +
                "Влажность: " + weatherResponse.main.temp + " %\n" +
                "Восход солнца: " + dateTime(weatherResponse.sys.sunrise + weatherResponse.timezone).ToString("t") +
                "\nЗакат солнца: " + dateTime(weatherResponse.sys.sunset + weatherResponse.timezone).ToString("t");
            path = @"C:\Users\sasas\Desktop\" + DateTime.Now.ToString("d") + ".txt";
            Console.WriteLine("Файл сохранен в: " + path);
            File.WriteAllText(path, textToFile);
        }

    }
}