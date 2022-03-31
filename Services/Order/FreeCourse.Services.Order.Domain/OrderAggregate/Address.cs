﻿using FreeCourse.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    public class Address:ValueObject
    {
        public string Province { get;private set; }
        public string Distinct { get; private set; }
        public string Street { get; private set; }
        public string ZipCode { get; private set; }
        public string Line { get; private set; }

        public Address(string province, string distinct, string street, string zipCode, string line)
        {
            Province = province;
            Distinct = distinct;
            Street = street;
            ZipCode = zipCode;
            Line = line;
        }

        public void SetZipCode(string zipcode)
        {
            // Business rules

            ZipCode= zipcode;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Province;
            yield return Distinct;
            yield return Street;
            yield return ZipCode;
            yield return Line;  
        }
    }
}
