using GezginRobotProjesi.Abstractions;
using GezginRobotProjesi.Entity;
using GezginRobotProjesi.Entity.Enums;
using GezginRobotProjesi.Helpers;
using GezginRobotProjesi.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GezginRobotProjesi
{
    public class Application
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly GameMenu _menu;
        private bool isGameOver {get; set;}

        public Application(ServiceProvider serviceProvider){
            Console.TreatControlCAsInput = true;
            _serviceProvider = serviceProvider;
            _menu = _serviceProvider.GetRequiredService<GameMenu>();
        }

        public async Task GameLoop(){
            while (true) {
                _menu.Draw();
                if(_menu.GetTakenAction() == 0){
                    Console.WriteLine("Bay bay");
                    break;
                }
                if(_menu.GetTakenAction() == 1){
                    Response<GameMap> gameMap = await _menu.CreateMapFromUrl(_serviceProvider);
                    StartGame(gameMap, ProblemType.Problem1);
                }
                if(_menu.GetTakenAction() == 2){
                    Response<MapSize> mapSize = _menu.AskMapSize();
                    if(mapSize.IsSuccess){
                        Response<GameMap> gameMap = _menu.CreateLabyrinth(_serviceProvider, mapSize.Result.Height, mapSize.Result.Width);
                        StartGame(gameMap, ProblemType.Problem2);
                    }
                }

                if(_menu.GetTakenAction() == 3){
                    _menu.SwitchMapUrl();
                }
            }
        }

        public void StartGame(Response<GameMap> gameMap, ProblemType problemType){
            if(gameMap.IsSuccess){
                isGameOver = false;
                bool isFirstMoveAfterAction2 = false;
                PlayerRobot player = _serviceProvider.GetRequiredService<IPlayerRobotFactory>().CreateInstance(problemType, gameMap.Result.StartingPosition);
                gameMap.Result.Draw(player.VisitedCoordinates, player.CurrentPosition);
                while(!isGameOver){
                    while(player.GetAction() == -1){
                        player.WaitForAction();
                        player.ValidateAction();
                    }
                    int playerAction = player.GetAction();
                    if(playerAction == 1){
                        List<Coordinate> availableBlocks = gameMap.Result.GetAccesiblePaths(player.CurrentPosition);
                        gameMap.Result.UpdateBlocks(availableBlocks, player.VisitedCoordinates);
                        player.SetVisibleBlocks(availableBlocks);
                        gameMap.Result.UpdateBlock(player.CurrentPosition, false);
                        player.Move();
                        player.ResetAction();
                    }

                    if(playerAction == 2){
                        if(isFirstMoveAfterAction2){
                            Thread.Sleep(200);
                        }
                        isFirstMoveAfterAction2 = true;
                        List<Coordinate> availableBlocks = gameMap.Result.GetAccesiblePaths(player.CurrentPosition);
                        gameMap.Result.UpdateBlocks(availableBlocks, player.VisitedCoordinates);
                        player.SetVisibleBlocks(availableBlocks);
                        gameMap.Result.UpdateBlock(player.CurrentPosition, false);
                        player.Move();
                    }
                    gameMap.Result.UpdateBlock(player.CurrentPosition, true);
                    IsGameOver(gameMap.Result.EndingPosition, player.CurrentPosition);
                    if(playerAction == 3) {
                        player.ShowGameEndingMessage();
                        isGameOver = true;
                    }
                }
                if(player.GetAction() != 3)
                {
                    gameMap.Result.DrawShortestPath(player.VisitedCoordinates, player.ShortestPath());
                    Console.ReadKey();
                }
            }else{
                _menu.ShowError(gameMap.ErrorMessage);
            }
        }

        private bool IsGameOver(Coordinate mapEnd, Coordinate playerPosition){
            if(mapEnd.IsEqual(playerPosition)){
                isGameOver = true;
            }
            return isGameOver;
        }
    }
}