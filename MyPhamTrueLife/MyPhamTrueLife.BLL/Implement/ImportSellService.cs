using MyPhamTrueLife.BLL.Interface;
using MyPhamTrueLife.DAL.Models1;
using MyPhamTrueLife.DAL.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MyPhamTrueLife.BLL.Implement
{
    public class ImportSellService : IImportSellService
    {
        public readonly dbDevNewContext _unitOfWork;
        public ImportSellService(dbDevNewContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateImportSellServiceAsync(InfoImportSell value, int staffId)
        {
            if (value == null || staffId <= 0)
            {
                return false;
            }
            value.Total = null;
            value.CreateAt = DateTime.Now;
            value.CreateUser = staffId;
            value.DeleteFlag = false;
            await _unitOfWork.Repository<InfoImportSell>().AddAsync(value);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
        public async Task<bool> DeleteImportSellServiceAsync(int id, int staffId)
        {
            if (id <= 0 || staffId <= 0)
            {
                return false;
            }
            var supplier = await _unitOfWork.Repository<InfoImportSell>().Where(x => x.DeleteFlag != true && x.ImportSellId.Equals(id)).AsNoTracking().FirstOrDefaultAsync();
            if (supplier == null)
            {
                return false;
            }
            supplier.DeleteFlag = true;
            supplier.UpdateAt = DateTime.Now;
            supplier.UpdateUser = staffId;
            _unitOfWork.Repository<InfoImportSell>().UpdateRange(supplier);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
        public async Task<ResponseList> ListImportSellServiceAsync(int page = 1, int limit = 25)
        {
            var listData = new ResponseList();
            listData.ListData = null;
            var listInfoImportSell = await _unitOfWork.Repository<InfoImportSell>().Where(x => x.DeleteFlag != true).AsNoTracking().ToListAsync();
            var totalRows = listInfoImportSell.Count();
            listData.Paging = new Paging(totalRows, page, limit);
            int start = listData.Paging.start;
            listInfoImportSell = listInfoImportSell.Skip(start).Take(limit).ToList();
            listData.ListData = listInfoImportSell;
            return listData;
        }
    }
}
