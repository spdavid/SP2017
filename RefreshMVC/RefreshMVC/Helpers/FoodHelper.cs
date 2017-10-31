using RefreshMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RefreshMVC.Helpers
{
    public class FoodHelper
    {
        public static List<Drink> GetDrinks()
        {
            List<Drink> drinks = new List<Drink>
            {
                new Drink() { Title = "Soda", Url = "http://media.beam.usnews.com/28/76/090af7a34b4681895921789ffe6b/150617-sodaglass-stock.jpg" },
                new Drink() { Title = "Milk", Url = "https://upload.wikimedia.org/wikipedia/commons/0/0e/Milk_glass.jpg" },
                new Drink() { Title = "Beer", Url = "https://www.drinkpreneur.com/wp-content/uploads/2017/04/drinkpreneur_2016-01-26-1453821995-8643361-beermain.jpg" },
                new Drink() { Title = "Vodka", Url = "https://www.wikihow.com/images/thumb/f/f4/Drink-Vodka-Step-3-preview.jpg/550px-nowatermark-Drink-Vodka-Step-3-preview.jpg" }
            };

            return drinks;
        }

    }
}