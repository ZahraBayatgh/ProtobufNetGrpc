using Domain.IServices;
using Domain.Models;
using System;

namespace Server.Services
{
    public class CounterService : ICounterService
    {
        private int counter = 0;

        public IncrementResponce Increment(IncrementRequest request)
        {
            try
            {
                if (request.Inc == 0)
                    throw new NullReferenceException("IncrementRequest model is null");

                counter += request.Inc;

                var result = new IncrementResponce { Result = counter };
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public DecrementResponse Decrement(DecrementRequest request)
        {
            try
            {
                if (request.Dec == 0)
                    throw new NullReferenceException("DecrementRequest model is null");

                counter -= request.Dec;

                var result = new DecrementResponse(counter);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
