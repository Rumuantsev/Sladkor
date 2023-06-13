using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Linq;

class Product : ICloneable
{
    private static int nextId = 0;
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }

    public Product()
    {
        Id = ++nextId;
    }
    public object Clone()
    {
        return new Product { Id = this.Id, Name = this.Name, Price = this.Price };
    }
}

class CartProduct
{
    public Product Product { get; set; }
    public int Quantity { get; set; }

    public CartProduct(Product product, int quantity)
    {
        Product= product;
        Quantity = quantity;
    }
}

class Program
{
    static List<Product> ProductsList = new List<Product>();
    static List<CartProduct> CartList = new List<CartProduct>();

    static void Main()
    {

        Product ExampleProduct1 = new Product { Name = "Шоколад Milka", Price = 100 };
        ProductsList.Add(ExampleProduct1);

        Product ExampleProduct2 = new Product { Name = "Шоколад Schogetten", Price = 120 };
        ProductsList.Add(ExampleProduct2);

        Product ExampleProduct3 = new Product { Name = "Леденец Sweet Ness", Price = 70 };
        ProductsList.Add(ExampleProduct3);

        Product ExampleProduct4 = new Product { Name = "Газ. вода Fanta", Price = 90 };
        ProductsList.Add(ExampleProduct4);

        Product ExampleProduct5 = new Product { Name = "Газ. вода Dr.Pepper", Price = 110 };
        ProductsList.Add(ExampleProduct5);

        bool MainMenuExit = false;

        while (!MainMenuExit)
        {
            Console.WriteLine("Добро пожаловать в интернет-магазин Сладкое королевство!");
            Console.WriteLine("Авторизация");
            Console.WriteLine("Выберите кто будет пользоваться приложением:");
            Console.WriteLine("1. Пользователь");
            Console.WriteLine("2. Администратор");
            Console.WriteLine();
            Console.WriteLine("Для выхода из приложения нажмите 0");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Выберите: ");

            string MainMenuChoice = Console.ReadLine();

            switch (MainMenuChoice)
            {
                case "1":
                    Console.Clear();    
                    UserMenu();
                    break;
                case "2":
                    string InputPassword;
                    Console.Clear();
                    Console.WriteLine("Для авторизации от лица администратора требуется пароль");
                    Console.WriteLine("Введите пароль:"); InputPassword = Console.ReadLine();
                    if (InputPassword == "1") 
                    {
                        Console.Clear();
                        AdminMenu();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Неверный пароль!");
                    }
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "0":
                    MainMenuExit = true;
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
            }
        }
    }

    static void UserMenu()
    {
        bool UserMenuExit = false;

        while (!UserMenuExit)
        {
            Console.WriteLine("Меню пользователя");
            Console.WriteLine("1. Посмотреть каталог");
            Console.WriteLine("2. Добавить товар в корзину");
            Console.WriteLine("3. Очистить корзину");
            Console.WriteLine("4. Просмотреть корзину");
            Console.WriteLine("5. Оформить заказ");
            Console.WriteLine("6. Поменять пользователя");
            Console.WriteLine();
            Console.WriteLine("Выберите действие:");

            string UserMenuChoice = Console.ReadLine();

            switch (UserMenuChoice)
            {
                case "1":
                    Console.Clear();
                    ShowAllProducts();
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "2":
                    Console.Clear();
                    AddToCart();
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "3":
                    Console.Clear();
                    CartList.Clear();
                    Console.WriteLine("Корзина очищена.");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "4":
                    Console.Clear();
                    ViewCart();
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "5":
                    Console.Clear();
                    PlaceOrder();
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "6":
                    UserMenuExit = true;
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                    break;
            }
        }

    }

    static void AddToCart()
    {
        Console.WriteLine("Введите ID товара для добавления в корзину:");
        int id = int.Parse(Console.ReadLine());

        Product SelectedProduct = ProductsList.Find(ItemProducts => ItemProducts.Id == id);
        if (SelectedProduct != null)
        {
            Console.WriteLine($"Выбран товар: {SelectedProduct.Name}, Цена: {SelectedProduct.Price}");

            Console.WriteLine("Введите количество:");
            int QuantitySelectedProduct = int.Parse(Console.ReadLine());
            Product ProductToCart = (Product)SelectedProduct.Clone();
            CartProduct NewCartProduct = new CartProduct(ProductToCart, QuantitySelectedProduct);
            CartList.Add(NewCartProduct);

            Console.WriteLine("Товар успешно добавлен в корзину.");
        }
        else
        {
            Console.WriteLine("Товар с указанным ID не найден.");
        }
    }


