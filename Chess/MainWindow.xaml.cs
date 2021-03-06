﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Chess.converters;
using ChessLib.controllers;
using ChessLib.models;

namespace Chess
{
    public partial class MainWindow : Window
    {

        Dictionary<Tuple<int, int>, Tile> tiles = new Dictionary<Tuple<int, int>, Tile>();
        ChessController controller;
        #region Color
        Color softWhite = new Color()
        {
            R = 164,
            B = 9,
            G = 10,
            A = 255
        };
        Color darkSlate = new Color()
        {
            R = 115,
            B = 1,
            G = 3,
            A = 255
        };
        #endregion


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateBoard();
        }

        private void PopulateBoard()
        {
            controller = new ChessController("../../../ChessTest/test_data/start.txt");
            gridBoard.Columns = 8;
            gridBoard.Rows = 8;
            // Set-up events for when pieces are moved or placed.
            controller.game.ChessBoard.OnPieceMoved += ChessBoard_OnPieceMoved;
            controller.game.ChessBoard.OnPiecePlaced += ChessBoard_OnPiecePlaced;
            controller.game.OnMoveFailure += Game_OnMoveFailure;
            controller.game.OnCheckMate += Game_OnCheckMate;
            // Bind player's move to label.
            lblTurn.DataContext = controller.game;
            Binding binding = new Binding("WhiteToMove");
            binding.Converter = new BoolToMessageConverter();
            lblTurn.SetBinding(Label.ContentProperty, binding);
            // Populate grid with tiles
            for (int i = 7; i >= 0; i--)
            {
                for (int j = 0; j < 8; j++)
                {
                    Color color = (i % 2 == 0 && j % 2 == 0) || (i % 2 == 1 && j % 2 == 1) ? darkSlate : softWhite;
                    Tuple<int, int> position = new Tuple<int, int>(i, j);
                    Tile tile = new Tile(color);
                    tiles.Add(position, tile);
                    gridBoard.Children.Add(tile.Grid);
                }
            }
            controller.PlayFromFile();
        }

        private void Game_OnCheckMate(object sender, CheckMateArgs e)
        {
            lblMessage.Text = $"Check Mate: {e.WinningColor} wins!";
            txtBoxCommand.IsEnabled = false;
        }

        private void Game_OnMoveFailure(object sender, MovementFailureArgs e)
        {
            lblMessage.Text = e.Message;
        }

        private void ChessBoard_OnPiecePlaced(object sender, PlacementArgs e)
        {
            tiles[e.PiecePlaced.Position].Piece = e.PiecePlaced;
            // Add event listener for kings.
            if (e.PiecePlaced is King)
            {
                King king = (King)e.PiecePlaced;
                king.OnCheck += King_OnCheck;
            }
        }

        private void King_OnCheck(object sender, CheckedArgs e)
        {
            lblMessage.Text = (e.Color == ChessLib.enums.Color.LIGHT ? "White" : "Black") + " king is in check!";
        }

        private void ChessBoard_OnPieceMoved(object sender, MovementArgs e)
        {
            tiles[e.OldPosition].Piece = null;
            tiles[e.PieceMoved.Position].Piece = e.PieceMoved;
        }

        private void txtBoxCommand_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key is System.Windows.Input.Key.Enter)
            {
                string userInput = txtBoxCommand.Text;
                if (FileReader.CommandIsValid(userInput))
                {
                    txtBoxCommand.Clear();
                    lblMessage.Text = "";
                    controller.PerformCommand(userInput);
                }
            }
        }

        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            tiles = new Dictionary<Tuple<int, int>, Tile>();
            gridBoard.Children.Clear();
            PopulateBoard();
            txtBoxCommand.IsEnabled = true;
        }
    }
}
