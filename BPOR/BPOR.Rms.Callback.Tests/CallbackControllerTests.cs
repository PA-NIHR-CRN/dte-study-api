using BPOR.Rms.Callback.Controllers;
using BPOR.Rms.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using NIHR.NotificationService.Settings;
using System.Net;
using Moq.Protected;

namespace BPOR.Rms.Callback.Tests;

public class CallbackControllerTests
{
    private readonly Mock<IHttpClientFactory> _mockHttpClientFactory;
    private readonly CallbackController _controller;

    public CallbackControllerTests()
    {
        var settings = new NotificationServiceSettings { BearerToken = "valid_token" };
        Mock<IOptions<NotificationServiceSettings>> mockSettings = new();
        mockSettings.Setup(s => s.Value).Returns(settings);

        _mockHttpClientFactory = new Mock<IHttpClientFactory>();

        _controller = new CallbackController(mockSettings.Object, _mockHttpClientFactory.Object);
    }

    private void SetupHttpContext(string token)
    {
        var context = new DefaultHttpContext();
        if (!string.IsNullOrEmpty(token))
        {
            context.Request.Headers.Authorization = $"Bearer {token}";
        }
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = context
        };
    }

    private HttpClient CreateMockHttpClient(HttpStatusCode statusCode, string content = "")
    {
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(content)
            });

        return new HttpClient(mockHttpMessageHandler.Object);
    }

    [Theory]
    [InlineData(null, typeof(UnauthorizedResult))]
    [InlineData("invalid_token", typeof(UnauthorizedResult))]
    public async Task DeliveryStatus_ReturnsUnauthorized_WhenTokenIsInvalidOrMissing(string token, Type expectedResult)
    {
        // Arrange
        SetupHttpContext(token);
        var message = new NotifyCallbackMessage();

        // Act
        var result = await _controller.DeliveryStatus(message, CancellationToken.None);

        // Assert
        Assert.IsType(expectedResult, result);
    }

    [Theory]
    [InlineData("", typeof(BadRequestObjectResult))]
    [InlineData("unknown-env-reference", typeof(BadRequestObjectResult))]
    public async Task DeliveryStatus_ReturnsBadRequest_WhenReferenceIsInvalidOrUnknown(string reference, Type expectedResult)
    {
        // Arrange
        SetupHttpContext("valid_token");
        var message = new NotifyCallbackMessage { Reference = reference };

        // Act
        var result = await _controller.DeliveryStatus(message, CancellationToken.None);

        // Assert
        Assert.IsType(expectedResult, result);
    }

    [Fact]
    public async Task DeliveryStatus_ReturnsOk_WhenRequestIsValid()
    {
        // Arrange
        SetupHttpContext("valid_token");
        var message = new NotifyCallbackMessage { Reference = "dev-valid-reference" };
        var httpClient = CreateMockHttpClient(HttpStatusCode.OK, "response content");
        _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        // Act
        var result = await _controller.DeliveryStatus(message, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        Assert.Equal("response content", okResult.Value);
    }

    [Fact]
    public async Task DeliveryStatus_HandlesHttpClientErrorGracefully()
    {
        // Arrange
        SetupHttpContext("valid_token");
        var message = new NotifyCallbackMessage { Reference = "dev-valid-reference" };
        var httpClient = CreateMockHttpClient(HttpStatusCode.InternalServerError, "error response");
        _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        // Act
        var result = await _controller.DeliveryStatus(message, CancellationToken.None);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal((int)HttpStatusCode.InternalServerError, objectResult.StatusCode);
        Assert.Equal("error response", objectResult.Value);
    }

    [Theory]
    [InlineData("dev-some-reference", "https://dev.studies.bepartofresearch.nihr.ac.uk/NotifyCallback/ReceiveCallback")]
    [InlineData("test-some-reference", "https://test.studies.bepartofresearch.nihr.ac.uk/NotifyCallback/ReceiveCallback")]
    [InlineData("uat-some-reference", "https://uat.studies.bepartofresearch.nihr.ac.uk/NotifyCallback/ReceiveCallback")]
    [InlineData("oat-some-reference", "https://oat.studies.bepartofresearch.nihr.ac.uk/NotifyCallback/ReceiveCallback")]
    [InlineData("prod-some-reference", "https://studies.bepartofresearch.nihr.ac.uk/NotifyCallback/ReceiveCallback")]
    [InlineData("unknown-some-reference", "")]
    public void GetTargetUrl_ReturnsCorrectUrl(string reference, string expectedUrl)
    {
        // Act
        var result = CallbackController.GetTargetUrl(reference);

        // Assert
        Assert.Equal(expectedUrl, result);
    }

    [Fact]
    public async Task DeliveryStatus_CopiesRequestHeaders()
    {
        // Arrange
        SetupHttpContext("valid_token");
        _controller.ControllerContext.HttpContext.Request.Headers["Custom-Header"] = "CustomValue";
        var message = new NotifyCallbackMessage { Reference = "dev-valid-reference" };

        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .Callback<HttpRequestMessage, CancellationToken>((request, token) =>
            {
                // Assert
                Assert.True(request.Headers.Contains("Custom-Header"));
                Assert.Equal("CustomValue", request.Headers.GetValues("Custom-Header").First());
            })
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("response content")
            });

        var httpClient = new HttpClient(mockHttpMessageHandler.Object);
        _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

        // Act
        await _controller.DeliveryStatus(message, CancellationToken.None);
    }
}
