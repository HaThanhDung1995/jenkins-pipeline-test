using MyPhamTrueLife.BLL.Interface;
using MyPhamTrueLife.DAL.Models1;
using MyPhamTrueLife.DAL.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyPhamTrueLife.DAL.Models1.Utils;

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
            foreach (var item in listInfoImportSell)
            {
                item.Supplier = item.SupplierId != null ? await _unitOfWork.Repository<InfoSupplier>().Where(x=>x.DeleteFlag != true && x.SupplierId == item.SupplierId).AsNoTracking().FirstOrDefaultAsync() : null;
                item.Staff = item.StaffId != null ? await _unitOfWork.Repository<InfoStaff>().Where(x => x.DeleteFlag != true && x.StaffId == item.StaffId).AsNoTracking().FirstOrDefaultAsync() : null;
            }
            
            var totalRows = listInfoImportSell.Count();
            listData.Paging = new Paging(totalRows, page, limit);
            int start = listData.Paging.start;
            listInfoImportSell = listInfoImportSell.Skip(start).Take(limit).ToList();
            listData.ListData = listInfoImportSell;
            return listData;
        }

        public async Task<bool> ThemDanhSachChiTietNhapHang(List<InfoDetailImportSell> value, int staffId, int importSellId)
        {
            var price = 0;
            var supplier = await _unitOfWork.Repository<InfoImportSell>().Where(x => x.DeleteFlag != true && x.ImportSellId.Equals(importSellId)).AsNoTracking().FirstOrDefaultAsync();
            if (supplier != null)
            {
                foreach (var item in value)
                {
                    var product = await _unitOfWork.Repository<InfoPriceProduct>().Where(x => x.DeleteFlag != true && x.ProductId == item.ProductId).AsNoTracking().FirstOrDefaultAsync();
                    if (product != null)
                    {
                        var detaial = new InfoDetailImportSell();
                        int priceI = product.Price.Value + 10000;
                        price += (item.Amount.Value * priceI);
                        detaial.Prize = priceI;
                        detaial.ProductId = item.ProductId;
                        detaial.Amount = item.Amount;
                        detaial.ImportSellId = importSellId;
                        detaial.CreateAt = DateTime.Now;
                        detaial.CreateUser = staffId;
                        detaial.DeleteFlag = false;
                        await _unitOfWork.Repository<InfoDetailImportSell>().AddAsync(detaial);
                        await _unitOfWork.SaveChangeAsync();
                    }
                }
                supplier.DeleteFlag = false;
                supplier.UpdateAt = DateTime.Now;
                supplier.UpdateUser = staffId;
                supplier.Total = price;
                _unitOfWork.Repository<InfoImportSell>().Update(supplier);
                await _unitOfWork.SaveChangeAsync();
                return true;
            }
            return false;
        }

        public async Task<ResponseList> DetailImportSellAsync(int importSellId, int page = 1, int limit = 25)
        {
            var listData = new ResponseList();
            listData.ListData = null;
            var importSell = await _unitOfWork.Repository<InfoImportSell>().Where(x => x.DeleteFlag != true && x.ImportSellId == importSellId).AsNoTracking().FirstOrDefaultAsync();
            if (importSell == null)
            {
                return listData;
            }
            var listInfoImportSell = await _unitOfWork.Repository<InfoDetailImportSell>().Where(x => x.DeleteFlag != true && x.ImportSellId == importSell.ImportSellId).AsNoTracking().ToListAsync();
            var listInfo = new List<InfoDetailImportList>();
            foreach (var item in listInfoImportSell)
            {
                var info = new InfoDetailImportList();
                info.ImportSellId = item.ImportSellId;
                info.ProductId = item.ProductId;
                info.infoProduct = await _unitOfWork.Repository<InfoProduct>().Where(x=>x.DeleteFlag != true && x.ProductId == item.ProductId).AsNoTracking().FirstOrDefaultAsync();
                info.Amount = item.Amount;
                info.Prize = item.Prize;
                info.CapacityId = item.CapacityId;
                info.infoCapacity = item.CapacityId == null ? null : await _unitOfWork.Repository<InfoCapacity>().Where(x => x.DeleteFlag != true && x.CapacityId == item.CapacityId).AsNoTracking().FirstOrDefaultAsync(); ;
                info.StartAt = item.StartAt;
                info.EndAt = item.EndAt;
                info.Trademark = item.Trademark;
                listInfo.Add(info);
            }

            var totalRows = listInfo.Count();
            listData.Paging = new Paging(totalRows, page, limit);
            int start = listData.Paging.start;
            listInfo = listInfo.Skip(start).Take(limit).ToList();
            listData.ListData = listInfo;
            return listData;
        }

        public async Task<bool> CapNhatTrangThaiDonNhapHang(int importSellId, int staffId, string Status)
        {
            var importSell = await _unitOfWork.Repository<InfoImportSell>().Where(x => x.DeleteFlag != true && x.ImportSellId == importSellId).AsNoTracking().FirstOrDefaultAsync();
            if (importSell != null)
            {
                importSell.StaffId = staffId;
                importSell.DeleteFlag = false;
                importSell.UpdateAt = DateTime.Now;
                importSell.UpdateUser = staffId;
                importSell.Status = Status;
                _unitOfWork.Repository<InfoImportSell>().Update(importSell);
                await _unitOfWork.SaveChangeAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> CapNhatTrangThaiThanhToanDonNhapHang(int importSellId, int staffId)
        {
            var importSell = await _unitOfWork.Repository<InfoImportSell>().Where(x => x.DeleteFlag != true && x.ImportSellId == importSellId).AsNoTracking().FirstOrDefaultAsync();
            if (importSell != null)
            {
                importSell.StaffId = staffId;
                importSell.DeleteFlag = false;
                importSell.UpdateAt = DateTime.Now;
                importSell.UpdateUser = staffId;
                importSell.IsPay = true;
                _unitOfWork.Repository<InfoImportSell>().Update(importSell);
                await _unitOfWork.SaveChangeAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> CapNhatDonNhapHangVaoKho(int importSellId, int staffId)
        {
            var importSell = await _unitOfWork.Repository<InfoImportSell>().Where(x => x.DeleteFlag != true && x.ImportSellId == importSellId).AsNoTracking().FirstOrDefaultAsync();
            if (importSell != null)
            {
                importSell.StaffId = staffId;
                importSell.DeleteFlag = false;
                importSell.UpdateAt = DateTime.Now;
                importSell.UpdateUser = staffId;
                importSell.IsAddTosTock = true;
                _unitOfWork.Repository<InfoImportSell>().Update(importSell);
                await _unitOfWork.SaveChangeAsync();
                //Cập nhật số lượng vào kho
                var detail = await _unitOfWork.Repository<InfoDetailImportSell>().Where(x => x.DeleteFlag != true && x.ImportSellId == importSell.ImportSellId).AsNoTracking().ToListAsync();
                foreach (var item in detail)
                {
                    var infoProduct = await _unitOfWork.Repository<InfoProduct>().Where(x => x.DeleteFlag != true && x.ProductId == item.ProductId).AsNoTracking().FirstOrDefaultAsync();
                    if (infoProduct != null)
                    {
                        infoProduct.Amount += item.Amount;
                        infoProduct.StatusProduct = "CONHANG";
                        _unitOfWork.Repository<InfoProduct>().Update(infoProduct);
                        await _unitOfWork.SaveChangeAsync();
                        if (infoProduct.IsExpiry == true)
                        {
                            var expiry = await _unitOfWork.Repository<InfoExpiryProduct>().Where(x => x.DeleteFlag != true && x.ProductId == item.ProductId).AsNoTracking().OrderByDescending(x=>x.CreateAt).FirstOrDefaultAsync();
                            expiry.Amount += item.Amount;
                            _unitOfWork.Repository<InfoExpiryProduct>().Update(expiry);
                            await _unitOfWork.SaveChangeAsync();
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public async Task<bool> XoaChiTietNhapHang(int importSellId, int productId, int? capacityId, int staffId)
        {
            var importSell = await _unitOfWork.Repository<InfoImportSell>().Where(x => x.DeleteFlag != true && x.ImportSellId == importSellId).AsNoTracking().FirstOrDefaultAsync();
            if (importSell != null)
            {
                var productInfo = await _unitOfWork.Repository<InfoProduct>().Where(x => x.DeleteFlag != true && x.ProductId == productId).AsNoTracking().FirstOrDefaultAsync();
                if (productInfo != null)
                {
                    var importDetailSell = await _unitOfWork.Repository<InfoDetailImportSell>().Where(x => x.DeleteFlag != true && x.ImportSellId == importSellId && x.ProductId == productId).AsNoTracking().FirstOrDefaultAsync();
                    if (capacityId != null)
                    {
                        importDetailSell = await _unitOfWork.Repository<InfoDetailImportSell>().Where(x => x.DeleteFlag != true && x.ImportSellId == importSellId && x.ProductId == productId && x.CapacityId == capacityId).AsNoTracking().FirstOrDefaultAsync();
                    }
                    if (importDetailSell != null)
                    {
                        int price = (importDetailSell.Amount.Value * importDetailSell.Prize.Value);
                        importDetailSell.DeleteFlag = true;
                        importDetailSell.UpdateAt = DateTime.Now;
                        importDetailSell.UpdateUser = staffId;
                        _unitOfWork.Repository<InfoDetailImportSell>().Update(importDetailSell);
                        await _unitOfWork.SaveChangeAsync();
                        //Cập nhật số lượng vào kho
                        importSell.Total -= price;
                        importSell.DeleteFlag = false;
                        importSell.UpdateAt = DateTime.Now;
                        importSell.UpdateUser = staffId;
                        _unitOfWork.Repository<InfoImportSell>().Update(importSell);
                        await _unitOfWork.SaveChangeAsync();
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
