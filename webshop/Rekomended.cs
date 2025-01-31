using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webshop.Helpers;
using webshop.Models;

namespace webshop
{
    internal class Rekomended
    {
        private List<Models.Garment> garments;
        public List<Models.Garment> show { get; private set; } = new List<Models.Garment>();

        public Rekomended()
        {
            var getGarments = new Helpers.Dapper();
            garments = getGarments.GetGraments();
        }

        public void SetShow()
        {
            foreach (var garment in garments)
            {
                garment.Rekomended = false;
            }

            List<string> text = garments
                .Distinct()
                .Select(p => $"{p.Id}: {p.Name.PadRight(25)} Price: {Math.Round(p.Price, 2)}")
                .ToList();
            var window = new WindowManager("", 0, 0, text);
            window.Draw();

            Console.WriteLine("Choose five desired products to show on the front page");

            List<Models.Garment> garmentsToShow = new List<Models.Garment>();
            for (int i = 0; i < 5; i++)
            {
                while (true)
                {
                    Console.WriteLine("Product " + (i + 1) + ": ");
                    if (int.TryParse(Console.ReadLine(), out int choice) &&
                        garments.Any(p => p.Id == choice))
                    {
                        var selectedGarment = garments.First(p => p.Id == choice);
                        selectedGarment.Rekomended = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please enter a valid product ID.");
                    }
                }
            }
            Console.Clear();

            show = garmentsToShow;
        }


        public List<Models.Garment> GetShow()
        {
            show = garments
                .Where(g => g.Rekomended == true)
                .Take(5)
                .ToList();

            if (!show.Any())
            {
                show = GetRandomGarments(5);
            }
            return show;
        }

        private List<Models.Garment> GetRandomGarments(int count)
        {
            Random rnd = new Random();
            return garments
                .Distinct()
                .OrderBy(_ => rnd.Next(garments.Count()))
                .Take(count)
                .ToList();
        }
    }
}
