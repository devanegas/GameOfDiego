using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfDiego
{
    public class SolverService
    {
        public int CheckNeighbors(HashSet<Cell> startingBoard, Cell cell)
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
                if (startingBoard.Any(cell=>cell.x == comparisonCells[i].x && cell.y == comparisonCells[i].y && cell.IsAlive==true))
                {
                    aliveNeighbors++;
                }
            }

            return aliveNeighbors;
        }

        public HashSet<Cell> CreateFullBoard(HashSet<Cell> startingBoard)
        {
            HashSet<Cell> fullBoard = new HashSet<Cell>();
            foreach (var cell in startingBoard){
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

                //For every cell around
                for (int i = 0; i < comparisonCells.Count; i++)
                {
                    if (startingBoard.Any(c => c.x == comparisonCells[i].x && c.y == comparisonCells[i].y && c.IsAlive == true))
                    {
                        if(fullBoard.Any(c => c.x == comparisonCells[i].x && c.y == comparisonCells[i].y && c.IsAlive == true))
                        {
                            continue;
                        }

                        var aliveCell = comparisonCells[i];
                        aliveCell.IsAlive = true;
                        fullBoard.Add(aliveCell);
                    }
                    else
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

                if (fullBoard.Any(c => c.x == cell.x && c.y == cell.y))
                {
                    continue;
                }
                else
                {
                    fullBoard.Add(cell);
                }
            }

            return fullBoard;
        }
    }
}

