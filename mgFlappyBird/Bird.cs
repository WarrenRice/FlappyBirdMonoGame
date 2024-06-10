using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace mgFlappyBird
{
    internal class Bird : Sprite
    {
        private Rectangle rect;         // define rect as Rectangle
        private float vel;              // define vel as float
        private bool clicked;           // define clicked as boolean
        public float rotation;          // define rotation as float

        private List<Texture2D> textures;       // define textures as List<Texture2D>
        private byte index = 0;                 // define and instantiate index as byte = 0
        private byte counter = 0;               // define and instantiate counter as byte = 0
        private static byte flapCooldown = 5;   // define and instantiate flapCooldown as byte = 5


        public Rectangle Rect
        {

            /*=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
            *
            * Method                    :    Rect
            *
            * Method parameters         :    none
            *
            * Method return             :    rect 
            *
            * Synopsis                  :    This method returns rect data member
            * 
            * References                :    Monogame. (2012). MONOGAME Documentation. Retrieved November 15, 2023,
            *                                   from https://docs.monogame.net/
            *                                Cloud Skills Challenge, Microsoft (2023). Use Visual C# to read from and write to a text file. Retrieved November 15, 2023,
            *                                   from https://learn.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/language-compilers/read-write-text-file
            *                                   
            * Modifications             :
            *                                Date            Developer                Notes
            *                                ----            ---------                -----
            *                                2023-10-16      W. Poomarin              Design Layout
            *                                2023-10-17      W. Poomarin              Display Images
            *                                2023-10-18      W. Poomarin              Move Background and Pipe, Move Player
            *                                2023-10-19      W. Poomarin              Add Gravity and click to jump, Add Collision
            *                                2023-10-20      W. Poomarin              Add Score mechanic
            *                                2023-10-21      W. Poomarin              Load and save file
            *                                2023-10-22      W. Poomarin              Add Button
            *                                2023-11-25      W. Poomarin              Art works
            *                                2023-12-03      W. Poomarin              Add input player's name
            *
            *=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-= */

            get
            {
                return rect;
            }
        }



        public Bird(FlappyBird g, Texture2D texture, Vector2 position) : base(g, texture, position)
        {

            /*=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
            *
            * Method                    :    Bird Constructor
            *
            * Method parameters         :    FlappyBird, Texture2D, Vector2
            *
            * Method return             :    Bird 
            *
            * Synopsis                  :    This method constructs Bird object.
            * 
            * References                :    Monogame. (2012). MONOGAME Documentation. Retrieved November 15, 2023,
            *                                   from https://docs.monogame.net/
            *              
            * Modifications             :
            *                                Date            Developer                Notes
            *                                ----            ---------                -----
            *                                2023-10-16      W. Poomarin              Design Layout
            *                                2023-10-17      W. Poomarin              Display Images
            *                                2023-10-18      W. Poomarin              Move Background and Pipe, Move Player
            *                                2023-10-19      W. Poomarin              Add Gravity and click to jump, Add Collision
            *                                2023-10-20      W. Poomarin              Add Score mechanic
            *                                2023-10-21      W. Poomarin              Load and save file
            *                                2023-10-22      W. Poomarin              Add Button
            *                                2023-11-25      W. Poomarin              Art works
            *                                2023-12-03      W. Poomarin              Add input player's name
            *                                
            *=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-= */

            rect = new Rectangle((int)position.X, (int)position.Y, (int)texture.Width, (int)texture.Height);    //instantiate rect with position and size
            this.vel = 0;               //set vel = 0
            this.clicked = false;       //set clicked = false
            this.rotation = 0;          //set rotation = 0

            this.textures = new List<Texture2D>();  //instantiate textures as List<Texture2D>
            textures.Add(texture);                  //add texture to textures
        }

        public override void Update()
        {

            /*=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
            *
            * Method                    :    Update
            *
            * Method parameters         :    none
            *
            * Method return             :    void
            *
            * Synopsis                  :    This method update bird transformation
            * 
            * References                :    Monogame. (2012). MONOGAME Documentation. Retrieved November 15, 2023,
            *                                   from https://docs.monogame.net/
            *          
            * Modifications             :
            *                                Date            Developer                Notes
            *                                ----            ---------                -----
            *                                2023-10-16      W. Poomarin              Design Layout
            *                                2023-10-17      W. Poomarin              Display Images
            *                                2023-10-18      W. Poomarin              Move Background and Pipe, Move Player
            *                                2023-10-19      W. Poomarin              Add Gravity and click to jump, Add Collision
            *                                2023-10-20      W. Poomarin              Add Score mechanic
            *                                2023-10-21      W. Poomarin              Load and save file
            *                                2023-10-22      W. Poomarin              Add Button
            *                                2023-11-25      W. Poomarin              Art works
            *                                2023-12-03      W. Poomarin              Add input player's name
            *                                
            *=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-= */

            //When is flying
            if (game.flying == true)
            {
                //gravity = 0.5f
                vel = vel + 0.5f;                       //apply gravity to vel = vel + 0.5
                if (vel > 8) {                          //limit vel = 8
                    vel = 8;                          
                }
            }


            //When above ground
            if (rect.Y + rect.Height/2 <= 768)          //make the bird falls
            {
                rect.Y = (int)(rect.Y + vel);           //set Y = Y + vel
            }

            if (game.gameOver == false)
            {
                //Jump when mouse is clicked
                if (Mouse.GetState().LeftButton == ButtonState.Pressed && this.clicked == false)
                {                                       //when mouse is clicked
                    this.clicked = true;                //set clicked = true
                    this.vel = -10;                     //set vel = -10
                }

                //Reset when mouse is released
                if (Mouse.GetState().LeftButton == ButtonState.Released)
                {                                       //when mouse is released
                    this.clicked = false;               //set clicked = false
                }


                //Animattion
                this.counter += 1;                      //counter += 1

                if (this.counter > flapCooldown)        //relay animation
                {
                    this.counter = 0;                   //reset counter = 0
                    this.index += 1;                    //set index += 1
                    if (this.index >= textures.Count)   //when index > size of textures
                    {
                        this.index = 0;                 //reset index = 0
                    }
                }

                //rotate the bird according vel
                this.rotation = this.vel*0.05f;         //set rotation = vel*0.05f
            }
            else
            {
                this.rotation = MathHelper.Pi / 2;      //rotation : head down
            }


            base.Update();
        }

        public override void Draw()
        {

            /*=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
            *
            * Method                    :    Draw
            *
            * Method parameters         :    none
            *
            * Method return             :    void 
            *
            * Synopsis                  :    This method draw sprite on the screen
            * 
            * References                :    Monogame. (2012). MONOGAME Documentation. Retrieved November 15, 2023,
            *                                   from https://docs.monogame.net/
            * Modifications             :
            *                                Date            Developer                Notes
            *                                ----            ---------                -----
            *                                2023-10-16      W. Poomarin              Design Layout
            *                                2023-10-17      W. Poomarin              Display Images
            *                                2023-10-18      W. Poomarin              Move Background and Pipe, Move Player
            *                                2023-10-19      W. Poomarin              Add Gravity and click to jump, Add Collision
            *                                2023-10-20      W. Poomarin              Add Score mechanic
            *                                2023-10-21      W. Poomarin              Load and save file
            *                                2023-10-22      W. Poomarin              Add Button
            *                                2023-11-25      W. Poomarin              Art works
            *                                2023-12-03      W. Poomarin              Add input player's name
            *                                
            *=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-= */

            game._spriteBatch.Draw(this.textures[this.index], this.Rect, null, Color.White, this.rotation, new Vector2(this.Rect.Width / 2, this.Rect.Height / 2), SpriteEffects.None, 0f);     //draw sprite
        }

        public void reset()
        {

            /*=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
            *
            * Method                    :    reset
            *
            * Method parameters         :    none
            *
            * Method return             :    void 
            *
            * Synopsis                  :    This method reset bird data members
            * 
            * References                :    Monogame. (2012). MONOGAME Documentation. Retrieved November 15, 2023,
            *                                   from https://docs.monogame.net/
            *      
            * Modifications             :
            *                                Date            Developer                Notes
            *                                ----            ---------                -----
            *                                2023-10-16      W. Poomarin              Design Layout
            *                                2023-10-17      W. Poomarin              Display Images
            *                                2023-10-18      W. Poomarin              Move Background and Pipe, Move Player
            *                                2023-10-19      W. Poomarin              Add Gravity and click to jump, Add Collision
            *                                2023-10-20      W. Poomarin              Add Score mechanic
            *                                2023-10-21      W. Poomarin              Load and save file
            *                                2023-10-22      W. Poomarin              Add Button
            *                                2023-11-25      W. Poomarin              Art works
            *                                2023-12-03      W. Poomarin              Add input player's name
            *                                
            *=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-= */

            this.rect.Y = (int)(game.SCREEN_HEIGHT / 2);    //set Y = SCREEN_HEIGHT / 2
            this.vel = 0f;                                  //set vel = 0f
        }

        public void addTextures(Texture2D newTexture)
        {

            /*=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
            *
            * Method                    :    Draw
            *
            * Method parameters         :    none
            *
            * Method return             :    void 
            *
            * Synopsis                  :    This method add Textures2D to the textures list
            * 
            * References                :    Monogame. (2012). MONOGAME Documentation. Retrieved November 15, 2023,
            *                                   from https://docs.monogame.net/
            *                                   
            * Modifications             :
            *                                Date            Developer                Notes
            *                                ----            ---------                -----
            *                                2023-10-16      W. Poomarin              Design Layout
            *                                2023-10-17      W. Poomarin              Display Images
            *                                2023-10-18      W. Poomarin              Move Background and Pipe, Move Player
            *                                2023-10-19      W. Poomarin              Add Gravity and click to jump, Add Collision
            *                                2023-10-20      W. Poomarin              Add Score mechanic
            *                                2023-10-21      W. Poomarin              Load and save file
            *                                2023-10-22      W. Poomarin              Add Button
            *                                2023-11-25      W. Poomarin              Art works 
            *                                2023-12-03      W. Poomarin              Add input player's name
            *                                
            *=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-= */

            textures.Add(newTexture);   //add newTexture to textures List
        }
    }
}