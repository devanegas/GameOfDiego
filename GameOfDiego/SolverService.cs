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
                if (startingBoard.Any(cell=>cell.x == comparisonCells[i].x && cell.y == comparisonCells[i].y))
                {
                    aliveNeighbors++;
                }
            }

            return aliveNeighbors;
        }

        //public bool DetermineFate(Cell)
    }
}

