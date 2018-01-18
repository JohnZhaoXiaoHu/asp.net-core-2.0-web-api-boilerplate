using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Subscription;
using SalesApi.Models.Settings;
using SalesApi.Repositories.Subscription;
using SalesApi.Repositories.Settings;
using SalesApi.Shared.Enums;

namespace SalesApi.Services.Subscription
{
    public interface ISubscriptionDayService
    {
        Task Initialzie(DateTime date, string userName);
        Task Confirm(DateTime date, string userName);
    }

    public class SubscriptionDayService : ISubscriptionDayService
    {
        private readonly ISubscriptionDayRepository _collectiveDayRepository;
        private readonly ISubscriptionProductSnapshotRepository _collectiveProductSnapshotRepository;
        private readonly IProductForSubscriptionRepository _productForSubscriptionRepository;
        private readonly IProductRepository _productRepository;

        public SubscriptionDayService(
            ISubscriptionDayRepository collectiveDayRepository, 
            ISubscriptionProductSnapshotRepository collectiveProductSnapshotRepository, 
            IProductForSubscriptionRepository productForSubscriptionRepository, 
            IProductRepository productRepository)
        {
            _collectiveDayRepository = collectiveDayRepository;
            _collectiveProductSnapshotRepository = collectiveProductSnapshotRepository;
            _productForSubscriptionRepository = productForSubscriptionRepository;
            _productRepository = productRepository;
        }

        public async Task Initialzie(DateTime date, string userName)
        {
            var dateStr = date.ToString("yyyy-MM-dd");
            var item = await _collectiveDayRepository.GetSingleAsync(x => x.Date == dateStr, x => x.SubscriptionProductSnapshots);
            var products = await _productRepository.All.Where(x => !x.Deleted).ToListAsync();
            var productForSubscriptions = await _productForSubscriptionRepository.All.Where(x => !x.Deleted).ToListAsync();
            if (item != null)
            {
                if (item.Status >= SubscriptionDayStatus.已初始化)
                {
                    throw new Exception("该专送日已经初始化, 操作失败");
                }
                item.Status = SubscriptionDayStatus.已初始化;
                item.SetModification(userName, "初始化");
                InitializeSubscriptionProducts(userName, item, productForSubscriptions, products);
                _collectiveDayRepository.Update(item);
            }
            else
            {
                item = new SubscriptionDay
                {
                    Date = dateStr,
                    Status = SubscriptionDayStatus.已初始化
                };
                item.SetCreation(userName, "初始化");
                InitializeSubscriptionProducts(userName, item, productForSubscriptions, products);
                _collectiveDayRepository.Add(item);
            }
        }

        public async Task Confirm(DateTime date, string userName)
        {
            var dateStr = date.ToString("yyyy-MM-dd");
            var item = await _collectiveDayRepository.GetSingleAsync(x => x.Date == dateStr, x => x.SubscriptionProductSnapshots);
            if (item != null)
            {
                if (item.Status >= SubscriptionDayStatus.已报货)
                {
                    throw new Exception("该专送日已报货, 操作失败");
                }
                item.Status = SubscriptionDayStatus.已报货;
                item.SetModification(userName, "报货");
                _collectiveDayRepository.Update(item);
            }
            else
            {
                throw new Exception("该专送日还未初始化 操作失败");
            }
        }

        private void InitializeSubscriptionProducts(string userName, SubscriptionDay item, List<ProductForSubscription> productForSubscriptions, List<Product> products)
        {
            var dbDayProducts = item.SubscriptionProductSnapshots;
            foreach (var dayProduct in dbDayProducts)
            {
                var productForSubscription =
                    productForSubscriptions.SingleOrDefault(x => x.Id == dayProduct.ProductForSubscriptionId);
                if (productForSubscription == null)
                {
                    throw new Exception($"未能找到商超产品: {dayProduct.Name}");
                }
                var product = products.SingleOrDefault(x => x.Id == productForSubscription.ProductId);
                if (product == null)
                {
                    throw new Exception($"未能找到产品: {dayProduct.Name}");
                }
                SetSubscriptionProductSnapshot(dayProduct, productForSubscription, product);
                dayProduct.SetModification(userName, "重新初始化");
                _collectiveProductSnapshotRepository.Update(dayProduct);
            }
            var dayProductIds = dbDayProducts.Select(x => x.ProductForSubscriptionId).ToList();
            var collectiveProductIds = productForSubscriptions.Select(x => x.Id).ToList();
            var toAddIds = collectiveProductIds.Except(dayProductIds).ToList();
            var toAdd = productForSubscriptions.Where(x => toAddIds.Contains(x.Id)).ToList();
            foreach (var pr in toAdd)
            {
                var dayProduct = new SubscriptionProductSnapshot
                {
                    ProductForSubscriptionId = pr.Id,
                };
                dayProduct.SetCreation(userName, "初始化");
                var product = products.SingleOrDefault(x => x.Id == pr.ProductId);
                if (product == null)
                {
                    throw new Exception($"未能找到产品, 专送产品ID: {pr.Id}");
                }
                SetSubscriptionProductSnapshot(dayProduct, pr, product);
                item.SubscriptionProductSnapshots.Add(dayProduct);
            }
        }

        private void SetSubscriptionProductSnapshot(SubscriptionProductSnapshot ps, ProductForSubscription r, Product p)
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
