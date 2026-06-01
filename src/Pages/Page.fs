module Jot.Pages.Page

open Feliz
open ElmishLand
open Jot.Domain
open Jot.Shared
open Jot.Pages

type Model = Jot

type Msg =
    | LayoutMsg of Layout.Msg
    | Create of Jot

let init () =
    Unchecked.defaultof<_>,
    Command.none

let update (msg: Msg) (model: Model) =
    match msg with
    | LayoutMsg _ -> model, Command.none
    | Create jot -> jot, Command.none

let view (_model: Model) (_dispatch: Msg -> unit) =
    Html.form [
        prop.id "createJot"
        prop.children [
            Html.label [
                prop.children [
                    Html.text "Summary"
                    Html.input [
                        prop.name "Summary"
                    ]
                ]
            ]
        ]
    ]

let page (_shared: SharedModel) (_route: HomeRoute) =
    Page.from init update view () LayoutMsg
