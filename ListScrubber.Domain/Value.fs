namespace ListScrubber.Domain

open ListScrubber.Common

[<AutoOpen>]
module Column =

    type Column = Index of int

    let value (Index value) = value
     
[<AutoOpen>]
module Name =
    
    type Name = Name of string

    let value (Name value) = value

    let append name s =
        let n = value name
        Name (n + space + s)