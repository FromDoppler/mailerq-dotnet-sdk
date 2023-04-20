using AutoFixture;
using EasyNetQ;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MailerQ.RabbitMQ
{
    public class EasyNetQQueueManagerTest
    {
        readonly Fixture specimens = new();

        EasyNetQQueueManager CreateSut(
            MailerQConfiguration mailerQConfiguration = null,
            IAdvancedBus advancedBus = null)
        {
            mailerQConfiguration ??= specimens.Create<MailerQConfiguration>();

            return new EasyNetQQueueManager(
                options: Options.Create(mailerQConfiguration),
                advancedBus: advancedBus ?? Mock.Of<IAdvancedBus>()
                );
        }

        [Fact]
        public async Task DeclareDeadLetterQueueForPublishAsync_should_declare_a_queue()
        {
            // Arrange
            var queueName = specimens.Create<string>();
            var advanceBusMock = new Mock<IAdvancedBus>();
            IQueueDeclarer sut = CreateSut(advancedBus: advanceBusMock.Object);

            // Act
            await sut.DeclareDeadLetterQueueForPublishAsync(queueName);

            // Assert
            advanceBusMock.Verify(bus =>
                bus.QueueDeclareAsync(
                    It.Is<string>(name => name.Equals(queueName)),
                    It.IsAny<Action<IQueueDeclareConfiguration>>(),
                    It.IsAny<CancellationToken>()),
                Times.Once());
        }

        [Fact]
        public void DeclareDeadLetterQueueForPublish_should_declare_a_queue()
        {
            // Arrange
            var queueName = specimens.Create<string>();
            var advanceBusMock = new Mock<IAdvancedBus>();
            IQueueDeclarer sut = CreateSut(advancedBus: advanceBusMock.Object);

            // Act
            sut.DeclareDeadLetterQueueForPublish(queueName);

            // Assert
            advanceBusMock.Verify(bus =>
                bus.QueueDeclareAsync(
                    It.Is<string>(name => name.Equals(queueName)),
                    It.IsAny<Action<IQueueDeclareConfiguration>>(),
                    It.IsAny<CancellationToken>()),
                Times.Once());
        }
    }
}
