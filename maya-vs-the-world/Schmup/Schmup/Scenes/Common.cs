﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using LuxEngine;

namespace Schmup
{
    class Common
    {
        // Sert aux évènements aléatoires
        private static Random rand = new Random();
        public static Random Rand
        {
            get { return rand; }
        }
        // Entiers servant pour le jeu
        // A enlever une fois les collisions codées?
        // HeroHit : Nombre de fois que le héros a été touché
        public static int HeroHit;
        // PowerHit : Nombre de tirs que la charge a croisé
        // Sert aussi pour le score
        public static decimal PowerHit;
        // BossHit : Nombre de fois que le boss a été touché
        public static int BossHit;
    }
}
