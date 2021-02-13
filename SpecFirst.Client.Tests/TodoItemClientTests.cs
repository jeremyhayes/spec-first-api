using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Xunit;

namespace SpecFirst.Client.Tests
{
    public class TodoItemClientTests
    {
        [Fact]
        public async void ShouldGetAllTodoItemsForList()
        {
            // Arrange
            var mockResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(await File.ReadAllTextAsync(@"./data/response/todo-items.json"))
            };

            var mockHandler = SetupHandler(mockResponse);
            using var httpClient = new HttpClient(mockHandler.Object, false)
            {
                BaseAddress = new Uri("https://todo.domain.tld/")
            };

            // Act
            var todoListId = 123;
            var target = new TodoItemClient(httpClient);
            var result = await target.TodoItemAsync(todoListId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            VerifyHandler(mockHandler, HttpMethod.Get, $"https://todo.domain.tld/todo-list/{todoListId}/todo-item");
        }


        private static Mock<HttpMessageHandler> SetupHandler(HttpResponseMessage responseMessage)
        {
            var mockHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage)
                .Verifiable();

            return mockHandler;
        }

        private static void VerifyHandler(Mock<HttpMessageHandler> mockHandler, HttpMethod expectedMethod, string expectedRequestUri)
        {
            mockHandler.Protected().Verify(
                "SendAsync",
                Times.Once(),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == expectedMethod
                    && req.RequestUri == new Uri(expectedRequestUri)
                ),
                ItExpr.IsAny<CancellationToken>());
        }
    }
}
