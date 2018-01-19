using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Features.Common;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Subscription.Promotion;
using SalesApi.Repositories.Subscription;
using SalesApi.Repositories.Subscription.Promotion;
using SalesApi.Services.Subscription;
using SalesApi.Shared.Enums;
using SalesApi.ViewModels.Subscription.Promotion;
using SalesApi.Web.Controllers.Bases;

namespace SalesApi.Web.Controllers.Subscription.Promotion
{
    [Route("api/sales/[controller]")]
    public class SubscriptionMonthPromotionController : SubscriptionController<SubscriptionMonthPromotionController>
    {
        private readonly ISubscriptionMonthPromotionRepository _subscriptionMonthPromotionRepository;
        private readonly ISubscriptionMonthPromotionBonusRepository _subscriptionMonthPromotionBonusRepository;
        private readonly ISubscriptionMonthPromotionBonusDateRepository _subscriptionMonthPromotionBonusDateRepository;
        private readonly IProductForSubscriptionRepository _productForSubscriptionRepository;

        public SubscriptionMonthPromotionController(ISubscriptionService<SubscriptionMonthPromotionController> subscriptionService,
            ISubscriptionMonthPromotionRepository subscriptionMonthPromotionRepository,
            ISubscriptionMonthPromotionBonusDateRepository subscriptionMonthPromotionBonusDateRepository,
            ISubscriptionMonthPromotionBonusRepository subscriptionMonthPromotionBonusRepository, 
            IProductForSubscriptionRepository productForSubscriptionRepository) : base(subscriptionService)
        {
            _subscriptionMonthPromotionRepository = subscriptionMonthPromotionRepository;
            _subscriptionMonthPromotionBonusDateRepository = subscriptionMonthPromotionBonusDateRepository;
            _subscriptionMonthPromotionBonusRepository = subscriptionMonthPromotionBonusRepository;
            _productForSubscriptionRepository = productForSubscriptionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _subscriptionMonthPromotionRepository.All.ToListAsync();
            var results = Mapper.Map<IEnumerable<SubscriptionMonthPromotionViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("{id}", Name = "GetSubscriptionMonthPromotion")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _subscriptionMonthPromotionRepository.GetSingleAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var result = Mapper.Map<SubscriptionMonthPromotionViewModel>(item);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SubscriptionMonthPromotionViewModel subscriptionMonthPromotionVm)
        {
            if (subscriptionMonthPromotionVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = Mapper.Map<SubscriptionMonthPromotion>(subscriptionMonthPromotionVm);
            newItem.SetCreation(UserName);
            foreach (var bonus in newItem.SubscriptionMonthPromotionBonuses)
            {
                bonus.SetCreation(UserName);
                foreach (var date in bonus.SubscriptionMonthPromotionBonusDates)
                {
                    date.SetCreation(UserName);
                }
            }
            _subscriptionMonthPromotionRepository.Add(newItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }

            var vm = Mapper.Map<SubscriptionMonthPromotionViewModel>(newItem);

            return CreatedAtRoute("GetSubscriptionMonthPromotion", new { id = vm.Id }, vm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SubscriptionMonthPromotionViewModel subscriptionMonthPromotionVm)
        {
            if (subscriptionMonthPromotionVm == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbItem = await _subscriptionMonthPromotionRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            Mapper.Map(subscriptionMonthPromotionVm, dbItem);
            dbItem.SetModification(UserName);
            _subscriptionMonthPromotionRepository.Update(dbItem);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "保存时出错");
            }
            var vm = Mapper.Map<SubscriptionMonthPromotionViewModel>(dbItem);
            return Ok(vm);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<SubscriptionMonthPromotionViewModel> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var dbItem = await _subscriptionMonthPromotionRepository.GetSingleAsync(id);
            if (dbItem == null)
            {
                return NotFound();
            }
            var toPatchVm = Mapper.Map<SubscriptionMonthPromotionViewModel>(dbItem);
            patchDoc.ApplyTo(toPatchVm, ModelState);

            TryValidateModel(toPatchVm);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map(toPatchVm, dbItem);

            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "更新时出错");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _subscriptionMonthPromotionRepository.GetSingleAsync(x => x.Id == id, x => x.SubscriptionMonthPromotionBonuses);
            if (model == null)
            {
                return NotFound();
            }
            var dates = await _subscriptionMonthPromotionBonusDateRepository.All
                .Where(x => x.SubscriptionMonthPromotionBonus.SubscriptionMonthPromotionId == id).ToListAsync();
            _subscriptionMonthPromotionBonusDateRepository.DeleteRange(dates);
            _subscriptionMonthPromotionBonusRepository.DeleteRange(model.SubscriptionMonthPromotionBonuses);
            _subscriptionMonthPromotionRepository.Delete(model);
            if (!await UnitOfWork.SaveAsync())
            {
                return StatusCode(500, "删除时出错");
            }
            return NoContent();
        }

