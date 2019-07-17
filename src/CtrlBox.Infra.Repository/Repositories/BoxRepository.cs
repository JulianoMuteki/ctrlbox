using System;
using System.Collections.Generic;
using System.Linq;
using CtrlBox.CrossCutting;
using CtrlBox.Domain.Entities;
using CtrlBox.Domain.Interfaces.Repository;
using CtrlBox.Infra.Context;
using CtrlBox.Infra.Repository.Common;
using Microsoft.EntityFrameworkCore;

namespace CtrlBox.Infra.Repository.Repositories
{
    public class BoxRepository : GenericRepository<Box>, IBoxRepository
    {
        public BoxRepository(CtrlBoxContext context)
            : base(context)
        {

        }

        public ICollection<Box> GetAllWithBoxTypeAndProduct()
        {
            try
            {
                return _context.Set<Box>()
                    .Include(x => x.Product)
                    .Include(x => x.BoxType)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxRepository>("Unexpected error fetching GetAll", nameof(this.GetAllWithBoxTypeAndProduct), ex);
            }
        }

        public ICollection<Box> GetBoxesParentsWithBoxType()
        {
            try
            {
                return _context.Set<Box>()
                    .Include(x => x.BoxType)
                    .Where(x=>x.BoxChildID == null)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw CustomException.Create<BoxRepository>("Unexpected error fetching GetAll", nameof(this.GetBoxesParentsWithBoxType), ex);
            }
        }
    }
}
