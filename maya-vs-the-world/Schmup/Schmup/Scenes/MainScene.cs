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
        private Texture2D bulletTexture;
        private Texture2D enemyTexture;

        public MainScene(LuxGame game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            this.bulletTexture = this.Content.Load<Texture2D>("bullet001-1");
            this.enemyTexture = this.Content.Load<Texture2D>("commonEnemy");
            List<string> skinName = new List<string>(3);
            skinName.Add("carre");
            skinName.Add("bullet001-1");
            skinName.Add("bullet001-2");
            skinName.Add("hero");
            Vector2 vect = new Vector2(0,1);
            Vector2 rien = new Vector2(0,0);
            // Les creations de sprite doivent être dans Initialize.
            
            Hero hero = new Hero(this.LuxGame, 1, 0, 0, null, 5, 2);
            Sprite heroSprite = new Sprite(hero, skinName);
            // Instancions 20 ennemis communs et un boss.
            List<TestEnemy> commonEnemies = new List<TestEnemy>(20);
            for (int i = 0; i < 20; i++)
            {
                commonEnemies.Add(new TestEnemy(this.LuxGame, 10, 2, 3, false, (uint)i*5+20, null, bulletTexture));
                commonEnemies[i].Skin = new Sprite(commonEnemies[i],new List<Texture2D>() { enemyTexture }, null);
                commonEnemies[i].Skin.SetAnimation(enemyTexture.Name);
                commonEnemies[i].Position = new Vector2(i*20, 300 - i*20);
                // Il faut appliquer "SetAnimation" au sprite pour qu'il affiche quelque chose.
                Game.Components.Add(commonEnemies[i]);
            }
            hero.Position = new Vector2(400, 200);
            hero.Skin = heroSprite;
            TestEnemy boss = new TestEnemy(this.LuxGame, 10, 2, 3, false, 10, null, bulletTexture); 
            boss.Skin = new Sprite(boss,new List<string>() { "boss" });
            boss.Skin.SetAnimation("boss");
            boss.Skin.Position = new Vector2(100, 100);
            // Il faut ajouter au jeu les élements que vous créez.
            Game.Components.Add(hero);
            Game.Components.Add(heroSprite);
            Game.Components.Add(boss);

            heroSprite.SetAnimation("hero");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
