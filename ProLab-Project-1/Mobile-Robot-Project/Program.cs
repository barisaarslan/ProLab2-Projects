using GezginRobotProjesi;
using GezginRobotProjesi.Abstractions;
using GezginRobotProjesi.Implementations.Factory;
using GezginRobotProjesi.Implementations.Menu;
using GezginRobotProjesi.Interfaces;
using Microsoft.Extensions.DependencyInjection;

var builder = new ServiceCollection()
    .AddSingleton<IPlayerRobotFactory, PlayerRobotFactory>()
    .AddSingleton<IGameMapFactory, GameMapFactory>()
    .AddSingleton<GameMenu, ConsoleMenu>()
    .BuildServiceProvider();

Application game = new Application(builder);

game.GameLoop().Wait();




