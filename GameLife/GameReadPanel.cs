﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLife
{
    abstract class GameReadPanel
    {
        protected OutputMatrix output;
        abstract public char Space { get; set; }
        abstract public int X { get; set; }
        abstract public int Y { get; set; }
        abstract public int Width { get; set; }
        abstract public int Height { get; }
        abstract public void Write();
        abstract public string Read();
        abstract public void Clear();
        public GameReadPanel(OutputMatrix output)
        {
            this.output = output;
            X = Y = 0;
            Width = 0;
        }
    }
}