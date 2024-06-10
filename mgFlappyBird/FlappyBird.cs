using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;

namespace mgFlappyBird
{
    public class FlappyBird : Game
    {
        private GraphicsDeviceManager _graphics;            //define _graphics as GraphicsDeviceManager 
        public SpriteBatch _spriteBatch;                    //define _spriteBatch as SpriteBatch
        private SpriteFont scoreFont;                       //define scoreFont as SpriteFont

        public int SCREEN_WIDTH = 864;                      //define and instantiate SCREEN_WIDTH as int set to 864
        public int SCREEN_HEIGHT = 936;                     //define and instantiate SCREEN_HEIGHT as int set to 936

        Bird flappy;                                        //define flappy as Bird object
        Ground ground;                                      //define ground as Ground object
        Sprite background;                                  //define bg as Sprite object
        List<Pipe> pipes;                                   //define pipes as List<Pipe> object
        Pipe pipeBtm, pipeTop;                              //define pipeBtm, pipeTop as Pipe object
        BottonS resetButton;                                //define resetButton as BottonS object
        
        Random random = new Random();
        byte speed = 4;                                     //define and instantiate speed as byte set to 4                     
        int pipeGap = 200;                                  //define and instantiate pipeGap as int
        int timeNowMillis;                                  //define timeNowMillis as int
        int pipeHeight;                                     //define pipeHeight as int
        static int pipeFrequency = 1500;                    //define and instantiate pipeFrequency as int set to 1500
        int lastPipe = -1500;                               //define and instantiate lastPipe as int set to -1500 
        bool passPipe = false;                              //define and instantiate passPipe as boolean set to false

        public bool flying = false;                         //define and instantiate flying as boolean set to false
        public bool gameOver = false;                       //define and instantiate gameOver as boolean set to false
        public bool loaded = false;                         //define and instantiate loaded as boolean set to false
        public bool saved = false;                          //define and instantiate saved as boolean set to false
        public bool newHiScore = false;                     //define and instantiate newHiScore as boolean set to false

        int score = 0;                                      //define and instantiate score as int set to 0   
        int hiScore;                                        //define hiScore as int 

        string fileName = "HighScoreName.txt";              //define and instantiate fileName as string

