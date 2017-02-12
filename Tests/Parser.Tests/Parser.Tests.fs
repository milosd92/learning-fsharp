module StockChartBot.Parser.Tests

open System
open NUnit.Framework

open StockChartBot.Parser

[<TestFixture("@StockChartBot LNKD 1/1/2000 12/31/2015", "@StockChartBot", "LNKD", 1, 1, 2000, 12, 31, 2015)>]
[<TestFixture("@AnotherBot LNKD 1/2/2000 5/31/2015", "@AnotherBot", "LNKD", 1, 2, 2000, 5, 31, 2015)>]
[<TestFixture("@AnotherBot MSFT 1/5/2001 6/30/2015", "@AnotherBot", "MSFT", 1, 5, 2001, 6, 30, 2015)>]
type ``Given some valid text``(text, sender, ticker, fm, fd, fy, tm, td, ty) =
    [<Test>]
    member this.``the parser parses the text into sender, ticker, from and to``() =
        let expected = 
            {
                Sender = sender
                Ticker = ticker
                From = DateTime(fy, fm, fd)
                To = DateTime(ty, tm, td)
            }
        let actual = Parse text
        Assert.AreEqual(expected, actual)
