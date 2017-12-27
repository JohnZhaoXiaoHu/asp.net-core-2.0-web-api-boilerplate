using System;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using SalesApi.Models.Retail;
using SalesApi.Repositories.Retail;
using SharedSettings.Tools;

namespace SalesApi.Services.Retail
{
    public interface IRetailOrderService
    {
        Task<RetailOrder> SaveOrderAsync(int retailProductSnapshotId, int productForRetailId, int retailAgentId, DateTime date,
            int ordered, int gift, string userName);
    }

    public class RetailOrderService : IRetailOrderService
    {
        private readonly IRetailOrderRepository _retailOrderRepository;
        private readonly IRetailPromotionEventRepository _retailPromotionEventRepository;
        private readonly IRetailPromotionGiftOrderRepository _retailPromotionGiftOrderRepository;

        public RetailOrderService(IRetailOrderRepository retailOrderRepository,
            IRetailPromotionEventRepository retailPromotionEventRepository,
            IRetailPromotionGiftOrderRepository retailPromotionGiftOrderRepository)
        {
            _retailOrderRepository = retailOrderRepository;
            _retailPromotionEventRepository = retailPromotionEventRepository;
            _retailPromotionGiftOrderRepository = retailPromotionGiftOrderRepository;
        }

        public async Task<RetailOrder> SaveOrderAsync(int retailProductSnapshotId, int productForRetailId, int retailerId, DateTime date,
            int ordered, int gift, string userName)
        {
            var dateStr = date.ToString(DateTools.OrderDateFormat);
            var retailOrder = await _retailOrderRepository.GetSingleAsync(x =>
                    x.RetailProductSnapshotId == retailProductSnapshotId && x.RetailerId == retailerId &&
                    x.Date == dateStr,
                x => x.RetailPromotionGiftOrders);
            var promotionEvent = await _retailPromotionEventRepository.GetSingleAsync(
                x => x.Date == date && x.ProductForRetailId == productForRetailId, x => x.RetailPromotionEventBonuses);
            if (retailOrder == null)
            {
                retailOrder = new RetailOrder
                {
                    RetailProductSnapshotId = retailProductSnapshotId,
                    RetailerId = retailerId,
                    Date = dateStr,
                    Ordered = ordered,
                    Gift = gift
                };
                if (promotionEvent != null)
                {
                    retailOrder.RetailPromotionEventId = promotionEvent.Id;
                    retailOrder.RetailPromotionGiftOrders = promotionEvent.RetailPromotionEventBonuses
                        .Select(x => new RetailPromotionGiftOrder
                        {
                            RetailPromotionEventBonusId = x.Id,
                            PromotionGift = ordered / promotionEvent.PurchaseBase * x.BonusCount,
                            CreateTime = DateTime.Now,
                            UpdateTime = DateTime.Now,
                            CreateUser = userName,
                            UpdateUser = userName,
                            LastAction = "Add"
                        }).ToList();
                }
                retailOrder.SetCreation(userName);
                _retailOrderRepository.Add(retailOrder);
            }
            else
            {
                if (promotionEvent != null)
                {
                    if (promotionEvent.Id != retailOrder.RetailPromotionEventId)
                    {
                        retailOrder.RetailPromotionEventId = promotionEvent.Id;
                    }
                    var dbOrderBonusIds = retailOrder.RetailPromotionGiftOrders
                        .Select(x => x.RetailPromotionEventBonusId).ToList();
                    var dbPromotionBonusIds = promotionEvent.RetailPromotionEventBonuses.Select(x => x.Id).ToList();
                    var toDeleteIds = dbOrderBonusIds.Except(dbPromotionBonusIds).ToList();
                    if (toDeleteIds.Any())
                    {
                        var toDeleteGiftOrders = retailOrder.RetailPromotionGiftOrders.Where(x => toDeleteIds.Contains(x.Id))
                            .ToList();
                        if (toDeleteGiftOrders.Any())
                        {
                            _retailPromotionGiftOrderRepository.DeleteRange(toDeleteGiftOrders);
                        }
                    }
                    foreach (var promotionEventBonus in promotionEvent.RetailPromotionEventBonuses)
                    {
                        var giftOrder = retailOrder.RetailPromotionGiftOrders.SingleOrDefault(x =>
                            x.RetailPromotionEventBonusId == promotionEventBonus.Id);
                        if (giftOrder == null)
                        {
                            giftOrder = new RetailPromotionGiftOrder
                            {
                                RetailOrderId = retailOrder.Id,
                                RetailPromotionEventBonusId = promotionEventBonus.Id,
                                PromotionGift = ordered / promotionEvent.PurchaseBase * promotionEventBonus.BonusCount,
                                CreateTime = DateTime.Now,
                                UpdateTime = DateTime.Now,
                                CreateUser = userName,
                                UpdateUser = userName,
                                LastAction = "Add"
                            };
                            _retailPromotionGiftOrderRepository.Add(giftOrder);
                        }
                        else
                        {
                            giftOrder.PromotionGift =
                                ordered / promotionEvent.PurchaseBase * promotionEventBonus.BonusCount;
                            giftOrder.SetModification(userName);
                            _retailPromotionGiftOrderRepository.Update(giftOrder);
                        }
                    }
                }
                retailOrder.Ordered = ordered;
                retailOrder.Gift = gift;
                retailOrder.SetModification(userName);
                _retailOrderRepository.Update(retailOrder);
            }
            return retailOrder;
        }

    }
}
