﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LuxEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Schmup
{

    class Hero : Character
    {
        private const float SQRT2 = 1.414f;

        private uint mana;
        private bool cannotShoot;
        private uint currentWeapon;
        private uint speed;
        private uint currentPower;
        private List<uint> weaponList;
        private List<uint> speedList;
        private List<uint> powerList;
        private uint invincibleTime;

        public Hero(LuxGame game, uint life = 0, uint takenDamageCollision = 0, uint givenDamageCollision = 0, Sprite skin = null, uint speed = 0)
            : base(game, life, takenDamageCollision, givenDamageCollision, skin)
        {
            this.speed = speed;
        }

        public override void Update(GameTime gameTime)
        {
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

            // Gestion des tirs
            if (Input.isActionDone(Input.Action.Confirm, true))
            {
                List<string> skinName = new List<string>(1);
                skinName.Add("bullet003-1");
                Shot shot = new Shot(this.LuxGame, 0, null);
                Sprite shotSprite = new Sprite(shot, skinName);
                shot.Speed = new Vector2(0, -10);
                shot.Position = this.Position;
                shot.Skin = shotSprite;
                shotSprite.SetAnimation("bullet003-1");
                Game.Components.Add(shot);
                Game.Components.Add(shotSprite);
            }

            base.Update(gameTime);
        }
    }
}
