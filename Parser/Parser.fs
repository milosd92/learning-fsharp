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
    | [|sender; ticker; from; until|] ->
        {
            Sender = sender
            Ticker = ticker
            From = from |> parseDate
            To = until |> parseDate
        } |> Outcome.Success
    | _ -> Outcome.Failure "Invalid text"