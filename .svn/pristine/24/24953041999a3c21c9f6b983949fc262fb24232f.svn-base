open System
open System.Diagnostics

open ListScrubber.Common
open ListScrubber.Domain
open ListScrubber.Domain.Scubber
open ListScrubber.Infrastructure

[<EntryPoint>]
let main argv =

    let duration f = 
        let timer = new Stopwatch()
        timer.Start()
        let returnValue = f()
        printfn "Elapsed Time: %i" timer.ElapsedMilliseconds
        returnValue    

    let printScrubbed choice left right =
        let printScrubbed (rs:seq<File>) =
            rs 
            |> Seq.iter(fun r -> 
                match r with
                | CsvFile c -> 
                    (match c.File.Headers with
                    | Some _ -> (c.File.Rows |> Seq.length) - 1
                    | None ->  c.File.Rows |> Seq.length)
                    |> printfn "Scrubbed:%i"
                    c.File.SaveToString() 
                    |> printfn"Data:\n%s"
                | ExcelFile _ -> ())
        let printError =
            printfn "Error: %s"
        match scrub choice left right with
        | Ok rs -> printScrubbed rs
        | Error e -> printError e

    let getCsv name index csvFile =
        [CsvFile { Name = Name name;
           Column = Index index;
           File = CsvRepository.read csvFile}]

    let left =
        getCsv "Left" 0 "C:\Src\ListScrubber\data\y.csv"
    let right = 
        getCsv "Right" 0 "C:\Src\ListScrubber\data\x.csv"

    printfn "Scrub Start..."

    let printPhoneScrubbed() =
        printScrubbed Phone left right
    
    printPhoneScrubbed |> duration

    printfn "Scrub Stop..."
    printfn ""
    
    printfn "Press [Enter] to stop"
    Console.ReadKey() |> ignore

    Environment.ExitCode