using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    
    internal class ServerConnection
    {
        HttpClient client = new HttpClient();
        public List<Cars> result = new List<Cars>();
        public List<Owners> resultt = new List<Owners>();
        public List<Brand> resulttt = new List<Brand>();
        string baseurl = "";
        public ServerConnection(string url)
        {

            if (!url.StartsWith("http://")) throw new ArgumentException("Hibas url http:// megadasa kotelezo");
            baseurl = url;
        }
        public async Task<List<Cars>> GetCars()
        {


            string url = baseurl + "/car";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                result = JsonSerializer.Deserialize<List<Cars>>(await response.Content.ReadAsStringAsync());

            }
            catch (Exception e)
            {

                Console.Write(e.Message);
            }
            return result;
        }
        public async Task<List<Owners>> GetOwners()
        {

            string url = baseurl + "/owner";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                resultt = JsonSerializer.Deserialize<List<Owners>>(await response.Content.ReadAsStringAsync());

            }
            catch (Exception e)
            {

                Console.Write(e.Message);
            }
            return resultt;
        }
        public async Task<List<Brand>> GetBrands()
        {

            string url = baseurl + "/manufacturers";
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                resulttt = JsonSerializer.Deserialize<List<Brand>>(await response.Content.ReadAsStringAsync());

            }
            catch (Exception e)
            {

                Console.Write(e.Message);
            }
            return resulttt;
        }
        public async Task<Message> PostCars(string model, int brandid, int makeyear, int wheelsize, int performance)
        {
            Message message = new Message();
            string url = baseurl + "/car";
            try
            {
                var jsondata = new
                {
                    model = model,
                    brandid = brandid,
                    makeyear = makeyear,
                    wheelsize = wheelsize,
                    performance = performance
                };
                string jsonstring = JsonSerializer.Serialize(jsondata);
                HttpContent content = new StringContent(jsonstring, Encoding.UTF8, "Application/JSON");
                HttpResponseMessage response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                message = JsonSerializer.Deserialize<Message>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

            return message;
        }
        public async Task<Message> PostOwners( int carid, string name, string address, int birthyear)
        {
            Message message = new Message();
            string url = baseurl + "/owner";
            try
            {
                var jsondata = new
                {
                    
                    carid = carid,
                    name = name,
                    address = address,
                    birthyear = birthyear
                };
                string jsonstring = JsonSerializer.Serialize(jsondata);
                HttpContent content = new StringContent(jsonstring, Encoding.UTF8, "Application/JSON");
                HttpResponseMessage response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                message = JsonSerializer.Deserialize<Message>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

            return message;
        }
        public async Task<Message> PostBrands( string name, int foundingyear, string country, int makeyear)
        {
            Message message = new Message();
            string url = baseurl + "/manufacturers";
            try
            {
                var jsondata = new
                {
                    
                    name = name,
                    foundingyear = foundingyear,
                    country = country,
                    makeyear = makeyear
                };
                string jsonstring = JsonSerializer.Serialize(jsondata);
                HttpContent content = new StringContent(jsonstring, Encoding.UTF8, "Application/JSON");
                HttpResponseMessage response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                message = JsonSerializer.Deserialize<Message>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

            return message;
        }
        public async Task<Message> DeleteCar(int id)
        {
            Message message = new Message();
            string url = baseurl + "/car/" + id;
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(url);
                response.EnsureSuccessStatusCode();
                message = JsonSerializer.Deserialize<Message>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

            return message;
        }
        public async Task<Message> DeleteOwner(int id)
        {
            Message message = new Message();
            string url = baseurl + "/owner/" + id;
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(url);
                response.EnsureSuccessStatusCode();
                message = JsonSerializer.Deserialize<Message>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

            return message;
        }
        public async Task<Message> DeleteBrand(int id)
        {
            Message message = new Message();
            string url = baseurl + "/manufacturers/" + id;
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(url);
                response.EnsureSuccessStatusCode();
                message = JsonSerializer.Deserialize<Message>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

            return message;
        }
    }
}
