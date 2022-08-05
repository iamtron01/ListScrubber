namespace ListScrubber.Tests.Unit.Domain

open Xunit

open ListScrubber.Common
open ListScrubber.Domain
open ListScrubber.Domain.Scubber

module ScrubberTests =
        
    [<Fact>]
    let ``scrub Ok``() =
        let left =
            (["Left",1,"Name,Phone\nJohn,(203) 222-7801\n" + 
                "Fred,(203) 225-6102\nSteve,(203) 222-7803"])
            |> File.parseMany Csv
        let right =
            (["Right",1,"Name,Phone\nJohn,(203) 222-7801"])
            |> File.parseMany Csv 
  
        let expected = 
            Ok 2
        let actual = 
            scrub Phone left right 
            |> Result.map(fun result -> 
                result
                |> Seq.choose(isCsv)
                |> Seq.bind(fun csv -> csv.File.Rows) 
                |> Seq.length)

        Assert.Equal(expected, actual)

    [<Fact>]
    let ``scrub Error``() =
        let left =
            (["Left",1,"Name,Phone\nJohn,(203) 222-7801"]) 
            |> File.parseMany Csv        
        let right =
            (["Right",1,"Name,Phone\nJohn,(203) 222-780A\n" + 
                "Fred,(203) 225-610B\nSteve,(203) 222-7803"])
            |> File.parseMany Csv 

        let expected = 
            Error "Left List: Right List:(203) 222-780A, (203) 225-610B"
        let actual = 
            scrub Phone left right

        Assert.Equal(expected, actual)