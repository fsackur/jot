namespace SupposedlySentient.Jot.Api

open System
open System
open System

type Markdown = string

module Tag =
    type Tag = {
        Id: int
        Label: string
        Links: Link seq
    }

    and Workstream = {
        Id: int
        Label: string
        Links: Link seq
    } with
        static member Default = {
            Id = 0
            Label = "(unfiled)"
            Links = []
        }

    and Link = {
        Tag: Tag
        Direction: Direction
    }

    and Direction =
        | Parent
        | Child
        | Peer

type Tag = Tag.Tag
type Workstream = Tag.Workstream

type Jot =
    | Bookmark of Bookmark
    | Note of Note
    | Todo of Todo

and Bookmark = {
    Id: int
    Url: Uri
    Detail: Markdown
    Tags: Tag seq
    Links: Jot seq
}

and Note = {
    Id: int
    Precis: Markdown
    Detail: Markdown
    Tags: Tag seq
    Links: Jot seq
}

and Todo = {
    Id: int
    Precis: Markdown
    Detail: Markdown
    Workstream: Workstream
    Tags: Tag seq
    Links: Jot seq
    Importance: int
    Urgency: int
    CalendarItems: CalItem seq
}

and CalItem = {
    Id: int
    Item: Todo
    Time: DateTime
    Reminders: TimeSpan seq
    Recurrence: TimeSpan seq
}

module Jot =
    type NewJot = {
        Precis: Markdown
        Detail: Markdown
        Tags: Tag seq
        Links: Jot seq
        Workstream: Workstream option
        Importance: int option
        Urgency: int option
        CalendarItems: CalItem seq option
    }

    let create (input: NewJot) : Jot =
        let mutable url = null
        if
            input.Workstream.IsSome || input.Importance.IsSome || input.Urgency.IsSome || input.CalendarItems.IsSome
        then
            {
                Id = 0
                Precis = input.Precis
                Detail = input.Detail
                Workstream = defaultArg input.Workstream Workstream.Default
                Tags = input.Tags
                Links = input.Links
                Importance = defaultArg input.Importance Unchecked.defaultof<_>
                Urgency = defaultArg input.Urgency Unchecked.defaultof<_>
                CalendarItems = defaultArg input.CalendarItems []
            }
            |> Jot.Todo
        elif
            Uri.TryCreate(input.Precis.ToString(), UriKind.Absolute, ref url)
        then
            {
                Id = 0
                Url = url
                Detail = input.Detail
                Tags = input.Tags
                Links = input.Links
            }
            |> Jot.Bookmark
        else
            {
                Id = 0
                Precis = input.Precis
                Detail = input.Detail
                Tags = input.Tags
                Links = input.Links
            }
            |> Jot.Note


type JotApi = {
    newJot: Jot.NewJot -> Async<Jot>
}
