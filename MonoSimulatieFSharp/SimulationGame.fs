
module game

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open GameStatus
open Truck






type SimulationGame () as this =
    inherit Game()

    do this.Content.RootDirectory <- "Content"
    let graphics = new GraphicsDeviceManager(this)
    let mutable spriteBatch = Unchecked.defaultof<SpriteBatch>

    //---------------------------------------------------------
    let mutable gameStatus:GameStatus = Unchecked.defaultof<GameStatus>

    override this.Initialize() =
        do base.Initialize()

    override this.LoadContent() =
        do spriteBatch <- new SpriteBatch(this.GraphicsDevice)

        let background = this.Content.Load<Texture2D>("background.png")
        let truck = {texture = this.Content.Load<Texture2D>("volvo.png"); position=new Vector2(0.f,80.f); velocity=new Vector2(30.f,0.f)}

        gameStatus <- {background=background;truck=truck}

        ()

    override this.Update (gameTime) =
        gameStatus <- gameStatus.Update(gameTime.ElapsedGameTime.TotalSeconds |> float32)
        ()

    /// gametime is deltatime between the last and this frame
    override this.Draw (gameTime) =
        do this.GraphicsDevice.Clear Color.LightGreen       
        spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

        gameStatus.Draw(spriteBatch)

        spriteBatch.End() 
        ()