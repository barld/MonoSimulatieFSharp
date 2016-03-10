module Truck
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

type Truck =
    {
        position: Vector2
        velocity: Vector2
    }
    member this.Update dt =
        {
            this with position = new Vector2(   this.position.X + this.velocity.X * dt, //x
                                                this.position.Y + this.velocity.Y * dt) //y
        }

let getTruckDrawer (truckTexture:Texture2D) =
    let truckDrawer (truck:Truck) (spriteBatch:SpriteBatch) =
        let effect =
            if truck.velocity.X < 0.f then
                SpriteEffects.FlipHorizontally
            else
                SpriteEffects.None
        spriteBatch.Draw(truckTexture, truck.position, System.Nullable(), Color.White, 0.f, Vector2.Zero, 0.3f, effect, 0.f)

    truckDrawer
        