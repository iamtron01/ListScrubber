namespace ListScrubber.Presentation

open System.Diagnostics
open Xamarin.Forms
open Fabulous
open Fabulous.XamarinForms

module App = 
    type Model = 
      { Value : string }

    type Msg = 
        | FileOpen
        | FileClosed of File.Path
        | Nothing

    let initModel = { Value = "" }

    let init () = initModel, Cmd.none

    let openPath =
        async {
            match! File.openPath with
            | Some x -> 
                return Some (FileClosed x)
            | None -> 
                return None
        }

    let update msg model =
        match msg with
        | FileOpen -> 
            model, Cmd.ofAsyncMsgOption openPath
        | FileClosed x -> 
            Debug.WriteLine x
            model, Cmd.none
        | Nothing -> 
            model, Cmd.none

    let view (model: Model) dispatch =

        let mkButton text command row column =
            View.Button(text = text, command=(fun () -> dispatch command))
                .Row(row)
                .Column(column)
                .FontSize(FontSize 36.0)

        View.ContentPage(
            content = View.Grid(
                rowdefs=[Star; Star; Star;],
                coldefs=[Star; Star; Star; Star; Star; Star; Star; Star],
                children= [
                    mkButton "New" FileOpen 0 0;
                    mkButton "Edit" Nothing 0 1;
                    mkButton "Delete" Nothing 0 2;
                    mkButton "Scrub" Nothing 0 3;
                    mkButton "List Type" Nothing 0 4;
                    mkButton "Exit" Nothing 0 5;
                    mkButton "Help" Nothing 0 6;
                    mkButton "License" Nothing 0 7
                    View.TableView(
                        root = View.TableRoot(
                            items = [
                                View.TableSection(
                                    title = "Right List",
                                    items = [
                                        View.SwitchCell(on = true, text = "Right List 1") 
                                        View.SwitchCell(on = true, text = "Right List 2")
                                        View.SwitchCell(on = true, text = "Right List 3") 
                                        View.SwitchCell(on = true, text = "Right List 4") 
                                        View.SwitchCell(on = true, text = "Right List 5") 
                                        View.SwitchCell(on = true, text = "Right List 6") 
                                    ])
                                ])
                            )
                            .Row(1)
                            .ColumnSpan(8)
                    View.TableView(
                        root = View.TableRoot(
                            items = [
                                View.TableSection(
                                    title = "Left List",
                                    items = [
                                        View.SwitchCell(on = true, text = "Left List 1") 
                                        View.SwitchCell(on = true, text = "Left List 2")
                                        View.SwitchCell(on = true, text = "Left List 3") 
                                        View.SwitchCell(on = true, text = "Left List 4") 
                                        View.SwitchCell(on = true, text = "Left List 5") 
                                        View.SwitchCell(on = true, text = "Left List 6") 
                                    ])
                                ])
                            )
                            .Row(2)
                            .ColumnSpan(8)
                ],
                minimumWidth = 640.,
                minimumHeight = 480.
            ))

    let program = Program.mkProgram init update view

type App () as app = 
    inherit Application ()

    let runner = 
        App.program
#if DEBUG
        |> Program.withConsoleTrace
#endif
        |> XamarinFormsProgram.run app

#if DEBUG
    // Uncomment this line to enable live update in debug mode. 
    // See https://fsprojects.github.io/Fabulous/Fabulous.XamarinForms/tools.html#live-update for further  instructions.
    //
    //do runner.EnableLiveUpdate()
#endif    

    // Uncomment this code to save the application state to app.Properties using Newtonsoft.Json
    // See https://fsprojects.github.io/Fabulous/Fabulous.XamarinForms/models.html#saving-application-state for further  instructions.
#if APPSAVE
    let modelId = "model"
    override __.OnSleep() = 

        let json = Newtonsoft.Json.JsonConvert.SerializeObject(runner.CurrentModel)
        Console.WriteLine("OnSleep: saving model into app.Properties, json = {0}", json)

        app.Properties.[modelId] <- json

    override __.OnResume() = 
        Console.WriteLine "OnResume: checking for model in app.Properties"
        try 
            match app.Properties.TryGetValue modelId with
            | true, (:? string as json) -> 

                Console.WriteLine("OnResume: restoring model from app.Properties, json = {0}", json)
                let model = Newtonsoft.Json.JsonConvert.DeserializeObject<App.Model>(json)

                Console.WriteLine("OnResume: restoring model from app.Properties, model = {0}", (sprintf "%0A" model))
                runner.SetCurrentModel (model, Cmd.none)

            | _ -> ()
        with ex -> 
            App.program.onError("Error while restoring model found in app.Properties", ex)

    override this.OnStart() = 
        Console.WriteLine "OnStart: using same logic as OnResume()"
        this.OnResume()
#endif