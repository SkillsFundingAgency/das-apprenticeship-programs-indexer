﻿namespace Sfa.Das.ApplicationServices.Models
{
    public sealed class Address
    {
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Town { get; set; }

        public string County { get; set; }

        public string Postcode { get; set; }

        public double Lat { get; set; }

        public double Long { get; set; }
    }
}