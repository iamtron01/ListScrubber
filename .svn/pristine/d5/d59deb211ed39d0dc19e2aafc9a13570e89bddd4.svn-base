namespace ListScrubber.Domain

open ListScrubber.Common
open ListScrubber.Domain

[<AutoOpen>]
module File =
    
    type Choice = 
        | Csv
        | Excel

    type File =
        | CsvFile of Csv
        | ExcelFile of Excel

        member x.CsvFileValue = 
            match x with 
            | CsvFile x -> x 
            | ExcelFile _ -> invalidOp "Not Csv"

        member x.ExcelFileValue = 
            match x with 
            | ExcelFile x -> x 
            | CsvFile _ -> invalidOp "Not Excel"

    let parse choice (name, index, text) =
        match choice with
        | Csv ->
            (name, index, text)
            |> (Csv.parse >> CsvFile)
        | Excel -> 
            (name, index, text)
            |> (Excel.parse >> ExcelFile)

    let parseMany choice input =
        input |> Seq.map(parse choice)

    let isCsv file =
        match file with
        | CsvFile x -> Some x
        | ExcelFile _ -> None

    let isExcel file =
        match file with
        | ExcelFile x -> Some x
        | CsvFile _ -> None

    let chooseCsv files =
        files |> Seq.choose(isCsv)

    let chooseExcel files =
        files |> Seq.choose(isExcel)

[<AutoOpen>]
module internal FileHelpers =

    open ListScrubber.Domain.CsvHelpers

    let bindToColumn files =
        files |> chooseCsv |> bindToColumn

    let (-) left right = 
        let left' =
            left 
            |> chooseCsv
        (left' - right)
        |> Seq.map(CsvFile)