        string playerName = "";                             //define and instantiate playerName as string
        string hiScoreName = "";                            //define and instantiate hiScoreName as string
        public FlappyBird()
        {

            /*=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
            *
            * Method                    :    FlappyBird Constructor
            *
            * Method parameters         :    none
            *
            * Method return             :    FlappyBird 
            *
            * Synopsis                  :    This method constructs FlappyBird object.
            * 
            * References                :    Monogame. (2012). MONOGAME Documentation. Retrieved November 15, 2023,
            *                                   from https://docs.monogame.net/
            *                                Tutorialspoint (2023). C# int.TryParse Method. Retrieved November 15, 2023,
            *                                   from https://www.tutorialspoint.com/chash-int-tryparse-method
            *                                Cloud Skills Challenge, Microsoft (2023). Use Visual C# to read from and write to a text file. Retrieved November 15, 2023,
            *                                   from https://learn.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/language-compilers/read-write-text-file
            *                                w3schools (2023). C# Foreach Loop. Retrieved November 15, 2023,
            *                                   from https://www.w3schools.com/cs/cs_foreach_loop.php
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

            _graphics = new GraphicsDeviceManager(this);    
            Content.RootDirectory = "Content";              //set RootDirectory
            IsMouseVisible = true;                          //set IsMouseVisible = true

            // Set the desired frame rate to 60 FPS
            IsFixedTimeStep = true;     // Set to false for variable time step
            TargetElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / 60);

            //enable TextInputHandler
            Window.TextInput += TextInputHandler;
        }

        private void TextInputHandler(object sender, TextInputEventArgs args)
        {
            var pressedKey = args.Key;                                          //get TextInputEventArgs Key
            var character = args.Character;                                     //get TextInputEventArgs Character


            if ( ((int)character != 8) && playerName.Length < 3)                //if key is not BACKSPACE
            {
                playerName = playerName + character;                            //set playerName
            }
            else if (((int)character == 8) && playerName.Length > 0)            
            {
                playerName = playerName.Substring(0, playerName.Length - 1);    //remove 1 letter
            }

        }

        protected override void Initialize()
        {

            /*=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
            *
            * Method                    :    Initialize
            *
            * Method parameters         :    none
            *
            * Method return             :    void
            *
            * Synopsis                  :    This method initialize game
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

            // Set the desired resolution
            _graphics.PreferredBackBufferWidth = SCREEN_WIDTH;      // Set width
            _graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;    // Set Height
            _graphics.ApplyChanges();

            pipes = new List<Pipe>();                               // instantiate pipes as List<Pipe>

            base.Initialize();
        }

        protected override void LoadContent()
        {

            /*=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
            *
            * Method                    :    LoadContent
            *
            * Method parameters         :    none
            *
            * Method return             :    void
            *
            * Synopsis                  :    This method loads contents in the game
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

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            //create bird object and load sprite images
            Texture2D birdTexture = Content.Load<Texture2D>("bird1");                   
            flappy = new Bird(this, birdTexture, new Vector2(100, SCREEN_HEIGHT/2));    
            flappy.addTextures(Content.Load<Texture2D>("bird2"));                       
            flappy.addTextures(Content.Load<Texture2D>("bird3"));

            //create background object and load sprite images
            Texture2D bgTexture = Content.Load<Texture2D>("bg");
            background = new Sprite(this, bgTexture, Vector2.Zero);

            //create ground object and load sprite images
            Texture2D groundTexture = Content.Load<Texture2D>("ground");                
            ground = new Ground(this, groundTexture, new Vector2(0, 768), speed);

            //create BottonS object and load sprite images
            Texture2D resetButtonTexture = Content.Load<Texture2D>("restart");
            resetButton = new BottonS(this, resetButtonTexture, new Vector2(SCREEN_WIDTH / 2 - resetButtonTexture.Width/2, SCREEN_HEIGHT / 2 - resetButtonTexture.Height/2));

            //create font
            scoreFont = Content.Load<SpriteFont>("scoreFont");

        }

        protected override void Update(GameTime gameTime)
        {

            /*=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
            *
            * Method                    :    Update
            *
            * Method parameters         :    GameTime
            *
            * Method return             :    void
            *
            * Synopsis                  :    This method update elements on the screen and game states
            * 
            * References                :    Monogame. (2012). MONOGAME Documentation. Retrieved November 15, 2023,
            *                                   from https://docs.monogame.net/
            *                                Cloud Skills Challenge, Microsoft (2023). Use Visual C# to read from and write to a text file. Retrieved November 15, 2023,
            *                                   from https://learn.microsoft.com/en-us/troubleshoot/developer/visualstudio/csharp/language-compilers/read-write-text-file
            *                                w3schools (2023). C# Foreach Loop. Retrieved November 15, 2023,
            *                                   from https://www.w3schools.com/cs/cs_foreach_loop.php
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

            //define and instantiate KeyboardState and MouseState
            KeyboardState keyboardState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            Rectangle textBoxRectangle = new Rectangle(100, 100, 200, 30);

            //Exit when pressed 'Escape'
            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            //When mouse is clicked
            if (mouseState.LeftButton == ButtonState.Pressed && flying == false && gameOver == false)
            {
                flying = true;      //set flying = true
            }

            //When the bird it the ground
            if (flappy.Rect.Intersects(ground.Rect))
            {
                gameOver = true;    //set gameOver = true
            }

            //When the bird is out off the top screen
            if (flappy.Rect.Top < 0)
            {
                gameOver = true;    //set gameOver = true
            }

            //When gameOver is not true
            if (gameOver == false && flying == true)
            {
                
                timeNowMillis = (int)gameTime.TotalGameTime.TotalMilliseconds;  //set timeNowMillis = current time
                if ((timeNowMillis - lastPipe) > pipeFrequency) {               //cool down
                    pipeHeight = random.Next(-100, 101);                        //pipeHeight = random from -100 to 101
                    Texture2D pipeTexture = Content.Load<Texture2D>("pipe");    //load pipeTexture
                    //create and instantiate Pipe objects
                    pipeBtm = new Pipe(this, pipeTexture, new Vector2(SCREEN_WIDTH, SCREEN_HEIGHT / 2 + pipeHeight), speed, true, pipeGap);
                    pipeTop = new Pipe(this, pipeTexture, new Vector2(SCREEN_WIDTH, SCREEN_HEIGHT / 2 + pipeHeight), speed, false, pipeGap);
                    pipes.Add(pipeBtm);                                         //add pipeBtm to pipes
                    pipes.Add(pipeTop);                                         //add pipeTop to pipes
                    lastPipe = timeNowMillis;                                   //set lastPipe = timeNowMillis
                }

                //pipes is not empty
                if (pipes.Count != 0)
                {
                    try
                    {
                        foreach (var pipe in pipes)                 //for every elements in pipes
                        {
                            pipe.Update();                          //update position
                            if (pipe.Rect.Intersects(flappy.Rect))  //if pipe collide with the bird
                            {
                                gameOver = true;                    //set gameOver = true
                            }
                        }

                        //when the bird passes the left side of the pipe
                        if (flappy.Rect.Left > pipes[0].Rect.Left && flappy.Rect.Right < pipes[0].Rect.Right && passPipe == false)
                        {
                            passPipe = true;                        //set passPipe = true
                        }

                        //when the bird passes the right side of the pipe
                        if (passPipe == true)
                        {
                            if (flappy.Rect.Left > pipes[0].Rect.Right)
                            {
                                score++;                            //set score++
                                passPipe = false;                   //set passPipe = false
                            }
                        }

                        //when pipe is off the screen
                        if (pipes[0].Rect.Right < 0)
                        {
                            pipes.RemoveRange(0, 2);                //destroy pipe object
                        }
                    } 
                    catch
                    {
                        Debug.WriteLine("Error: pipes array");      //display error message
                    }
                    
                }

                ground.Update();                                    //Update the ground
            }


            flappy.Update();                                        //Update the bird

            if (gameOver ==  true)                                  //if gameOver ==  true
            {
                loadFile(fileName);                                 //load the file
            }



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            /*=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
            *
            * Method                    :    Draw
            *
            * Method parameters         :    GameTime
            *
            * Method return             :    void
            *
            * Synopsis                  :    This method draws elements on the screen
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


            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);      //Shapeness for pixel art, start drawing

            background.Draw();                                              //Draw Background

            flappy.Draw();                                                  //Draw Bird

            if (pipes.Count != 0)                                           //if pipes is not empty
            {
                try
                {
                    foreach (var pipe in pipes)                             //for every elements pipe in pipes
                    {
                        pipe.Draw();                                        //draw pipe
                    }
                } 
                catch 
                {
                    Debug.WriteLine("Error: pipes array");                  //display error message
                }
    
            }
    
            ground.Draw();                                                  //Draw Ground
            
            //display text
            _spriteBatch.DrawString(scoreFont, $"{score}", new Vector2(SCREEN_WIDTH / 2, 20), Color.Black);

            //when gameOver == true
            if (gameOver == true)
            {

                //if it is a new high score
                if (newHiScore == true)
                {
                    //display text
                    _spriteBatch.DrawString(scoreFont, "New High Score", new Vector2(SCREEN_WIDTH / 4, 120), Color.Black);
                    _spriteBatch.DrawString(scoreFont, $"{score}", new Vector2(SCREEN_WIDTH / 2, 220), Color.Black);
                    _spriteBatch.DrawString(scoreFont, "Enter Name: " + playerName, new Vector2(SCREEN_WIDTH / 7, 320), Color.Black);

                    //when the resetButton is clicked
                    if (resetButton.DrawButton() == true)
                    {
                        
                        hiScoreName = playerName;               //set hiScoreName = playerName
                        hiScore = score;                        //set hiScore = score
                        saveFile(fileName);                     //save new high score
                        resetGame();                            //reset the game
                    }

                }
                else
                {
                    //display text
                    _spriteBatch.DrawString(scoreFont, "High Score", new Vector2(SCREEN_WIDTH / 3, 120), Color.Black);
                    _spriteBatch.DrawString(scoreFont, $"{hiScoreName}: {hiScore}", new Vector2((int)(SCREEN_WIDTH / 2.5), 220), Color.Black);
                    //when the resetButton is clicked
                    if (resetButton.DrawButton() == true)
                    {
                        resetGame();                        //reset the game
                    }
                }
            }
            


            _spriteBatch.End();                             //end drawing

            base.Draw(gameTime);
        }

        public void resetGame()
        {

            /*=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
            *
            * Method                    :    resetGame
            *
            * Method parameters         :    none
            *
            * Method return             :    void
            *
            * Synopsis                  :    This method resets game data members
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

            pipes = new List<Pipe>();           //clear pipes List
            flappy.reset();                     //reset bird's data member
            gameOver = false;                   //set gameOver = false
            flying = false;                     //set flying = false
            loaded = false;                     //set loaded = false
            saved = false;                      //set saved = false
            newHiScore = false;                 //set newHiScore = false
            score = 0;                          //set score = 0
        }

        public void loadFile(string filePath)
        {

            /*=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
            *
            * Method                    :    loadFile
            *
            * Method parameters         :    string
            *
            * Method return             :    void
            *
            * Synopsis                  :    This method loads file
            * 
            * References                :    Monogame. (2012). MONOGAME Documentation. Retrieved November 15, 2023,
            *                                   from https://docs.monogame.net/
            *                                Tutorialspoint (2023). C# int.TryParse Method. Retrieved November 15, 2023,
            *                                   from https://www.tutorialspoint.com/chash-int-tryparse-method
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

            //load file
            if (loaded == false)
            {
                playerName = "";
                // Load all lines from the file
                try
                {
                    string[] lines = File.ReadAllLines(filePath);

                    // Check if there are at least two lines in the file
                    if (lines.Length > 0)
                    {
                        // Split the line into name and score
                        string[] data = lines[0].Split(' ');

                        // Get the string and integer values from the file
                        if (data[0].Length > 0)
                        {
                            hiScoreName = data[0];
                        }
                        else
                        {
                            hiScoreName = "n/a";
                        }
                        
                        // Try parsing the score
                        if (int.TryParse(data[1], out hiScore))
                        {
                            if (score > hiScore)        //if score > hiScore then
                            {
                                newHiScore = true;      //set newHiScore = true
                            } 
                            else
                            {
                                newHiScore = false;     //set newHiScore = false
                            }
                        }

                    }
                } 
                catch 
                {
                    Debug.WriteLine("Cannot load the file");    //display error message
                }

                loaded = true;                                  //set loaded = true
            }
        }

        public void saveFile(string filePath)
        {

            /*=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
            *
            * Method                    :    saveFile
            *
            * Method parameters         :    string
            *
            * Method return             :    void
            *
            * Synopsis                  :    This method saves file
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

            if (saved == false)
            {
                // Create an array of strings representing lines
                string[] linesToSave = { $"{hiScoreName} {hiScore}" };
                // Save File
                try
                {
                    //Write File
                    File.WriteAllLines(filePath, linesToSave);
                    /*
                    if (!File.Exists(filePath))
                    {
                        // Create a new file and write the data
                        File.WriteAllLines(filePath, linesToSave);
                    }
                    else
                    {
                        // Write the data to the existing file
                        File.WriteAllLines(filePath, linesToSave);

                        //Debug.WriteLine("Data appended to existing highScoreSave.txt");
                    }
                    */
                }
                catch
                {
                    Debug.WriteLine("Cannot save the file");    //display error message
                }

                saved = true;                                   //set saved = true
            }
        }
    }
}