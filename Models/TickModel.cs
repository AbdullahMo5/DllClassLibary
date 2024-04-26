﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goorge.Model
{
    public class TickModel
    {
        public double Bid { get; set; }
        public double Ask { get; set; }
        public double Last { get; set; }
        public string Symbol { get; set; }
    }
}