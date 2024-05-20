using System;

public class Program
{
    static void Main(String[] args)
    {
        SudokuType selectedType = GetSudokuTypeFromUser();

        GameController gameController = new GameController();
        gameController.loadRenderer(selectedType);
        gameController.loadBoard(selectedType); 
        gameController.displayBoard(selectedType);
    }

    static SudokuType GetSudokuTypeFromUser()
    {
        Console.WriteLine("Choose the type of Sudoku:");
        Console.WriteLine("1. Four by Four");
        Console.WriteLine("2. Six by Six");
        Console.WriteLine("3. Nine by Nine");
        Console.WriteLine("4. Samurai");
        Console.WriteLine("5. Jigsaw");
        Console.Write("Enter the number corresponding to the desired type: ");

        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5)
        {
            Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
            Console.Write("Enter the number corresponding to the desired type: ");
        }

        return (SudokuType)(choice - 1);
    }
}