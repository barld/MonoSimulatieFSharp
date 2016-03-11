module Factory
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Truck
open UnitOfMeasures

type RawFactory<[<Measure>]'t> = 
    {   
        Position: Vector2
        Production: float32<'t/sec>
        inStock: float32<'t>
    }

type Factory =
    | Mine of RawFactory<ore>
    | Ikea of RawFactory<product>

    member this.Update dt =
        match this with
        | Mine rf -> Mine {rf with inStock = rf.inStock + rf.Production*dt }
        | Ikea rf -> Ikea {rf with inStock = rf.inStock + rf.Production*dt }
        
    member this.GetTruck () =
        match this with 
        | Mine rf -> 
            if rf.inStock > 80000.f<ore> then
                Mine {rf with inStock = rf.inStock - 60000.f<ore> } ,
                Some {position = (Factory.getGetTruckLoadPosition this); velocity = (Factory.getTruckVelocity this); container = Some OreContainer}
            else
                Mine rf, None
        | Ikea rf -> 
            if rf.inStock > 90000.f<product> then
                Ikea {rf with inStock = rf.inStock - 60000.f<product> } ,
                Some {position = (Factory.getGetTruckLoadPosition this); velocity = (Factory.getTruckVelocity this); container = Some ProductContainer}
            else
                Ikea rf, None

    static member getGetTruckLoadPosition =
        function
        | Mine _ -> Vector2(210.f,70.f)
        | Ikea _ -> Vector2(450.f,320.f)

    static member getTruckVelocity =
        function
        | Mine _ -> Vector2(80.f, 0.f)
        | Ikea _ -> Vector2(-60.f, 0.f)


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




let baseMine =
    Mine 
        {
            Position=new Vector2(60.f, 40.f)
            Production = 15000.f<ore/sec>
            inStock = 0.f<ore>
        }

let baseIkea = 
    Ikea 
        {
            Position=new Vector2(600.f, 320.f)
            Production = 20000.f<product/sec>
            inStock = 0.f<product>
        }
