using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Retail;
using SalesApi.Models.Settings;
using SalesApi.Repositories.Retail;
using SalesApi.Repositories.Settings;

namespace SalesApi.Services.Retail
{
    public interface IRetailDayService
    {
        Task Initialzie(DateTime date, string userName);
    }

    public class RetailDayService : IRetailDayService
    {
        private readonly IRetailDayRepository _retailDayRepository;
        private readonly IRetailProductSnapshotRepository _retailProductSnapshotRepository;
        private readonly IProductForRetailRepository _productForRetailRepository;
        private readonly IProductRepository _productRepository;

        public RetailDayService(
            IRetailDayRepository retailDayRepository, 
            IRetailProductSnapshotRepository retailProductSnapshotRepository, 
            IProductForRetailRepository productForRetailRepository, 
            IProductRepository productRepository)
        {
            _retailDayRepository = retailDayRepository;
            _retailProductSnapshotRepository = retailProductSnapshotRepository;
            _productForRetailRepository = productForRetailRepository;
            _productRepository = productRepository;
        }

        public async Task Initialzie(DateTime date, string userName)
        {
            var dateStr = date.ToString("yyyy-MM-dd");
            var item = await _retailDayRepository.GetSingleAsync(x => x.Date == dateStr, x => x.RetailProductSnapshots);
            var products = await _productRepository.All.Where(x => !x.Deleted).ToListAsync();
            var productForRetails = await _productForRetailRepository.All.Where(x => !x.Deleted).ToListAsync();
            if (item != null)
            {
                if (item.Initialized)
                {
                    throw new Exception("该零售日已经初始化, 操作失败");
                }
                item.Initialized = true;
                item.SetModification(userName, "初始化");
                InitializeRetailProducts(userName, item, productForRetails, products);
                _retailDayRepository.Update(item);
            }
            else
            {
                item = new RetailDay
                {
                    Date = dateStr,
                    Initialized = true
                };
                item.SetCreation(userName, "初始化");
                InitializeRetailProducts(userName, item, productForRetails, products);
                _retailDayRepository.Add(item);
            }
        }

        private void InitializeRetailProducts(string userName, RetailDay item, List<ProductForRetail> productForRetails, List<Product> products)
        {
            var dbDayProducts = item.RetailProductSnapshots;
            foreach (var dayProduct in dbDayProducts)
            {
                var productForRetail =
                    productForRetails.SingleOrDefault(x => x.Id == dayProduct.ProductForRetailId);
                if (productForRetail == null)
                {
                    throw new Exception($"未能找到零售产品: {dayProduct.Name}");
                }
                var product = products.SingleOrDefault(x => x.Id == productForRetail.ProductId);
                if (product == null)
                {
                    throw new Exception($"未能找到产品: {dayProduct.Name}");
                }
                SetRetailProductSnapshot(dayProduct, productForRetail, product);
                dayProduct.SetModification(userName, "重新初始化");
                _retailProductSnapshotRepository.Update(dayProduct);
            }
            var dayProductIds = dbDayProducts.Select(x => x.ProductForRetailId).ToList();
            var retailProductIds = productForRetails.Select(x => x.Id).ToList();
            var toAddIds = retailProductIds.Except(dayProductIds).ToList();
            var toAdd = productForRetails.Where(x => toAddIds.Contains(x.Id)).ToList();
            foreach (var pr in toAdd)
            {
                var dayProduct = new RetailProductSnapshot
                {
                    ProductForRetailId = pr.Id,
                };
                dayProduct.SetCreation(userName, "初始化");
                var product = products.SingleOrDefault(x => x.Id == pr.ProductId);
                if (product == null)
                {
                    throw new Exception($"未能找到产品, 零售产品ID: {pr.Id}");
                }
                SetRetailProductSnapshot(dayProduct, pr, product);
                item.RetailProductSnapshots.Add(dayProduct);
            }
        }

        private void SetRetailProductSnapshot(RetailProductSnapshot ps, ProductForRetail r, Product p)
        {
            ps.EquivalentBox = r.EquivalentBox;
            ps.Price = r.Price;
            ps.InternalPrice = r.InternalPrice;
            ps.BoxPrice = r.BoxPrice;
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
