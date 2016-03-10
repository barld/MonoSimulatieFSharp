
module game

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input

type GameStatus = 
    {
        background: Texture2D
        truck: Texture2D
        position: Vector2
        Velocity: Vector2
    }
    member this.Update dt =
        {
            background = this.background
            truck = this.truck
            position = new Vector2(this.position.X + this.Velocity.X * dt, this.position.Y + this.Velocity.Y * dt)
            Velocity = this.Velocity
        }

    member this.Draw (spriteBatch:SpriteBatch) =
        spriteBatch.Draw(this.background, Vector2.Zero, Color.White);

        let effect =
            if this.Velocity.X < 0.f then
                SpriteEffects.FlipHorizontally
            else
                SpriteEffects.None
        spriteBatch.Draw(this.truck, this.position, System.Nullable(), Color.White, 0.f, Vector2.Zero, 0.3f, effect, 0.f)
        ()






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
        let truck = this.Content.Load<Texture2D>("volvo.png")

        gameStatus <- {background=background;truck=truck;position=new Vector2(0.f,80.f); Velocity=new Vector2(30.f,0.f)}

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