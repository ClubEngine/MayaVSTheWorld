﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using LuxEngine;
using Microsoft.Xna.Framework.Graphics;

namespace Schmup
{
    class TestEnemy3 : Enemy
    {
        private bool shootsHero;
        private uint waitTimeFrames;
        private uint shootNb;
        /// <summary>
        /// Angle de tir de l'attaquant, se rétrécit pendant l'attaque
        /// </summary>
        private uint shootAngle;
        private float shootSpeed;
        private Vector2 speed = new Vector2((float)2, (float)1);
        private Vector2 accel = new Vector2(0, (float)-0.01);
        private Texture2D bulletTexture;

        public TestEnemy3(LuxGame game, uint life, uint takenDamageCollision, uint givenDamageCollision, bool shootsHero, uint waitTimeFrames, Sprite skin)
            : base(game, life, takenDamageCollision, givenDamageCollision, skin)
        {
            this.shootsHero = shootsHero;
            this.waitTimeFrames = waitTimeFrames;
        }

        public override void Initialize()
        {
            base.Initialize();
            List<string> skinName = new List<string>(1);
            skinName.Add("bullet001-1");
            Vector2 vector = new Vector2(0, 5);
            if (shootsHero)
            {
                Vector2 vect = new Vector2(1, 1);
            }
            shootNb = 1;
            shootSpeed = (float)2;
            shootAngle = 45;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.Position += speed;
            this.speed += accel;
            if (shootNb < 20)
            {
                if (waitTimeFrames == 0)
                {
                    shootNb++;
                    shootSpeed = (float)1.1 * shootSpeed + (float)0.15;
                    shootAngle --;
                    Vector2 vector = new Vector2(0, (float)shootSpeed);
                    if (shootsHero)
                    {
                        Vector2 vect = new Vector2(1, 1);
                    }
                    ShotPattern bPatternTest = new ShotPattern(this.LuxGame, 4, vector, shootAngle, bulletTexture);
                    bPatternTest.Position = this.Position;
                    Game.Components.Add(bPatternTest);
                    waitTimeFrames += 4;
                }
            }
            waitTimeFrames--;
        }
    }
}
