module StockChartBot.Parser.Tests

open System
open NUnit.Framework

open StockChartBot.Parser
open StockChartBot.Rop

[<TestFixture("@StockChartBot LNKD 1/1/2000 12/31/2015", "@StockChartBot", "LNKD", 1, 1, 2000, 12, 31, 2015)>]
[<TestFixture("@AnotherBot LNKD 1/2/2000 5/31/2015", "@AnotherBot", "LNKD", 1, 2, 2000, 5, 31, 2015)>]
[<TestFixture("@AnotherBot MSFT 1/5/2001 6/30/2015", "@AnotherBot", "MSFT", 1, 5, 2001, 6, 30, 2015)>]
[<TestFixture("@AnotherBot LNKD 2015", "@AnotherBot", "LNKD", 1, 1, 2015, 12, 31, 2015)>]
type ``Given some valid text``(text, sender, ticker, fm, fd, fy, tm, td, ty) =
    [<Test>]
    member __.``the parser parses the text into sender, ticker, from and to``() =
        let expected : Choice.Result<Query, string> = 
            {
                Sender = sender
                Ticker = ticker
                From = DateTime(fy, fm, fd)
                To = DateTime(ty, tm, td)
            } |> Choice.succeed
        let actual = Parse text
        Assert.AreEqual(expected, actual)

[<TestFixture("@A AAPL 1/1/A015 12/31/2016")>]
[<TestFixture("@A AAPL 1/1/2015 12/32/2016")>]
[<TestFixture("AAPL 1/1/2015 12/31/2016")>]
type ``Given some text with an invalid date``(text) =
    [<Test>]
    member __.``the parse function returns a Choice.Failure for the date``() =
        let expected : Choice.Result<Query, string> = Choice.fail "Could not parse date."
        let actual = Parse text
        Assert.AreEqual(expected, actual)

[<TestFixture("")>]
[<TestFixture("@A AAPL 1/1/2015 12/31/2016 B")>]
type ``Given some invalid text``(text) =
    [<Test>]
    member __.``the parse function returns a Choice.Failure for the request``() =
        let expected : Choice.Result<Query, string> = Choice.fail "Could not parse request."
        let actual = Parse text
        Assert.AreEqual(expected, actual)
