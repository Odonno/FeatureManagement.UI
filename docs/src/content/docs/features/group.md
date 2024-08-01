---
title: Group feature
description: A feature that can be configured differently per each group of users.
sidebar:
  order: 2
---

A group feature give you the ability to set a value based on a user affected group. It can be useful when you give certain rights to users like (Azure AD) roles or when you want to gradually roll out features to some groups of users. If one group matches, the value of the group is applied.

If no group match, the default value will be applied.

In order to use this type of feature, you need to install the [FeatureManagement.UI.Configuration.GroupFeature](https://www.nuget.org/packages/FeatureManagement.UI.Configuration.GroupFeature/) package.

```csharp
configuration
    .ClientFeature(
        "Beta",
        defaultValue: false,
        configuration: new GroupFeatureConfiguration<bool>
        {
            Groups = new List<GroupFeature<bool>>
            {
                new GroupFeature<bool> { Group = "Ring1", Value = true },
                new GroupFeature<bool> { Group = "Ring2", Value = true },
                new GroupFeature<bool> { Group = "Ring3", Value = false },
                new GroupFeature<bool> { Group = "Ring4", Value = false }
            }
        }
    );
```
