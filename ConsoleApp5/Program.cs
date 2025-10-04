namespace ConsoleApp5
{
    internal class Program
    {
        static ServerConnection connection;
        static async Task Main(string[] args)
        {
            connection = new ServerConnection("http://127.0.0.1:3000");
            while (true)
            {
                await menu();
            }
        }
        static async Task menu()
        {

            Console.Clear();
            Console.WriteLine("1 osszes marka");
            Console.WriteLine("2 marka letrehozas");
            Console.WriteLine("3 marka torles");
            Console.WriteLine("4 osszes auto");
            Console.WriteLine("5 auto letrehozasa");
            Console.WriteLine("6 auto torlese");
            Console.WriteLine("7 osszes tulaj");
            Console.WriteLine("8 tulaj letrehozasa");
            Console.WriteLine("9 tulaj torlese");


            Console.WriteLine("\n Valassz: ");
            string choice = Console.ReadLine();

            await Switchmenu(choice);

            
            Console.ReadKey();
        }

        static async Task Switchmenu(string choice)
        {
          
            switch (choice)
            {
                case "1":
                    getManufacturer();
                    break;
                case "2":
                    postManufacturer();
                    break;
                case "3":
                    deleteManufacturer();
                    break;
                case "4":
                    getCars();
                    break;
                case "5":
                    postCar();
                    break;
                case "6":
                    deleteCar();
                    break;
                case "7":
                    getOwner();
                    break;
                case "8":
                    postOwner();
                    break;
                case "9":
                    deleteOwner();
                    break;
                default:
                    
                    Console.WriteLine("Ez nem egy opcio");
                    break;
            }
        }
        static async Task getCars()
        {
            Console.WriteLine();
            
            Console.WriteLine("Osszes auto: ");
            List<Cars> all = await connection.GetCars();
            foreach (Cars item in all)
            {
                Console.WriteLine($" - ID: {item.id}, Modell: {item.model}, performance: {item.performace}, Gyartasi ev: {item.makeyear}, Kerek meret: {item.wheelsize}");
            }

            
        }
        static async Task getManufacturer()
        {
            Console.WriteLine();
            
            Console.WriteLine("Osszes marka: ");
            List<Brand> all = await connection.GetBrands();
            foreach (Brand item in all)
            {
                Console.WriteLine($" - ID: {item.id}, name: {item.name}, Alapitas eve: {item.foundingyear}, country ev: {item.country}");
            }

            
        }
        static async Task getOwner()
        {
            Console.WriteLine();
         
            Console.WriteLine("Osszes tulajdonos: ");
            List<Owners> all = await connection.GetOwners();
            foreach (Owners item in all)
            {
                Console.WriteLine($"ID, {item.id}, name: {item.name}, address: {item.address}, birthyear: {item.birthyear}");
            }

            
        }

        static async Task postCar()
        {

            try
            {
                Console.WriteLine();
                Console.WriteLine("Kerlek add meg az alabbi adatokat:");

                int brandid;
                Console.Write("Marka ID: ");

                while (!int.TryParse(Console.ReadLine(), out brandid))
                {
                    Console.Write("Szamot adj meg (ami letezik pls) : ");
                }

                Console.Write("Modell: ");
                string model = Console.ReadLine();

                int performance;
                Console.Write("performance: ");
                while (!int.TryParse(Console.ReadLine(), out performance))
                {
                    Console.Write("Szamot adj meg: ");
                }

                int makeyear;
                Console.Write("Gyartasi ev: ");
                while (!int.TryParse(Console.ReadLine(), out makeyear))
                {
                    Console.Write("Szamot adj meg: ");
                }

                int wheelsize;
                Console.Write("Kerek meret: ");
                while (!int.TryParse(Console.ReadLine(), out wheelsize))
                {
                    Console.Write("Szamot adj meg: ");
                }

                Message response = await connection.PostCars(model, brandid,  performance, makeyear, wheelsize);
               
                Console.WriteLine(response.message);
             

            }
            catch (Exception e)
            {

                
                Console.WriteLine("Ez nem egy opcio");
            }
        }
        static async Task postOwner()
        {

            try
            {
                Console.WriteLine();
                Console.WriteLine("Kerlek add meg az alabbi adatokat:");

                int carid;
                Console.Write("Auto ID: ");
                while (!int.TryParse(Console.ReadLine(), out carid))
                {
                    Console.Write("Szamot adj meg (ami letezik pls) : ");
                }

                Console.Write("name: ");
                string name = Console.ReadLine();
                while (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Ne legyen ures kerlek");
                    Console.Write("name: ");
                    name = Console.ReadLine();
                }


                Console.Write("address: ");
                string address = Console.ReadLine();
                while (string.IsNullOrEmpty(address))
                {
                    Console.WriteLine("Ne legyen ures kerlek");
                    Console.Write("address: ");
                    address = Console.ReadLine();
                }

                int birthyear;
                Console.Write("Szuletesi ev: ");
                while (!int.TryParse(Console.ReadLine(), out birthyear))
                {
                    Console.Write("Szamot adj meg: ");
                }

                Message response = await connection.PostOwners(carid, name, address, birthyear);        
                Console.WriteLine(response.message);

            }
            catch (Exception e)
            {
                Console.WriteLine("Ez nem egy opcio");
            }
        }
        static async Task postManufacturer()
        {

            try
            {
                Console.WriteLine();
                Console.WriteLine("Kerlek add meg az alabbi adatokat:");

                Console.Write("name: ");
                string name = Console.ReadLine();
                while (string.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Ne legyen ures kerlek");
                    Console.Write("name: ");
                    name = Console.ReadLine();
                }

                int foundingyear;
                Console.Write("Alapitas eve: ");
                while (!int.TryParse(Console.ReadLine(), out foundingyear))
                {
                    Console.Write("Szamot adj meg: ");
                }

                Console.WriteLine("country: ");
                string country = Console.ReadLine();
                while (string.IsNullOrEmpty(country))
                {
                    Console.WriteLine("Ne legyen ures kerlek");
                    Console.WriteLine("country: ");
                    country = Console.ReadLine();
                }
                int makeyear;
                Console.WriteLine("makeyear: ");
                while (!int.TryParse(Console.ReadLine(), out makeyear))
                {
                    Console.Write("Szamot adj meg: ");
                }

                Message response = await connection.PostBrands(name, foundingyear, country,makeyear);
                Console.WriteLine(response.message);

            }
            catch (Exception e)
            {

                
                Console.WriteLine("Ez nem egy opcio");
            }
        }


        static async void deleteCar()
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("Add meg az id-jet amit torolni akarsz");
                int id = Convert.ToInt32(Console.ReadLine());
                Message response = await connection.DeleteCar(id);

                
                Console.WriteLine(response.message);


            }
            catch (Exception e)
            {
                
                Console.WriteLine(e.Message);

            }
        }
        static async void deleteOwner()
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("Add meg az id-jet amit torolni akarsz");
                int id = Convert.ToInt32(Console.ReadLine());
                Message response = await connection.DeleteOwner(id);

                
                Console.WriteLine(response.message);


            }
            catch (Exception e)
            {
             
                Console.WriteLine(e.Message);
            }
        }
        static async void deleteManufacturer()
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("Add meg az id-jet amit torolni akarsz");
                int id = Convert.ToInt32(Console.ReadLine());
                Message response = await connection.DeleteBrand(id);

                
                Console.WriteLine(response.message);


            }
            catch (Exception e)
            {
                
                Console.WriteLine(e.Message);

            }
        }
    }
}