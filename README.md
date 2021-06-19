# DotVVM.Diagnostics.ViewHotReload

This package extends [DotVVM](https://github.com/riganti/dotvvm) with a **hot reload** feature for markup files. 

Whenever you edit any markup file, the page is automatically refreshed while the viewmodel state is kept - this allows to speed up developer inner loop experience.

## Installation

First, install the NuGet package:

* For ASP.NET Core, use `DotVVM.Diagnostics.ViewHotReload.AspNetCore` (minimum supported version is .NET Core 3.1) 
* For OWIN and old .NET Framework, use `DotVVM.Diagnostics.ViewHotReload.AspNetCore`

Second, register the extension in `DotvvmStartup.cs` file:

```CSHARP
		public void ConfigureServices(IDotvvmServiceCollection options)
    {
        ...
        options.AddViewHotReload();
		}
```

And finally, register the required middlewares in `Startup.cs` file:

**ASP.NET Core**

```CSHARP
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        ...
        app.UseDotVVM<DotvvmStartup>(...);
        ... 
        app.UseEndpoints(builder =>
        {
            ...
            builder.MapDotvvmViewHotReload();
        });
    }
```

**OWIN**

```CSHARP
    public void Configuration(IAppBuilder app)
    {
        ...
        app.UseDotVVM<DotvvmStartup>(...);

        app.UseDotvvmViewHotReload();
        ...
    }
```

### Note

If you are already using SignalR in your application, this extension adds an extra SignalR hub. If you configure any global settings or authorization policies to SignalR, make sure you exclude the `DotvvmViewHotReloadHub` class from these.


## Hot reload for ViewModels

If you want to use hot reload for method bodies in viewmodel, you will need to use .NET 6 which brings this functionality. This is not covered by this package as it is a feature of the .NET runtime.

