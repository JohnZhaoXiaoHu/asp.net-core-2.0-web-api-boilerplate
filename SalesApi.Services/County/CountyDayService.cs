using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.County;
using SalesApi.Models.Settings;
using SalesApi.Repositories.County;
using SalesApi.Repositories.Settings;

namespace SalesApi.Services.County
{
    public interface ICountyDayService
    {
        Task Initialzie(DateTime date, string userName);
    }

    public class CountyDayService : ICountyDayService
    {
        private readonly ICountyDayRepository _collectiveDayRepository;
        private readonly ICountyProductSnapshotRepository _collectiveProductSnapshotRepository;
        private readonly IProductForCountyRepository _productForCountyRepository;
        private readonly IProductRepository _productRepository;

        public CountyDayService(
            ICountyDayRepository collectiveDayRepository, 
            ICountyProductSnapshotRepository collectiveProductSnapshotRepository, 
            IProductForCountyRepository productForCountyRepository, 
            IProductRepository productRepository)
        {
            _collectiveDayRepository = collectiveDayRepository;
            _collectiveProductSnapshotRepository = collectiveProductSnapshotRepository;
            _productForCountyRepository = productForCountyRepository;
            _productRepository = productRepository;
        }

        public async Task Initialzie(DateTime date, string userName)
        {
            var dateStr = date.ToString("yyyy-MM-dd");
            var item = await _collectiveDayRepository.GetSingleAsync(x => x.Date == dateStr, x => x.CountyProductSnapshots);
            var products = await _productRepository.All.Where(x => !x.Deleted).ToListAsync();
            var productForCountys = await _productForCountyRepository.All.Where(x => !x.Deleted).ToListAsync();
            if (item != null)
            {
                if (item.Initialized)
                {
                    throw new Exception("该郊县日已经初始化, 操作失败");
                }
                item.Initialized = true;
                item.SetModification(userName, "初始化");
                InitializeCountyProducts(userName, item, productForCountys, products);
                _collectiveDayRepository.Update(item);
            }
            else
            {
                item = new CountyDay
                {
                    Date = dateStr,
                    Initialized = true
                };
                item.SetCreation(userName, "初始化");
                InitializeCountyProducts(userName, item, productForCountys, products);
                _collectiveDayRepository.Add(item);
            }
        }

        private void InitializeCountyProducts(string userName, CountyDay item, List<ProductForCounty> productForCountys, List<Product> products)
        {
            var dbDayProducts = item.CountyProductSnapshots;
            foreach (var dayProduct in dbDayProducts)
            {
                var productForCounty =
                    productForCountys.SingleOrDefault(x => x.Id == dayProduct.ProductForCountyId);
                if (productForCounty == null)
                {
                    throw new Exception($"未能找到集体户产品: {dayProduct.Name}");
                }
                var product = products.SingleOrDefault(x => x.Id == productForCounty.ProductId);
                if (product == null)
                {
                    throw new Exception($"未能找到产品: {dayProduct.Name}");
                }
                SetCountyProductSnapshot(dayProduct, productForCounty, product);
                dayProduct.SetModification(userName, "重新初始化");
                _collectiveProductSnapshotRepository.Update(dayProduct);
            }
            var dayProductIds = dbDayProducts.Select(x => x.ProductForCountyId).ToList();
            var collectiveProductIds = productForCountys.Select(x => x.Id).ToList();
            var toAddIds = collectiveProductIds.Except(dayProductIds).ToList();
            var toAdd = productForCountys.Where(x => toAddIds.Contains(x.Id)).ToList();
            foreach (var pr in toAdd)
            {
                var dayProduct = new CountyProductSnapshot
                {
                    ProductForCountyId = pr.Id,
                };
                dayProduct.SetCreation(userName, "初始化");
                var product = products.SingleOrDefault(x => x.Id == pr.ProductId);
                if (product == null)
                {
                    throw new Exception($"未能找到产品, 集体户产品ID: {pr.Id}");
                }
                SetCountyProductSnapshot(dayProduct, pr, product);
                item.CountyProductSnapshots.Add(dayProduct);
            }
        }

        private void SetCountyProductSnapshot(CountyProductSnapshot ps, ProductForCounty r, Product p)
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
