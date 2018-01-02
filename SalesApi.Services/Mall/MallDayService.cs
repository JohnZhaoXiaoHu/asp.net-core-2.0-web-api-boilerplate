using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Mall;
using SalesApi.Models.Settings;
using SalesApi.Repositories.Mall;
using SalesApi.Repositories.Settings;

namespace SalesApi.Services.Mall
{
    public interface IMallDayService
    {
        Task Initialzie(DateTime date, string userName);
    }

    public class MallDayService : IMallDayService
    {
        private readonly IMallDayRepository _collectiveDayRepository;
        private readonly IMallProductSnapshotRepository _collectiveProductSnapshotRepository;
        private readonly IProductForMallRepository _productForMallRepository;
        private readonly IProductRepository _productRepository;

        public MallDayService(
            IMallDayRepository collectiveDayRepository, 
            IMallProductSnapshotRepository collectiveProductSnapshotRepository, 
            IProductForMallRepository productForMallRepository, 
            IProductRepository productRepository)
        {
            _collectiveDayRepository = collectiveDayRepository;
            _collectiveProductSnapshotRepository = collectiveProductSnapshotRepository;
            _productForMallRepository = productForMallRepository;
            _productRepository = productRepository;
        }

        public async Task Initialzie(DateTime date, string userName)
        {
            var dateStr = date.ToString("yyyy-MM-dd");
            var item = await _collectiveDayRepository.GetSingleAsync(x => x.Date == dateStr, x => x.MallProductSnapshots);
            var products = await _productRepository.All.Where(x => !x.Deleted).ToListAsync();
            var productForMalls = await _productForMallRepository.All.Where(x => !x.Deleted).ToListAsync();
            if (item != null)
            {
                if (item.Initialized)
                {
                    throw new Exception("该商超日已经初始化, 操作失败");
                }
                item.Initialized = true;
                item.SetModification(userName, "初始化");
                InitializeMallProducts(userName, item, productForMalls, products);
                _collectiveDayRepository.Update(item);
            }
            else
            {
                item = new MallDay
                {
                    Date = dateStr,
                    Initialized = true
                };
                item.SetCreation(userName, "初始化");
                InitializeMallProducts(userName, item, productForMalls, products);
                _collectiveDayRepository.Add(item);
            }
        }

        private void InitializeMallProducts(string userName, MallDay item, List<ProductForMall> productForMalls, List<Product> products)
        {
            var dbDayProducts = item.MallProductSnapshots;
            foreach (var dayProduct in dbDayProducts)
            {
                var productForMall =
                    productForMalls.SingleOrDefault(x => x.Id == dayProduct.ProductForMallId);
                if (productForMall == null)
                {
                    throw new Exception($"未能找到商超产品: {dayProduct.Name}");
                }
                var product = products.SingleOrDefault(x => x.Id == productForMall.ProductId);
                if (product == null)
                {
                    throw new Exception($"未能找到产品: {dayProduct.Name}");
                }
                SetMallProductSnapshot(dayProduct, productForMall, product);
                dayProduct.SetModification(userName, "重新初始化");
                _collectiveProductSnapshotRepository.Update(dayProduct);
            }
            var dayProductIds = dbDayProducts.Select(x => x.ProductForMallId).ToList();
            var collectiveProductIds = productForMalls.Select(x => x.Id).ToList();
            var toAddIds = collectiveProductIds.Except(dayProductIds).ToList();
            var toAdd = productForMalls.Where(x => toAddIds.Contains(x.Id)).ToList();
            foreach (var pr in toAdd)
            {
                var dayProduct = new MallProductSnapshot
                {
                    ProductForMallId = pr.Id,
                };
                dayProduct.SetCreation(userName, "初始化");
                var product = products.SingleOrDefault(x => x.Id == pr.ProductId);
                if (product == null)
                {
                    throw new Exception($"未能找到产品, 商超产品ID: {pr.Id}");
                }
                SetMallProductSnapshot(dayProduct, pr, product);
                item.MallProductSnapshots.Add(dayProduct);
            }
        }

        private void SetMallProductSnapshot(MallProductSnapshot ps, ProductForMall r, Product p)
        {
            ps.EquivalentBox = r.EquivalentBox;
            ps.IsOrderByBox = r.IsOrderByBox;
            ps.MinOrderCount = r.MinOrderCount;
            ps.OrderDivisor = r.OrderDivisor;

            ps.LegacyProductId = p.LegacyProductId;
            ps.Name = p.Name;
            ps.FullName = p.FullName;
            ps.Specification = p.Specification;
            ps.ProductUnit = p.ProductUnit;
            ps.ShelfLife = p.ShelfLife;
            ps.EquivalentTon = p.EquivalentTon;
            ps.Barcode = p.Barcode;
            ps.TaxRate = p.TaxRate;

            ps.Order = p.Order;
        }
    }
}
