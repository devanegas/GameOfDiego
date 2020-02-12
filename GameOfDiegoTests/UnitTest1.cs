using GameOfDiego;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace GameOfDiegoTests
{
    public class Tests
    {
        public List<Cell> startingBoard { get; set; }

        public List<Cell> fullBoard { get; set; }

        [SetUp]
        public void Setup()
        {
            startingBoard = new List<Cell>
            {
                new Cell{x=-1, y=1, IsAlive=true},
                new Cell{x=1, y=1, IsAlive=true},
                new Cell{x=0, y=0, IsAlive=true},
                new Cell{x=1, y=0, IsAlive=true},
                new Cell{x=-1, y=-1, IsAlive=true},
                new Cell{x=1, y=-1, IsAlive=true},
            };

            fullBoard = new List<Cell>
            {
                new Cell{x=-2, y=-2, IsAlive=false},
                new Cell{x=-2, y=-1, IsAlive=false},
                new Cell{x=-2, y=0, IsAlive=false},
                new Cell{x=-2, y=1, IsAlive=false},
                new Cell{x=-2, y=2, IsAlive=false},
                new Cell{x=-1, y=-2, IsAlive=false},
                new Cell{x=-1, y=-1, IsAlive=true},
                new Cell{x=-1, y=0, IsAlive=false},
                new Cell{x=-1, y=1, IsAlive=true},
                new Cell{x=-1, y=2, IsAlive=false},
                new Cell{x=0, y=-2, IsAlive=false},
                new Cell{x=0, y=-1, IsAlive=false},
                new Cell{x=0, y=0, IsAlive=true},
                new Cell{x=0, y=1, IsAlive=false},
                new Cell{x=0, y=2, IsAlive=false},
                new Cell{x=1, y=-2, IsAlive=false},
                new Cell{x=1, y=-1, IsAlive=true},
                new Cell{x=1, y=0, IsAlive=true},
                new Cell{x=1, y=1, IsAlive=true},
                new Cell{x=1, y=2, IsAlive=false},
                new Cell{x=2, y=-2, IsAlive=false},
                new Cell{x=2, y=-1, IsAlive=false},
                new Cell{x=2, y=0, IsAlive=false},
                new Cell{x=2, y=1, IsAlive=false},
                new Cell{x=2, y=2, IsAlive=false},
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

        [TestCase(0, 0, 5)]
        [TestCase(-1, 1, 1)]
        [TestCase(1, 1, 2)]
        [TestCase(1, 0, 3)]
        [TestCase(-1, -1, 1)]
        [TestCase(1, -1, 2)]
        public void CheckNeighborsOfCell_FullBoard_ReturnsCorrectNumberOfNeighbors(int x, int y, int expected)
        {
            Cell cell = new Cell { x = x, y = y };
            var service = new SolverService();
            var actual = service.CheckNeighbors(fullBoard, cell);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CallFullBoard_FullBoardIsCreated()
        {
            var service = new SolverService();
            var actual = service.CreateFullBoard(startingBoard);
            foreach(Cell cell in actual)
            {  
               Assert.That(fullBoard.Any(c => c.x == cell.x && c.y == cell.y && c.IsAlive == cell.IsAlive));
            }
        }
        
        [Test]
        public void Solver_Solves_Correctly()
        {
            var expected = new List<Cell>
            {
                new Cell{x=-1, y=0, IsAlive=true},
                new Cell{x=1, y=1, IsAlive=true},
                new Cell{x=1, y=0, IsAlive=true},
                new Cell{x=1, y=-1, IsAlive=true},
                new Cell{x=2, y=0, IsAlive=true},
            };

            var service = new SolverService();
            var actual = service.SolveBoard(startingBoard);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}