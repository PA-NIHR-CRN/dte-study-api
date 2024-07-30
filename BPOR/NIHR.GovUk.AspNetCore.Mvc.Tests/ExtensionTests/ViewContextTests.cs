using System.ComponentModel.DataAnnotations;
using EmptyModelMetadataProvider = Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider;
using ModelStateDictionary = Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary;
using ViewContext = Microsoft.AspNetCore.Mvc.Rendering.ViewContext;
using ViewDataDictionary = Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary;

namespace NIHR.GovUk.AspNetCore.Mvc.Tests.ExtensionTests;

public class ViewContextTests
{
    public class ErrorSummaryTestModel
    {
        [Display(Order = 1)]
        public ErrorSummaryNestedTestModel Parent1 { get; set; } = new();

        [Display(Order = 2)] 
        public ErrorSummaryNestedTestModel Parent2 { get; set; } = new();
        
        public ErrorSummaryNestedTestModel Parent3 { get; set; } = new();
    }

    public class ErrorSummaryNestedTestModel()
    {
        [Display(Order = 1)]
        public string Child1 { get; set; }
        
        [Display(Order = 2)]
        public string Child2 { get; set; }
        
        public string Child3 { get; set; }
        
        [Display(Order = 4)]
        public ErrorSummaryFurtherNestedTestModel Child4 { get; set; } = new();
    }
    
    public class ErrorSummaryFurtherNestedTestModel()
    {
        [Display(Order = 1)]
        public string SubChild1 { get; set; }
        
        [Display(Order = 2)]
        public string SubChild2 { get; set; }
        
        public string SubChild3 { get; set; }
    }
    
    [Fact]
    public void Errors_ShouldGenerateOrderedListOfErrors_WhenPropertiesHaveDisplayOrder()
    {
        // Arrange
        var modelState = new ModelStateDictionary();
        modelState.AddModelError("Parent3", "Error message 1");
        modelState.AddModelError("Parent1", "Error message 2");
        modelState.AddModelError("Parent2", "Error message 3");
        
        var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), modelState)
        {
            Model = new ErrorSummaryTestModel()
        };

        var viewContext = new ViewContext
        {
            ViewData = viewData
        };

        // Act
        var orderedErrors = viewContext.Errors();

        // Assert
        Assert.NotNull(orderedErrors);
        Assert.Equal(3, orderedErrors.Count());
        Assert.Equal("Parent1", orderedErrors.First().Key);
        Assert.Equal("Error message 2", orderedErrors.First().Value.Errors.First().ErrorMessage);
        Assert.Equal("Parent2", orderedErrors.Skip(1).First().Key);
        Assert.Equal("Error message 3", orderedErrors.Skip(1).First().Value.Errors.First().ErrorMessage);
        Assert.Equal("Parent3", orderedErrors.Skip(2).First().Key);
        Assert.Equal("Error message 1", orderedErrors.Skip(2).First().Value.Errors.First().ErrorMessage);
    }
    
    [Fact]
    public void Errors_ShouldGenerateOrderedListOfErrors_WhenNestedPropertiesExistWithNoDisplayAttribute()
    {
        // Arrange
        var modelState = new ModelStateDictionary();
        modelState.AddModelError("Parent3.Child1", "Error message 1");
        modelState.AddModelError("Parent1.Child1", "Error message 2");
        
        var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), modelState)
        {
            Model = new ErrorSummaryTestModel()
        };

        var viewContext = new ViewContext
        {
            ViewData = viewData
        };

        // Act
        var orderedErrors = viewContext.Errors();

        // Assert
        Assert.NotNull(orderedErrors);
        Assert.Equal(2, orderedErrors.Count());
        Assert.Equal("Parent1.Child1", orderedErrors.First().Key);
        Assert.Equal("Error message 2", orderedErrors.First().Value.Errors.First().ErrorMessage);
        Assert.Equal("Parent3.Child1", orderedErrors.Skip(1).First().Key);
        Assert.Equal("Error message 1", orderedErrors.Skip(1).First().Value.Errors.First().ErrorMessage);
    }
    
    [Fact]
    public void Errors_ShouldGenerateOrderedListOfErrors_WhenNestedPropertiesExist()
    {
        // Arrange
        var modelState = new ModelStateDictionary();
        modelState.AddModelError("Parent3.Child1", "Error message 1");
        modelState.AddModelError("Parent1.Child1", "Error message 2");
        
        var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), modelState)
        {
            Model = new ErrorSummaryTestModel()
        };

        var viewContext = new ViewContext
        {
            ViewData = viewData
        };

        // Act
        var orderedErrors = viewContext.Errors();

        // Assert
        Assert.NotNull(orderedErrors);
        Assert.Equal(2, orderedErrors.Count());
        Assert.Equal("Parent1.Child1", orderedErrors.First().Key);
        Assert.Equal("Error message 2", orderedErrors.First().Value.Errors.First().ErrorMessage);
        Assert.Equal("Parent3.Child1", orderedErrors.Skip(1).First().Key);
        Assert.Equal("Error message 1", orderedErrors.Skip(1).First().Value.Errors.First().ErrorMessage);
    }
    
    [Fact]
    public void Errors_ShouldGenerateOrderedListOfErrors_WhenFurtherNestedPropertiesExist()
    {
        // Arrange
        var modelState = new ModelStateDictionary();
        modelState.AddModelError("Parent3", "Error message 1");
        modelState.AddModelError("Parent1.Child4", "Error message 2");
        modelState.AddModelError("Parent1.Child4.SubChild3", "Error message 3");
        modelState.AddModelError("Parent1.Child4.SubChild1", "Error message 4");
        
        var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), modelState)
        {
            Model = new ErrorSummaryTestModel()
        };

        var viewContext = new ViewContext
        {
            ViewData = viewData
        };

        // Act
        var orderedErrors = viewContext.Errors();

        // Assert
        Assert.NotNull(orderedErrors);
        Assert.Equal(4, orderedErrors.Count());
        Assert.Equal("Parent1.Child4", orderedErrors.First().Key);
        Assert.Equal("Error message 2", orderedErrors.First().Value.Errors.First().ErrorMessage);
        Assert.Equal("Parent1.Child4.SubChild1", orderedErrors.Skip(1).First().Key);
        Assert.Equal("Error message 4", orderedErrors.Skip(1).First().Value.Errors.First().ErrorMessage);
        Assert.Equal("Parent1.Child4.SubChild3", orderedErrors.Skip(2).First().Key);
        Assert.Equal("Error message 3", orderedErrors.Skip(2).First().Value.Errors.First().ErrorMessage);
        Assert.Equal("Parent3", orderedErrors.Skip(3).First().Key);
        Assert.Equal("Error message 1", orderedErrors.Skip(3).First().Value.Errors.First().ErrorMessage);
    }
    
}