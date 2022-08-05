namespace ListScrubber.Presentation.WPF

open System

open Xamarin.Forms
open Xamarin.Forms.Platform.WPF

open ListScrubber.Presentation

type MainWindow() = 
    inherit FormsApplicationPage()

module Main = 
    [<EntryPoint>]
    [<STAThread>]
    let main(_args) =

        let app = new System.Windows.Application()
        Forms.Init()
        let window = MainWindow() 
        window.LoadApplication(new App())

        app.Run(window)
