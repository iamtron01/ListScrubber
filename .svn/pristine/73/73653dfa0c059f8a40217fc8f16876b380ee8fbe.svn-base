namespace ListScrubber.Common

open System.Text.RegularExpressions
open System

[<AutoOpen>]
module System =

    let isNullOrEmpty string1 string2 =
        (String.IsNullOrEmpty(string1)) &&
        (String.IsNullOrEmpty(string2))
    
    let isMatch pattern input =
        not (isNullOrEmpty pattern input) &&
        Regex.IsMatch(input, pattern)
    
    let newLine =
        System.Environment.NewLine
    
    let concat seq =
        String.concat ", " seq

    let space =
        " "
[<AutoOpen>]
module Array =

    let mapP mapping array =
        Array.Parallel.map mapping array

    let bind binder array = 
        Array.map binder array 
        |> Array.concat

    let binaryContains value array =
        Array.BinarySearch(array, value) > -1

[<AutoOpen>]
module Seq =
    
    let bind binder seq =
        Seq.map binder seq
        |> Seq.concat

[<AutoOpen>]
[<RequireQualifiedAccess>] 
module Async =

    let map f xA = 
        async { 
        let! x = xA
        return f x 
        }

    let retn x = 
        async.Return x

    let apply fA xA = 
        async { 

        let! fChild = Async.StartChild fA
        let! x = xA

        let! f = fChild
        return f x 
        }

    let bind f xA = 
        async.Bind(xA,f)

[<RequireQualifiedAccess>] 
module AsyncResult =

    let liftAsync x = async {
        let! x' = x
        return Ok x' 
        }

    let map f x =
        Async.map (Result.map f) x

    let bind f xAsyncResult = async {
        let! xResult = xAsyncResult 
        match xResult with
        | Ok x -> return! f x
        | Error err -> return (Error err)
        }

    let ignore x = 
            x |> map ignore    

    let catch f x =
        x
        |> Async.Catch
        |> Async.map(function
            | Choice1Of2 (Ok v) -> Ok v
            | Choice1Of2 (Error err) -> Error err
            | Choice2Of2 ex -> Error (f ex))