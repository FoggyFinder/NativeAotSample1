namespace ClassLibrary1

open System
open System.Collections.Generic

type SomeDU =
    | A
    | B of IReadOnlyCollection<SomeDU>
    override t.ToString() =
        match t with
        | A -> "A"
        | B l -> "B" + "(" + (l |> Seq.map (fun v -> v.ToString()) |> String.concat ", ") + ")"

type Foo(key:string, value:string) =
    member _.Key = key
    member _.Value = value

type Bar(guid:Guid, xs:Foo list) =
    member _.Guid = guid
    member _.Xs = xs