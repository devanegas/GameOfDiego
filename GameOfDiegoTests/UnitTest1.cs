using GameOfDiego;
using NUnit.Framework;
using System.Collections.Generic;

namespace GameOfDiegoTests
{
    public class Tests
    {
        public List<Cell> startingBoard { get; set; }
        [SetUp]
        public void Setup()
        {
            startingBoard = new List<Cell>
            {
                new Cell{x=-1, y=1},
                new Cell{x=1, y=1},
                new Cell{x=0, y=0},
                new Cell{x=1, y=0},
                new Cell{x=-1, y=-1},
                new Cell{x=1, y=-1},
            };
        }

        [TestCase(0, 0, 5)]
        [TestCase(-1, 1, 1)]
        [TestCase(1, 1, 2)]
        [TestCase(1, 0, 3)]
        [TestCase(-1, -1, 1)]
        [TestCase(1, -1, 2)]
        public void CheckNeighborsOfCell_ReturnsCorrectNumberOfNeighbors(int x, int y, int expected)
        {
            Cell cell = new Cell { x = x, y = y };
            var service = new SolverService();
            var actual = service.CheckNeighbors(startingBoard, cell);
            Assert.AreEqual(expected, actual);
        }
    }
}