module GameStatus
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Truck

type GameStatus = 
    {
        background: Texture2D
        truck: Truck
        truckDrawer: (Truck->SpriteBatch->Unit)
    }
    member this.Update dt =
        {
            this with
                truck = this.truck.Update(dt)
        }

    member this.Draw (spriteBatch:SpriteBatch) =
        spriteBatch.Draw(this.background, Vector2.Zero, Color.White);
        this.truckDrawer this.truck spriteBatch
        ()
