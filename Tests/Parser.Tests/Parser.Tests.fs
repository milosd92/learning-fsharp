module StockChartBot.Parser.Tests

open System
open NUnit.Framework

type ``This is a test``() =
    [<Test>]
    member this.``my first test works``() =
        let expected = true
        let actual = true
        Assert.AreEqual(expected, actual)