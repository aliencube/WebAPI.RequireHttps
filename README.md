# WebAPI.RequireHttps #

**WebAPI.RequireHttps** provides a custom action attribute using [ActionFilterAttribute](http://msdn.microsoft.com/en-us/library/system.web.http.filters.actionfilterattribute(v=vs.118).aspx), to allow HTTPS connection based on configuration.


## Acknowledgement ##

This library is based on the article, [Working with SSL in Web API](http://www.asp.net/web-api/overview/security/working-with-ssl-in-web-api).


## Getting Started ##

**WebAPI.RequireHttps** is a custom action filter attribute, therefore, it should be used for either Web API controllers or individual actions.


### Controller Level Definition ###

```csharp
[RequireHttps(RequireHttpsConfigurationSettingsProviderType =
                  typeof(RequireHttpsConfigurationSettingsProvider))]
public class SampleApiController : ApiController
{
    ...
}
```


### Action Level Definition ###

```csharp
public class SampleApiController : ApiController
{
    [RequireHttps(RequireHttpsConfigurationSettingsProviderType =
                      typeof(RequireHttpsConfigurationSettingsProvider))]
    public HttpResponseMessage Get()
    {
        ...
    }
}
```


## Configuration ##

In order to configure the `RequireHttpsAttribute` instance, `Web.config` should be considered.

```xml
<applicationSettings>
    <Aliencube.WebApi.RequireHttps.Properties.Settings>
        <setting name="BypassHttps" serializeAs="String">
            <value>False</value>
        </setting>
        <setting name="ApplicationServiceProviders" serializeAs="String">
            <value />
        </setting>
    </Aliencube.WebApi.RequireHttps.Properties.Settings>
</applicationSettings>
```

* `BypassHttps`: If it is set to `true`, the `RequireHttpsAttribute` instance assumes the request is over HTTPS connection. Default value is `false`.
* `ApplicationServiceProviders`: In case that additional check based on application service provider is required, this should be defined. Currently (version 1.5.0.0), this value can be either, `Default` or `AppHarbor`. Default value is `nil`.


# Contribution #

Your contribution is always welcome! All your work should be done in the`dev` branch. Once you finish your work, please send us a pull request on `dev` for review. Make sure that all your changes **MUST** be covered with test codes; otherwise yours won't get accepted.




## License ##

**WebAPI.RequireHttps** is released under [MIT License](http://opensource.org/licenses/MIT).

> The MIT License (MIT)
> 
> Copyright (c) 2014 [aliencube.org](http://aliencube.org)
> 
> Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is
> furnished to do so, subject to the following conditions:
> 
> The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
> 
> THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
