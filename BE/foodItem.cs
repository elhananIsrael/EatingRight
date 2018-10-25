﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class foodItem
    {

        public long Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public nutrition Nutritions { get; set; }

        public foodItem(long id, string name, string imageUrl, nutrition ntn = null)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
            Nutritions = ntn;
        }

    }
}