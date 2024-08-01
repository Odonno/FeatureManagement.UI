---
title: Realtime updates
description: You can use an event handlers to detect when a feature is updated.
---

There are event handlers you can use that are triggered when a feature is updated.

When a server feature is updated:

```csharp
c.OnServerFeatureUpdated = (IFeature feature) =>
{
    // Do something when a server feature is updated
};
```

When a client feature is updated, we also get the id of the user that updated the feature:

```csharp
c.OnClientFeatureUpdated = (IFeature feature, string clientId) =>
{
    // Do something when a client feature is updated
};
```
