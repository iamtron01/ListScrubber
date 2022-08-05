namespace ListScrubber.Domain

open ListScrubber.Common.System

[<AutoOpen>]
module Choice =

    type Choice =
        | Phone
        | Email

    let internal choices =
        Map [Phone, "^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$"
             Email, "^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"]

[<AutoOpen>]
module private Error =

    let concat left right =
        "Left List:" + concat left + space +
        "Right List:" + concat right

    let (+) left right =
        concat left right

module Scubber =

    open ListScrubber.Domain.Array

    let scrub choice left right =
        let left' =
            bindToColumn left
        let right' =       
            bindToColumn right
        let isMatch = 
            isMatch (choices.Item choice)
        match forall2 isMatch left' right' with
        | Ok _,_ -> Ok (left - right')
        | Error left,right -> Error (left + right)