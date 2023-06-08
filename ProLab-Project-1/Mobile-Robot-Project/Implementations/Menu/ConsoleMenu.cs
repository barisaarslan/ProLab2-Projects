using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GezginRobotProjesi.Abstractions;
using GezginRobotProjesi.Entity;
using Microsoft.Extensions.DependencyInjection;

namespace GezginRobotProjesi.Implementations.Menu
{
    public class ConsoleMenu : GameMenu
    {
        private bool shouldRedraw {get; set;}
        private bool shouldClearConsole {get; set;}
        private string errorMessage {get; set;}

        public ConsoleMenu() : base(){
            shouldRedraw = true;
            shouldClearConsole = true;
            errorMessage = string.Empty;
        }

        public override void Draw()
        {
            shouldRedraw = true;
            while(shouldRedraw){
                if(shouldClearConsole){
                    Console.ResetColor();
                    Console.Clear();
                }
                if(!string.IsNullOrWhiteSpace(errorMessage)){
                    Console.WriteLine(errorMessage);
                    Console.Write("\n");
                }
                SetTakenAction(-1);
                shouldClearConsole = true;
                Console.WriteLine("Gezgin Robot Projesi");
                Console.WriteLine("Aydın Can Altun (180202117) - Barış Arslan(180202112)");
                Console.WriteLine(string.Format("1) Problem 1 ({0})", Constant.MapUrls[currentUrlId]));
                Console.WriteLine("2) Problem 2");
                Console.WriteLine("3) Problem 1 Link Değiştir");
                Console.Write("Alınmak İstenen Aksiyon :");
                TakeAction();
                Console.Write("\n");
            }
            
        }

        protected override void TakeAction(){
            try{
                ConsoleKeyInfo input = Console.ReadKey();
                int action;
                if(char.IsDigit(input.KeyChar) && int.TryParse(input.KeyChar.ToString(), out action)){
                    SetTakenAction(action);
                    shouldRedraw = false;
                }else{
                    Console.WriteLine("Hatalı giriş yaptınız! Lütfen tekrar deneyiniz!");
                    shouldClearConsole = false;
                } 
            }catch(Exception ex){
                Console.WriteLine(string.Format("Hatalı giriş yaptınız! Lütfen tekrar deneyiniz!"));
                shouldClearConsole = false;
            }
        }

        public override Response<MapSize> AskMapSize()
        {
            Response<MapSize> mapSize = new Response<MapSize>();
            string heightInput = string.Empty;
            string widthInput = string.Empty;
            int height = 0;
            int width = 0;
            Console.Write("Uzunluk: ");
            heightInput = Console.ReadLine();
            Console.Write("Genişlik: ");
            widthInput = Console.ReadLine();
            mapSize.ErrorMessage = string.Empty;
            if(string.IsNullOrWhiteSpace(heightInput) || string.IsNullOrWhiteSpace(widthInput)){
                mapSize.IsSuccess = false;
                mapSize.ErrorMessage = "Girilen uzunluk ve genişlik değerleri boş olamaz!";
            }
            if(!int.TryParse(heightInput, out height)){
                mapSize.IsSuccess = false;
                mapSize.ErrorMessage = "Girilen uzunluk bir tam sayı olmalıdır";
            }
            if(!int.TryParse(widthInput, out width)){
                mapSize.IsSuccess = false;
                mapSize.ErrorMessage = "Girilen genişlik bir tam sayı olmalıdır!";
            }
            if(width <= 0 || height <= 0){
                mapSize.IsSuccess = false;
                mapSize.ErrorMessage = "Girilen uzunluk ve genişlik sıfırdan büyük bir tam sayı olmalıdır!";
            }
            mapSize.IsSuccess = string.IsNullOrWhiteSpace(mapSize.ErrorMessage);
            mapSize.Result = new MapSize(height, width);
            return mapSize;
        }

        public override void ShowError(string errorMessage)
        {
            this.errorMessage = errorMessage;
        }
    }
}