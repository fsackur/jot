module Jot.Domain
open System


type Markdown = string

type Category = {
    Id: int
    Name: string
}

type Tag = {
    Id: int
    Name: string
    Parents: Tag seq
}

type Jot =
    | Note of Note
    | Todo of Todo

and Note = {
    Id: int
    Summary: Markdown
    Detail: Markdown
    Tags: Tag seq
    Links: Jot seq
}

and Todo = {
    Id: int
    Summary: Markdown
    Detail: Markdown
    Category: Category
    Tags: Tag seq
    Links: Jot seq
    Importance: int
    Urgency: int
    CalendarItems: CalItem seq
}

and CalItem = {
    Id: int
    Item: Todo
    Due: DateTime
    Reminders: TimeSpan seq
    Recurrence: TimeSpan seq
}
