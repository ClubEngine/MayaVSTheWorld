﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LuxEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Schmup
{
    class MainScene : Scene
    {

        // Textures utilisées pendant tout le combat.
        private Texture2D bullet1Texture;
        private Texture2D bullet2Texture;
        private Texture2D enemyTexture;

        public MainScene(LuxGame game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            this.bullet1Texture = this.Content.Load<Texture2D>("bullet001-1");
            this.bullet2Texture = this.Content.Load<Texture2D>("bullet002-1");
            this.enemyTexture = this.Content.Load<Texture2D>("commonEnemy");
            List<string> skinName = new List<string>(3);
            skinName.Add("carre");
            skinName.Add("bullet001-1");
            skinName.Add("bullet001-2");
            skinName.Add("hero");
            Vector2 vect = new Vector2(0, 1);
            Vector2 rien = new Vector2(0, 0);
            // Les creations de sprite doivent être dans Initialize.

            BigBoss boss = new BigBoss(this.LuxGame, 10, 10, 10, false, 1, null);
            boss.Skin = new Sprite(boss, new List<string>() { "boss" });
            boss.Skin.SetAnimation("boss");
            boss.Position = new Vector2(400, 50);

            Hero hero = new Hero(this.LuxGame, 1, 0, 0, null, 5, 2);
            Sprite heroSprite = new Sprite(hero, skinName);
            hero.Position = new Vector2(400, 400);
            Common.HeroPosition = hero.Position;
            hero.Skin = heroSprite;
            // Il faut ajouter au jeu les élements que vous créez.
            Game.Components.Add(boss);
            Game.Components.Add(hero);
            Game.Components.Add(heroSprite);

            heroSprite.SetAnimation("hero");
        }

        //public void Create()
        //{
        //    foreach (TestEnemy enemy in commonEnemies)
        //    {
        //        enemy.Create();
        //    }
        //}

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //if (gameTime.TotalGameTime.TotalSeconds.Equals(15))
            //{
            //    this.Create();
            //}
        }
    }
}
