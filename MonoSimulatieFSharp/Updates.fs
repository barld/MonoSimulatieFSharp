module Updates
open GameStatus
open Factory
open UnitOfMeasures
open Truck
open Factory

let RemoveTrucksOutOfScreen () =
    fun (s:GameStatus) ->
        let rec filterTruck (trucks: Truck.Truck list) =
            match trucks with
            | [] -> []
            | h::t -> 
                match h.Position.X > -50.f && h.Position.X < 1000.f with
                | true -> h:: filterTruck t
                | _ -> filterTruck t
        (), {s with Trucks = filterTruck s.Trucks }

let GetNewTrucks () =
    fun (s:GameStatus) ->
        let candidates, rest = 
            s.Factorys |> 
            List.partition (fun fact -> 
                        match fact with 
                        | Ikea rf -> rf.inStock > 80000.f<_> 
                        | Mine rf -> rf.inStock > 90000.f<_>)

        let newTrucks = candidates |> List.map(fun fact -> 
                        match fact with 
                        | Ikea rf -> baseTruckIkea
                        | Mine rf -> baseTruckMine)

        let updatedCandidates = candidates |> List.map (fun fact -> 
                        match fact with 
                        | Ikea rf -> Ikea {rf with inStock = rf.inStock - 60000.f<_> }
                        | Mine rf -> Mine {rf with inStock = rf.inStock - 60000.f<_> })

        (),{s with Factorys = updatedCandidates@rest; Trucks = newTrucks@s.Trucks}

let DriveTrucks dt = 
    fun (s:GameStatus) ->
        (),{s with Trucks = s.Trucks |> List.map (Truck.Update dt)}

let FactorysProduce dt time =
    fun (s:GameStatus) ->
        (),{s with Factorys = s.Factorys |> List.map (Factory.Update dt time)}

let UpdateTime dt = 
    fun (s:GameStatus) -> 
        let time' = (s.Time + dt) % 12.f<sec>
        time', {s with Time = time'}