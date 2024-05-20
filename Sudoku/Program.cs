using System;

public class Program
{
    static void Main(String[] args)
    {
        SudokuType selectedType = GetSudokuTypeFromUser();

        GameController gameController = new GameController();
        gameController.loadRenderer(selectedType);
        gameController.loadBoard(selectedType); 
        gameController.displayBoard();
    }

    static SudokuType GetSudokuTypeFromUser()
    {
        Console.WriteLine("Kies het type Sudoku:");
        Console.WriteLine("1. Vier bij vier");
        Console.WriteLine("2. Zes bij zes");
        Console.WriteLine("3. Negen bij negen");
        Console.WriteLine("4. Samurai");
        Console.WriteLine("5. Jigsaw");
        Console.Write("Voer het nummer in van het gewenste type: ");

        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5)
        {
            Console.WriteLine("Ongeldige invoer. Voer een nummer in tussen 1 en 5.");
            Console.Write("Voer het nummer in van het gewenste type: ");
        }

        return (SudokuType)(choice - 1);
    }
}