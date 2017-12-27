using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using SalesApi.Models.County;
using SalesApi.Repositories.County;
using SharedSettings.Tools;

namespace SalesApi.Services.County
{
    public interface ICountyOrderService
    {
        Task<CountyOrder> SaveOrderAsync(int countyProductSnapshotId, int productForCountyId, int countyAgentId, DateTime date,
            int ordered, int gift, decimal price, string userName);
    }

    public class CountyOrderService : ICountyOrderService
    {
        private readonly ICountyOrderRepository _countyOrderRepository;
        private readonly ICountyPromotionEventRepository _countyPromotionEventRepository;
        private readonly ICountyPromotionGiftOrderRepository _countyPromotionGiftOrderRepository;

        public CountyOrderService(ICountyOrderRepository countyOrderRepository,
            ICountyPromotionEventRepository countyPromotionEventRepository,
            ICountyPromotionGiftOrderRepository countyPromotionGiftOrderRepository)
        {
            _countyOrderRepository = countyOrderRepository;
            _countyPromotionEventRepository = countyPromotionEventRepository;
            _countyPromotionGiftOrderRepository = countyPromotionGiftOrderRepository;
        }

        public async Task<CountyOrder> SaveOrderAsync(int countyProductSnapshotId, int productForCountyId, int countyAgentId, DateTime date,
            int ordered, int gift, decimal price, string userName)
        {
            var dateStr = date.ToString(DateTools.OrderDateFormat);
            var countyOrder = await _countyOrderRepository.GetSingleAsync(x =>
                    x.CountyProductSnapshotId == countyProductSnapshotId && x.CountyAgentId == countyAgentId &&
                    x.Date == dateStr,
                x => x.CountyPromotionGiftOrders);
            var promotionEvent = await _countyPromotionEventRepository.GetSingleAsync(
                x => x.Date == date && x.ProductForCountyId == productForCountyId, x => x.CountyPromotionEventBonuses);
            if (countyOrder == null)
            {
                countyOrder = new CountyOrder
                {
                    CountyProductSnapshotId = countyProductSnapshotId,
                    CountyAgentId = countyAgentId,
                    Date = dateStr,
                    Ordered = ordered,
                    Gift = gift,
                    Price = price
                };
                if (promotionEvent != null)
                {
                    countyOrder.CountyPromotionEventId = promotionEvent.Id;
                    countyOrder.CountyPromotionGiftOrders = promotionEvent.CountyPromotionEventBonuses
                        .Select(x => new CountyPromotionGiftOrder
                        {
                            CountyPromotionEventBonusId = x.Id,
                            PromotionGift = ordered / promotionEvent.PurchaseBase * x.BonusCount,
                            CreateTime = DateTime.Now,
                            UpdateTime = DateTime.Now,
                            CreateUser = userName,
                            UpdateUser = userName,
                            LastAction = "Add"
                        }).ToList();
                }
                countyOrder.SetCreation(userName);
                _countyOrderRepository.Add(countyOrder);
            }
            else
            {
                if (promotionEvent != null)
                {
                    if (promotionEvent.Id != countyOrder.CountyPromotionEventId)
                    {
                        countyOrder.CountyPromotionEventId = promotionEvent.Id;
                    }
                    var dbOrderBonusIds = countyOrder.CountyPromotionGiftOrders
                        .Select(x => x.CountyPromotionEventBonusId).ToList();
                    var dbPromotionBonusIds = promotionEvent.CountyPromotionEventBonuses.Select(x => x.Id).ToList();
                    var toDeleteIds = dbOrderBonusIds.Except(dbPromotionBonusIds).ToList();
                    if (toDeleteIds.Any())
                    {
                        var toDeleteGiftOrders = countyOrder.CountyPromotionGiftOrders.Where(x => toDeleteIds.Contains(x.Id))
                            .ToList();
                        if (toDeleteGiftOrders.Any())
                        {
                            _countyPromotionGiftOrderRepository.DeleteRange(toDeleteGiftOrders);
                        }
                    }
                    foreach (var promotionEventBonus in promotionEvent.CountyPromotionEventBonuses)
                    {
                        var giftOrder = countyOrder.CountyPromotionGiftOrders.SingleOrDefault(x =>
                            x.CountyPromotionEventBonusId == promotionEventBonus.Id);
                        if (giftOrder == null)
                        {
                            giftOrder = new CountyPromotionGiftOrder
                            {
                                CountyOrderId = countyOrder.Id,
                                CountyPromotionEventBonusId = promotionEventBonus.Id,
                                PromotionGift = ordered / promotionEvent.PurchaseBase * promotionEventBonus.BonusCount,
                                CreateTime = DateTime.Now,
                                UpdateTime = DateTime.Now,
                                CreateUser = userName,
                                UpdateUser = userName,
                                LastAction = "Add"
                            };
                            _countyPromotionGiftOrderRepository.Add(giftOrder);
                        }
                        else
                        {
                            giftOrder.PromotionGift =
                                ordered / promotionEvent.PurchaseBase * promotionEventBonus.BonusCount;
                            giftOrder.SetModification(userName);
                            _countyPromotionGiftOrderRepository.Update(giftOrder);
                        }
                    }
                }
                countyOrder.Ordered = ordered;
                countyOrder.Gift = gift;
                countyOrder.Price = price;
                countyOrder.SetModification(userName);
                _countyOrderRepository.Update(countyOrder);
            }
            return countyOrder;
        }

    }
}
