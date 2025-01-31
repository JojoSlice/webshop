using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webshop.Helpers;

namespace webshop
{
    internal class StartPage
    {
        public async Task StartWindow()
        {
            var getPopular = new Popular();
            var popular = await getPopular.GetPopularAsync();

            var showPopular = popular
                .Select(p => $"{p.Name.PadRight(25)} Price: {Math.Round(p.Price, 2)}")
                .ToList();
            
            var getSale = new Sale();
            var garmentsOnSale = await getSale.GetSaleAsync();
            var sale = garmentsOnSale.Select(g => $"{g.Name.PadRight(25)} - Discount price: {Math.Round(g.Price * g.SaleTT, 2)}").Distinct().Take(5).ToList();
            
            var rekomended = new Rekomended();
            var show = rekomended.GetShow();
            var showSelected = show
                .Select(p => $"{p.Name.PadRight(25)} Price: {Math.Round(p.Price, 2)}")
                .ToList();

            Welcome();
            Box1Popular(showPopular);
            Box2Sale(sale);
            Box3Show(showSelected);
            Box4Menu();
            Box5Cart();
            await StartPageMovement(popular, garmentsOnSale, show);
        }

        public void Welcome()
        {
            List<string> text = new List<string>
            { 
@"                ██╗   ██╗██╗  ████████╗██╗███╗   ███╗ █████╗ ████████╗███████╗    ███████╗██╗  ██╗ ██████╗ ██████╗  ██╗",
@"                ██║   ██║██║  ╚══██╔══╝██║████╗ ████║██╔══██╗╚══██╔══╝██╔════╝    ██╔════╝██║  ██║██╔═══██╗██╔══██╗ ██║",
@"                ██║   ██║██║     ██║   ██║██╔████╔██║███████║   ██║   █████╗      ███████╗███████║██║   ██║██████╔╝ ██║",
@"                ██║   ██║██║     ██║   ██║██║╚██╔╝██║██╔══██║   ██║   ██╔══╝      ╚════██║██╔══██║██║   ██║██╔═══╝  ╚═╝", 
@"                ╚██████╔╝███████╗██║   ██║██║ ╚═╝ ██║██║  ██║   ██║   ███████╗    ███████║██║  ██║╚██████╔╝██║      ██╗",
@"                 ╚═════╝ ╚══════╝╚═╝   ╚═╝╚═╝     ╚═╝╚═╝  ╚═╝   ╚═╝   ╚══════╝    ╚══════╝╚═╝  ╚═╝ ╚═════╝ ╚═╝      ╚═╝",   
            };

            var welcomeWindow = new WindowManager("Welcome to", 0, 0, text);
            welcomeWindow.Draw();
        }

        public void Box1Popular(List<string> popular)
        {
            var boxWindow = new WindowManager("Popular", 2, 10, popular);
            boxWindow.Draw();
        }

        public void Box2Sale(List<string> sale)
        {
            var boxWindow = new WindowManager("On Sale", 50, 10, sale);
            boxWindow.Draw();
        }

        public void Box3Show(List<string> show)
        {
            var boxWindow = new WindowManager("Rekommended", 2, 17, show);
            boxWindow.Draw();
        }

        public void Box4Menu()
        {
            string LogReg;
            if (Account.Loggedin)
            { LogReg = "'L' to LogOut"; }
            else
            { LogReg = "'L' to Login or Register"; }

            List<string> text = new List<string> { "Press designated key for navigation:",
                LogReg,
                "'S' to Search",
                "'P' to see popular products",
                "'B' to see products on sale",
                "'R' to see rekommended products",
                "'C' to see all categories"};

            if (Account.Loggedin)
            {
                string accountSpecifik;
                if(Account.Admin)
                {
                    accountSpecifik = "'A' to view Admin page";
                }
                else
                {
                    accountSpecifik = "'A' to view Account page";
                }
                text.Add(accountSpecifik);
            }
            var boxWindow = new WindowManager("", 108, 10, text);
            boxWindow.Draw();
        }

        public void Box5Cart()
        {
            if (TempCart.GetTempCart().Any())
            {
                var cart = new ViewCart();
                var list = cart.View();
                var boxWindow = new WindowManager("Cart", 50, 17, list);
                boxWindow.Draw();
            }
        }

        public async Task StartPageMovement(List<Models.Garment> popular, List<Models.Garment> sale, List<Models.Garment> show)
        {
            var key = Console.ReadKey(true);
            var select = new SelectionPage();

            switch (key.Key)
            {
                case ConsoleKey.L:
                    if (Account.Loggedin)
                    { Account.ResetStatus(); }
                    else
                    {
                        Console.WriteLine("Press 1 to LogIn, 2 to Register");
                        var choice = Console.ReadKey(true);
                        if (choice.Key == ConsoleKey.D1)
                        {
                            var login = new LogIn();
                            await login.Log();
                        }
                        if (choice.Key == ConsoleKey.D2)
                        {
                            var reg = new Reg();
                            await reg.ReggIn();
                        }
                    }
                    break;
                case ConsoleKey.S:
                    var search = new Search();
                    await search.SearchBarAsync();
                    break;
                case ConsoleKey.P:
                    select.Selection(popular);
                    break;
                case ConsoleKey.B:
                    select.Selection(sale);
                    break;
                case ConsoleKey.R:
                    select.Selection(show);
                    break;
                case ConsoleKey.C:
                    var category = new Categories();
                    await category.Run();
                    break;
                case ConsoleKey.A:
                    if (Account.Loggedin)
                    {
                        if (Account.Admin)
                        {
                            var page = new AdminPage();
                            await page.Run();
                        }
                        else
                        {
                            var page = new CustomerPage();
                            await page.Run(Account.LoggedinID);
                        }
                    }
                    break;
            }
        }
    }
}
