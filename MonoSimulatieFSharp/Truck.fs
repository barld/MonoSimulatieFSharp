module Truck
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open UnitOfMeasures

type Container =
    | OreContainer
    | ProductContainer


type Truck =
    {
        position: Vector2
        velocity: Vector2
        container: Option<Container>
    }
    member this.Update (dt:float32<sec>) =
        {
            this with position = new Vector2(   this.position.X + this.velocity.X * (dt |> float32), //x
                                                this.position.Y + this.velocity.Y * (dt |> float32)) //y
        }

let getTruckDrawer (truckTexture:Texture2D) productContainer oreContainer =
    let truckDrawer (spriteBatch:SpriteBatch) (truck:Truck) =
        let effect =
            if truck.velocity.X < 0.f then
                SpriteEffects.FlipHorizontally
            else
                SpriteEffects.None
        spriteBatch.Draw(truckTexture, truck.position, System.Nullable(), Color.White, 0.f, Vector2.Zero, 0.3f, effect, 0.f)

        match truck.container with
        | Some c -> 
            let texture = 
                match c with
                | OreContainer ->  oreContainer
                | ProductContainer -> productContainer

            let position = 
                if truck.velocity.X > 0.f then
                    new Vector2(truck.position.X , truck.position.Y-12.f)
                else
                    new Vector2(truck.position.X+60.f, truck.position.Y-25.f)

            spriteBatch.Draw(texture, position, System.Nullable(), Color.White, 0.f, Vector2.Zero, 0.3f, effect, 0.f)
        | None -> ()

    in truckDrawer
        