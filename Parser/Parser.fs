module StockChartBot.Parser

open System
open System.Globalization

type Outcome<'S> =
| Success of result:'S
| Failure of message:string

type Query =
    {
        Sender : string
        Ticker : string
        From : DateTime
        To : DateTime
    }

let private parseDate (s : string) =
    DateTime.ParseExact(
        s,
        "M/d/yyyy", 
        CultureInfo.InvariantCulture, 
        DateTimeStyles.AssumeLocal
    )

let Parse (text : string) =
    let elements = text.Split([|' '|])
    match elements with
    | [|sender; ticker; yearStr|] ->
        try
            let year = Int32.Parse yearStr
            let from = DateTime(year, 1, 1)
            let until = DateTime(year, 12, 31)
            {
                Sender = sender
                Ticker = ticker
                From = from
                To = until
            } |> Outcome.Success
        with
        | e -> Outcome.Failure "Invalid text" // todo - clarify
    | [|sender; ticker; from; until|] ->
        try
        {
            Sender = sender
            Ticker = ticker
            From = from |> parseDate
            To = until |> parseDate
        } |> Outcome.Success
        with
        | e -> Outcome.Failure "Invalid text" // todo - clarify
    | _ -> Outcome.Failure "Invalid text"