module StateMonad

type State<'a,'s> = 's -> 'a * 's

type StateBuilder() =
    member this.Bind(p, k) =
        fun s ->
            let result, state = p s
            k result state

    member this.Return x : State<'a,'s> =
        fun s ->
            x, s

let stateFlow = new StateBuilder()

type sampleState = {x:int;y:int}

let startState = {x=0;y=0}

let updateX v =
    fun (s:sampleState) ->
        v, {s with x=v}

let updateY v =
    fun (s:sampleState) ->
        v, {s with y=v}

let flow = 
    stateFlow 
        {
            let! step1 = updateX 5
            let! step2 = updateX (step1 + 10)
            let! step3 = updateY 25
            return step3
        }


