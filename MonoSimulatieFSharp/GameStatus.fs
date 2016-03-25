module GameStatus
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Truck
open Factory
open UnitOfMeasures

type GameStatus = 
    {
        Background: Texture2D
        Trucks: list<Truck>
        Factorys: list<Factory>
        TruckDrawer: (Truck->Unit)
        FactoryDrawer: (Factory->Unit)
        Time: float32<sec>
        Sun: Texture2D
    }

    member this.Draw (spriteBatch:SpriteBatch) =
        spriteBatch.Draw(this.Background, Vector2.Zero, Color.White);
        this.Trucks |> List.iter this.TruckDrawer
        this.Factorys |> List.iter this.FactoryDrawer
        DayParts.drawSun this.Time spriteBatch this.Sun
        ()
