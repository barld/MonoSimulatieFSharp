module DayParts

open UnitOfMeasures

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