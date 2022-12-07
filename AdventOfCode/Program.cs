// In dotnet-watch mode keep running the last run


using System.Reflection;

var problems = Assembly.GetEntryAssembly().GetTypes().Where(type => typeof(BaseProblem).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract).OrderBy(x => x.Name).ToArray();

if (Environment.GetEnvironmentVariable("DOTNET_WATCH") != default)
{
    Solver.Solve(config =>
    {
        config.ShowConstructorElapsedTime = true;
        config.ShowTotalElapsedTimePerDay = true;
    }, problems.Last());
}
else
{
    switch (args.Length)
    {
        case 0:
            await Solver.Solve(opt => opt.ClearConsole = false, problems.Last());
            break;
        case 1 when args[0].Contains("all", StringComparison.CurrentCultureIgnoreCase):
            await Solver.Solve(opt =>
            {
                opt.ShowConstructorElapsedTime = true;
                opt.ShowTotalElapsedTimePerDay = true;
            }, problems);
            break;
        default:
        {
            var indexes = args.Select(arg => uint.TryParse(arg, out var index) ? index : uint.MaxValue);

            await Solver.Solve(indexes.Where(i => i < uint.MaxValue));
            break;
        }
    }
}