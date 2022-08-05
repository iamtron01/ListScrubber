namespace ListScrubber.Infrastructure

open FSharp.Data

module CsvRepository =

    let read (path:string) =
        CsvFile.Load(path)

    let readMany (paths:string[]) =
        paths 
        |> Array.Parallel.map read

module FileRepository =
    let read (path:string) =
        ()

    let readMany (paths:string[]) =
        paths
        |> Array.Parallel.map read