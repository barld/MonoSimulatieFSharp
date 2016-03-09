
module game

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input

type SimulationGame () as this =
    inherit Game()

    do this.Content.RootDirectory <- "Content"
    let graphics = new GraphicsDeviceManager(this)
    let mutable spriteBatch = Unchecked.defaultof<SpriteBatch>
    let mutable texture = Unchecked.defaultof<Texture2D>

    override this.Initialize() =
        do base.Initialize()

    override this.LoadContent() =
        do spriteBatch <- new SpriteBatch(this.GraphicsDevice)
        ()

    override this.Update (gameTime) =
        ()

    override this.Draw (gameTime) =
        do this.GraphicsDevice.Clear Color.LightGreen       
        spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

        let background = this.Content.Load<Texture2D>("background.png")

        spriteBatch.Draw(background, Vector2.Zero, Color.White);

        spriteBatch.End() 
        ()