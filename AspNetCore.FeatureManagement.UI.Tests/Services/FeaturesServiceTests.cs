using AspNetCore.FeatureManagement.UI.Core.Data;
using AspNetCore.FeatureManagement.UI.Services;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCore.FeatureManagement.UI.Tests.Services
{
    public class FeaturesServiceTests
    {
        private readonly FeatureManagementDb _db;

        public FeaturesServiceTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FeatureManagementDb>();
            optionsBuilder.UseInMemoryDatabase("FeatureManagement");

            _db = new FeatureManagementDb(optionsBuilder.Options);

            // Delete existing db before creating a new one
            _db.Database.EnsureDeleted();
            _db.Database.EnsureCreated();
        }

        private void InitializeFeaturesData()
        {
            var themes = new List<string>
            {
                "light",
                "dark"
            };
            var delays = new List<int>
            {
                500,
                1000,
                1500,
                2000
            };
            var percents = new List<decimal>
            {
                10,
                12.5m,
                15,
                17.5m,
                20
            };

            _db.Features.Add(new Feature
            {
                Name = "Beta",
                Type = FeatureTypes.Boolean,
                BooleanValue = true
            });
            _db.Features.Add(new Feature
            {
                Name = "Theme",
                Description = "Choose a theme for the frontend",
                Type = FeatureTypes.String,
                StringValue = themes[0],
                StringFeatureChoices = themes
                    .Select(theme => new StringFeatureChoice
                    {
                        Choice = theme
                    })
                    .ToList()
            });
            _db.Features.Add(new Feature
            {
                Name = "WelcomeMessage",
                Type = FeatureTypes.String,
                StringValue = "Welcome to my Blog"
            });
            _db.Features.Add(new Feature
            {
                Name = "Delay",
                Description = "Animation delay (in ms)",
                Type = FeatureTypes.Integer,
                IntValue = delays[1],
                IntFeatureChoices = delays
                    .Select(delay => new IntFeatureChoice
                    {
                        Choice = delay
                    })
                    .ToList()
            });
            _db.Features.Add(new Feature
            {
                Name = "TaxPercent",
                Description = "Tax percentage",
                Type = FeatureTypes.Decimal,
                DecimalValue = percents[1],
                DecimalFeatureChoices = percents
                    .Select(percent => new DecimalFeatureChoice
                    {
                        Choice = percent
                    })
                    .ToList()
            });

            _db.SaveChanges();
        }

        [Fact]
        public async Task GetAllShouldReturnZeroFeatures()
        {
            // Arrange
            var featuresService = new FeaturesService(_db);

            // Act
            var features = await featuresService.GetAll();

            // Assert
            features.ShouldBeEmpty();
        }

        [Fact]
        public async Task GetAllShouldReturnFiveFeatures()
        {
            // Arrange
            InitializeFeaturesData();
            var featuresService = new FeaturesService(_db);

            // Act
            var features = await featuresService.GetAll();

            // Assert
            features.Count.ShouldBe(5);
        }

        [Fact]
        public async Task GetShouldReturnAnExistingFeature()
        {
            // Arrange
            InitializeFeaturesData();
            var featuresService = new FeaturesService(_db);

            // Act
            var feature = await featuresService.Get("Beta");

            // Assert
            feature.ShouldNotBeNull();
        }

        [Fact]
        public async Task GetShouldReturnNullIfNoFeatureFound()
        {
            // Arrange
            InitializeFeaturesData();
            var featuresService = new FeaturesService(_db);

            // Act
            var feature = await featuresService.Get("X");

            // Assert
            feature.ShouldBeNull();
        }

        [Fact]
        public async Task GetValueShouldReturnBooleanValue()
        {
            // Arrange
            InitializeFeaturesData();
            var featuresService = new FeaturesService(_db);

            // Act
            bool value = await featuresService.GetValue<bool>("Beta");

            // Assert
            value.ShouldBeTrue();
        }
        [Fact]
        public async Task GetValueShouldFailIfMismatchTypeForBooleanValue()
        {
            // Arrange
            InitializeFeaturesData();
            var featuresService = new FeaturesService(_db);

            // Act
            var exception = await featuresService.GetValue<string>("Beta").ShouldThrowAsync<Exception>();

            // Assert
            exception.Message.ShouldBe("The feature Beta is a boolean feature...");
        }

        [Fact]
        public async Task GetValueShouldReturnIntValue()
        {
            // Arrange
            InitializeFeaturesData();
            var featuresService = new FeaturesService(_db);

            // Act
            int value = await featuresService.GetValue<int>("Delay");

            // Assert
            value.ShouldBe(1000);
        }
        [Fact]
        public async Task GetValueShouldFailIfMismatchTypeForIntValue()
        {
            // Arrange
            InitializeFeaturesData();
            var featuresService = new FeaturesService(_db);

            // Act
            var exception = await featuresService.GetValue<bool>("Delay").ShouldThrowAsync<Exception>();

            // Assert
            exception.Message.ShouldBe("The feature Delay is not an integer feature...");
        }

        [Fact]
        public async Task GetValueShouldReturnDecimalValue()
        {
            // Arrange
            InitializeFeaturesData();
            var featuresService = new FeaturesService(_db);

            // Act
            decimal value = await featuresService.GetValue<decimal>("TaxPercent");

            // Assert
            value.ShouldBe(12.5m);
        }
        [Fact]
        public async Task GetValueShouldFailIfMismatchTypeForDecimalValue()
        {
            // Arrange
            InitializeFeaturesData();
            var featuresService = new FeaturesService(_db);

            // Act
            var exception = await featuresService.GetValue<int>("TaxPercent").ShouldThrowAsync<Exception>();

            // Assert
            exception.Message.ShouldBe("The feature TaxPercent is not a decimal feature...");
        }

        [Fact]
        public async Task GetValueShouldReturnStringValue()
        {
            // Arrange
            InitializeFeaturesData();
            var featuresService = new FeaturesService(_db);

            // Act
            string value = await featuresService.GetValue<string>("Theme");

            // Assert
            value.ShouldBe("light");
        }
        [Fact]
        public async Task GetValueShouldFailIfMismatchTypeForStringValue()
        {
            // Arrange
            InitializeFeaturesData();
            var featuresService = new FeaturesService(_db);

            // Act
            var exception = await featuresService.GetValue<decimal>("Theme").ShouldThrowAsync<Exception>();

            // Assert
            exception.Message.ShouldBe("The feature Theme is not a string feature...");
        }

        [Fact]
        public async Task GetValueShouldFailIfNoFeatureFound()
        {
            // Arrange
            InitializeFeaturesData();
            var featuresService = new FeaturesService(_db);

            string featureName = "X";

            // Act
            var exception = await featuresService.GetValue<string>(featureName).ShouldThrowAsync<Exception>();

            // Assert
            exception.Message.ShouldBe($"The feature {featureName} does not exist...");
        }

        [Fact]
        public async Task SetValueShouldUpdateBooleanFeature()
        {
            // Arrange
            InitializeFeaturesData();
            var featuresService = new FeaturesService(_db);

            // Act
            var result = await featuresService.SetValue("Beta", false);

            // Assert
            result.BooleanValue.Value.ShouldBeFalse();
            result.IntValue.ShouldBeNull();
            result.DecimalValue.ShouldBeNull();
            result.StringValue.ShouldBeNull();
        }
        [Fact]
        public async Task SetValueShouldFailIfMismatchTypeForBooleanValue()
        {
            // Arrange
            InitializeFeaturesData();
            var featuresService = new FeaturesService(_db);

            // Act
            var exception = await featuresService.SetValue("Beta", 10).ShouldThrowAsync<Exception>();

            // Assert
            exception.Message.ShouldBe("The feature Beta is a boolean feature...");
        }
        
        [Fact]
        public async Task SetValueShouldUpdateIntFeature()
        {
            // Arrange
            InitializeFeaturesData();
            var featuresService = new FeaturesService(_db);

            int newValue = 500;

            // Act
            var result = await featuresService.SetValue("Delay", newValue);

            // Assert
            result.BooleanValue.ShouldBeNull();
            result.IntValue.ShouldBe(newValue);
            result.DecimalValue.ShouldBeNull();
            result.StringValue.ShouldBeNull();
        }
        [Fact]
        public async Task SetValueShouldFailIfMismatchTypeForIntValue()
        {
            // Arrange
            InitializeFeaturesData();
            var featuresService = new FeaturesService(_db);

            // Act
            var exception = await featuresService.SetValue("Delay", false).ShouldThrowAsync<Exception>();

            // Assert
            exception.Message.ShouldBe("The feature Delay is an integer feature...");
        }

        [Fact]
        public async Task SetValueShouldUpdateDecimalFeature()
        {
            // Arrange
            InitializeFeaturesData();
            var featuresService = new FeaturesService(_db);

            decimal newValue = 10;

            // Act
            var result = await featuresService.SetValue("TaxPercent", newValue);

            // Assert
            result.BooleanValue.ShouldBeNull();
            result.IntValue.ShouldBeNull();
            result.DecimalValue.ShouldBe(newValue);
            result.StringValue.ShouldBeNull();
        }
        [Fact]
        public async Task SetValueShouldFailIfMismatchTypeForDecimalValue()
        {
            // Arrange
            InitializeFeaturesData();
            var featuresService = new FeaturesService(_db);

            // Act
            var exception = await featuresService.SetValue("TaxPercent", "123").ShouldThrowAsync<Exception>();

            // Assert
            exception.Message.ShouldBe("The feature TaxPercent is a decimal feature...");
        }

        [Fact]
        public async Task SetValueShouldUpdateStringFeature()
        {
            // Arrange
            InitializeFeaturesData();
            var featuresService = new FeaturesService(_db);

            string newValue = "dark";

            // Act
            var result = await featuresService.SetValue("Theme", newValue);

            // Assert
            result.BooleanValue.ShouldBeNull();
            result.IntValue.ShouldBeNull();
            result.DecimalValue.ShouldBeNull();
            result.StringValue.ShouldBe(newValue);
        }
        [Fact]
        public async Task SetValueShouldFailIfMismatchTypeForStringValue()
        {
            // Arrange
            InitializeFeaturesData();
            var featuresService = new FeaturesService(_db);

            // Act
            var exception = await featuresService.SetValue("Theme", true).ShouldThrowAsync<Exception>();

            // Assert
            exception.Message.ShouldBe("The feature Theme is a string feature...");
        }

        [Fact]
        public async Task SetValueShouldFailIfNoFeatureFound()
        {
            // Arrange
            InitializeFeaturesData();
            var featuresService = new FeaturesService(_db);

            string featureName = "X";

            // Act
            var exception = await featuresService.SetValue(featureName, "test").ShouldThrowAsync<Exception>();

            // Assert
            exception.Message.ShouldBe($"The feature {featureName} does not exist...");
        }
    }
}
