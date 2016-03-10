module Truck
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

type Truck =
    {
        texture: Texture2D
        position: Vector2
        velocity: Vector2
    }
    member this.Update dt =
        {
            texture = this.texture
            position = new Vector2( this.position.X + this.velocity.X * dt, //x
                                    this.position.Y + this.velocity.Y * dt) //y
            velocity = this.velocity
        }

    member this.Draw (spriteBatch:SpriteBatch) =
        let effect =
            if this.velocity.X < 0.f then
                SpriteEffects.FlipHorizontally
            else
                SpriteEffects.None
        spriteBatch.Draw(this.texture, this.position, System.Nullable(), Color.White, 0.f, Vector2.Zero, 0.3f, effect, 0.f)
