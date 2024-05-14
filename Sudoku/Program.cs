using System;

public class Program
{
    static void Main(String[] args)
    {
        GameController gameController = new GameController();
        gameController.loadBoard(SudokuType.FOUR_BY_FOUR); 
        gameController.displayBoard();
    }
}