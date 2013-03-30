﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LuxEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Schmup
{
    class ThreeEnemies : World
    {
        private Hero hero;
        private List<Enemy> enemies = new List<Enemy>();
        private List<Shot> badShots = new List<Shot>();
        private List<Shot> goodShots = new List<Shot>();

        private Texture2D enemyTexture;

        // Liste de position initiale d'enemie
        public List<Vector2> ListePosition = new List<Vector2>();

        // Pour le test!
        private ShotPool shots;

        private double elapsed;

        public ThreeEnemies(LuxGame game)
            : base(game)
        {
        }

        public Hero Hero
        {
            get
            {
                return hero;
            }
        }

        public List<Shot> BadShots
        {
            get
            {
                return badShots;
            }
        }

        public List<Shot> GoodShots
        {
            get
            {
                return goodShots;
            }
        }

        public List<Enemy> Enemies
        {
            get
            {
                return enemies;
            }
        }

        public override void Initialize()
        {
            enemyTexture = this.Content.Load<Texture2D>("commonEnemy");

            base.Initialize();
            Texture2D bullet2Texture = this.Content.Load<Texture2D>("bullet002-1");
            hero = new Hero(this.LuxGame, this, 200, 1, 1, 5, 2, null);
            Sprite heroSprite = new Sprite(hero, new List<String>() { "hero" });
            hero.Skin = heroSprite;
            hero.Position = new Vector2(400, 400);

            ListePosition.Add(new Vector2(100,100));
            ListePosition.Add(new Vector2(700,100));
            ListePosition.Add(new Vector2(400,100));

            ShotPool shots = new ShotPool(LuxGame, this, false, 0.2, 150,150, 8, 4, bullet2Texture, null);

            GenericEnemy enem;
            for (int i = 0; i < 3; i++)
            {
                enem = new GenericEnemy(LuxGame, this, 500, 10, 10, null);
                Sprite enemySkin = new Sprite(enem, new List<Texture2D>() { enemyTexture }, null);
                enem.Skin = enemySkin;
                enem.Skin.SetAnimation(enemyTexture.Name);
                enem.Position = new Vector2(400, 100);
                enemies.Add(enem);
                Game.Components.Add(enem);
                Game.Components.Add(enemySkin);
            }
            foreach (Enemy enemy in enemies.ToList<Enemy>())
            {
                Game.Components.Add(new ShootsHero(this.LuxGame, this, shots, enemy));
            }
            Game.Components.Add(shots);
            Game.Components.Add(hero);
            Game.Components.Add(heroSprite);

            // Phase de test
            shots = new ShotPool(LuxGame, this);
            Game.Components.Add(shots);

            heroSprite.SetAnimation("hero");
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            elapsed += gameTime.ElapsedGameTime.TotalSeconds;

            // Reapparition enemies
            int nb_a_rajouter = 0;
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].Life <= 0)
                {
                    enemies[i].Die();
                    nb_a_rajouter++;
                }
            }
            GenericEnemy enem;
            for (; nb_a_rajouter > 0; nb_a_rajouter--)
            {
                enem = new GenericEnemy(LuxGame, this, 500, 10, 10, null);
                enem.Position = new Vector2(400, 100);
                enemies.Add(enem);
                
            }


            // Gestion Heros -- Balles ennemies
            if (!hero.IsInvincible())
            {
                foreach (Shot badShot in badShots)
                {
                    if (Vector2.Distance(badShot.Position, hero.Position) < badShot.Hitbox + hero.HurtBox && !hero.IsInvincible())
                    {
                        hero.Hurt(badShot.Damage);
                        hero.InvincibleTimeSec = badShot.InvincibleTimeSec;
                        System.Console.WriteLine("Tu t'es fait frapper. Il te reste {0} points de vie.",hero.Life );

                        //TODO : Changer ça peut-être?
                        badShot.Position = new Vector2(-40, -40);
                        badShot.Speed = new Vector2(0, 0);
                        badShot.Accel = new Vector2(0, 0);
                    }
                }
            }

            // Gestion Ennemis -- Balles du heros
            foreach (Enemy enemy in enemies.ToList<Enemy>())
            {
                enemy.Update(gameTime);
                foreach (Shot goodShot in goodShots)
                {
                    if (Vector2.Distance(goodShot.Position, enemy.Position) < goodShot.Hitbox + enemy.HurtBox)
                    {
                        // L'ennemi a mal.
                        enemy.Hurt(goodShot.Damage);

                        // La balle disparait.
                        // TODO : Changer ça! Implémenter une méthode qui fasse disparaitre la balle!
                        goodShot.Position = new Vector2(-150, -150);
                        goodShot.Speed = new Vector2(0, 0);
                        goodShot.Accel = new Vector2(0, 0);
                    }
                }
            }

            // Gestion Ennemis -- Heros
            foreach (Enemy enemy in enemies)
            {
                // TODO : Trouver une hitbox pour le héros? Comment faire?
                if (Vector2.Distance(hero.Position, enemy.Position) < enemy.HurtBox + hero.HurtBox - 10)
                {
                    hero.Collide(enemy.GivenDamageCollision);
                    enemy.Collide(hero.GivenDamageCollision);
                    System.Console.WriteLine("Collision");
                }
            }

            //if (elapsed % 1 < 0.1)
            //{
            //    if (elapsed > 5)
            //    {
            //        shots.Check();
            //        elapsed = 0;
            //    }
            //    shots.Shoot(1,1,1,1,1,1,false,false,false);
            //    shots.Print();
            //}

            // Système de débug
            if (elapsed > 1000)
            {
                elapsed = 0;
                System.Console.WriteLine(hero.Position);
                System.Console.WriteLine(badShots.Count);
                System.Console.WriteLine(goodShots.Count);
                System.Console.WriteLine(enemies.Count);
                foreach (Shot badShot in badShots)
                {
                    System.Console.WriteLine(badShot.Position);
                }
                foreach (Shot goodShot in goodShots)
                {
                    System.Console.WriteLine(goodShot.Position);
                }
                foreach (Enemy enemy in enemies)
                {
                    System.Console.WriteLine(enemy.Position);
                }
            }


        }
    }
}