    static void ViewCart()
    {
        if (CartList.Count == 0)
        {
            Console.WriteLine("Корзина пуста.");
            return;
        }

        Console.WriteLine("Содержимое корзины:");
        foreach (CartProduct CurrentCartProduct in CartList)
        {
            Console.WriteLine($"Товар: {CurrentCartProduct.Product.Name}, Количество: {CurrentCartProduct.Quantity}, Цена за единицу: {CurrentCartProduct.Product.Price}");
        }
    }

    static void PlaceOrder()
    {
        if (CartList.Count == 0)
        {
            Console.WriteLine("Корзина пуста.");
            return;
        }

        Console.WriteLine("Оформление заказа");
        Console.WriteLine("Укажите адресс доставки");
        Console.WriteLine("ВНИМАНИЕ: доставка возможна только в городе Кемерово!");
        string Adress = Console.ReadLine();
        Console.Clear();

        Console.WriteLine("Оформление заказа");
        Console.WriteLine("Укажите время доставки");
        Console.WriteLine("ВНИМАНИЕ: доставка возможна только с 10:00 до 18:00!");
        string Time = Console.ReadLine();
        Console.Clear();

        Console.WriteLine("Ваш заказ:");
        double OrderAmount = 0;

        foreach (CartProduct CurrentCartProduct in CartList)
        {
            double CartProductAmount = CurrentCartProduct.Product.Price * CurrentCartProduct.Quantity;
            Console.WriteLine($"Товар: {CurrentCartProduct.Product.Name}, Количество: {CurrentCartProduct.Quantity}, Сумма: {CartProductAmount}");
            OrderAmount += CartProductAmount;
        }

        Console.WriteLine($"Общая сумма заказа: {OrderAmount}");
        Console.WriteLine($"Адрес: {Adress}");
        Console.WriteLine($"Время: {Time}");

        Console.WriteLine("Заказ успешно оформлен. Спасибо за покупку!");

        CartList.Clear();
    }

    static void AdminMenu()
    {
        bool AdminMenuExit = false;

        while (!AdminMenuExit)
        {
            Console.WriteLine("Меню администратора");
            Console.WriteLine("1. Показать все товары");
            Console.WriteLine("2. Добавить товар");
            Console.WriteLine("3. Удалить товар");
            Console.WriteLine("4. Поменять пользователя");
            Console.WriteLine();
            Console.WriteLine("Выберите действие:");

            string AdminMenuChoice = Console.ReadLine();

            switch (AdminMenuChoice)
            {
                case "1":
                    Console.Clear();
                    ShowAllProducts();
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "2":
                    Console.Clear();
                    AddProduct();
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "3":
                    Console.Clear();
                    DeleteProduct();
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case "4":
                    AdminMenuExit = true;
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                    break;
            }
        }

    }

    static void ShowAllProducts()
    {
        if (ProductsList.Count == 0)
        {
            Console.WriteLine("Товары отсутствуют.");
            return;
        }

        Console.WriteLine("Список товаров:");
        foreach (Product CurrentProduct in ProductsList)
        {
            Console.WriteLine($"ID: {CurrentProduct.Id}, Название: {CurrentProduct.Name}, Цена: {CurrentProduct.Price}");
        }
    }

    static void AddProduct()
    {
        Console.WriteLine("Введите название товара:");
        string NewProductName = Console.ReadLine();

        Console.WriteLine("Введите цену товара:");
        double NewProductPrice = double.Parse(Console.ReadLine());

        Product newProduct = new Product { Name = NewProductName, Price = NewProductPrice };
        ProductsList.Add(newProduct);

        Console.WriteLine("Товар успешно добавлен.");
    }

    static void DeleteProduct()
    {
        Console.WriteLine("Введите ID товара для удаления:");
        int id = int.Parse(Console.ReadLine());

        Product SelectedProduct = ProductsList.Find(ItemProducts => ItemProducts.Id == id);
        if (SelectedProduct != null)
        {
            ProductsList.Remove(SelectedProduct);
            Console.WriteLine("Товар успешно удален.");
        }
        else
        {
            Console.WriteLine("Товар с указанным ID не найден.");
        }
    }
}
