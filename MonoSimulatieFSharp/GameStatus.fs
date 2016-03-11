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

    }
    member this.Update dt =
        let trucks = 
            this.Trucks 
            |> List.filter (fun truck -> truck.position.X > -50.f && truck.position.X < 1000.f)
            |> List.map (fun truck -> truck.Update dt)

        let newTrucks = this.Factorys |> List.map (fun fact -> fact.GetTruck())
        let factorys = newTrucks |> List.map fst
        let trucks' = (newTrucks |> List.map snd |> List.choose id) @ trucks
        {
            this with
                Trucks = trucks'

                Factorys = factorys
                |> List.map (fun fact -> fact.Update dt)
        }

    member this.Draw (spriteBatch:SpriteBatch) =
        spriteBatch.Draw(this.Background, Vector2.Zero, Color.White);
        this.Trucks |> List.iter this.TruckDrawer
        this.Factorys |> List.iter this.FactoryDrawer
        ()
