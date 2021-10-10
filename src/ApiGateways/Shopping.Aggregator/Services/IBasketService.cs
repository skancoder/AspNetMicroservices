using Shopping.Aggregator.Models;
using System;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    public interface IBasketService
    {
        Task<BasketModel> GetBasket(string userName);
    }
}
