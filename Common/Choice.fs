module StockChartBot.Rop

[<RequireQualifiedAccess>]
module Choice =
    // Success or failure
    type Result<'S, 'F> =
        | Success of 'S
        | Failure of 'F

    // Convert a single value into a success two-track result
    let succeed x =
        Success x

    // Convert a single value into a failure two-track result	
    let fail x =
        Failure x	