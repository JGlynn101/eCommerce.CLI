﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Library.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name {  get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        public Customer() { }
    }
}
