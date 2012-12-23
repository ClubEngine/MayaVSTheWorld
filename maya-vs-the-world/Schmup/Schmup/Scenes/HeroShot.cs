﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LuxEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Schmup
{
    class HeroShot : Shot
    {
        public HeroShot(LuxGame game, World world, int invincibleTimeMillisec, Sprite skin = null)
            : base(game, invincibleTimeMillisec, true, world, 1, null)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
