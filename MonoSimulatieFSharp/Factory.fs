module Factory
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

type RawFactory = 
    {   
        Position: Vector2
        production: int
    }

type Factory =
    | Mine of RawFactory
    | Ikea of RawFactory
