using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private string currentTurn;
		private string[,] chessStates;

		public MainWindow()
		{
			InitializeComponent();

			currentTurn = "X";
			chessStates = new string[3,3];

			ClearChessBoard();
		}

		private void ClearChessBoard()
		{
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					chessStates[i, j] = "";
					Button theButton = uxGrid.Children.Cast<Button>().First(b => Grid.GetRow(b) == i && Grid.GetColumn(b) == j);
					theButton.Content = "";
					theButton.IsEnabled = true;
				}
			}
			uxTurn.Text = currentTurn + "'s turn";
		}

		private void uxExit_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			// get button from sender
			Button clickedButton = (sender as Button);
			
			// change the button content to 'X' or 'O'
			clickedButton.Content = currentTurn;

			// set the state in the corresponding array
			String positionStr = clickedButton.Tag.ToString();
			char[] splitters = { ',' };
			string[] parts = positionStr.Split(splitters);

			int row = Int32.Parse(parts[0]);
			int col = Int32.Parse(parts[1]);
			chessStates[row, col] = currentTurn;

			//bool isDeclared = false;
			string winner = "";

			// check whether to declare winner

			// check each row
			for (int i = 0; i < 3; i++)
			{
				string firstElement = chessStates[i, 0];
				bool isIdentical = true;
				for (int j = 1; j < 3; j++)
				{
					if (firstElement != chessStates[i, j])
					{
						isIdentical = false;
						break;
					}
				}

				if (isIdentical && firstElement != "")
				{
					winner = firstElement;
				}
				
			}

			// check each column
			for (int i = 0; i < 3; i++)
			{
				string firstElement = chessStates[0, i];
				bool isIdentical = true;
				for (int j = 1; j < 3; j++)
				{
					if (firstElement != chessStates[j, i])
					{
						isIdentical = false;
						break;
					}
				}

				if (isIdentical && firstElement != "")
				{
					winner = firstElement;
				}

			}

			// check diagonal
			{
				string firstElement = chessStates[0, 0];
				bool isIdentical = true;
				for (int j = 1; j < 3; j++)
				{
					if (firstElement != chessStates[j, j])
					{
						isIdentical = false;
						break;
					}
				}

				if (isIdentical && firstElement != "")
				{
					winner = firstElement;
				}
			}

			{
				string firstElement = chessStates[0, 2];
				bool isIdentical = true;
				for (int j = 1; j < 3; j++)
				{
					if (firstElement != chessStates[j, 2 - j])
					{
						isIdentical = false;
						break;
					}
				}

				if (isIdentical && firstElement != "")
				{
					winner = firstElement;
				}
			}


			// 
			if (winner != "")
			{
				uxTurn.Text = winner + " is a winner";

				// disable all bottons
				for (int i = 0; i < 3; i++)
				{
					for (int j = 0; j < 3; j++)
					{
						chessStates[i, j] = "";
						Button theButton = uxGrid.Children.Cast<Button>().First(b => Grid.GetRow(b) == i && Grid.GetColumn(b) == j);
						theButton.IsEnabled = false;
					}
				}
			}

			clickedButton.IsEnabled = false;

			// flip the turn
			if (currentTurn == "X")
			{
				currentTurn = "O";
			}
			else
			{
				currentTurn = "X";
			}

			if (winner == "")
			{
				uxTurn.Text = currentTurn + "'s turn";
			}
		}

		private void uxNewGame_Click(object sender, RoutedEventArgs e)
		{
			ClearChessBoard();
		}
	}
}
