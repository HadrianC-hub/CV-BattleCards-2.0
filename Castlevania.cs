using System.IO;
using System;
namespace BattleCards
{
    class Program
    {
        static void Main(string[] args)
        {
            //Extraer contenido de cartas del juego base
            string code = new StreamReader("edit\\Cards.txt").ReadToEnd();
            //Analizar contenido de cartas del juego base
            (Card[] GameCards, List<string> GameErrors) = GameLaunch.Get(code);
            //Extraer contenido de edición
            string Edit = new StreamReader("edit\\Editor.txt").ReadToEnd();
            (Card[] EditCards, List<string> EditErrors) = GameLaunch.Get(Edit);
            if(GameErrors.Count!=0)
            {
                Console.WriteLine("El archivo edit\\Cards.txt no es válido, no se puede iniciar el juego...");
                foreach(string x in GameErrors)
                {
                    Console.WriteLine(x);
                }
                Console.ReadLine();
                throw new Exception();
            }
            Card[] AllCards = new Card[]{};
            foreach(Card x in GameCards)
            {
                AllCards=EnumerableFunctions.AddCard(AllCards, x);
            }
            if(EditErrors.Count==0)
            {
                foreach(Card x in EditCards)
                {
                    AllCards=EnumerableFunctions.AddCard(AllCards, x);
                }
            }
            //Comienza el juego
            Screen.GameManager(AllCards,EditErrors);           
        }
    }
}
