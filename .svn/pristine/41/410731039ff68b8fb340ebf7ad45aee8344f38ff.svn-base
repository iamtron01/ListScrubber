namespace ListScrubber.Domain

open ClosedXML.Excel
open ListScrubber.Domain

[<AutoOpen>]
module XLWorkbook =
    
    let parse (file:string) = 
        new XLWorkbook(file)

    let filter (predicate:FSharpFunc<_,bool>) (workbook:XLWorkbook) =
        workbook.FindRows(System.Func<_,bool>(predicate))
        //Return a new workbook with filtered rows?

[<AutoOpen>]
module Excel =

    type Excel = 
        { Name: Name
          Column: Column
          File : XLWorkbook }

    let parse (name, index, text) =
        { Name = Name name;
          Column = Index index;
          File = XLWorkbook.parse text} 

    let parseMany input =
        input |> Seq.map(parse)