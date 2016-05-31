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
    public class highScore : Microsoft.Xna.Framework.DrawableGameComponent
    {
       
            SpriteBatch spriteBatch;
            SpriteFont spriteFont;
            
            KeyboardState kbs;
            KeyboardState prevKeyPress;
            bool started = false;
            Game mainGame;
            menu menu1;
            Texture2D TetrisImage;
            Texture2D Chipo;
            SpriteFont RS;
            HighScoreData hsInfo;
            string[] names;
            int[] scores;
           // string[] scoresStr;
            string highScoreName;
            bool gotHighScore = false;
            SpriteFont spriteFontBig;
            public highScore(Game game)
                : base(game)
            {
                mainGame = game;
                names = new string[5];
                scores = new int[5];
              //  scoresStr = new string[5];
                hsInfo = new HighScoreData("Chips Challenge");
                names = hsInfo.getNames();
                scores = hsInfo.getScores();
                highScoreName = "";

             //   scoresStr = hsInfo.getScoresStr();


                
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
                spriteFontBig = Game.Content.Load<SpriteFont>("SpriteFont2");
                Chipo = Game.Content.Load<Texture2D>("noshadowstaticchip");
                //TetrisImage = Game.Content.Load<Texture2D>("Tetris");
                //RS = Game.Content.Load<SpriteFont>("SpriteFont4");
               
                // TODO: use this.Content to load your game content here
            }

            /// <summary>
            /// Allows the game component to update itself.
            /// </summary>
            /// <param name="gameTime">Provides a snapshot of timing values.</param>
            public override void Update(GameTime gameTime)
            {

               // prevKeysPressed = keysPressed;


                if (((Game1)mainGame).stage == 2)
                {
                    // TODO: Add your update code here
                    prevKeyPress = kbs;
                    kbs = Keyboard.GetState();
                    gotHighScore = hsInfo.checkScore_HighIsGood(((Game1)mainGame).score);
                    if (gotHighScore)
                    {
                      
                        if (kbs.IsKeyDown(Keys.Back) && !prevKeyPress.IsKeyDown(Keys.Back))
                            {
                                if (highScoreName == "")
                                {

                                }
                                else
                                {
                                    highScoreName = highScoreName.Substring(0, highScoreName.Length - 1);
                                }
                            }
                        if (highScoreName.Length < 11)
                        {
                            updateHighScoreDisplay();
                        }
                    };

                    if (kbs.IsKeyDown(Keys.Enter) && !prevKeyPress.IsKeyDown(Keys.Enter))
                    {
                        hsInfo.insertHighScore_HighIsGood(((Game1)mainGame).score, highScoreName);
                        hsInfo.writeFile();

                        ((Game1)mainGame).stage = 0;
                        menu1 = new menu(mainGame);
                        mainGame.Components.RemoveAt(0);
                        Game.Components.Add(menu1);
                        this.Dispose();
                    }
                };
                base.Update(gameTime);
            }
            private void updateHighScoreDisplay()
            {
                //keysPressed = kbs.GetPressedKeys();
                //if (keysPressed.GetLength(0) > 0 && prevKeysPressed.GetLength(0) == 0)
                //{
                //    highScoreName = highScoreName + keysPressed.GetValue(0);
                //}
                if (kbs.IsKeyDown(Keys.A) && !prevKeyPress.IsKeyDown(Keys.A))
                {
                    highScoreName = highScoreName + "A";
                }
                else if (kbs.IsKeyDown(Keys.B) && !prevKeyPress.IsKeyDown(Keys.B))
                {
                    highScoreName = highScoreName + "B";
                }
                else if (kbs.IsKeyDown(Keys.C) && !prevKeyPress.IsKeyDown(Keys.C)) { highScoreName = highScoreName + "C"; }
                else if (kbs.IsKeyDown(Keys.D) && !prevKeyPress.IsKeyDown(Keys.D)) { highScoreName = highScoreName + "D"; }
                else if (kbs.IsKeyDown(Keys.E) && !prevKeyPress.IsKeyDown(Keys.E)) { highScoreName = highScoreName + "E"; }
                else if (kbs.IsKeyDown(Keys.F) && !prevKeyPress.IsKeyDown(Keys.F)) { highScoreName = highScoreName + "F"; }
                else if (kbs.IsKeyDown(Keys.G) && !prevKeyPress.IsKeyDown(Keys.G)) { highScoreName = highScoreName + "G"; }
                else if (kbs.IsKeyDown(Keys.H) && !prevKeyPress.IsKeyDown(Keys.H)) { highScoreName = highScoreName + "H"; }
                else if (kbs.IsKeyDown(Keys.I) && !prevKeyPress.IsKeyDown(Keys.I)) { highScoreName = highScoreName + "I"; }
                else if (kbs.IsKeyDown(Keys.J) && !prevKeyPress.IsKeyDown(Keys.J)) { highScoreName = highScoreName + "J"; }
                else if (kbs.IsKeyDown(Keys.K) && !prevKeyPress.IsKeyDown(Keys.K)) { highScoreName = highScoreName + "K"; }
                else if (kbs.IsKeyDown(Keys.L) && !prevKeyPress.IsKeyDown(Keys.L)) { highScoreName = highScoreName + "L"; }
                else if (kbs.IsKeyDown(Keys.M) && !prevKeyPress.IsKeyDown(Keys.M)) { highScoreName = highScoreName + "M"; }
                else if (kbs.IsKeyDown(Keys.N) && !prevKeyPress.IsKeyDown(Keys.N)) { highScoreName = highScoreName + "N"; }
                else if (kbs.IsKeyDown(Keys.O) && !prevKeyPress.IsKeyDown(Keys.O)) { highScoreName = highScoreName + "O"; }
                else if (kbs.IsKeyDown(Keys.P) && !prevKeyPress.IsKeyDown(Keys.P)) { highScoreName = highScoreName + "P"; }
                else if (kbs.IsKeyDown(Keys.Q) && !prevKeyPress.IsKeyDown(Keys.Q)) { highScoreName = highScoreName + "Q"; }
                else if (kbs.IsKeyDown(Keys.R) && !prevKeyPress.IsKeyDown(Keys.R)) { highScoreName = highScoreName + "R"; }
                else if (kbs.IsKeyDown(Keys.S) && !prevKeyPress.IsKeyDown(Keys.S)) { highScoreName = highScoreName + "S"; }
                else if (kbs.IsKeyDown(Keys.T) && !prevKeyPress.IsKeyDown(Keys.T)) { highScoreName = highScoreName + "T"; }
                else if (kbs.IsKeyDown(Keys.U) && !prevKeyPress.IsKeyDown(Keys.U)) { highScoreName = highScoreName + "U"; }
                else if (kbs.IsKeyDown(Keys.V) && !prevKeyPress.IsKeyDown(Keys.V)) { highScoreName = highScoreName + "V"; }
                else if (kbs.IsKeyDown(Keys.W) && !prevKeyPress.IsKeyDown(Keys.W)) { highScoreName = highScoreName + "W"; }
                else if (kbs.IsKeyDown(Keys.X) && !prevKeyPress.IsKeyDown(Keys.X)) { highScoreName = highScoreName + "X"; }
                else if (kbs.IsKeyDown(Keys.Y) && !prevKeyPress.IsKeyDown(Keys.Y)) { highScoreName = highScoreName + "Y"; }
                else if (kbs.IsKeyDown(Keys.Z) && !prevKeyPress.IsKeyDown(Keys.Z)) { highScoreName = highScoreName + "Z"; }
                else if (kbs.IsKeyDown(Keys.D1) && !prevKeyPress.IsKeyDown(Keys.D1)) { highScoreName = highScoreName + "1"; }
                else if (kbs.IsKeyDown(Keys.D2) && !prevKeyPress.IsKeyDown(Keys.D2)) { highScoreName = highScoreName + "2"; }
                else if (kbs.IsKeyDown(Keys.D3) && !prevKeyPress.IsKeyDown(Keys.D3)) { highScoreName = highScoreName + "3"; }
                else if (kbs.IsKeyDown(Keys.D4) && !prevKeyPress.IsKeyDown(Keys.D4)) { highScoreName = highScoreName + "4"; }
                else if (kbs.IsKeyDown(Keys.D5) && !prevKeyPress.IsKeyDown(Keys.D5)) { highScoreName = highScoreName + "5"; }
                else if (kbs.IsKeyDown(Keys.D6) && !prevKeyPress.IsKeyDown(Keys.D6)) { highScoreName = highScoreName + "6"; }
                else if (kbs.IsKeyDown(Keys.D7) && !prevKeyPress.IsKeyDown(Keys.D7)) { highScoreName = highScoreName + "7"; }
                else if (kbs.IsKeyDown(Keys.D8) && !prevKeyPress.IsKeyDown(Keys.D8)) { highScoreName = highScoreName + "8"; }
                else if (kbs.IsKeyDown(Keys.D9) && !prevKeyPress.IsKeyDown(Keys.D9)) { highScoreName = highScoreName + "9"; }
                else if (kbs.IsKeyDown(Keys.D0) && !prevKeyPress.IsKeyDown(Keys.D0)) { highScoreName = highScoreName + "0"; }
                else if (kbs.IsKeyDown(Keys.Space) && !prevKeyPress.IsKeyDown(Keys.Space)) { highScoreName = highScoreName + " "; }
                else if (kbs.IsKeyDown(Keys.OemMinus) && !prevKeyPress.IsKeyDown(Keys.OemMinus)) { highScoreName = highScoreName + "-"; }
                else if (kbs.IsKeyDown(Keys.OemPeriod) && !prevKeyPress.IsKeyDown(Keys.OemPeriod)) { highScoreName = highScoreName + "-"; }

                if (started == false)
                {
                    Console.WriteLine(highScoreName);
                    started = true;
                    highScoreName = "";
                }

                
               

            }
            public override void Draw(GameTime gameTime)
            {
                GraphicsDevice.Clear(Color.Black);

                spriteBatch.Begin();
               
                Rectangle Ricky = new Rectangle(300, 60, 200, 250);
                spriteBatch.Draw(Chipo, Ricky, Color.White);
               

                


                //base.Draw(gameTime);
                //GraphicsDevice.Clear(Color.CornflowerBlue);

                //spriteBatch.Begin();
                spriteBatch.DrawString(spriteFontBig, "SCORE:" + ((Game1)mainGame).score.ToString(), new Vector2(25.0f, 10.0f), Color.BlueViolet);
                if (gotHighScore)
                {

                    spriteBatch.DrawString(spriteFont, "HIGH SCORE! ENTER", new Vector2(25.0f, 170.0f), Color.BlueViolet);
                    spriteBatch.DrawString(spriteFont, "A NAME AND PRESS ENTER", new Vector2(25.0f, 190.0f), Color.BlueViolet);
                    Ricky.Height = 250;
                    //spriteBatch.Draw(TetrisImage, Ricky, Color.White);
                    spriteBatch.DrawString(spriteFont, highScoreName, new Vector2(25.0f, 210.0f), Color.BlueViolet);
                }
                else
                {

                    spriteBatch.DrawString(spriteFont, "PRESS ENTER TO CONTINUE", new Vector2(25.0f, 170.0f), Color.BlueViolet);
                    Ricky.Height = 325;
                   
                    //spriteBatch.DrawString(spriteFont, highScoreName, new Vector2(50.0f, 375.0f), Color.BlueViolet);
                }

                spriteBatch.DrawString(spriteFont, "THANKS FOR PLAYING!", new Vector2(25.0f, 250.0f), Color.BlueViolet);
                spriteBatch.DrawString(spriteFont, "CHECK OUT MY TETRIS TOO!", new Vector2(25.0f, 270.0f), Color.BlueViolet);
                spriteBatch.DrawString(spriteFont, "IT'S AESTHETICALLY PLEASING!", new Vector2(25.0f, 290.0f), Color.BlueViolet);
                spriteBatch.DrawString(spriteFont, "" , new Vector2(50.0f, 140.0f), Color.Black);
                spriteBatch.DrawString(spriteFont, "HIGH SCORES:", new Vector2(25.0f, 40.0f), Color.BlueViolet);
                for (int i = 0; i < 5; i++)
                {
                    spriteBatch.DrawString(spriteFont, names[i], new Vector2(25.0f, 60 + i * 20.0f), Color.BlueViolet);
                    spriteBatch.DrawString(spriteFont, scores[i].ToString(), new Vector2(150.0f, 60 + i * 20.0f), Color.BlueViolet);
                }


                //spriteBatch.End();
                spriteBatch.End();

                base.Draw(gameTime);
            }
        }
    }

