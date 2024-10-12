#if !LIGHT_EXPRESSION

using System;
using System.Linq.Expressions;
using NUnit.Framework;

namespace FastExpressionCompiler.IssueTests;

[TestFixture]
public class Issue421_Date_difference_is_giving_wrong_negative_value : ITest
{
    public int Run()
    {
        // Original_case();
        return 1;
    }

    public class Contract
    {
        public readonly DateTime StartDate = new DateTime(2024, 1, 1);
    }

    [Test]
    public void Original_case()
    {
        var contract = new Contract();
        Expression<Func<double>> e = () => (DateTime.Now - contract.StartDate).TotalDays;

        e.PrintCSharp(_ => "contract");

        var fs = e.CompileSys();
        fs.PrintIL();

        var ff = e.CompileFast(true);
        ff.PrintIL();

        Assert.GreaterOrEqual(fs(), 250);
        Assert.GreaterOrEqual(ff(), 250);
    }
}

#endif