        [HttpGet]
        [Route("NotDeleted")]
        public async Task<IActionResult> GetNotDeleted()
        {
            var items = await _subscriptionMonthPromotionRepository.All.Where(x => !x.Deleted).ToListAsync();
            var results = Mapper.Map<IEnumerable<SubscriptionMonthPromotionViewModel>>(items);
            return Ok(results);
        }

        [HttpGet]
        [Route("SubscriptionPromotionType")]
        public IActionResult GetSubscriptionMonthPromotionType()
        {
            var types = Enum.GetValues(typeof(SubscriptionPromotionType)).OfType<SubscriptionPromotionType>().Select(x => new KeyValuePair<string, SubscriptionPromotionType>(x.ToString(), x)).ToList();
            return Ok(types);
        }

        [HttpGet("ByYear/{year}")]
        public async Task<IActionResult> GetByYear(int year)
        {
            var items = await _subscriptionMonthPromotionRepository.All.Where(x => x.Year == year).ToListAsync();
            var results = Mapper.Map<IEnumerable<SubscriptionMonthPromotionViewModel>>(items);
            return Ok(results);
        }

        [HttpGet("ByYearAndMonth/{year}/{month}")]
        public async Task<IActionResult> GetByYearAndMonth(int year, int month)
        {
            var items = await _subscriptionMonthPromotionRepository.AllIncluding(x => x.SubscriptionMonthPromotionBonuses)
                .Where(x => x.Year == year && x.Month == month).ToListAsync();
            var ids = items.Select(x => x.Id).ToList();
            await _subscriptionMonthPromotionBonusDateRepository.All.Where(x =>
                ids.Contains(x.SubscriptionMonthPromotionBonus.SubscriptionMonthPromotionId)).LoadAsync();
            var results = Mapper.Map<IEnumerable<SubscriptionMonthPromotionViewModel>>(items);
            return Ok(results);
        }

        [HttpGet("ByMonthRange/{startYear}/{startMonth}/{endYear}/{endMonth}")]
        public async Task<IActionResult> GetByMonthRange(int startYear, int startMonth, int endYear, int endMonth)
        {
            var start = startYear * 100 + startMonth;
            var end = endYear * 100 + endMonth;
            var products = await _productForSubscriptionRepository.AllIncluding(x => x.Product).ToListAsync();
            var items = await _subscriptionMonthPromotionRepository.AllIncluding(x => x.SubscriptionMonthPromotionBonuses)
                .Where(x => x.Year * 100 + x.Month >= start && x.Year * 100 + x.Month <= end).ToListAsync();
            var ids = items.Select(x => x.Id).ToList();
            await _subscriptionMonthPromotionBonusDateRepository.All.Where(x =>
                ids.Contains(x.SubscriptionMonthPromotionBonus.SubscriptionMonthPromotionId)).LoadAsync();
            foreach (var promotion in items)
            {
                var product = products.Single(x => x.Id == promotion.ProductForSubscriptionId);
                var sb= new StringBuilder(product.Product.Name).AppendFormat("连续订购").AppendFormat(promotion.PromotionType.ToString()).AppendFormat("赠送:");
                var index = 0;
                foreach (var bonus in promotion.SubscriptionMonthPromotionBonuses)
                {
                    var bonusProduct = products.Single(x => x.Id == bonus.ProductForSubscriptionId);
                    var totalCount =
                        bonus.SubscriptionMonthPromotionBonusDates.Aggregate(0, (p, c) => p + c.DayBonusCount);
                    if (index > 0)
                    {
                        sb.AppendFormat("/n/r");
                    }
                    sb.AppendFormat(bonusProduct.Product.Name).AppendFormat("共")
                        .AppendFormat(totalCount.ToString()).AppendFormat("个, 派送日期:");
                    foreach (var date in bonus.SubscriptionMonthPromotionBonusDates)
                    {
                        sb.AppendFormat(" ").AppendFormat(date.Date.ToString("yyyy-MM-dd")).AppendFormat(":")
                            .AppendFormat(date.DayBonusCount.ToString()).AppendFormat("个;");
                    }
                    index++;
                }
            }
            var results = Mapper.Map<IEnumerable<SubscriptionMonthPromotionViewModel>>(items);
            return Ok(results);
        }
    }
}
