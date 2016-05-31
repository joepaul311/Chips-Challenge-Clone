using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media; //32 x 32 grid, only see 9x9
using System.IO;
namespace Chips_Challenge
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        SpriteFont spriteFont;
        SpriteFont tiny;
        public int score;
        public int stage; //0 = menu; 1 = game; 2 = highscore
        menu menu1;
        highScore hs;


        int anothercounter = 0;

        KeyboardState prevKeyPress;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont clocktimefont;
        string[,] board = new string[40, 40];
        int[] chiploc = new int[2];
        int[] leafmonsterloc = new int[2];


        int level = 1; ///////////////////////////////////////////////////////////////level


        bool leafmonsterpresent = true;
        bool hasyellowkey = false;
        bool hasredkey = false;
        bool hasgreenkey = false;
        bool hasbluekey = false;
        bool hasflippers = false;
        bool hasiceskates = false;
        bool moveable = true;
        bool hasfireboots = false;
        bool hassuctionboots = false;
        int balldiry = 1;
        int balldirx = 0;
        int[] ballloc = new int[2];
        int chips;
        double intervalTime = 0;
        double prevIntervalTime = 0;
        bool dosplash = false;
        bool dowater = false;
        bool doarrow = false;
        Texture2D Cboard;
        bool isaball = false;
        bool startdrawing = false;
        bool teleportnow = false;
        int telecount = 0;
        int ballmovetime = 0;
        KeyboardState kbs;
        public Song ZARATHUSTRA;
        int time = 0;
        int heldcountl = 0;
        int heldcountr = 0;
        int heldcountd = 0;
       
        double smallertime = 0;

        KeyboardState prevkbs;
        int timessplashed = 0;
        bool doice = false;
        int icerlaa = 1;
        int heldcount = 0;

        const int numItems = 47; /////////////////////////////////////////////////////////////////////////////////////////numitems
    
        Texture2D[] itemsimg = new Texture2D[numItems];
        Texture2D usedchipimg;
        string[] itemsname = new string[numItems];
        bool loaded = false;
        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            menu1 = new menu(this);
            score = 0;
            stage = 0;
            this.graphics.PreferredBackBufferHeight = 328;
            this.graphics.PreferredBackBufferWidth = 540;
            Components.Add(menu1);
        }
        void setfalse()
        {
            hasbluekey = false;
            hasfireboots = false;
            hasflippers = false;
            hasgreenkey = false;
            hasiceskates = false;
            hasredkey = false;
            hassuctionboots = false;
            hasyellowkey = false;

        }
        void setboard()
        {
            //StreamReader sf = new StreamReader(file);
            //string s = sf.ReadLine();
            deleteboard();
            removeitems();
            setfalse();
            loaded = true;
            string file = "level1";
            switch (level)
            {
                case 1:
                    removeitems();
                    setfalse();
                    isaball = true;
                    file = "level1";
                    chiploc[0] = 16;
                    chiploc[1] = 16;
                    ballloc[0] = 16;
                    ballloc[1] = 22;

                    for (int i = 12; i < 21; i++)
                    {
                        board[i, 12] = itemsname[2]; //top wall
                        board[12, i] = itemsname[2]; //left wall
                        board[20, i] = itemsname[2];
                        board[i, 20] = itemsname[2];
                    }
                    board[ballloc[0], ballloc[1]] = itemsname[29];
                    break;
                case 2:
                    removeitems();
                    setfalse();
                    chiploc[0] = 12;
                    chiploc[1] = 15;
                    isaball = false;
                    file = "level2";
                    break;
                case 3:
                    chiploc[0] = 8;
                    chiploc[1] = 13;
                    isaball = false;
                    file = "level3";
                    break;
                case 4:
                    chiploc[0] = 19;
                    chiploc[1] = 21;
                    for (int i = 0; i < 40; i++)
                    {
                        board[i, 35] = "block";
                    }
                    file = "level4";
                    break;
                case 5:
                    chiploc[0] = 19;
                    chiploc[1] = 19;
                    for (int i = 0; i < 40; i++)
                    {
                        for (int j = 0; j < 40; j++)
                        {
                            board[i, j] = "transformblock";
                        }
                    }
                    for (int i = 1; i < 40; i += 4)
                    {
                        for (int j = 1; j < 40; j++)
                        {
                            board[i, j] = "block";
                        }
                    }
                    for (int i = 1; i < 40; i++)
                    {
                        for (int j = 1; j < 40; j+=4)
                        {
                            board[i, j] = "block";
                        }
                    }
                    for (int i = 4; i < 37; i++)
                    {
                        board[i, 4] = itemsname[2];
                        board[4, i] = itemsname[2];
                        board[36, i] = itemsname[2];
                        board[i, 36] = itemsname[2];
                    }
                    file = "level5";
                    break;
                case 6:
                    //chiploc[0] = 12;
                    //chiploc[1] = 15;

                    ////for (int i = 0; i < 40; i++)
                    ////{
                    ////    for (int j = 0; j < 40; j++)
                    ////    {
                    ////        board[i, j] = "ice";
                    ////    }
                    ////}
                    //for (int i = 4; i < 38; i++)
                    //{
                    //    board[i, 4] = itemsname[2];
                    //    board[5, i] = itemsname[2];
                    //    board[36, i] = itemsname[2];
                    //    board[i, 35] = itemsname[2];
                    //}
                    //file = "level6";
                    stage = 2;
                    level = 1;
                    loaded = false;
                    hs = new highScore(this);
                    Components.Add(hs);
                    break;
                case 7:
                    
                    stage = 2;
                    level = 1;
                    loaded = false;
                    hs = new highScore(this);
                    Components.Add(hs);
                    //file = "level7";
                    break;
                case 8:
                     chiploc[0] = 12;
                    chiploc[1] = 15;
                    file = "level8";
                    break;
                case 9:
                     chiploc[0] = 12;
                    chiploc[1] = 15;
                    file = "level9";
                    break;
                case 10:
                     chiploc[0] = 12;
                    chiploc[1] = 15;
                    file = "level10";
                    break;
                case 11:
                    stage = 2;
                    level = 1;
                    loaded = false;
                    hs = new highScore(this);
                    Components.Add(hs);
                    break;
            }
            file += ".txt";
            StreamReader sf = new StreamReader(file);
            if (level == 1)
            {
                chiploc[0] = 16;
                chiploc[1] = 16;
                ballloc[0] = 16;
                ballloc[1] = 22;
                for (int i = 12; i < 21; i++)
                {
                    board[i, 12] = itemsname[2]; //top wall
                    board[12, i] = itemsname[2]; //left wall
                    board[20, i] = itemsname[2];
                    board[i, 20] = itemsname[2];
                }
                isaball = true;
                board[ballloc[0], ballloc[1]] = itemsname[29];
            }
            while (sf.Peek() != -1)
            {
                string s = sf.ReadLine();
                string i = s[0] + "" + s[1];
                string j = s[2] + "" + s[3];
                int slength = s.Length - 4;
                //Console.WriteLine(slength);
                string t = s.Substring(4, slength);
                //Console.WriteLine(t);

                board[int.Parse(i.ToString()), int.Parse(j.ToString())] = t;

            }



            sf.Close();
        }
        void deleteboard()
        {
            for (int i = 0; i < 40; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    board[i, j] = itemsname[0];
                }
            }
            for (int i = 4; i < 37; i++)
            {
                board[i, 4] = itemsname[2];
                board[4, i] = itemsname[2];
                board[36, i] = itemsname[2];
                board[i, 36] = itemsname[2];
            }
        }
        void moveup()
        {
            if (kbs.IsKeyDown(Keys.Up) && !prevkbs.IsKeyDown(Keys.Up))
            {
                usedchipimg = itemsimg[23];
                interactionstatic("up");
                if (board[chiploc[0], chiploc[1]] == itemsname[1])
                {
                    usedchipimg = itemsimg[28];
                }
                //if (board[chiploc[0], chiploc[1] - 1] != itemsname[2])
                //{
                //    chiploc[1]--;
                //    usedchipimg = upchipimg;
                //}
                //if (board[chiploc[0] + 1, chiploc[1] - 1] != gate)
                //{
                //    if (chips == 0)
                //    {
                //        chiploc[0]++;
                //    }
                //}
            }
            else if (kbs.IsKeyDown(Keys.Up) && prevkbs.IsKeyDown(Keys.Up))
            {
                heldcount++;
                if (heldcount % 10 == 0)
                {
                    usedchipimg = itemsimg[23];
                    interactionstatic("up");
                    if (board[chiploc[0], chiploc[1]] == itemsname[1])
                    {
                        usedchipimg = itemsimg[28];
                    }
                }
            }
            else
            {
                heldcount = 0;
            }

        }
        void movedown()
        {
            if (kbs.IsKeyDown(Keys.Down) && !prevkbs.IsKeyDown(Keys.Down))
            {
                interactionstatic("down");
                if (!prevkbs.IsKeyDown(Keys.Up))
                {
                    usedchipimg = itemsimg[20];
                }
                if (board[chiploc[0], chiploc[1]] == itemsname[1])
                {
                    usedchipimg = itemsimg[27];
                }
                //if (board[chiploc[0], chiploc[1] + 1] != itemsname[2])
                //{

                //    chiploc[1]++;
                //}
                //if (board[chiploc[0], chiploc[1] + 1] != gate)
                //{
                //    if (chips == 0)
                //    {
                //        chiploc[0]++;
                //    }
                //}

            }
            else if (kbs.IsKeyDown(Keys.Down) && prevkbs.IsKeyDown(Keys.Down))
            {
                heldcountd++;
                if (heldcountd % 10 == 0)
                {
                    
                    if (!prevkbs.IsKeyDown(Keys.Up))
                    {
                        interactionstatic("down");
                        usedchipimg = itemsimg[20];
                    }
                    if (board[chiploc[0], chiploc[1]] == itemsname[1])
                    {
                        usedchipimg = itemsimg[27];
                    }
                }
            }
            else
            {
                heldcountd = 0;
            }
        
        }
        void dotelecport()
        {
            if (teleportnow)
            {

           
                for (int i = 4; i < 36; i++)
                {
                    if (board[i, chiploc[1]] == "teleport")
                    {
                        if (i != chiploc[0])
                        {
                            if (usedchipimg == itemsimg[22])
                            {
                                chiploc[0] = i;
                                chiploc[0]++;

                            }
                            else if (usedchipimg == itemsimg[21])
                            {

                                chiploc[0] = i;
                                chiploc[0]--;
                                teleportnow = false;
                                break;
                            }

                            teleportnow = false;

                        }
                    }
                }
                for (int i = 4; i < 36; i++)
                {
                    if (board[chiploc[0], i] == "teleport")
                    {
                        if (i != chiploc[1])
                        {
                            if (usedchipimg == itemsimg[23]) //up
                            {
                                chiploc[1] = i;
                                chiploc[1]--;
                                teleportnow = false;
                                break;
                            }
                            else if (usedchipimg == itemsimg[20]) // down
                            {

                                chiploc[1] = i;
                                chiploc[1]++;
                                teleportnow = false;

                            }

                            teleportnow = false;

                        }
                    }
                }
            }

        }
        void moveleft()
        {
            if (kbs.IsKeyDown(Keys.Left) && !prevkbs.IsKeyDown(Keys.Left))
            {
                interactionstatic("left");
                if (!kbs.IsKeyDown(Keys.Right))
                {
                    usedchipimg = itemsimg[21];
                }
                
                if (board[chiploc[0], chiploc[1]] == itemsname[1])
                {
                    usedchipimg = itemsimg[25];
                }
                //if (board[chiploc[0] - 1, chiploc[1]] != itemsname[2])
                //{
                //    chiploc[0]--;
                //}
                //if (board[chiploc[0] - 1, chiploc[1]] != gate)
                //{
                //    if (chips == 0)
                //    {
                //        chiploc[0]--;
                //    }
                //}
            }
            else if (kbs.IsKeyDown(Keys.Left) && prevkbs.IsKeyDown(Keys.Left))
            {
                heldcountl++;
                if (heldcountl % 10 == 0)
                {
                   
                    if (!kbs.IsKeyDown(Keys.Right))
                    {
                        interactionstatic("left");
                        usedchipimg = itemsimg[21];
                    }
                    if (board[chiploc[0], chiploc[1]] == itemsname[1])
                    {
                        usedchipimg = itemsimg[25];
                    }
                }
            }
            else
            {
                heldcountl = 0;
            }
        }
        void moveright()
        {
            if (kbs.IsKeyDown(Keys.Right) && !prevkbs.IsKeyDown(Keys.Right))
            {
                interactionstatic("right");
                usedchipimg = itemsimg[22];
                if (board[chiploc[0], chiploc[1]] == itemsname[1])
                {
                    usedchipimg = itemsimg[26];
                }
                //if (board[chiploc[0] + 1, chiploc[1]] != block)
                //{
                //    chiploc[0]++;
                //}
                //if (board[chiploc[0] + 1, chiploc[1]] != gate)
                //{
                //    if (chips == 0)
                //    {
                //        chiploc[0]++;
                //    }
                //}
            }
            else if (kbs.IsKeyDown(Keys.Right) && prevkbs.IsKeyDown(Keys.Right))
            {
                heldcountr++;
                if (heldcountr % 10 == 0)
                {
                    interactionstatic("right");
                    usedchipimg = itemsimg[22];
                    if (board[chiploc[0], chiploc[1]] == itemsname[1])
                    {
                        usedchipimg = itemsimg[26];
                    }
                }
            }
            else
            {
                heldcountr = 0;
            }
        }
        private bool Timer(GameTime gt, int pause)  //pause in milliseconds
        {
            bool resetInterval = false;

            intervalTime += (double)gt.ElapsedGameTime.Milliseconds;
            intervalTime = intervalTime % pause;
            if (intervalTime < prevIntervalTime) { resetInterval = true; }
            prevIntervalTime = intervalTime;
            return resetInterval;
        }
        void move()
        {
            kbs = Keyboard.GetState();
            moveup();
            movedown();
            moveleft();
            moveright();
            prevkbs = kbs;
        }
        void interactions()
        {
            int x = chiploc[0];
            int y = chiploc[1];
            if (board[x, y] == itemsname[3])
            {
                board[x, y] = itemsname[0];
                chips--;
            }
            if (board[x, y] == itemsname[9])
            {
                board[x, y] = itemsname[0];
                hasyellowkey = true;
            }
            if (board[x, y] == itemsname[24])
            {
                board[x, y] = itemsname[0];
                hasgreenkey = true;
            }
            if (board[x, y] == itemsname[10])
            {
                board[x, y] = itemsname[0];
                hasredkey = true;
            }
            if (board[x, y] == itemsname[4])
            {
                board[x, y] = itemsname[0];
                hasbluekey = true;
            }
            if (board[x, y] == itemsname[14])
            {
                board[x, y] = itemsname[0];
                hasflippers = true;
            }
            if (board[x, y] == itemsname[15])
            {
                board[x, y] = itemsname[0];
                hasfireboots = true;
            }
            if (board[x, y] == itemsname[16])
            {
                board[x, y] = itemsname[0];
                hassuctionboots = true;
            }
            if (board[x, y] == itemsname[17])
            {
                board[x, y] = itemsname[0];
                hasiceskates = true;
            }

        }
        void countchips()
        {
            chips = 0;
            for (int i = 0; i < 40; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    if (board[i, j] == itemsname[3])
                    {
                        chips++;
                    }
                }
            }
        }
        void moveball()
        {
            board[ballloc[0], ballloc[1]] = itemsname[0];
            if (balldirx == 0)
            {
                //Console.WriteLine("A");
                int wouldbe = ballloc[1] + balldiry;
                if (board[ballloc[0], wouldbe] != itemsname[0])
                {
                    // Console.WriteLine("B");
                    balldiry *= -1;
                }
                else
                {
                    //Console.WriteLine("C");
                    ballloc[1] += balldiry;
                }
            }
            board[ballloc[0], ballloc[1]] = itemsname[29];
            if (ballloc[0] == chiploc[0] && ballloc[1] == chiploc[1])
            {
                dosplash = true;
            }
        }
        void movearrow()
        {
            if (!hassuctionboots)
            {
                if (doarrow)
                {
                    //moveable = false;
                    if (anothercounter % 6 == 0)
                    {
                        int x = chiploc[0];
                        int y = chiploc[1];

                        if (board[x, y] == "uparrow")
                        {

                            chiploc[1]--;
                        }
                        if (board[x, y] == "leftarrow")
                        {
                            chiploc[0]--;
                        }
                        if (board[x, y] == "rightarrow")
                        {
                            chiploc[0]++;
                        }
                        if (board[x, y] == "downarrow")
                        {
                            chiploc[1]++;
                        }

                    }
                    anothercounter++;
                }
                else
                {

                    //anothercounter = 0;
                    //moveable = true;
                    doarrow = false;
                }
            }
        }
        void removeitems()
        {
            hasbluekey = false;
            hasredkey = false;
            hasgreenkey = false;
            hasyellowkey = false;
            hasflippers = false;
            hasfireboots = false;
            hasiceskates = false;
            hassuctionboots = false;
        }
        void moveice(GameTime gt)
        {

            if (doice)
            {
                string BLI = "bottomlefticebumper";
                string TLI = "toplefticebumper";
                string TRI = "toprighticebumper";
                string BRI = "bottomrighticebumper";
                moveable = false;

                if (icerlaa % 6 == 0)
                {

                    int x = chiploc[0];
                    int y = chiploc[1];
                    if (usedchipimg == itemsimg[23] && board[x, y - 1] == "block")
                    {
                        if (board[x, y] != TLI) //or tri
                        {
                            usedchipimg = itemsimg[20];
                            chiploc[1]++;
                            moveable = false;
                            doice = true;
                        }
                        else
                        {

                            usedchipimg = itemsimg[22]; //dis is tli or tri
                            chiploc[0]++;
                        }
                    }
                    else if (usedchipimg == itemsimg[20] && board[x, y + 1] == "block")
                    {
                        if (board[x, y] != BLI ) //or tri
                        {
                            usedchipimg = itemsimg[23];
                            chiploc[1]--;
                            moveable = false;
                            doice = true;
                        }
                        else
                        {

                            usedchipimg = itemsimg[22];
                            chiploc[0]++;
                        }
                    }
                    else if (usedchipimg == itemsimg[21] && board[x - 1, y] == "block")
                    {
                        if (board[x, y] != TLI && board[x,y] != BLI ) //or tri
                        {
                            usedchipimg = itemsimg[22];
                            chiploc[0]++;
                            moveable = false;
                            doice = true;
                        }
                        else
                        {
                           
                            if (board[x, y] == TLI)
                            {
                               
                                usedchipimg = itemsimg[20];
                                chiploc[1]++;
                            }
                            else
                            {
                                usedchipimg = itemsimg[23];
                                chiploc[1]--;
                            }
                            
                        }
                    }
                    else   if (usedchipimg == itemsimg[22] && board[x + 1, y] == "block")
                    {
                        if (board[x, y] != TLI) //or tri
                        {
                            usedchipimg = itemsimg[21];
                            chiploc[1]++;
                            moveable = false;
                            doice = true;
                        }
                        else
                        {

                            usedchipimg = itemsimg[20];
                            chiploc[1]++;
                            
                        }
                    }
                    else if (usedchipimg == itemsimg[21])
                    {

                        if (board[x, y] == "ice")
                        {
                            chiploc[0]--;
                        }
                        else if (board[x, y] == BLI)
                        {
                            usedchipimg = itemsimg[23];
                            chiploc[1]--;
                        }
                        else if (board[x, y] == TLI)
                        {
                            usedchipimg = itemsimg[20];
                            chiploc[1]++;
                        }
                        else
                        {
                            moveable = true;
                            doice = false;
                        }
                    }
                    else if (usedchipimg == itemsimg[22])
                    {

                        if (board[x, y] == "ice")
                        {
                            chiploc[0]++;
                        }

                        else
                        {
                            doice = false;
                            moveable = true;
                        }
                    }
                    else if (usedchipimg == itemsimg[23])
                    {

                        if (board[x, y] == "ice")
                        {
                            chiploc[1]--;
                        }
                        else if (board[x, y] == BLI)
                        {
                            usedchipimg = itemsimg[22];
                            chiploc[0]++; ;
                        }
                        else if (board[x, y] == TLI)
                        {
                            usedchipimg = itemsimg[22];
                            chiploc[0]++;
                        }
                        else
                        {
                            doice = false;
                            moveable = true;
                        }
                    }
                    else if (usedchipimg == itemsimg[20])
                    {

                        if (board[x, y] == "ice")
                        {
                            chiploc[1]++;
                        }
                        else if (board[x, y] == BLI)
                        {
                            usedchipimg = itemsimg[22];
                            chiploc[0]++; ;
                        }
                        else if (board[x, y] == TLI)
                        {
                            usedchipimg = itemsimg[22];
                            chiploc[0]++;
                        }
                        else
                        {
                            doice = false;
                            moveable = true;
                        }
                    }




                }
                icerlaa++;
            }

        }
        //void leafmonstermove()
        //{
        //    int orx = leafmonsterloc[0];
        //    int ory = leafmonsterloc[1];
        //    int dirmovex = leafmonsterloc[0];
        //    int dirmovey = leafmonsterloc[1];
        //    int disx = Math.Abs(leafmonsterloc[0] - chiploc[0]);
        //    int disy = Math.Abs(leafmonsterloc[1] - chiploc[1]);
        //    Console.WriteLine(disx + " " + disy);
        //    board[orx, ory] = empty;
        //    if (disx <= disy && disx != 0)
        //    {
        //        Console.WriteLine("Y");
        //        if (dirmovey > chiploc[1])
        //        {

        //            usedleafmonster = upleafmonster;
        //            dirmovey--;
        //        }
        //        else if (dirmovey < chiploc[1])
        //        {
        //            usedleafmonster = staticleafmonster;
        //            dirmovey++;
        //        }
        //        if (board[dirmovex, dirmovey] == empty)
        //        {
        //            board[leafmonsterloc[0], leafmonsterloc[1]] = empty;
        //            leafmonsterloc[0] = dirmovex;
        //            leafmonsterloc[1] = dirmovey;
        //            //board[dirmovex, dirmovey] = leafmonster;
        //        }
        //        //else
        //        //{
        //        //    dirmovex = leafmonsterloc[0];
        //        //    if (dirmovex > chiploc[0])
        //        //    {
        //        //        usedleafmonster = leftleafmonster;
        //        //        dirmovex--;
        //        //    }
        //        //    else if (dirmovex < chiploc[0])
        //        //    {
        //        //        usedleafmonster = rightleafmonster;
        //        //        dirmovex++;
        //        //    }
        //        //    if (board[dirmovex, ory] == empty)
        //        //    {

        //        //        board[leafmonsterloc[0], leafmonsterloc[1]] = empty;
        //        //        leafmonsterloc[0] = dirmovex;
        //        //        leafmonsterloc[1] = dirmovey;
        //        //        //board[dirmovex, ory] = leafmonster;
        //        //    }
        //        //}

        //    }
        //    else if (disy < disx && disy != 0 )
        //    {
        //        Console.WriteLine("X");
        //        if (dirmovex > chiploc[0])
        //        {
        //            usedleafmonster = leftleafmonster;
        //            dirmovex--;
        //        }
        //        else if (dirmovex < chiploc[0])
        //        {
        //            usedleafmonster = rightleafmonster;
        //            dirmovex++;
        //        }
        //        if (board[dirmovex, dirmovey] == empty)
        //        {

        //            board[leafmonsterloc[0], leafmonsterloc[1]] = empty;
        //            leafmonsterloc[0] = dirmovex;
        //            leafmonsterloc[1] = dirmovey;
        //           // board[dirmovex, dirmovey] = leafmonster;
        //        }
        //        //else
        //        //{
        //        //    if (dirmovey > chiploc[1])
        //        //    {

        //        //        usedleafmonster = upleafmonster;
        //        //        dirmovey--;
        //        //    }
        //        //    else if (dirmovey < chiploc[1])
        //        //    {
        //        //        usedleafmonster = staticleafmonster;
        //        //        dirmovey++;
        //        //    }
        //        //    dirmovey = leafmonsterloc[1];
        //        //    if (board[orx, dirmovey] == empty)
        //        //    {
        //        //        board[leafmonsterloc[0], leafmonsterloc[1]] = empty;

        //        //        leafmonsterloc[0] = dirmovex;
        //        //        leafmonsterloc[1] = dirmovey;
        //        //        //board[orx, dirmovey] = leafmonster;
        //        //    }

        //        //}
        //    }
        //    board[dirmovex, dirmovey] = leafmonster;


        //}
        void interactionstatic(string dir)
        {
            int dirmovex = chiploc[0];
            int dirmovey = chiploc[1];
            if (dir == "left")
            {
                dirmovex = chiploc[0] - 1;
            }
            if (dir == "right")
            {
                dirmovex = chiploc[0] + 1;
            }
            if (dir == "up")
            {
                dirmovey = chiploc[1] - 1;
            }
            if (dir == "down")
            {
                dirmovey = chiploc[1] + 1;
            }
            if (board[dirmovex, dirmovey] == "block")
            {
                //if (dir == "left")
                //{
                //    chiploc[0]--;
                //}
                //if (dir == "right")
                //{
                //    chiploc[0]++;
                //}
                //if (dir == "up")
                //{
                //    chiploc[1]--;
                //}
                //if (dir == "down")
                //{
                //    chiploc[1]++;
                //}
            }

            else if (board[dirmovex, dirmovey] == "transformblock")
            {
                if (dir == "left")
                {
                    chiploc[0]--;
                }
                if (dir == "right")
                {
                    chiploc[0]++;
                }
                if (dir == "up")
                {
                    chiploc[1]--;
                }
                if (dir == "down")
                {
                    chiploc[1]++;
                }
                board[chiploc[0], chiploc[1]] = "block";
            }
            else if (board[dirmovex, dirmovey] == "mudblock")
            {
                bool dontdo = false;
                if (dir == "left")
                {
                    if (board[chiploc[0] - 2, chiploc[1]] != "empty")
                    {
                        
                        dontdo = true;
                    }
                }
                else if (dir == "right")
                {
                    if (board[chiploc[0] + 2, chiploc[1]] != "empty")
                    {
                        dontdo = true;
                    }
                }
                else if (dir == "up")
                {
                    if (board[chiploc[0] , chiploc[1] - 2] != "empty")
                    {
                        dontdo = true;
                    }
                }
                else if (dir == "down")
                {
                    if (board[chiploc[0], chiploc[1] + 2] != "empty")
                    {
                        dontdo = true;
                    }
                }
                if (dontdo == false)
                {
                    if (dir == "left")
                    {
                        chiploc[0]--;
                        board[chiploc[0] - 1, chiploc[1]] = "mudblock";
                    }
                    if (dir == "right")
                    {
                        chiploc[0]++;
                        board[chiploc[0] + 1, chiploc[1]] = "mudblock";
                    }
                    if (dir == "up")
                    {
                        chiploc[1]--;
                        board[chiploc[0], chiploc[1] - 1] = "mudblock";
                    }
                    if (dir == "down")
                    {
                        chiploc[1]++;
                        board[chiploc[0], chiploc[1] + 1] = "mudblock";
                    }
                    board[chiploc[0], chiploc[1]] = "empty";
                }
                else
                {
                    if (dir == "left")
                    {
                        if (board[chiploc[0] - 2, chiploc[1]] == "water")
                        {
                            board[chiploc[0] - 2, chiploc[1]] = "mud";
                            chiploc[0]--;
                            board[chiploc[0], chiploc[1]] = "empty";
                        }
                    }
                    else if (dir == "right")
                    {
                        if (board[chiploc[0] + 2, chiploc[1]] == "water")
                        {
                            board[chiploc[0] + 2, chiploc[1]] = "mud";
                            chiploc[0]++;
                            board[chiploc[0], chiploc[1]] = "empty";
                        }
                    }
                    else if (dir == "up")
                    {
                        if (board[chiploc[0], chiploc[1] - 2] == "water")
                        {
                            board[chiploc[0], chiploc[1] - 2] = "mud";
                            chiploc[1]--;
                            board[chiploc[0], chiploc[1]] = "empty";
                        }
                    }
                    else if (dir == "down")
                    {
                        
                        if (board[chiploc[0], chiploc[1] + 2] == "water")
                        {
                            board[chiploc[0], chiploc[1] + 2] = "mud";
                            chiploc[1]++;
                            board[chiploc[0],chiploc[1]] = "empty";
                        }
                    }

                }

                
            }
            else if (board[dirmovex, dirmovey] == "mud")
            {
                if (dir == "left")
                {
                    chiploc[0]--;
                }
                if (dir == "right")
                {
                    chiploc[0]++;
                }
                if (dir == "up")
                {
                    chiploc[1]--;
                }
                if (dir == "down")
                {
                    chiploc[1]++;
                }
                board[chiploc[0], chiploc[1]] = "empty";
            }
            else if (board[dirmovex, dirmovey] == "teleport")
            {
                if (dir == "left")
                {
                    chiploc[0]--;
                }
                if (dir == "right")
                {
                    chiploc[0]++;
                }
                if (dir == "up")
                {
                    chiploc[1]--;
                }
                if (dir == "down")
                {
                    chiploc[1]++;
                }
                teleportnow = true;
            }
            else if (board[dirmovex, dirmovey] == itemsname[36])
            {
                if (dir == "left")
                {
                    chiploc[0]--;
                }
                if (dir == "right")
                {
                    chiploc[0]++;
                }
                if (dir == "up")
                {
                    chiploc[1]--;
                }
                if (dir == "down")
                {
                    chiploc[1]++;
                }
                doarrow = true;
            }
            else if (board[dirmovex, dirmovey] == itemsname[37])
            {
                if (dir == "left")
                {
                    chiploc[0]--;
                }
                if (dir == "right")
                {
                    chiploc[0]++;
                }
                if (dir == "up")
                {
                    chiploc[1]--;
                }
                if (dir == "down")
                {
                    chiploc[1]++;
                }
                doarrow = true;
            }
            else if (board[dirmovex, dirmovey] == itemsname[40])
            {
                if (dir == "left")
                {
                    chiploc[0]--;
                }
                if (dir == "right")
                {
                    chiploc[0]++;
                }
                if (dir == "up")
                {
                    chiploc[1]--;
                }
                if (dir == "down")
                {
                    chiploc[1]++;
                }
                removeitems();
            }
            else if (board[dirmovex, dirmovey] == itemsname[38])
            {
                if (dir == "left")
                {
                    chiploc[0]--;
                }
                if (dir == "right")
                {
                    chiploc[0]++;
                }
                if (dir == "up")
                {
                    chiploc[1]--;
                }
                if (dir == "down")
                {
                    chiploc[1]++;
                }
                doarrow = true;
            }
            else if (board[dirmovex, dirmovey] == itemsname[39])
            {
                if (dir == "left")
                {
                    chiploc[0]--;
                }
                if (dir == "right")
                {
                    chiploc[0]++;
                }
                if (dir == "up")
                {
                    chiploc[1]--;
                }
                if (dir == "down")
                {
                    chiploc[1]++;
                }
                doarrow = true;
            }
            else if (board[dirmovex, dirmovey] == itemsname[31]) /////ice
            {
                if (!hasiceskates)
                {
                    doice = true;
                }
                //moveable = false;
                if (dir == "left")
                {
                    chiploc[0]--;
                }
                if (dir == "right")
                {
                    chiploc[0]++;
                }
                if (dir == "up")
                {
                    chiploc[1]--;
                }
                if (dir == "down")
                {
                    chiploc[1]++;
                }
            }
            else if (board[dirmovex, dirmovey] == itemsname[34])
            {
                //Console.WriteLine("HI");
                if (hasfireboots == false)
                {
                    //loaded = false;
                    // dowater = true;
                    dosplash = true;
                    if (dir == "left")
                    {
                        chiploc[0]--;
                    }
                    if (dir == "right")
                    {
                        chiploc[0]++;
                    }
                    if (dir == "up")
                    {
                        chiploc[1]--;
                    }
                    if (dir == "down")
                    {
                        chiploc[1]++;
                    }
                }
                else
                {
                    if (dir == "left")
                    {
                        chiploc[0]--;
                    }
                    if (dir == "right")
                    {
                        chiploc[0]++;
                    }
                    if (dir == "up")
                    {
                        chiploc[1]--;
                    }
                    if (dir == "down")
                    {
                        chiploc[1]++;
                    }
                }
            }
            else if (board[dirmovex, dirmovey] == itemsname[1])
            {
                if (hasflippers == false)
                {
                    //loaded = false;
                    dowater = true;
                    dosplash = true;
                    if (dir == "left")
                    {
                        chiploc[0]--;
                    }
                    if (dir == "right")
                    {
                        chiploc[0]++;
                    }
                    if (dir == "up")
                    {
                        chiploc[1]--;
                    }
                    if (dir == "down")
                    {
                        chiploc[1]++;
                    }
                }

                if (hasflippers == true)
                {
                    if (dir == "left")
                    {
                        chiploc[0]--;
                    }
                    if (dir == "right")
                    {
                        chiploc[0]++;
                    }
                    if (dir == "up")
                    {
                        chiploc[1]--;
                    }
                    if (dir == "down")
                    {
                        chiploc[1]++;
                    }
                }
            }
            else if (board[dirmovex, dirmovey] == itemsname[6])
            {
                if (chips == 0)
                {
                    board[dirmovex, dirmovey] = itemsname[0];
                    if (dir == "left")
                    {
                        chiploc[0]--;
                    }
                    if (dir == "right")
                    {
                        chiploc[0]++;
                    }
                    if (dir == "up")
                    {
                        chiploc[1]--;
                    }
                    if (dir == "down")
                    {
                        chiploc[1]++;
                    }
                }
            }
            else if (board[dirmovex, dirmovey] == itemsname[5])
            {
                if (dir == "left")
                {
                    chiploc[0]--;
                }
                if (dir == "right")
                {
                    chiploc[0]++;
                }
                if (dir == "up")
                {
                    chiploc[1]--;
                }
                if (dir == "down")
                {
                    chiploc[1]++;
                }
                score += 1000 * level;
                if (level == 1 || level == 2)
                {
                    if (time < 30)
                    {
                        score += 50 * (30 - time);
                    }
                }
                else if (level == 3)
                {
                    if (time < 60)
                    {
                        score += 75 * (60 - time);
                    }
                }
                else if (time < 120)
                {
                    score += 100 * (120 - time);
                }
                level++;
                time = 0;
                loaded = false;
            }
            else if (board[dirmovex, dirmovey] == itemsname[8])
            {

                if (hasyellowkey == true)
                {
                    board[dirmovex, dirmovey] = itemsname[0];
                    hasyellowkey = false;
                    if (dir == "left")
                    {
                        chiploc[0]--;
                    }
                    if (dir == "right")
                    {
                        chiploc[0]++;
                    }
                    if (dir == "up")
                    {
                        chiploc[1]--;
                    }
                    if (dir == "down")
                    {
                        chiploc[1]++;
                    }
                }


            }
            else if (board[dirmovex, dirmovey] == itemsname[11])
            {

                if (hasbluekey == true)
                {
                    board[dirmovex, dirmovey] = itemsname[0];
                    hasbluekey = false;
                    if (dir == "left")
                    {
                        chiploc[0]--;
                    }
                    if (dir == "right")
                    {
                        chiploc[0]++;
                    }
                    if (dir == "up")
                    {
                        chiploc[1]--;
                    }
                    if (dir == "down")
                    {
                        chiploc[1]++;
                    }
                }


            }
            else if (board[dirmovex, dirmovey] == itemsname[12])
            {

                if (hasredkey == true)
                {
                    board[dirmovex, dirmovey] = itemsname[0];
                    hasredkey = false;
                    if (dir == "left")
                    {
                        chiploc[0]--;
                    }
                    if (dir == "right")
                    {
                        chiploc[0]++;
                    }
                    if (dir == "up")
                    {
                        chiploc[1]--;
                    }
                    if (dir == "down")
                    {
                        chiploc[1]++;
                    }
                }


            }
            else if (board[dirmovex, dirmovey] == itemsname[13])
            {
                if (hasgreenkey == true)
                {
                    board[dirmovex, dirmovey] = itemsname[0];
                    if (dir == "left")
                    {
                        chiploc[0]--;
                    }
                    if (dir == "right")
                    {
                        chiploc[0]++;
                    }
                    if (dir == "up")
                    {
                        chiploc[1]--;
                    }
                    if (dir == "down")
                    {
                        chiploc[1]++;
                    }
                }
            }
            else
            {
                if (dir == "left")
                {
                    chiploc[0]--;
                }
                if (dir == "right")
                {
                    chiploc[0]++;
                }
                if (dir == "up")
                {
                    chiploc[1]--;
                }
                if (dir == "down")
                {
                    chiploc[1]++;
                }
            }
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }
        protected override void LoadContent()
        {
            
            itemsimg[0] = Content.Load<Texture2D>("emptybrick");
            itemsimg[1] = Content.Load<Texture2D>("water");
            itemsimg[2] = Content.Load<Texture2D>("block");
            itemsimg[3] = Content.Load<Texture2D>("chip");
            itemsimg[4] = Content.Load<Texture2D>("bluekey");
            itemsimg[5] = Content.Load<Texture2D>("endlevel");
            itemsimg[6] = Content.Load<Texture2D>("gate");
            itemsimg[7] = Content.Load<Texture2D>("monsterstatic");
            itemsimg[8] = Content.Load<Texture2D>("yellowlock");
            itemsimg[9] = Content.Load<Texture2D>("yellowkey");
            itemsimg[10] = Content.Load<Texture2D>("redkey");
            itemsimg[11] = Content.Load<Texture2D>("bluelock");
            itemsimg[12] = Content.Load<Texture2D>("redlock");
            itemsimg[13] = Content.Load<Texture2D>("greenlock");
            itemsimg[14] = Content.Load<Texture2D>("waterflipper");
            itemsimg[15] = Content.Load<Texture2D>("fireboot");
            itemsimg[16] = Content.Load<Texture2D>("suctionfeet");
            itemsimg[17] = Content.Load<Texture2D>("iceskate");
            itemsimg[18] = Content.Load<Texture2D>("pushblock");
            itemsimg[19] = Content.Load<Texture2D>("mud");
            itemsimg[20] = Content.Load<Texture2D>("staticchip");
            itemsimg[21] = Content.Load<Texture2D>("leftchip");
            itemsimg[22] = Content.Load<Texture2D>("rightchip");
            itemsimg[23] = Content.Load<Texture2D>("upchip");
            itemsimg[24] = Content.Load<Texture2D>("greenkey");
            itemsimg[25] = Content.Load<Texture2D>("swimleft");
            itemsimg[26] = Content.Load<Texture2D>("swimright");
            itemsimg[27] = Content.Load<Texture2D>("swimstatic");
            itemsimg[28] = Content.Load<Texture2D>("swimup");
            itemsimg[29] = Content.Load<Texture2D>("yellowball");
            itemsimg[30] = Content.Load<Texture2D>("splash");
            itemsimg[31] = Content.Load<Texture2D>("ice");
            itemsimg[32] = Content.Load<Texture2D>("toplefticebumper");
            itemsimg[33] = Content.Load<Texture2D>("bottomlefticebumper");
            itemsimg[34] = Content.Load<Texture2D>("fire");
            itemsimg[35] = Content.Load<Texture2D>("burntchip");
            itemsimg[36] = Content.Load<Texture2D>("uparrow");
            itemsimg[37] = Content.Load<Texture2D>("downarrow");
            itemsimg[38] = Content.Load<Texture2D>("leftarrow");
            itemsimg[39] = Content.Load<Texture2D>("rightarrow");
            itemsimg[40] = Content.Load<Texture2D>("agent");
            itemsimg[41] = Content.Load<Texture2D>("teleport");
            itemsimg[42] = Content.Load<Texture2D>("transformblock");
            itemsimg[43] = Content.Load<Texture2D>("mudblock");
            itemsimg[44] = Content.Load<Texture2D>("mud");
            itemsimg[45] = Content.Load<Texture2D>("toprighticebumper");
            itemsimg[46] = Content.Load<Texture2D>("bottomrighticebumper");
            itemsname[0] = "empty";
            itemsname[1] = "water";
            itemsname[2] = "block";
            itemsname[3] = "chip";
            itemsname[4] = "bluekey";
            itemsname[5] = "endlevel";
            itemsname[6] = "gate";
            itemsname[7] = "leafmonster";
            itemsname[8] = "yellowlock";
            itemsname[9] = "yellowkey";
            itemsname[10] = "redkey";
            itemsname[11] = "bluelock";
            itemsname[12] = "redlock";
            itemsname[13] = "greenlock";
            itemsname[14] = "flippers";
            itemsname[15] = "fireboots";
            itemsname[16] = "suctionboots";
            itemsname[17] = "iceskates";
            itemsname[18] = "pushblock";
            itemsname[19] = "mud";
            itemsname[20] = "staticchip";
            itemsname[21] = "leftchip";
            itemsname[22] = "rightchip";
            itemsname[23] = "upchip";
            itemsname[24] = "greenkey";
            itemsname[25] = "swimleft";
            itemsname[26] = "swimright";
            itemsname[27] = "swimstatic";
            itemsname[28] = "swimup";
            itemsname[29] = "yellowball";
            itemsname[30] = "splash";
            itemsname[31] = "ice";
            itemsname[32] = "toplefticebumper";
            itemsname[33] = "bottomlefticebumper";
            itemsname[34] = "fire";
            itemsname[35] = "burntchip";
            itemsname[36] = "uparrow";
            itemsname[37] = "downarrow";
            itemsname[38] = "leftarrow";
            itemsname[39] = "rightarrow";
            itemsname[40] = "agent";
            itemsname[41] = "teleport";
            itemsname[42] = "transformblock";
            itemsname[43] = "mudblock";
            itemsname[44] = "mud";
            itemsname[45] = "toprighticebumper";
            itemsname[46] = "bottomrighticebumper";
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            tiny = Content.Load<SpriteFont>("SpriteFont3");
            spriteFont = Content.Load<SpriteFont>("SpriteFont1");
            ZARATHUSTRA = Content.Load<Song>("ZARATHUSTRACUT");
            MediaPlayer.Play(ZARATHUSTRA);
            MediaPlayer.IsRepeating = true;
            Cboard = Content.Load<Texture2D>("block");
            //clocktimefont = Content.Load<SpriteFont>("Digital");

            // TODO: use this.Content to load your game content here
        }
        protected override void UnloadContent()
        {
        }
        protected override void Update(GameTime gameTime)
        {
            if (stage == 1)
            {
                if (loaded == false)
                {
                    startdrawing = true;
                    usedchipimg = itemsimg[20];
                    setboard();
                    loaded = true;

                }
                if (Timer(gameTime, 1000))
                {

                    time++;

                }
                if (kbs.IsKeyDown(Keys.R))
                {
                    loaded = false;
                }
                if (ballmovetime % 10 == 0)
                {
                    if (isaball)
                    {

                        moveball();
                    }
                }
                if (board[chiploc[0], chiploc[1]] == "agent")
                {
                    removeitems();
                }
                dotelecport();
                ballmovetime++;
                //if (Timer(gameTime, 100))
                //{
                //    leafmonstermove();
                //}
                countchips();
                if (moveable)
                {
                    move();

                }
                moveice(gameTime);
                movearrow();
                interactions();
                if (kbs.IsKeyDown(Keys.Q))
                {
                    stage = 2;
                    level = 1;
                    time = 0;
                    loaded = false;
                    hs = new highScore(this);
                    Components.Add(hs);
                }
            }
            base.Update(gameTime);
        }
        void drawinventory()
        {
            int items = 0;
            float yval = 52;
            int x = 300;
            
            if (hasbluekey == true)
            {
                items++;
                spriteBatch.Draw(itemsimg[4], new Vector2(x + items * 32, yval), Color.White);
            }
            if (hasredkey == true)
            {
                items++;
                spriteBatch.Draw(itemsimg[10], new Vector2(x + items * 32, yval), Color.White);
            }
            if (hasgreenkey == true)
            {
                items++;
                spriteBatch.Draw(itemsimg[24], new Vector2(x + items * 32, yval), Color.White);
            }
            if (hasyellowkey == true)
            {
                items++;
                spriteBatch.Draw(itemsimg[9], new Vector2(x + items * 32, yval), Color.White);
            }

            if (hasflippers == true)
            {
                items++;
                spriteBatch.Draw(itemsimg[14], new Vector2(x + items * 32, yval), Color.White);
            }
            if (hasfireboots == true)
            {
                items++;
                spriteBatch.Draw(itemsimg[15], new Vector2(x + items * 32, yval), Color.White);
            }
            if (hassuctionboots == true)
            {
                items++;
                spriteBatch.Draw(itemsimg[16], new Vector2(x + items * 32, yval), Color.White);
            }
            if (hasiceskates == true)
            {
                items++;
                spriteBatch.Draw(itemsimg[17], new Vector2(x + items * 32, yval), Color.White);
            }
            if (items == 4)
            {
                yval = 100 + 32;//132
                x = 300 - 128; //172
            }
            while (items < 8)
            {
                if (items == 4)
                {
                    yval = 52 + 32;//84
                    x = 300 - 128; //172
                }
                items++;
                spriteBatch.Draw(itemsimg[0], new Vector2(x + items * 32, yval), Color.White);
            }


        }
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.Black);
          
            if (stage == 1)
            {
                if (startdrawing)
                {
                    int seconds = time % 60;
                    int minutes = time / 60;
                    string sec = seconds.ToString();
                    string min = minutes.ToString();
                    if (int.Parse(sec) < 10)
                    {
                        sec = "0" + sec;
                    }  
                    spriteBatch.Draw(Cboard, new Rectangle(-10, -10, 600, 400), Color.ForestGreen);
                    spriteBatch.Draw(itemsimg[31], new Rectangle(324, 42, 143, 84), Color.Black);
                    spriteBatch.Draw(itemsimg[31], new Rectangle(324, 120, 143, 164), Color.Black);
                    spriteBatch.DrawString(spriteFont, "    Score", new Vector2(324, 120), Color.Lime);
                    spriteBatch.DrawString(spriteFont, "    Time", new Vector2(324, 170), Color.Lime);
                    spriteBatch.DrawString(spriteFont, " Chips Left", new Vector2(324, 220), Color.Lime);
                    spriteBatch.DrawString(spriteFont, "     " + chips.ToString(), new Vector2(324, 240), Color.Lime);
                    spriteBatch.DrawString(spriteFont, "    " + min + ":" + sec, new Vector2(324, 190), Color.Lime);
                    spriteBatch.DrawString(spriteFont, "    " + score.ToString(), new Vector2(324, 140), Color.Lime);
                    if (dosplash == true && timessplashed < 25)
                    {
                        if (dowater)
                        {
                            usedchipimg = itemsimg[30];
                        }
                        else
                        {
                            usedchipimg = itemsimg[35];
                            //timessplashed = 0;
                        }
                        timessplashed++;
                        moveable = false;

                    }
                    else if (dosplash == true && timessplashed > 24)
                    {

                        timessplashed = 0;
                        dosplash = false;
                        dowater = false;
                        loaded = false;
                        moveable = true;
                    }
                    int actuali = 0;
                    int actualj = 0;
                    bool normal = true;
                    int xlbound = chiploc[0] - 4;
                    int xubound = chiploc[0] + 5;
                    int ylbound = chiploc[1] - 4;
                    int yubound = chiploc[1] + 5;


                
                    
                 


                    //for (int i = xlbound; i < xubound; i++)
                    //{
                    //    actualj = 0;
                    //    for (int j = ylbound; j < yubound; j++)
                    //    {

                    //        for (int k = 0; k < numItems; k++)
                    //        {
                    //            if (board[i, j] == itemsname[k])
                    //            {
                    //                spriteBatch.Draw(itemsimg[k], new Vector2(actuali * 32.0f + 20, actualj * 32.0f + 20), Color.White);
                    //            }
                    //        }
                    //      // spriteBatch.DrawString(tiny, i.ToString() + "," + j.ToString(), new Vector2(actuali * 32.0f + 20.0f, actualj * 32.0f + 20.0f), Color.Black);
                    //        actualj++;
                    //    }
                    //    actuali++;
                    //}
                    drawgrid(board, itemsimg, itemsname, 4, 4, numItems,chiploc[0],chiploc[1]);

                    drawinventory();

                    if (normal)
                    {
                        if (usedchipimg == itemsimg[20])
                        {
                            spriteBatch.Draw(usedchipimg, new Vector2(128 + 20, 129 + 20), Color.White);
                        }
                        else if (usedchipimg == itemsimg[23])
                        {
                            spriteBatch.Draw(usedchipimg, new Vector2(128 + 20, 127 + 20), Color.White);
                        }
                        else
                        {
                            spriteBatch.Draw(usedchipimg, new Vector2(128 + 20, 128 + 20), Color.White);
                        }
                    }
                    else
                    {

                        Vector2 chipsloc = new Vector2(chiploc[0] % 10 + 20, chiploc[1] % 10 + 20);
                        spriteBatch.Draw(usedchipimg, chipsloc, Color.White);
                    }

                }
            
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
        void drawgrid(string[,] board, Texture2D[] images, string[] blocks, int radiusx, int radiusy, int blocksize, int xloc, int yloc)
        //board is the board, images are the images, blocks are the blocks name
        //make sure that blocks[x] and blocks[x] match up (images[5] is ice, so blocks[5] is ice)
        //radiusx is the distance you can see left from the center
        //radiusy is the distnace you can see right from the center
        //blocksize is # of images.
        {
            int actuali = 0;
            int actualj = 0;
            int xlbound = xloc - radiusx;
            int xubound = xloc +  radiusx + 1;
            int ylbound = yloc - radiusy;
            int yubound = yloc + radiusy + 1;
            for (int i = xlbound; i < xubound; i++)
            {
                actualj = 0;
                for (int j = ylbound; j < yubound; j++)
                {

                    for (int k = 0; k < blocksize; k++)
                    {
                        if (board[i, j] == blocks[k])
                        {
                            
                            spriteBatch.Draw(images[k], new Vector2(actuali * 32.0f + 20, actualj * 32.0f + 20), Color.White);
                        }
                    }
                    actualj++;
                }
                actuali++;
            }
        }
    }
}

//   int screenxmax;
//  int screenymax;
// int screenymin;
//int screenxmin;//31 ice