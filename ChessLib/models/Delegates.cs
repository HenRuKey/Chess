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
        public delegate void CheckedHandler(object sender, CheckedArgs e);
        public delegate void CheckMateHandler(object sender, CheckMateArgs e);
        public delegate void PawnPromotionHandler(object sender, PawnPromotionArgs e);
    }
}
