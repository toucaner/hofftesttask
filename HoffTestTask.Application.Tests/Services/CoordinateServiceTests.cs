using HoffTestTask.Application.Services.Coordinate;
using HoffTestTask.Application.Services.Coordinate.Models;
using HoffTestTask.Infrastructure.Enums;
using HoffTestTask.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace HoffTestTask.Application.Tests.Services;

public class CoordinateServiceTests
{
    private const int Radius = 2;
    private readonly ICoordinateService _coordinateService;

    public CoordinateServiceTests()
    {
        var geometryOptions = Options.Create(new GeometryOptions { Radius = Radius });
        _coordinateService = new CoordinateService(geometryOptions);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(1, -1)]
    [InlineData(-1, -1)]
    [InlineData(-1, 1)]
    public void CoordinatesInCircle_Success(int x, int y)
    {
        var coordinates = new Coordinate { X = x, Y = y };

        var result = _coordinateService.GetDateByCoordinates(coordinates);

        Assert.NotNull(result);
        Assert.IsType<ExchangeDate>(result.Data);
        Assert.True(result.Success);
    }

    [Theory]
    [InlineData(2, 2)]
    [InlineData(2, -2)]
    [InlineData(-2, -2)]
    [InlineData(-2, 2)]
    public void CoordinatesInCircle_Failure(int x, int y)
    {
        var coordinates = new Coordinate { X = x, Y = y };

        var result = _coordinateService.GetDateByCoordinates(coordinates);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Message);
        Assert.False(result.Success);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    public void CoordinatesIncorrect(int x, int y)
    {
        var coordinates = new Coordinate { X = x, Y = y };

        var result = _coordinateService.GetDateByCoordinates(coordinates);

        Assert.NotNull(result);
        Assert.NotEmpty(result.Message);
        Assert.False(result.Success);
    }

    [Theory]
    [InlineData(1, 1)]
    public void FirstCoordinateQuarter_ExchangeDate_Today(int x, int y)
    {
        var coordinates = new Coordinate { X = x, Y = y };

        var result = _coordinateService.GetDateByCoordinates(coordinates);

        Assert.NotNull(result);
        Assert.IsType<ExchangeDate>(result.Data);
        Assert.Equal(ExchangeDate.Today, result.Data);
        Assert.True(result.Success);
    }

    [Theory]
    [InlineData(-1, 1)]
    public void SecondCoordinateQuarter_ExchangeDate_Yesterday(int x, int y)
    {
        var coordinates = new Coordinate { X = x, Y = y };

        var result = _coordinateService.GetDateByCoordinates(coordinates);

        Assert.NotNull(result);
        Assert.IsType<ExchangeDate>(result.Data);
        Assert.Equal(ExchangeDate.Yesterday, result.Data);
        Assert.True(result.Success);
    }

    [Theory]
    [InlineData(-1, -1)]
    public void ThirdCoordinateQuarter_ExchangeDate_DayBeforeYesterday(int x, int y)
    {
        var coordinates = new Coordinate { X = x, Y = y };

        var result = _coordinateService.GetDateByCoordinates(coordinates);

        Assert.NotNull(result);
        Assert.IsType<ExchangeDate>(result.Data);
        Assert.Equal(ExchangeDate.DayBeforeYesterday, result.Data);
        Assert.True(result.Success);
    }

    [Theory]
    [InlineData(1, -1)]
    public void FourthCoordinateQuarter_ExchangeDate_Tomorrow(int x, int y)
    {
        var coordinates = new Coordinate { X = x, Y = y };

        var result = _coordinateService.GetDateByCoordinates(coordinates);

        Assert.NotNull(result);
        Assert.IsType<ExchangeDate>(result.Data);
        Assert.Equal(ExchangeDate.Tomorrow, result.Data);
        Assert.True(result.Success);
    }
}