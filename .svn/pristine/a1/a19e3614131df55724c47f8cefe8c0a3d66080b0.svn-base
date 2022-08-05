namespace ListScrubber.Domain

open FSharp.Data
open FSharp.Data.Runtime

open ListScrubber.Common

[<AutoOpen>]
module Csv =

    type Csv = 
        { Name: Name
          Column: Column
          File : CsvFile<CsvRow> }
    
    let parse (name, index, text) =
        { Name = Name name;
          Column = Index index;
          File = CsvFile.Parse(text) }

    let parseMany input =
        input |> Seq.map(parse)

module private CsvFile =
   
    let filter (predicate:FSharpFunc<_,bool>) (csvFile:CsvFile<_>) =
        csvFile.Filter(System.Func<_,bool>(predicate))

module internal CsvHelpers =

    let private (+) name s =
        append name s

    let getColumn (row:CsvRow) (column:Column) =
        let index = Column.value column
        row.GetColumn(index)

    let contains row column xs =
        xs |> Array.binaryContains (getColumn row column)

    let mapToColumn (column:Column) (csvFile:CsvFile<CsvRow>) =
        csvFile.Rows |> Array.ofSeq |> Array.mapP(fun row -> getColumn row column)

    let bindToColumn csvs =
        csvs 
        |> Array.ofSeq
        |> Array.bind(fun x -> 
            mapToColumn x.Column x.File)
        |> Array.sort

    let difference left right = 
        let filter file column =
            file
            |> CsvFile.filter(fun row -> 
            contains row column right |> not)
        left 
        |> Seq.map(fun csv -> 
        { csv with 
            Name = csv.Name + "-"; 
            File = filter csv.File csv.Column })

    let (-) left right =
        difference left right