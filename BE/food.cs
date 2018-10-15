using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Food
    {
        // Declares the variables with getters and setters
        public string Name { get; set; }
        public double Calcium { get; set; }
        public string ServingSize { get; set; }
        public double Weight { get; set; }
        public double PercentWater { get; set; }
        public double Calories { get; set; }
        public double Carbs { get; set; }
        public double Protein { get; set; }
        public double TotalFat { get; set; }
        public double SatFat { get; set; }
        public double UnSatFat { get; set; }
        public double MonoFat { get; set; }
        public double Iron { get; set; }
        public double Magnesium { get; set; }
        public double Sodium { get; set; }
        public double Phospherous { get; set; }
        public double Zinc { get; set; }
        public double Potassium { get; set; }
        public double Riboflavin { get; set; }
        public double Niacin { get; set; }
        public double Thiamin { get; set; }
        public double VitA { get; set; }
        public double VitB { get; set; }
        public double VitC { get; set; }
        public string Type { get; set; }

        // Constructor that associates variables with values
        public Food(string name = "", double calc = 0, string servingsize = "",
                    double weight = 0, double water = 0, double calories = 0, double carbs = 0,
                    double protein = 0, double totalfat = 0, double satfat = 0, double unsatfat = 0, double monofat = 0,
                    double iron = 0, double magnesium = 0, double sodium = 0, double phospherous = 0,
                    double zinc = 0, double potassium = 0, double ribo = 0, double niacin = 0,
                    double thiamin = 0, double vitA = 0, double vitB = 0, double vitC = 0, string type = "")
        {
            Name = name;
            Calcium = calc;
            ServingSize = servingsize;
            Weight = weight;
            PercentWater = water;
            Calories = calories;
            Carbs = carbs;
            Protein = protein;
            TotalFat = totalfat;
            SatFat = satfat;
            UnSatFat = unsatfat;
            MonoFat = monofat;
            Iron = iron;
            Magnesium = magnesium;
            Sodium = sodium;
            Phospherous = phospherous;
            Zinc = zinc;
            Potassium = potassium;
            Riboflavin = ribo;
            Niacin = niacin;
            Thiamin = thiamin;
            VitA = vitA;
            VitB = vitB;
            VitC = vitC;
            Type = type;
        }
    }
}
