---
title: Time window feature
description: A feature that can be enable for a period of time.
sidebar:
  order: 3
---

A time window feature give you the ability to set a value based on a speicifed date range. It can be useful to apply a different value differs based on the current period of time. Some benefits you can get would be:

- apply feature before or after next month/week/year, to roll out a new feature
- apply feature only during a day/week/month, to experiment a feature or to set a limited time event

If no time window match, the default value will be applied.

In order to use this type of feature, you need to install the [FeatureManagement.UI.Configuration.TimeWindow](https://www.nuget.org/packages/FeatureManagement.UI.Configuration.TimeWindow/) package.

```csharp
configuration
    .ServerFeature(
        "GameSeasons",
        defaultValue: "Summer",
        configuration: new TimeWindowFeatureConfiguration<string>
        {
            TimeWindows = new List<TimeWindowFeature<string>>
            {
                new TimeWindowFeature<string> { StartDate = new DateTime(2020, 01, 01), EndDate = new DateTime(2020, 04, 01), Value = "Winter" },
                new TimeWindowFeature<string> { StartDate = new DateTime(2020, 04, 01), EndDate = new DateTime(2020, 07, 01), Value = "Spring" },
                new TimeWindowFeature<string> { StartDate = new DateTime(2020, 07, 01), EndDate = new DateTime(2020, 10, 01), Value = "Summer" },
                new TimeWindowFeature<string> { StartDate = new DateTime(2020, 10, 01), EndDate = new DateTime(2021, 01, 01), Value = "Fall" }
            }
        }
    );
```
