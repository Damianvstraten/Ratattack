using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Ratattack_V3;
using System.Media;

namespace Ratattack
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rat _mouse;
        Weapon _weapon;

        MenuItem _menuBackground, _play, _playAgain, _howDoesItWork, _returnButton, _tutorial, _quit;
        Life _life1, _life2, _life3;

        Gamestate _gameState;

        Controls _controls;
        public bool _answer;
        public int _score;
        public string _gameOver = "";
        public bool _PointScored = false;
        private int CheeseTaken = 0;
   
        private Rectangle viewportBackground, viewportGrass;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 760;
            graphics.PreferredBackBufferWidth = 1280;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            _gameState = Gamestate.menu;
            // TODO: Add your initialization logic here
            // Create a new mouse with the textture mouse. 
            // And pass the font as a parameter through mouse so calculation class could use it.
            _controls = new Controls();
            _mouse = new Rat(100,
                new Vector2(1200, 475),
                Content.Load<SpriteFont>("Verdana"),
                this,
                _controls
            );
            _menuBackground = new MenuItem(
               Content.Load<Texture2D>("menu-achtergrond"),
               new Vector2((GraphicsDevice.Viewport.Width / 2) - 400, 0)
           );
            _play = new MenuItem(
                Content.Load<Texture2D>("spelen"),
                new Vector2((GraphicsDevice.Viewport.Width / 2) - 135, 350)
            );
            _playAgain = new MenuItem(
                Content.Load<Texture2D>("opnieuw-spelen"),
                new Vector2((GraphicsDevice.Viewport.Width / 2) - 135, 350)
            );
            _returnButton = new MenuItem(
                Content.Load<Texture2D>("return-button"),
                new Vector2(GraphicsDevice.Viewport.Width - 110, 10)
            );
            _howDoesItWork = new MenuItem(
                Content.Load<Texture2D>("hoewerkthet"),
                new Vector2((GraphicsDevice.Viewport.Width / 2) - 135, 470)
            );
            _tutorial = new MenuItem(
                Content.Load<Texture2D>("tut"),
                new Vector2(0, 0)
            );
            _quit = new MenuItem(
                Content.Load<Texture2D>("afsluiten"),
                new Vector2((GraphicsDevice.Viewport.Width / 2) - 135, 590)
            );
            _weapon = new Weapon(this,
                Content.Load<Texture2D>("weapon"),
                new Vector2(120, 383)
            );
            _life1 = new Life(new Vector2(310, 590), Content.Load<Texture2D>("kaasLeven"));
            _life2 = new Life(new Vector2(310, 555), Content.Load<Texture2D>("kaasLeven"));
            _life3 = new Life(new Vector2(310, 520), Content.Load<Texture2D>("kaasLeven"));
             

            this.IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public void UpdateMenu()
        {
            MouseState mouseState = Mouse.GetState();
            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);
            if (_play.GetBoundingBox().Contains(mousePosition))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    _gameState = Gamestate.running;
                }
                
            }
            if (_howDoesItWork.GetBoundingBox().Contains(mousePosition))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    _gameState = Gamestate.tutorial;
                }
            }
            if (_quit.GetBoundingBox().Contains(mousePosition))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    Exit();
                }
            }          
        }

        public void UpdateReplayMenu()
        {
            MouseState mouseState = Mouse.GetState();
            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);
            if (_playAgain.GetBoundingBox().Contains(mousePosition))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    _gameState = Gamestate.running;
                }
            }
            if (_quit.GetBoundingBox().Contains(mousePosition))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    Exit();
                }
            }
        }

        public void UpdateGame()
        {
             _mouse.Update();
            _controls.update();
            if (!_mouse._HasLife && Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                if (_mouse.VerifyAnswer(_controls._string))
                {
                    _weapon.ShootAnimation();
                    _answer = true;
                }
                else
                {
                    _answer = false;
                    _mouse.ClearAnswer();
                }
            }
            if (_answer)
            {
                _weapon.Shoot(_mouse, _controls);
            }

            if (CheeseTaken == 0)
            {
                if (_mouse.GetBoundingBox().Intersects(_life3.GetBoundingBox()))
                {

                    _life3.SetPosition(new Vector2(_mouse.GetLocation().X + 80, _mouse.GetLocation().Y + 60));

                    _mouse.TookLife(true);

                    if (_mouse.GetLocation().X > GraphicsDevice.Viewport.Width)
                    {
                        _life3.SetPosition(new Vector2(-100, -100));
                        _mouse.TookLife(false);
                        CheeseTaken++;
                        _mouse.GenerateNewSum();
                    }
                }
            }
            if (CheeseTaken == 1)
            {
                if (_mouse.GetBoundingBox().Intersects(_life2.GetBoundingBox()))
                {

                    _life2.SetPosition(new Vector2(_mouse.GetLocation().X + 80, _mouse.GetLocation().Y + 60));


                    _mouse.TookLife(true);

                    if (_mouse.GetLocation().X > GraphicsDevice.Viewport.Width)
                    {
                        _life2.SetPosition(new Vector2(-100, -100));
                        _mouse.TookLife(false);
                        CheeseTaken++;
                        _mouse.GenerateNewSum();
                    }
                }
            }
            if (CheeseTaken == 2)
            {
                if (_mouse.GetBoundingBox().Intersects(_life1.GetBoundingBox()))
                {

                    _life1.SetPosition(new Vector2(_mouse.GetLocation().X + 80, _mouse.GetLocation().Y + 60));

                    _mouse.TookLife(true);

                    if (_mouse.GetLocation().X > GraphicsDevice.Viewport.Width)
                    {
                        _life1.SetPosition(new Vector2(-100, -100));
                        _mouse.TookLife(false);
                        CheeseTaken++;
                        _mouse.GenerateNewSum();
                        _mouse.Stop();
                        _gameOver = "Game Over";
                        _gameState = Gamestate.restart;
                    }
                }
            }

            if (_mouse.GetLocation().X < 443)
            {
                _mouse.ReverseMouse();
            }

            _score = _weapon.deaths * 100;  

        }

        public void UpdateRestart()
        {
            InitializeTwo();
        }

        public void UpdateTutorial()
        {
            MouseState mouseState = Mouse.GetState();
            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);

            if (_returnButton.GetBoundingBox().Contains(mousePosition))
            {
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    _gameState = Gamestate.menu;
                }
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            switch (_gameState)
            {
                case Gamestate.menu:
                    UpdateMenu();
                    break;
                case Gamestate.restart:
                    UpdateRestart();
                    break;
                case Gamestate.running:
                    UpdateGame();
                    break;
                case Gamestate.tutorial:
                    UpdateTutorial();
                    break;
                case Gamestate.replaymenu:
                    UpdateReplayMenu();
                    break;
            }

            base.Update(gameTime);

        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            switch (_gameState)
            {
                case    Gamestate.menu :
                    DrawMenu();
                    break;
                case    Gamestate.restart :
                    RestartGame();
                    break;
                case    Gamestate.running :
                    DrawGame();
                    break;
                case Gamestate.tutorial:
                    DrawTutorial();
                    break;
                case Gamestate.replaymenu:
                    DrawReplayMenu();
                    break;
            }
            base.Draw(gameTime);
        }

        public void DrawMenu()
        {
            spriteBatch.Begin();
                viewportBackground = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
                spriteBatch.Draw(Content.Load<Texture2D>("achtergrond"), viewportBackground, Color.White);
               
                viewportGrass = new Rectangle(0, GraphicsDevice.Viewport.Height - 142, GraphicsDevice.Viewport.Width, 142);
                spriteBatch.Draw(Content.Load<Texture2D>("gras"), viewportGrass, Color.White);

                _menuBackground.Draw(spriteBatch);
                _play.Draw(spriteBatch);
                _howDoesItWork.Draw(spriteBatch);
                _quit.Draw(spriteBatch);

            spriteBatch.End();
        }

        public void DrawReplayMenu()
        {
            spriteBatch.Begin();
            viewportBackground = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            spriteBatch.Draw(Content.Load<Texture2D>("achtergrond"), viewportBackground, Color.White);

            viewportGrass = new Rectangle(0, GraphicsDevice.Viewport.Height - 142, GraphicsDevice.Viewport.Width, 142);
            spriteBatch.Draw(Content.Load<Texture2D>("gras"), viewportGrass, Color.White);

            _menuBackground.Draw(spriteBatch);
            _playAgain.Draw(spriteBatch);
            _quit.Draw(spriteBatch);

            spriteBatch.End();
        }

        public void DrawTutorial()
        {
            spriteBatch.Begin();

            _tutorial.Draw(spriteBatch);
            _returnButton.Draw(spriteBatch);

            spriteBatch.End();
        }

        public void DrawGame()
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            viewportBackground = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            spriteBatch.Draw(Content.Load<Texture2D>("achtergrond"), viewportBackground, Color.White);

            _mouse.Draw(spriteBatch, _controls._string);

            if (_life1 != null)
            {
                _life1.Draw(spriteBatch);
            }

            _life2.Draw(spriteBatch);
            _life3.Draw(spriteBatch);

            viewportGrass = new Rectangle(0, GraphicsDevice.Viewport.Height - 142, GraphicsDevice.Viewport.Width, 142);
            spriteBatch.Draw(Content.Load<Texture2D>("gras"), viewportGrass, Color.White);

            spriteBatch.DrawString(
                Content.Load<SpriteFont>("Verdana"),
                "Score : " + _score + "",
                new Vector2(GraphicsDevice.Viewport.Width / 2 - 40, 20),
                Color.White);

            spriteBatch.DrawString(
                Content.Load<SpriteFont>("Verdana"),
                _gameOver,
                new Vector2((GraphicsDevice.Viewport.Width / 2) - 170, (GraphicsDevice.Viewport.Height / 2) - 100),
                Color.White,
                0f,
                new Vector2(0, 0),
                new Vector2(3, 3),
                SpriteEffects.None,
                1f
            );

            _weapon.Draw(spriteBatch);

            spriteBatch.End();
        }


        public void RestartGame()
        {
            
        }

        protected void InitializeTwo()
        {
            CheeseTaken = 0;
            _gameOver = "";
            _gameState = Gamestate.replaymenu;

            _controls = new Controls();
            _mouse = new Rat(100,
                new Vector2(1200, 475),
                Content.Load<SpriteFont>("Verdana"),
                this,
                _controls
            );
            _menuBackground = new MenuItem(
               Content.Load<Texture2D>("menu-achtergrond"),
               new Vector2((GraphicsDevice.Viewport.Width / 2) - 400, 0)
           );
            _play = new MenuItem(
                Content.Load<Texture2D>("spelen"),
                new Vector2((GraphicsDevice.Viewport.Width / 2) - 135, 350)
            );
            _playAgain = new MenuItem(
                Content.Load<Texture2D>("opnieuw-spelen"),
                new Vector2((GraphicsDevice.Viewport.Width / 2) - 135, 350)
            );
            _returnButton = new MenuItem(
                Content.Load<Texture2D>("return-button"),
                new Vector2(GraphicsDevice.Viewport.Width - 110, 10)
            );
            _howDoesItWork = new MenuItem(
                Content.Load<Texture2D>("hoewerkthet"),
                new Vector2((GraphicsDevice.Viewport.Width / 2) - 135, 470)
            );
            _tutorial = new MenuItem(
                Content.Load<Texture2D>("tut"),
                new Vector2(0, 0)
            );
            _quit = new MenuItem(
                Content.Load<Texture2D>("afsluiten"),
                new Vector2((GraphicsDevice.Viewport.Width / 2) - 135, 470)
            );
            _weapon = new Weapon(this,
                Content.Load<Texture2D>("weapon"),
                new Vector2(120, 383)
            );
            _life1 = new Life(new Vector2(310, 590), Content.Load<Texture2D>("kaasLeven"));
            _life2 = new Life(new Vector2(310, 555), Content.Load<Texture2D>("kaasLeven"));
            _life3 = new Life(new Vector2(310, 520), Content.Load<Texture2D>("kaasLeven"));

            this.IsMouseVisible = true;

        }
       
    }



    



}
