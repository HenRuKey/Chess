using System;
using System.Diagnostics;
using ChessLib;
using ChessLib.controllers;
using ChessLib.Exceptions;
using ChessLib.models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessTest
{
    [TestClass]
    public class MovementTester
    {
        #region Queen Movement
        [TestMethod]
        public void QueenIsPlacedAtCorrectCoordinates()
        {
            Piece queen = Commander.CreatePiece("Qla4");
            Game game = new Game();
            game.PlacePiece(queen);

            Tuple<int, int> coordinates = new Tuple<int, int>(3, 0);
            Piece pieceAtCoordinates = game.GetPieceAtCoord(coordinates);
            Assert.AreEqual(queen, pieceAtCoordinates);
        }

        [TestMethod]
        public void QueenVerticalMovementSucceedsAndPerforms()
        {
            Piece queen = Commander.CreatePiece("Qla4");
            Trace.WriteLine(queen.Position);
            Game game = new Game();
            game.PlacePiece(queen);

            Tuple<int, int> initialPosition = queen.Position;
            Tuple<int, int> destination = new Tuple<int, int>(initialPosition.Item1 + 4, initialPosition.Item2);
            Tuple<int, int>[] coordinateArray = new Tuple<int, int>[]
            {
                initialPosition, destination
            };
            game.PerformMove(coordinateArray);

            Piece pieceAtCoordinates = game.GetPieceAtCoord(destination);
            Assert.AreEqual(queen, pieceAtCoordinates);
        }

        [TestMethod]
        public void QueenHorizontalMovementSucceedsAndPerforms()
        {
            Piece queen = Commander.CreatePiece("Qla4");
            Trace.WriteLine(queen.Position);
            Game game = new Game();
            game.PlacePiece(queen);

            Tuple<int, int> initialPosition = queen.Position;
            Tuple<int, int> destination = new Tuple<int, int>(initialPosition.Item1, initialPosition.Item2 + 4);
            Tuple<int, int>[] coordinateArray = new Tuple<int, int>[]
            {
                initialPosition, destination
            };
            game.PerformMove(coordinateArray);

            Piece pieceAtCoordinates = game.GetPieceAtCoord(destination);
            Assert.AreEqual(queen, pieceAtCoordinates);
        }

        [TestMethod]
        public void QueenDiagonalMovementSucceedsAndPerforms()
        {
            Piece queen = Commander.CreatePiece("Qla4");
            Trace.WriteLine(queen.Position);
            Game game = new Game();
            game.PlacePiece(queen);

            Tuple<int, int> initialPosition = queen.Position;
            Tuple<int, int> destination = new Tuple<int, int>(initialPosition.Item1 + 4, initialPosition.Item2 + 4);
            Tuple<int, int>[] coordinateArray = new Tuple<int, int>[]
            {
                initialPosition, destination
            };
            game.PerformMove(coordinateArray);

            Piece pieceAtCoordinates = game.GetPieceAtCoord(destination);
            Assert.AreEqual(queen, pieceAtCoordinates);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCommandException))]
        public void QueenMovementLikeKnightFailsAndThrowsException()
        {
            Piece queen = Commander.CreatePiece("Qla4");
            Trace.WriteLine(queen.Position);
            Game game = new Game();
            game.PlacePiece(queen);

            Tuple<int, int> initialPosition = queen.Position;
            Tuple<int, int> destination = new Tuple<int, int>(initialPosition.Item1 + 2, initialPosition.Item2 + 1);
            Tuple<int, int>[] coordinateArray = new Tuple<int, int>[]
            {
                initialPosition, destination
            };
            game.PerformMove(coordinateArray);
        }
        #endregion


        #region Knight Movement
        [TestMethod]
        public void KnightIsPlacedAtCorrectCoordinates()
        {
            Piece knight = Commander.CreatePiece("Nlb1");
            Game game = new Game();
            game.PlacePiece(knight);
            Trace.Write(knight.Position);

            Tuple<int, int> expectedPositionOfKnight = new Tuple<int, int>(0,1);
            Piece pieceAtCoordinates = game.GetPieceAtCoord(expectedPositionOfKnight);

            Assert.AreEqual(knight, pieceAtCoordinates);
        }

        [TestMethod]
        public void KnightMovementForwardSucceedsAndPerforms()
        {
            Piece knight = Commander.CreatePiece("Nlb1");
            Game game = new Game();
            game.PlacePiece(knight);

            Tuple<int, int> positionOfKnight = knight.Position;
            Tuple<int, int> newPosition = new Tuple<int, int>(knight.Position.Item1 + 2, knight.Position.Item1 + 1);
            Tuple<int, int>[] coordinateArray = new Tuple<int, int>[]
            {
                positionOfKnight, newPosition
            };

            game.PerformMove(coordinateArray);
            Tuple<int, int> expectedDestination = new Tuple<int, int>(2,2);
            Piece pieceAtCoordinates = game.GetPieceAtCoord(expectedDestination);
            Assert.AreEqual(knight, pieceAtCoordinates);
        }
        #endregion

    }
}
