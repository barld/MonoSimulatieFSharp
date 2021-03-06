﻿module GameStatus
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
    member this.Update dt =
        let rec filterTrucksInScreen (trucks: Truck list) : Truck list=
            match trucks with
            | [] -> []
            | truck::tail ->
                match truck.Position.X > -50.f && truck.Position.X < 1000.f with
                | true -> truck :: filterTrucksInScreen tail
                | false -> filterTrucksInScreen tail
        let trucks = 
            this.Trucks 
            |> filterTrucksInScreen
            |> List.map (fun truck -> truck.Update dt)

        let newTrucks = this.Factorys |> List.map (fun fact -> fact.GetTruck())
        let factorys = newTrucks |> List.map fst
        let trucks' = (newTrucks |> List.map snd |> List.choose id) @ trucks
        {
            this with
                Trucks = trucks'
                Factorys = factorys
                |> List.map (fun fact -> fact.Update dt this.Time)
                Time = DayParts.updateDayPart this.Time dt
        }

    member this.Draw (spriteBatch:SpriteBatch) =
        spriteBatch.Draw(this.Background, Vector2.Zero, Color.White);
        this.Trucks |> List.iter this.TruckDrawer
        this.Factorys |> List.iter this.FactoryDrawer
        DayParts.drawSun this.Time spriteBatch this.Sun
        ()