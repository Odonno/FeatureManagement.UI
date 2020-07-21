### How to generate new NuGet packages

Given the version `[VERSION]`:

```
dotnet pack -c Release /p:Version=[VERSION]
```