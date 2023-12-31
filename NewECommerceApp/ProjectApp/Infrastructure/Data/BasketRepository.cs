﻿using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IMemoryCache _cache;
        public BasketRepository(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            try
            {
                _cache.Remove(basketId);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            return _cache.Get<CustomerBasket>(basketId);

        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            _cache.Set(basket.Id, basket);

            return await GetBasketAsync(basket.Id);
        }
    }
}
