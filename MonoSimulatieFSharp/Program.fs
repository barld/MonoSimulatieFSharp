open game

[<EntryPoint>]
let main argv =     
    use g = new SimulationGame()
    g.Run()

    0 // return an integer exit code