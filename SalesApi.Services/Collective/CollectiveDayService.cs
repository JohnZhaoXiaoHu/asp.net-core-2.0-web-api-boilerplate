using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Collective;
using SalesApi.Models.Settings;
using SalesApi.Repositories.Collective;
using SalesApi.Repositories.Settings;

namespace SalesApi.Services.Collective
{
    public interface ICollectiveDayService
    {
        Task Initialzie(DateTime date, string userName);
    }

    public class CollectiveDayService : ICollectiveDayService
    {
        private readonly ICollectiveDayRepository _collectiveDayRepository;
        private readonly ICollectiveProductSnapshotRepository _collectiveProductSnapshotRepository;
        private readonly IProductForCollectiveRepository _productForCollectiveRepository;
        private readonly IProductRepository _productRepository;

        public CollectiveDayService(
            ICollectiveDayRepository collectiveDayRepository, 
            ICollectiveProductSnapshotRepository collectiveProductSnapshotRepository, 
            IProductForCollectiveRepository productForCollectiveRepository, 
            IProductRepository productRepository)
        {
            _collectiveDayRepository = collectiveDayRepository;
            _collectiveProductSnapshotRepository = collectiveProductSnapshotRepository;
            _productForCollectiveRepository = productForCollectiveRepository;
            _productRepository = productRepository;
        }

        public async Task Initialzie(DateTime date, string userName)
        {
            var dateStr = date.ToString("yyyy-MM-dd");
            var item = await _collectiveDayRepository.GetSingleAsync(x => x.Date == dateStr, x => x.CollectiveProductSnapshots);
            var products = await _productRepository.All.Where(x => !x.Deleted).ToListAsync();
            var productForCollectives = await _productForCollectiveRepository.All.Where(x => !x.Deleted).ToListAsync();
            if (item != null)
            {
                if (item.Initialized)
                {
                    throw new Exception("该零售日已经初始化, 操作失败");
                }
                item.Initialized = true;
                item.SetModification(userName, "初始化");
                InitializeCollectiveProducts(userName, item, productForCollectives, products);
                _collectiveDayRepository.Update(item);
            }
            else
            {
                item = new CollectiveDay
                {
                    Date = dateStr,
                    Initialized = true
                };
                item.SetCreation(userName, "初始化");
                InitializeCollectiveProducts(userName, item, productForCollectives, products);
                _collectiveDayRepository.Add(item);
            }
        }

        private void InitializeCollectiveProducts(string userName, CollectiveDay item, List<ProductForCollective> productForCollectives, List<Product> products)
        {
            var dbDayProducts = item.CollectiveProductSnapshots;
            foreach (var dayProduct in dbDayProducts)
            {
                var productForCollective =
                    productForCollectives.SingleOrDefault(x => x.Id == dayProduct.ProductForCollectiveId);
                if (productForCollective == null)
                {
                    throw new Exception($"未能找到零售产品: {dayProduct.Name}");
                }
                var product = products.SingleOrDefault(x => x.Id == productForCollective.ProductId);
                if (product == null)
                {
                    throw new Exception($"未能找到产品: {dayProduct.Name}");
                }
                SetCollectiveProductSnapshot(dayProduct, productForCollective, product);
                dayProduct.SetModification(userName, "重新初始化");
                _collectiveProductSnapshotRepository.Update(dayProduct);
            }
            var dayProductIds = dbDayProducts.Select(x => x.ProductForCollectiveId).ToList();
            var collectiveProductIds = productForCollectives.Select(x => x.Id).ToList();
            var toAddIds = collectiveProductIds.Except(dayProductIds).ToList();
            var toAdd = productForCollectives.Where(x => toAddIds.Contains(x.Id)).ToList();
            foreach (var pr in toAdd)
            {
                var dayProduct = new CollectiveProductSnapshot
                {
                    ProductForCollectiveId = pr.Id,
                };
                dayProduct.SetCreation(userName, "初始化");
                var product = products.SingleOrDefault(x => x.Id == pr.ProductId);
                if (product == null)
                {
                    throw new Exception($"未能找到产品, 零售产品ID: {pr.Id}");
                }
                SetCollectiveProductSnapshot(dayProduct, pr, product);
                item.CollectiveProductSnapshots.Add(dayProduct);
            }
        }

        private void SetCollectiveProductSnapshot(CollectiveProductSnapshot ps, ProductForCollective r, Product p)
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
        }
    }
}
