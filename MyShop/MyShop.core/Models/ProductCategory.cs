﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.core.Models
{
    public class ProductCategory : BaseEntity
    {
        //public string Id { get; set; }
        public string Category { get; set; }

        //public ProductCategory()
        //{
        //    this.Id = Guid.NewGuid().ToString();
        //}
    }
}
