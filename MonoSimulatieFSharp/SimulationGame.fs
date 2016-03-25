
module game

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open GameStatus
open Factory
open UnitOfMeasures
open StateMonad

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
        let sun = this.Content.Load<Texture2D>("sun.png")
        let truckDrawer = 
            Truck.getTruckDrawer 
                (this.Content.Load<Texture2D> "volvo.png")
                (this.Content.Load<Texture2D> "product_container.png")
                (this.Content.Load<Texture2D> "ore_container.png")

        gameStatus <- 
            {
                Background = background
                Sun = sun
                Trucks = []
                TruckDrawer=truckDrawer spriteBatch
                FactoryDrawer = 
                    getFactorDrawer 
                        (this.Content.Load<Texture2D> "mine.png") 
                        (this.Content.Load<Texture2D> "ikea.png")
                        (this.Content.Load<Texture2D> "mine_cart.png")
                        (this.Content.Load<Texture2D> "product_box.png")
                         spriteBatch
                Factorys = [baseMine;baseIkea]
                Time = 0.f<sec>
            }

        ()

    override this.Update (gameTime) =
        let dt = ((gameTime.ElapsedGameTime.TotalSeconds |> float32) * 1.0f<sec>)

        do gameStatus <- 
            gameStatus |> 
            stateFlow
                {
                    let! time' = Updates.UpdateTime dt

                    do! Updates.RemoveTrucksOutOfScreen ()
                    do! Updates.GetNewTrucks ()
                    do! Updates.DriveTrucks dt
                    do! Updates.FactorysProduce dt time'

                    

                    return ()
                }
            |> snd

        ()

    /// gametime is deltatime between the last and this frame
    override this.Draw (gameTime) =
        do this.GraphicsDevice.Clear Color.LightGreen       
        spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

        gameStatus.Draw(spriteBatch)

        spriteBatch.End() 
        ()