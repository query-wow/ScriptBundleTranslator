# ScriptBundleTranslator
[![CodeFactor](https://www.codefactor.io/repository/github/query-js/scriptbundletranslator/badge)](https://www.codefactor.io/repository/github/query-js/scriptbundletranslator)

[![forthebadge](https://forthebadge.com/images/badges/made-with-c-sharp.svg)](https://forthebadge.com)
[![forthebadge](https://forthebadge.com/images/badges/built-with-love.svg)](https://forthebadge.com)
[![forthebadge](https://forthebadge.com/images/badges/60-percent-of-the-time-works-every-time.svg)](https://forthebadge.com)

A simple .NET Script bundling translator, created to easily communicate with the `ResourceManager` class and it's made in conjuction with the `Microsoft.AspNet.Web.Optimization` bundler package in order to create translated scripts eliminating the need of creating global variables with translation keyvaluepairs.
These translated bundles are also cached by language, this means they won't be regenerated again, unless they change in the source code.

### How to add this to my project ###
Simple check the [Configuration Samples](https://github.com/query-js/ScriptBundleTranslator/tree/master/ScriptBundleTranslator/ConfigExamples) and add them to your project. 
Then on the MVC's `BundleConfig.cs`  simple call the bundler as shown down below.

```csharp
bundles.AddRange(ScriptTranslationBundle.Create("~/bundles/general", "~/Scripts/General/General.js"));
```

This will create bundles for all the avaiable cultures inside your CultureSettings configuration.

### How to use ###
Use in between curly brackers the name of your resource file (.resx) and with a slash (/) name the key you want to use.

```typescript
public success: string = "{JSTranslator/Success}";
public failed: string = "{JSTranslator/Failed}";
public loading: string = "{JSTranslator/Loading}";
```
Which will be rendered as the proper translation value.

EN:
```javascript
var success = "Success";
var failed = "Failed";
var loading = "Loading";
```

PT:
```javascript
var success = "Sucesso";
var failed = "Falhou";
var loading = "A Carregar";
```

Call the script on a razor page via

```csharp
@ScriptBundleTranslator.Mvc.HtmlHelpers.LocalizedJsBundle("~/bundles/general")
```

## Dependencies ##
- Microsoft.AspNet.Web.Optimization
- Microsoft.Web.Infrastructure (>= 1.0.0)
- WebGrease (>= 1.5.2)
