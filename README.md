# ScriptBundleTranslator
A simple .NET Script bundling translator that is made in conjuction with the `Microsoft.AspNet.Web.Optimization` bundler package in order to create translated scripts eliminating the need of creating global variables with translation keyvaluepairs.
These translated bundles are also cached by language, this means they won't be regenerated again, unless they change in the source code.

### How to use ###
Use in between curly brackers the name of your resource file (.resx) and with a slash (/) name the key you want to use.

```typescript
public success: string = "{JSTranslator/Success}";
public failed: string = "{JSTranslator/Failed}";
public loading: string = "{JSTranslator/Loading}";
```
Which will be rendered as the proper translation value
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
