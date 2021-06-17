using Domain.Models;
using Server.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Server.Tests
{
    public class CounterServiceUnitTest
    {
        private CounterService counterService;

        public CounterServiceUnitTest()
        {
            counterService = new CounterService();
        }

        [Fact]
        public void Increment_When_Inc_Is_Zero_Throw_NullReferenceException()
        {
            // Arrange
            var request = new IncrementRequest();

            // Act-Assert
            var result = Assert.Throws<NullReferenceException>((() =>
                         counterService.Increment(request)));
        }

        [Fact]
        public void Increment_When_Inc_Is_Greater_Than_Zero_Return_IncrementResponce()
        {
            // Arrange
            var request = new IncrementRequest { Inc = 3 };

            // Assert
            var counterServiceResult = counterService.Increment(request);

            var expected = new IncrementResponce { Result = 3 };

            // Act
            Assert.Equal(counterServiceResult.Result, expected.Result);
        }


        [Fact]
        public void Decrement_When_Dec_Is_Zero_Throw_NullReferenceException()
        {
            //Arrange
            var request = new DecrementRequest(0);

            // Act-Assert
            var result = Assert.Throws<NullReferenceException>((() =>
                         counterService.Decrement(request)));
        }
        [Fact]
        public void Decrement_When_Dec_Is_Greater_Than_Zero_Return_IncrementResponce()
        {
            // Arrange
            var request = new DecrementRequest(3);

            // Assert
            var counterServiceResult = counterService.Decrement(request);

            var expected = new DecrementResponse(-3);

            // Act
            Assert.Equal(counterServiceResult.Result, expected.Result);
        }
    }
}
