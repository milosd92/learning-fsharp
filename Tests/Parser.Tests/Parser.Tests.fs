module StockChartBot.Parser.Tests

open System
open NUnit.Framework

open StockChartBot.Parser

type ``Given some valid text``() =
    [<Test>]
    member this.``the parser parses the text into sender, ticker, from and to``() =
        let text = "@StockChartBot LNKD 1/1/2000 12/31/2015"
        let expected = 
            {
                Sender = "@StockChartBot"
                Ticker = "LNKD"
                From = DateTime(2000, 1, 1)
                To = DateTime(2015, 12, 31)
            }
        let actual = Parse text
        Assert.AreEqual(expected, actual)