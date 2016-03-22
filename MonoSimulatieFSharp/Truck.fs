module Truck
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open UnitOfMeasures

type Container =
    | OreContainer
    | ProductContainer

type Direction =
    | Left
    | Right


type Truck =
    {
        Position: Vector2
        Acceleration: float32<pixel/sec^2>
        Velocity: float32<pixel/sec>
        Direction: Direction
        Container: Option<Container>
    }
    member this.Update (dt:float32<sec>) =
        let velocity = this.Velocity + this.Acceleration * dt
        let distance = match this.Direction with
            | Left -> this.Velocity * dt * -1.0f
            | Right -> this.Velocity * dt
        {
            this with 
                Position = new Vector2(this.Position.X + (distance |> float32), this.Position.Y) //x,y
                Velocity = velocity
        }

let getTruckDrawer (truckTexture:Texture2D) productContainer oreContainer =
    let truckDrawer (spriteBatch:SpriteBatch) (truck:Truck) =
        let effect =
            if truck.Direction = Direction.Left then
                SpriteEffects.FlipHorizontally
            else
                SpriteEffects.None
        spriteBatch.Draw(truckTexture, truck.Position, System.Nullable(), Color.White, 0.f, Vector2.Zero, 0.3f, effect, 0.f)

        match truck.Container with
        | Some c -> 
            let texture = 
                match c with
                | OreContainer ->  oreContainer
                | ProductContainer -> productContainer

            let position = 
                if truck.Direction = Direction.Right then
                    new Vector2(truck.Position.X , truck.Position.Y-12.f)
                else
                    new Vector2(truck.Position.X+60.f, truck.Position.Y-25.f)

            spriteBatch.Draw(texture, position, System.Nullable(), Color.White, 0.f, Vector2.Zero, 0.3f, effect, 0.f)
        | None -> ()

    in truckDrawer
        