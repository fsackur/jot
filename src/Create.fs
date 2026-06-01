module Jot.Create

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

module Summary =
    let tryParse
