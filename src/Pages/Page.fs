module Jot.Pages.Page

open Fable.Form
open Fable.Form.Simple
open Fable.Form.Simple
open Fable.Form.Simple.Fields.Html
open Fable.Form.Simple.Bulma
// open Fable.Form.Simple.Bulma.Form
module Form = Fable.Form.Simple.Bulma.Form

open Feliz
open ElmishLand
open Jot.Domain
open Jot.Shared
open Jot.Pages

type NewJotForm = {
    Summary: Markdown
    Detail: Markdown
    Category: Category option
    Tags: Tag seq
    Links: Jot seq
    Importance: int option
    Urgency: int option
    CalendarItems: CalItem seq
}

type Model = NewJotForm

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
    let form : Form<_, _> =
        let emailField =
            Form.textField
                {
                    Parser =
                        fun value ->
                            if value.Contains("@") then
                                Ok value
                            else
                                Error "The e-mail address must contain a '@' symbol"
                    Value =
                        fun values -> values.Email
                    Update =
                        fun newValue values ->
                            { values with Email = newValue }
                    Error =
                        fun _ -> None
                    Attributes =
                        {
                            Label = "Email"
                            Placeholder = "some@email.com"
                            HtmlAttributes = [ ]
                        }
                }

        let passwordField =
            Form.passwordField
                {
                    // ...
                }

        let onSubmit =
            fun email password ->
                LogIn (email, password)

        Form.succeed onSubmit
            |> Form.append emailField
            |> Form.append passwordField

let page (_shared: SharedModel) (_route: HomeRoute) =
    Page.from init update view () LayoutMsg
