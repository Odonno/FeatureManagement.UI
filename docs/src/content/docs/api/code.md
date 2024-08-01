---
title: C# API
description: Features can be consumed directly in C#.
sidebar:
  order: 1
---

Inside your ASP.NET Web API, you can inject the `IFeaturesService`.

```cs
public interface IFeaturesService
{
    Task<List<Feature>> GetAll();
    Task<Feature> Get(string featureName);
    Task<T> GetValue<T>(string featureName, string? clientId = null);
    Task<Feature> SetValue<T>(string featureName, T value, string? clientId = null);
}
```

You can get and use all features at once, detect if a feature is valid or not and even update the value of a feature based on your needs.
