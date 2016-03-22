module Factory
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Truck
open UnitOfMeasures
open DayParts

let baseTruckMine =
    {
        Position = new Vector2(210.f,135.f)
        Acceleration = 5.f<pixel/sec^2> 
        Velocity = 5.f<pixel/sec>
        Direction = Direction.Right
        Container = Some OreContainer
    }

let baseTruckIkea =
    {
        Position = new Vector2(450.f,355.f)
        Acceleration = 8.f<pixel/sec^2> 
        Velocity = 6.f<pixel/sec>
        Direction = Direction.Left
        Container = Some ProductContainer
    }




type RawFactory<[<Measure>]'t> = 
    {   
        Position: Vector2
        GetProduction: DayPart -> float32<'t/sec>
        inStock: float32<'t>
    }

type Factory =
    | Mine of RawFactory<ore>
    | Ikea of RawFactory<product>

    member this.Update dt time =
        match this with
        | Mine rf -> 
            Mine {rf with inStock = rf.inStock + (rf.GetProduction (getDayPart time)) * dt }
        | Ikea rf -> 
            Ikea {rf with inStock = rf.inStock + (rf.GetProduction (getDayPart time)) * dt }
            
        
    member this.GetTruck () =
        match this with 
        | Mine rf -> 
            if rf.inStock > 80000.f<ore> then
                Mine {rf with inStock = rf.inStock - 60000.f<ore> } ,
                Some baseTruckMine
            else
                Mine rf, None
        | Ikea rf -> 
            if rf.inStock > 90000.f<product> then
                Ikea {rf with inStock = rf.inStock - 60000.f<product> } ,
                Some baseTruckIkea
            else
                Ikea rf, None



let getFactorDrawer (mine:Texture2D) (ikea:Texture2D) (mine_cart:Texture2D) (product_box:Texture2D) =
    let FatoryDrawer (spriteBatch:SpriteBatch) (factory:Factory) =
        let (texture, position) = 
            match factory with 
            | Mine rf -> mine, rf.Position 
            | Ikea rf -> ikea, rf.Position
        
        spriteBatch.Draw(texture, position, System.Nullable(), Color.White, 0.f, Vector2.Zero, 0.5f, SpriteEffects.None, 0.f)

        match factory with
        | Mine rf ->
            for i in 1.f..(rf.inStock / 10000.f<ore>) do                
                spriteBatch.Draw(mine_cart, Vector2(10.f,20.f*i), System.Nullable(), Color.White, 0.f, Vector2.Zero, 0.15f, SpriteEffects.None, 0.f)
        | Ikea rf ->
            for i in 1.f..(rf.inStock / 10000.f<product>) do
                spriteBatch.Draw(product_box, Vector2(750.f,400.f - 20.f*i), System.Nullable(), Color.White, 0.f, Vector2.Zero, 0.15f, SpriteEffects.None, 0.f)
        ()

    FatoryDrawer

let getProductionSpeed dayPart =
    match dayPart with
    | Night -> 1000.f<ore/sec>
    | Morning -> 8000.f<ore/sec>
    | AfterNoon -> 10000.f<ore/sec>
    | Evening -> 3000.f<ore/sec>

let baseMine =
    Mine 
        {
            Position=new Vector2(60.f, 40.f)
            GetProduction = getProductionSpeed
            inStock = 0.f<ore>
        }

let getProductionSpeedBaseMine dayPart =
    match dayPart with
    | Night -> 1000.f<product/sec>
    | Morning -> 8000.f<product/sec>
    | AfterNoon -> 2000.f<product/sec>
    | Evening -> 15000.f<product/sec>

let baseIkea = 
    Ikea 
        {
            Position=new Vector2(600.f, 320.f)
            GetProduction = getProductionSpeedBaseMine
            inStock = 0.f<product>
        }