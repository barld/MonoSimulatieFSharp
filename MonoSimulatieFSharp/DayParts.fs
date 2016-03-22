module DayParts

open UnitOfMeasures
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework

type DayPart =
    | Night
    | Morning
    | AfterNoon
    | Evening

let getDayPart (time:float32<sec>) =
    match time with
    | n when n < 3.f<sec> -> Night
    | n when n < 6.f<sec> -> Morning
    | n when n < 9.f<sec> -> AfterNoon
    | _ -> Evening

let updateDayPart time (dt:float32<sec>) =
    (time + dt) % 12.f<sec>

let drawSun (time:float32<sec>) (spriteBatch:SpriteBatch) (sunTexture:Texture2D) =
    let y = 5.f*((time |> float32)-6.f)**2.f
    let position = new Vector2(((time - 3.f<sec>) |> float32) * 134.f, y)
    
    spriteBatch.Draw(sunTexture, position, System.Nullable(), Color.White, 0.f, Vector2.Zero, 0.1f, SpriteEffects.None, 0.f)