using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfDiego
{
    public class SolverService
    {
        public int CheckNeighbors(List<Cell> startingBoard, Cell cell)
        {
            var aliveNeighbors = 0;
            
            List<Cell> comparisonCells = new List<Cell>{
                new Cell{ x = cell.x-1, y=cell.y-1 },
                new Cell{ x = cell.x, y=cell.y-1 },
                new Cell{ x = cell.x+1, y=cell.y-1 },
                new Cell{ x = cell.x-1, y=cell.y },
                new Cell{ x = cell.x+1, y=cell.y },
                new Cell{ x = cell.x-1, y=cell.y+1 },
                new Cell{ x = cell.x, y=cell.y+1 },
                new Cell{ x = cell.x+1, y=cell.y+1 },

            };

            for (int i = 0; i < comparisonCells.Count; i++)
            {
                if (startingBoard.Any(c=>c.x == comparisonCells[i].x && c.y == comparisonCells[i].y && c.IsAlive == true))
                {
                    aliveNeighbors++;
                }
            }

            return aliveNeighbors;
        }

        public List<Cell> CreateFullBoard(List<Cell> startingBoard)
        {
            List<Cell> fullBoard = new List<Cell>(startingBoard);
            foreach (var cell in startingBoard){
                List<Cell> comparisonCells = new List<Cell>{
                    new Cell{ x = cell.x-1, y=cell.y-1 },
                    new Cell{ x = cell.x-1, y=cell.y },
                    new Cell{ x = cell.x-1, y=cell.y+1 },
                    new Cell{ x = cell.x, y=cell.y-1 },
                    new Cell{ x = cell.x, y=cell.y+1 },
                    new Cell{ x = cell.x+1, y=cell.y-1 },
                    new Cell{ x = cell.x+1, y=cell.y },
                    new Cell{ x = cell.x+1, y=cell.y+1 },
                };


                //For every cell around a living cell
                for (int i = 0; i < comparisonCells.Count; i++)
                {
                    if(fullBoard.Any(c=>c.x == comparisonCells[i].x && c.y == comparisonCells[i].y)){
                        continue;
                    }
                    else
                    {
                        fullBoard.Add(comparisonCells[i]);
                    }
                }

            }

            //var transform = fullBoard.GroupBy(x => new { x.x, x.y, x.IsAlive }).Select(x => x.First()).ToList();

            return fullBoard;
        }
        public bool DecideFate(Cell cell, int neighbors)
        {
            if(cell.IsAlive == true)
            {
                //Isolation
                if (neighbors < 2)
                {
                    return false;
                }

                //Surviving
                if (neighbors == 3 || neighbors == 2)
                {
                    return true;
                }

                //Overcrouding
                if (neighbors > 3)
                {
                    return false;
                }
            }
            else
            {
                //Birth
                if(neighbors == 3)
                {
                    return true;
                }

            }
            
            return false;
            
        }

        public List<Cell> CreateNextBoard(List<Cell> newCells)
        {
           return newCells.Where(c => c.IsAlive == true).ToList();
        }

        public List<Cell> SolveBoard(List<Cell> cells)
        {

            //Create full board
            var fullBoard = CreateFullBoard(cells);

            //Decide Fate
            var newCells = new List<Cell>();
            foreach (Cell cell in fullBoard)
            {
                var neighbors = CheckNeighbors(fullBoard, cell);
                cell.IsAlive = DecideFate(cell, neighbors);
                newCells.Add(cell);
            }

            //Set new board
            var nextGen = CreateNextBoard(newCells);

            return nextGen;

        }
    }

}

