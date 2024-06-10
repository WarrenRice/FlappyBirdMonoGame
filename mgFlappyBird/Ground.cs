using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace mgFlappyBird
{
    internal class Ground : Sprite
    {
        private Rectangle rect;         //define rect as Rectangle
        private float vel;              //define vel as float

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
                return rect;            // return rect
            }
        }

        public Ground(FlappyBird g, Texture2D texture, Vector2 position, byte vel) : base(g, texture, position)
        {

            /*=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
            *
            * Method                    :    Ground Constructor
            *
            * Method parameters         :    FlappyBird, Texture2D, Vector2, byte
            *
            * Method return             :    Ground 
            *
            * Synopsis                  :    This method constructs Ground object.
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
            this.vel = vel;             // set data member vel = vel

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
            * Synopsis                  :    This method update the ground position
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

            //moving ground
            rect.X = (int)(rect.X - vel);       // set X = X - vel
            if (rect.X < -35) {                 // when X < -35
                rect.X = 0;                     // set X = 0
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

            game._spriteBatch.Draw(this.texture, this.Rect, Color.White);       //draw sprite
        }
    }
}
