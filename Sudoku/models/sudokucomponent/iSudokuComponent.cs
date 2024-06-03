﻿using Sudoku.models.visitors;

namespace Sudoku.models.SudokuComponent
{
	public interface iSudokuComponent
    {
		void Accept(iBoardVisitor visitor, int boardIndex, SudokuGroup board);

		void Accept(iBoardVisitor visitor, bool isCorrect, SudokuGroup board, int boardIndex, int celIndex, SudokuGroup fullBoard);

		public int Value //TODO
		{
			get { return 0; }
			set { }
		}

		public int? Block //TODO
		{
			get { return 0; }
			set { }
		}

		public bool IsFixed //TODO
		{
			get { return true; }
			set { }
		}

		public bool IsCorrect //TODO
		{
			get { return true; }
			set { }
		}

		public SudokuType Type //TODO
		{
			get { return 0; }
			set { }
		}

		public List<iSudokuComponent> Components
		{
			get { return new List<iSudokuComponent>(); }
		}
	}
}
