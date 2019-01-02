﻿using System;
using System.Collections.Generic;

namespace H_Plus_Sports.Models
{
    public partial class Salesperson
    {
        public Salesperson()
        {
            Order = new HashSet<Order>();
        }

        public int SalespersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
