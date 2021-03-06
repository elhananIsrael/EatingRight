﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.Entitys
{
    public class Nutrition
    {

        public double? Calories { get; set; }

        public double? Carbohydrate { get; set; }

        public double? Fat { get; set; }

        public double? Protein { get; set; }

        public double? Sodium { get; set; }

        public double? Sugar { get; set; }

        public Nutrition(double? calories,double? carbohydrate,double? fat,
                         double? protein, double? sodium, double? sugar)
        {
            Calories = calories;
            Carbohydrate = carbohydrate;
            Fat = fat;
            Protein = protein;
            Sodium = sodium;
            Sugar = sugar;
        }

        public Nutrition()
        {
            Calories = 0;
            Carbohydrate = 0;
            Fat = 0;
            Protein = 0;
            Sodium = 0;
            Sugar = 0;
        }

    }



}
