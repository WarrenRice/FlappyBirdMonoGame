using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace mgFlappyBird
{

    internal class BottonS : Sprite
    {
        private Rectangle rect;         // define rect as Rectangle
        bool clicked;                   // define clicked as boolean
        private Rectangle Rect
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
                return rect;            // return rect
            }
        }

        public BottonS(FlappyBird g, Texture2D texture, Vector2 position) : base(g, texture, position)
        {

            /*=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
            *
            * Method                    :    BottonS Constructor
            *
            * Method parameters         :    FlappyBird, Texture2D, Vector2
            *
            * Method return             :    BottonS 
            *
            * Synopsis                  :    This method constructs BottonS object.
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
            clicked = false;            //instantiate clicked = false
        }

        public bool DrawButton()
        {

            /*=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
            *
            * Method                    :    DrawButton
            *
            * Method parameters         :    none
            *
            * Method return             :    boolean 
            *
            * Synopsis                  :    This method draw sprite on the screen and return button state
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

            bool action = false;                            //create and instantiate action as bool set to false

            // Check if the mouse is over the button
            MouseState mouseState = Mouse.GetState();       //create and instantiate mouseState

            if (rect.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed && this.clicked == false)    //when is clicked
            {
                this.clicked = true;                        //set licked = true
            }
            if (rect.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Released && this.clicked == true)    //when is released
            {
                action = true;                              //set action = true
                this.clicked = false;                       //set licked = false
            }

            game._spriteBatch.Draw(this.texture, this.Rect, Color.White);       //draw sprite

            return action;                                                      //action 
        }
    }
}
