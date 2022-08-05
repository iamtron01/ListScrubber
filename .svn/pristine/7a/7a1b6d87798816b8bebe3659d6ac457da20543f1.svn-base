namespace ListScrubber.Presentation

open ListScrubber.Common

module File =

    open Plugin.FilePicker

    type Path = Path of string

    let openPath =
        async {
            return! 
                CrossFilePicker.Current.PickFile() 
                    |> Async.AwaitTask 
                    |> Async.map(fun x -> 
                        match x with 
                        | null -> None 
                        | x -> Some (Path x.FilePath))
            }