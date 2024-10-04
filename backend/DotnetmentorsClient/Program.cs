using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using RESTFulWCFService;

namespace DotnetmentorsClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //GetOrderDetails("10248");
            //GetOrderTotal("10250");
            //PlaceOrder(); 
            VisitaCliente();
            Console.ReadKey(true);
        }

        private static void GetOrderDetails(string orderID)
        {
            WebClient proxy = new WebClient();
            string serviceURL = string.Format("http://localhost:61090/OrderService.svc/GetOrderDetails/{0}", orderID); 
            byte[] data = proxy.DownloadData(serviceURL);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer obj = new DataContractJsonSerializer(typeof(OrderContract));
            OrderContract order = obj.ReadObject(stream) as OrderContract;
            Console.WriteLine("Order ID : " + order.OrderID);
            Console.WriteLine("Order Date : " + order.OrderDate);
            Console.WriteLine("Order Shipped Date : " + order.ShippedDate);
            Console.WriteLine("Order Ship Country : " + order.ShipCountry);
            Console.WriteLine("Order Total : " + order.OrderTotal);
        }

        private static void GetOrderTotal(string orderID)
        {
            Console.WriteLine();  
            Console.WriteLine("**** Output for GetOrderTotal ************");  
            WebClient proxy = new WebClient();
            string serviceURL = string.Format("http://localhost:61090/OrderService.svc/GetOrderTotal/{0}", orderID);
            byte[] data = proxy.DownloadData(serviceURL);
            Stream stream = new MemoryStream(data);
            DataContractJsonSerializer obj = new DataContractJsonSerializer(typeof(string));
            string order = Convert.ToString(obj.ReadObject(stream));
            Console.WriteLine(order);
        }

        private static void PlaceOrder()
        {            
            OrderContract order = new OrderContract
            {
                OrderID = "999999",
                OrderDate = DateTime.Now.ToString(),
                ShippedDate = DateTime.Now.AddDays(10).ToString(),
                ShipCountry = "Peru",
                OrderTotal = "781"
            };      
      
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(OrderContract));
            MemoryStream mem = new MemoryStream();
            ser.WriteObject(mem, order);
            string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
            WebClient webClient = new WebClient();            
            webClient.Headers["Content-type"] = "application/json";            
            webClient.Encoding = Encoding.UTF8;
            webClient.UploadString("http://localhost:61090/OrderService.svc/PlaceOrder", "POST", data);              
            Console.WriteLine("Order placed successfully...");  
        }

        private static void VisitaCliente()
        {
            DTOVisitaDet det = new DTOVisitaDet
            {
                IdVisita = 0,
                IdMotivo = 1,
                UserReg = "Desarrollo1"
            };

            List<DTOVisitaDet> oLista = new List<DTOVisitaDet>();
            oLista.Add(det);

            DTOVisitaCab order = new DTOVisitaCab
            {
                IdVisita = 0,
                CodCliente = "C20112273922",
                CodVendedor = 62,
                Comentario = "India",
                FecVisita = "20150402",
                UserReg = "Desarrollo1",
                ListDetalle = oLista
            };

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(DTOVisitaCab));
            MemoryStream mem = new MemoryStream();
            ser.WriteObject(mem, order);
            string data = Encoding.UTF8.GetString(mem.ToArray(), 0, (int)mem.Length);
            WebClient webClient = new WebClient();
            webClient.Headers["Content-type"] = "application/json";
            webClient.Encoding = Encoding.UTF8;
            webClient.UploadString("http://localhost:29969/Ventas.svc/EnviarVisita", "POST", data);
            Console.WriteLine("Order placed successfully...");
        }
    }
}
