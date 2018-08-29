using ChessLib.controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessLib.models
{
    public class Delegates
    {
        public delegate void MovementHandler(object sender, MovementArgs e);
        public delegate void PlacementHandler(object sender, PlacementArgs e);
        public delegate void MovementFailureHandler(object sender, MovementFailureArgs e);
        public delegate void Checked(object sender, CheckedArgs e);
    }
}
