namespace ListScrubber.Domain

module internal Array =

    let private filter =
        Array.filter

    let forall2 predicate array1 array2 =
        let forall =
            filter(not << predicate)
        match (forall array1),(forall array2) with
        | [||],[||] -> Ok [||],[||]
        | x,y -> Error x,y