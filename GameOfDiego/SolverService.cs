using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfDiego
{
    public class SolverService
    {
        public List<Cell> SolveBoard(List<Cell> cells)
        {

            //Create full board
            var fullBoard = CreateFullBoard(cells);

            //Decide Fate
            var newCells = new List<Cell>();
            foreach (Cell cell in fullBoard)
            {
                var neighbors = CheckNeighbors(fullBoard, cell);
                var alive = DecideFate(cell, neighbors);

                if(alive)
                {
                    newCells.Add(cell.BecomeAlive());
                }
                else
                {
                    newCells.Add(cell.Die());
                }
            }

            //Set new board
            var nextGen = CreateNextBoard(newCells);

            return nextGen;

        }

        public List<Cell> CreateFullBoard(List<Cell> startingBoard)
        {
            List<Cell> fullBoard = new List<Cell>(startingBoard);
            foreach (var cell in startingBoard)
            {
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
                    if (fullBoard.Any(c => c.x == comparisonCells[i].x && c.y == comparisonCells[i].y))
                    {
                        continue;
                    }
                    else
                    {
                        fullBoard.Add(comparisonCells[i]);
                    }
                }

            }


            return fullBoard;
        }
        public int CheckNeighbors(List<Cell> startingBoard, Cell cell)
        {   
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

            return startingBoard.Count(c => comparisonCells.Any(d => d.x == c.x && d.y == c.y) && c.IsAlive == true);
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

        
    }

}

