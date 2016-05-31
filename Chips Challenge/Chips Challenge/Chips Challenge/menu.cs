using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace Chips_Challenge
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class menu : Microsoft.Xna.Framework.DrawableGameComponent
    {

        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        int pauseTime = 0;
        Texture2D chip;
        Texture2D agent;
        Texture2D ics;
        Texture2D sf;
        Texture2D fb;
        Texture2D wb;
        //HIGH SCORE STUFF
        HighScoreData hsInfo;
       public string[] names;
       public int[] scores;
        string highScoreName;
        
        bool swammyswam = false;
        KeyboardState kbs;
        KeyboardState prevKeyPress;
        Game mainGame;

        public menu(Game game)
            : base(game)
        {
            mainGame = game;
            pauseTime = 0;
           
            
            //HIGH SCORE STUFF
            names = new string[5];
            scores = new int[5];
            //  scoresStr = new string[5];
            hsInfo = new HighScoreData("Chips Challenge");  //put your game name here
            names = hsInfo.getNames();
            scores = hsInfo.getScores();
            highScoreName = "";
         

            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
           
            base.Initialize();
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = Game.Content.Load<SpriteFont>("SpriteFont1");
            chip = Game.Content.Load<Texture2D>("chip");
            agent = Game.Content.Load<Texture2D>("agent");
            sf = Game.Content.Load<Texture2D>("suctionfeet");
            ics = Game.Content.Load<Texture2D>("iceskate");
            fb = Game.Content.Load<Texture2D>("fireboot");
            wb = Game.Content.Load<Texture2D>("waterflipper");
              //agent = Content
            
            // TODO: use this.Content to load your game content here
        }
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
        protected override void UnloadContent()
        {
            //MediaPlayer.Stop();
        }
        public override void Update(GameTime gameTime)
        {
            pauseTime++;
            // TODO: Add your update code here
            prevKeyPress = kbs;
            kbs = Keyboard.GetState();
            for (int i = 0; i < 5; i++)
            {
                //((Game1)Game).names[i] = names[i];
                //((Game1)Game).scores[i] = scores[i];

                //spriteBatch.DrawString(spriteFont, names[i], new Vector2(380.0f, 90 + i * 30.0f), Color.BlueViolet);
                //spriteBatch.DrawString(spriteFont, scores[i].ToString(), new Vector2(525.0f, 90 + i * 30.0f), Color.BlueViolet);
            }
            if (((Game1)mainGame).stage == 0)// && pauseTime > 100)
            {
                if (kbs.IsKeyDown(Keys.S) && !prevKeyPress.IsKeyDown(Keys.S))
                {
                    //set level to 0
                    ((Game1)mainGame).stage = 1;
                    ((Game1)mainGame).score = 0;

                    mainGame.Components.RemoveAt(0);

                    this.Dispose();
                }
                if (kbs.IsKeyDown(Keys.M) && !prevKeyPress.IsKeyDown(Keys.M))
                {
                    //set level to 0
                    if (swammyswam)
                    {
                        MediaPlayer.Play(((Game1)mainGame).ZARATHUSTRA);
                        swammyswam = false;
                    }
                    else
                    {
                        swammyswam = true;
                        MediaPlayer.Stop();
                    }
                }
                //if (kbs.IsKeyDown(Keys.A))
             
                if (pauseTime > 2000) pauseTime = 0;
                
                base.Update(gameTime);
            }
               
        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, "PRESS S TO START", new Vector2(25.0f, 10.0f), Color.BlueViolet);
            spriteBatch.DrawString(spriteFont, "THE AGENT STEALS GEAR,", new Vector2(25.0f, 40.0f), Color.BlueViolet);
            spriteBatch.Draw(agent, new Rectangle(270, 40, 20, 20), Color.White);
            spriteBatch.DrawString(spriteFont, "COLLECT ALL THE CHIPS,", new Vector2(25.0f, 60.0f), Color.BlueViolet);
            spriteBatch.Draw(chip, new Rectangle(270, 60, 20, 20), Color.White);
            spriteBatch.DrawString(spriteFont, "FIRE BOOTS LET YOU WALK IN FIRE", new Vector2(25.0f, 80.0f), Color.BlueViolet);
            spriteBatch.Draw(fb, new Rectangle(375, 80, 20, 20), Color.White);
            spriteBatch.DrawString(spriteFont, "WATER BOOTS LET YOU WALK ON WATER", new Vector2(25.0f, 100.0f), Color.BlueViolet);
            spriteBatch.Draw(wb, new Rectangle(395, 100, 20, 20), Color.White);
            spriteBatch.DrawString(spriteFont, "SUCTION BOOTS LET YOU WALK ON SLIDING PANELS", new Vector2(25.0f, 120.0f), Color.BlueViolet);
            spriteBatch.Draw(sf, new Rectangle(500, 120, 20, 20), Color.White);
            spriteBatch.DrawString(spriteFont, "ICE SKATES LET YOU WALK ON ICE", new Vector2(25.0f, 140.0f), Color.BlueViolet);
            spriteBatch.Draw(ics, new Rectangle(367, 143, 20, 20), Color.White);
            spriteBatch.DrawString(spriteFont, "GOODLUCK", new Vector2(25.0f, 160.0f), Color.BlueViolet);
            //HIGH SCORE STUFF
            spriteBatch.DrawString(spriteFont, "HIGH SCORES:", new Vector2(25.0f, 190.0f), Color.BlueViolet);
            for (int i = 0; i < 5; i++)
            {
                spriteBatch.DrawString(spriteFont, names[i], new Vector2(25.0f, 210 + i * 20.0f), Color.BlueViolet);
                spriteBatch.DrawString(spriteFont, scores[i].ToString(), new Vector2(200.0f, 210 + i * 20.0f), Color.BlueViolet);
            }
            spriteBatch.DrawString(spriteFont, "INSTRUCTIONS:", new Vector2(300.0f, 210.0f), Color.BlueViolet);
            spriteBatch.DrawString(spriteFont, "M - MUTE", new Vector2(300.0f, 230.0f), Color.BlueViolet);
            spriteBatch.DrawString(spriteFont, "Q - QUIT", new Vector2(300.0f, 250.0f), Color.BlueViolet);
            spriteBatch.DrawString(spriteFont, "R - RESTART LEVEL", new Vector2(300.0f, 270.0f), Color.BlueViolet);
            spriteBatch.DrawString(spriteFont, "By Joe Bernstein", new Vector2(300.0f, 290.0f), Color.Red);
            //spriteBatch.DrawString(spriteFont, "Based off Chuck Somerville's", new Vector2(200.0f, 140.0f), Color.BlueViolet);
            //spriteBatch.DrawString(spriteFont, "Chips Challenge", new Vector2(200.0f, 160.0f), Color.BlueViolet);
      
           
            {
              //  spriteBatch.DrawString(spriteFont, "(OFF)", new Vector2(350, 415), Color.Red);
            }

            
            spriteBatch.End();

 
            base.Draw(gameTime);
        }
    }
}