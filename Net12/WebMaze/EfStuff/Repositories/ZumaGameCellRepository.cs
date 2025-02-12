﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMaze.EfStuff.DbModel;

namespace WebMaze.EfStuff.Repositories
{
    public class ZumaGameCellRepository : BaseRepository<ZumaGameCell>
    {
        public ZumaGameCellRepository(WebContext webContext) : base(webContext)
        {

        }

        public void GetNear(ZumaGameCell currentCell, List<ZumaGameCell> nearCells)
        {
            if (currentCell != null)
            {
                var baseNear = currentCell.Field.Cells
                        .Where(cell =>
                        (cell.X == currentCell.X && Math.Abs(cell.Y - currentCell.Y) <= 1
                        || Math.Abs(cell.X - currentCell.X) <= 1 && cell.Y == currentCell.Y)
                        && cell.Color == currentCell.Color)
                        .ToList();

                var getNear = baseNear.Except(nearCells).ToList();

                if (getNear.Count() > 0)
                {
                    nearCells.AddRange(getNear);

                    foreach (var cellNear in getNear)
                    {
                        GetNear(cellNear, nearCells);
                    }

                }
            }
        }

        public List<ZumaGameCell> GetNear(ZumaGameCell currentCell)
        {
            var cellsToRemove = new List<ZumaGameCell>();

            GetNear(currentCell, cellsToRemove);

            return cellsToRemove;
        }

        public List<ZumaGameCell> GetNear(long id)
        {
            var currentCell = Get(id);
            var cellsToRemove = new List<ZumaGameCell>();

            GetNear(currentCell, cellsToRemove);

            return cellsToRemove;
        }

        public void UpdateCells(List<ZumaGameCell> cells)
        {
            foreach (var model in cells)
            {
                _webContext.Update(model);
            }
            _webContext.SaveChanges();
        }

        public override void Remove(ZumaGameCell model)
        {
            _dbSet.Remove(model);
            _webContext.SaveChanges();
        }
    }

}
