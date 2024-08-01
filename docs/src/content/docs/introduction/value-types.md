---
title: Value types
description: A description of all the value types that can be used in FeatureManagement.UI.
---

`FeatureManagement.UI` works with the following types of value:

- `boolean`
- `integer`
- `decimal`
- `string`

```csharp
configuration.ServerFeature("Beta", true);
```

By default, it provides a feature without strict limitation. You can however specify a list of choices to limit the number of options available.

```csharp
var themes = new List<string>
{
    "light",
    "dark"
};

configuration.ClientFeature("Theme", themes[0], "Choose a theme for the frontend", themes);
```
