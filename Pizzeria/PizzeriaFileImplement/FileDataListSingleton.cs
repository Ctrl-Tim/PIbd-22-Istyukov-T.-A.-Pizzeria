using PizzeriaContracts.Enums;
using PizzeriaFileImplement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace PizzeriaFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;

        private readonly string IngredientFileName = "Ingredient.xml";

        private readonly string OrderFileName = "Order.xml";

        private readonly string PizzaFileName = "Pizza.xml";

        private readonly string ClientFileName = "Client.xml";

        public List<Ingredient> Ingredients { get; set; }

        public List<Order> Orders { get; set; }

        public List<Pizza> Pizzas { get; set; }

        public List<Client> Clients { get; set; }

        private FileDataListSingleton()
        {
            Ingredients = LoadIngredients();
            Orders = LoadOrders();
            Pizzas = LoadPizzas();
            Clients = LoadClients();
        }

        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }

            return instance;
        }

        ~FileDataListSingleton()
        {
            SaveIngredients();
            SaveOrders();
            SavePizzas();
            SaveClients();
        }

        private List<Ingredient> LoadIngredients()
        {
            var list = new List<Ingredient>();

            if (File.Exists(IngredientFileName))
            {
                var xDocument = XDocument.Load(IngredientFileName);

                var xElements = xDocument.Root.Elements("Ingredient").ToList();

                foreach (var elem in xElements)
                {
                    list.Add(new Ingredient
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        IngredientName = elem.Element("IngredientName").Value
                    });
                }
            }

            return list;
        }

        private List<Order> LoadOrders()
        {
            var list = new List<Order>();
            if (File.Exists(OrderFileName))
            {
                XDocument xDocument = XDocument.Load(OrderFileName);
                var xElements = xDocument.Root.Elements("Order").ToList();
                foreach (var elem in xElements)
                {
                    OrderStatus status = (OrderStatus)0;
                    switch ((elem.Element("Status").Value))
                    {
                        case "Принят":
                            status = (OrderStatus)0;
                            break;
                        case "Выполняется":
                            status = (OrderStatus)1;
                            break;
                        case "Готов":
                            status = (OrderStatus)2;
                            break;
                        case "Оплачен":
                            status = (OrderStatus)3;
                            break;

                    }
                    if (string.IsNullOrEmpty(elem.Element("DateImplement").Value))
                    {
                        list.Add(new Order
                        {
                            Id = Convert.ToInt32(elem.Attribute("Id").Value),
                            ClientId = Convert.ToInt32(elem.Element("ClientId").Value),
                            PizzaId = Convert.ToInt32(elem.Element("PizzaId").Value),
                            Count = Convert.ToInt32(elem.Element("Count").Value),
                            Sum = Convert.ToDecimal(elem.Element("Sum").Value),
                            Status = status,
                            DateCreate = Convert.ToDateTime(elem.Element("DateCreate").Value)
                        });
                    }
                    else
                    {
                        list.Add(new Order
                        {
                            Id = Convert.ToInt32(elem.Attribute("Id").Value),
                            ClientId = Convert.ToInt32(elem.Element("ClientId").Value),
                            PizzaId = Convert.ToInt32(elem.Element("PizzaId").Value),
                            Count = Convert.ToInt32(elem.Element("Count").Value),
                            Sum = Convert.ToDecimal(elem.Element("Sum").Value),
                            Status = status,
                            DateCreate = Convert.ToDateTime(elem.Element("DateCreate").Value),
                            DateImplement = Convert.ToDateTime(elem.Element("DateImplement").Value)
                        });
                    }

                }
            }
            return list;
        }

        private List<Pizza> LoadPizzas()
        {
            var list = new List<Pizza>();

            if (File.Exists(PizzaFileName))
            {
                var xDocument = XDocument.Load(PizzaFileName);

                var xElements = xDocument.Root.Elements("Pizza").ToList();

                foreach (var elem in xElements)
                {
                    var pizIngr = new Dictionary<int, int>();
                    foreach (var ingredient in elem.Element("PizzaIngredients").Elements("PizzaIngredient").ToList())
                    {
                        pizIngr.Add(Convert.ToInt32(ingredient.Element("Key").Value), Convert.ToInt32(ingredient.Element("Value").Value));
                    }
                    list.Add(new Pizza
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        PizzaName = elem.Element("PizzaName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value),
                        PizzaIngredients = pizIngr
                    });
                }
            }

            return list;
        }

        private List<Client> LoadClients()
        {
            var list = new List<Client>();

            if (File.Exists(ClientFileName))
            {
                XDocument xDocument = XDocument.Load(ClientFileName);
                var xElements = xDocument.Root.Elements("Client").ToList();

                foreach (var elem in xElements)
                {
                    list.Add(new Client
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ClientFIO = elem.Element("ClientFIO").Value,
                        Email = elem.Element("Email").Value,
                        Password = elem.Element("Password").Value,
                    });
                }
            }
            return list;
        }

        private void SaveIngredients()
        {
            if (Ingredients != null)
            {
                var xElement = new XElement("Ingredients");

                foreach (var ingredient in Ingredients)
                {
                    xElement.Add(new XElement("Ingredient",
                        new XAttribute("Id", ingredient.Id),
                        new XElement("IngredientName", ingredient.IngredientName)));
                }

                var xDocument = new XDocument(xElement);
                xDocument.Save(IngredientFileName);
            }
        }

        private void SaveOrders()
        {
            if (Orders != null)
            {
                var xElement = new XElement("Orders");
                foreach (var order in Orders)
                {
                    xElement.Add(new XElement("Order",
                 new XAttribute("Id", order.Id),
                 new XElement("ClientId", order.ClientId),
                 new XElement("PizzaId", order.PizzaId),
                 new XElement("Count", order.Count),
                 new XElement("Sum", order.Sum),
                 new XElement("Status", order.Status),
                 new XElement("DateCreate", order.DateCreate),
                 new XElement("DateImplement", order.DateImplement)
                 ));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        } 

        private void SavePizzas()
        {
            if (Pizzas != null)
            {
                var xElement = new XElement("Pizzas");

                foreach (var pizza in Pizzas)
                {
                    var ingrElement = new XElement("PizzaIngredients");
                    foreach (var ingredient in pizza.PizzaIngredients)
                    {
                        ingrElement.Add(new XElement("PizzaIngredient",
                        new XElement("Key", ingredient.Key),
                        new XElement("Value", ingredient.Value)));
                    }
                    xElement.Add(new XElement("Pizza",
                        new XAttribute("Id", pizza.Id),
                        new XElement("PizzaName", pizza.PizzaName),
                        new XElement("Price", pizza.Price),
                        ingrElement));
                }

                var xDocument = new XDocument(xElement);
                xDocument.Save(PizzaFileName);
            }
        }

        private void SaveClients()
        {
            if (Clients != null)
            {
                var xElement = new XElement("Clients");

                foreach (var client in Clients)
                {
                    xElement.Add(new XElement("Client",
                    new XAttribute("Id", client.Id),
                    new XElement("ClientFIO", client.ClientFIO),
                    new XElement("Email", client.Email),
                    new XElement("Password", client.Password)));
                }

                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ClientFileName);
            }
        }

        public static void Save()
        {
            instance.SaveIngredients();
            instance.SaveOrders();
            instance.SavePizzas();
            instance.SaveClients();
        }
    }
}
