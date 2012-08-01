﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LuxEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Schmup
{

    class Hero : Character
    {
        private const float SQRT2 = 1.414f;

        private int mana;
        private bool cannotShoot;
        private int currentWeapon;
        private int speed;
        private int currentPower;
        private List<int> weaponList;
        private List<int> speedList;
        private List<int> powerList;
        private int invincibleTime;
        private int speed1;
        private int speed2;
        /// <summary>
        /// Détermine le type de vitesse
        /// </summary>
        private bool speedType;
        private static int weakShotNb = 30;
        private static int strongShotNb = 10;
        private List<HeroShot> weakShots = new List<HeroShot>(weakShotNb);
        private List<HeroShot> strongShots = new List<HeroShot>(strongShotNb);
        private Texture2D weakBulletText;
        private Texture2D strongBulletText;
        private bool powerActivated;
        private HeroShot powerShot;
        private Texture2D powerShotText;

        public Hero(LuxGame game, int life = 0, int takenDamageCollision = 0, int givenDamageCollision = 0, Sprite skin = null, int speed1 = 0, int speed2 = 0)
            : base(game, life, takenDamageCollision, givenDamageCollision, skin)
        {
            this.speed1 = speed1;
            this.speed2 = speed2;
            this.weakBulletText = this.Content.Load<Texture2D>("bullet003-1");
            this.strongBulletText = this.Content.Load<Texture2D>("bullet004-1");
            this.powerShotText = this.Content.Load<Texture2D>("bomb");
        }

        public override void Initialize()
        {
            base.Initialize();
            for (int i = 0; i < weakShotNb; i++)
            {
                HeroShot shot = new HeroShot(this.LuxGame, 0, null);
                Sprite shotSprite = new Sprite(shot, new List<Texture2D>() { weakBulletText }, null);
                shot.Skin = shotSprite;
                shotSprite.SetAnimation(weakBulletText.Name);
                Game.Components.Add(shot);
                Game.Components.Add(shotSprite);
                weakShots.Add(shot);
            }
            for (int i = 0; i < strongShotNb; i++)
            {
                HeroShot shot = new HeroShot(this.LuxGame, 0, null);
                Sprite shotSprite = new Sprite(shot, new List<Texture2D>() { strongBulletText }, null);
                shot.Skin = shotSprite;
                shotSprite.SetAnimation(strongBulletText.Name);
                Game.Components.Add(shot);
                Game.Components.Add(shotSprite);
                strongShots.Add(shot);
            }
            powerShot = new HeroShot(this.LuxGame, 0, null);
            Sprite powerShotSprite = new Sprite(powerShot, new List<Texture2D>() { powerShotText }, null);
            powerShot.Skin = powerShotSprite;
            powerShotSprite.SetAnimation(powerShotText.Name);
            Game.Components.Add(powerShot);
            Game.Components.Add(powerShotSprite);
        }

        public void WeakShoot()
        {
            int cnt = 0;
            bool fini = false;

            while (cnt < weakShotNb && !fini)
            {
                if (weakShots[cnt].IsOutOfRange == true)
                {
                    weakShots[cnt].Speed = new Vector2(0, -10);
                    weakShots[cnt].Position = this.Position;
                    weakShots[cnt].Shoot();
                    fini = true;
                }
                else
                {
                    cnt++;
                }
            }
        }

        public void StrongShoot()
        {
            int cnt = 0;
            bool fini = false;

            while (cnt < strongShotNb && !fini)
            {
                if (strongShots[cnt].IsOutOfRange == true)
                {
                    strongShots[cnt].Speed = new Vector2(0, -20);
                    strongShots[cnt].Position = this.Position;
                    strongShots[cnt].Shoot();
                    fini = true;
                }
                else
                {
                    cnt++;
                }
            }
        }

        public void PowerShoot()
        {
            if (powerShot.IsOutOfRange == true)
            {
                powerShot.Accel = new Vector2(0, -1);
                powerShot.Speed = new Vector2(0, 0);
                powerShot.Position = this.Position;
                powerShot.Shoot();
            }
        }

        public override void Update(GameTime gameTime)
        {
            bool speedChange;

            speedChange = Input.isActionDone(Input.Action.SpeedChange, false);
            if (speedChange)
            {
                if (speedType)
                {
                    speedType = false;
                }
                else
                {
                    speedType = true;
                }
            }
            if (speedType)
            {
                speed = speed1;
            }
            else
            {
                speed = speed2;
            }
            // Gestion du déplacement
            if (Input.isActionDone(Input.Action.Up,true))
            {
                if (Input.isActionDone(Input.Action.Right, true))
                {
                    Position.X += speed / SQRT2;
                    Position.Y -= speed / SQRT2;
                }
                else if (Input.isActionDone(Input.Action.Left, true))
                {
                    Position.X -= speed / SQRT2;
                    Position.Y -= speed / SQRT2;
                }
                else
                {
                    Position.Y -= speed;
                }
            }
            else if (Input.isActionDone(Input.Action.Down, true))
            {
                if (Input.isActionDone(Input.Action.Right, true))
                {
                    Position.X += speed / SQRT2;
                    Position.Y += speed / SQRT2;
                }
                else if (Input.isActionDone(Input.Action.Left, true))
                {
                    Position.X -= speed / SQRT2;
                    Position.Y += speed / SQRT2;
                }
                else
                {
                    Position.Y += speed;
                }
            }
            else if (Input.isActionDone(Input.Action.Left, true))
            {
                Position.X -= speed;
            }
            else if (Input.isActionDone(Input.Action.Right, true))
            {
                Position.X += speed;
            }

            // Gestion des bordures

            if (Position.X < 0)
            {
                Position.X = 0;
            }
            if (Position.Y < 0)
            {
                Position.Y = 0;
            }
            if (Position.X > 800)
            {
                Position.X = 800;
            }
            if (Position.Y > 480)
            {
                Position.Y = 480;
            }

            // Gestion des tirs

            if (Input.isActionDone(Input.Action.Shoot, true))
            {
                if (speedType)
                {
                    WeakShoot();
                }
                else
                {
                    StrongShoot();
                }
            }

            // Gestion des pouvoirs

            if (Input.isActionDone(Input.Action.Power, true))
            {
                PowerShoot();
            }

            // Mise à jour de la position pour les ennemis
            Common.HeroPosition = this.Position;

            base.Update(gameTime);
        }
    }
}
