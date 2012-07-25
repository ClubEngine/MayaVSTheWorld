﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LuxEngine;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Schmup
{
    class LockShotPattern : ShotPattern
    {

        public LockShotPattern(LuxGame game, uint shotNb, Vector2 direction, uint angleBtwShotsDegrees, Texture2D bulletText, Vector2 position)
            : base(game, shotNb, direction, angleBtwShotsDegrees, bulletText)
        {
            this.Direction(position);
        }

        public void Shoot(Vector2 direction)
        {
            this.Direction(direction);
            this.Shoot();
        }
    }
}
