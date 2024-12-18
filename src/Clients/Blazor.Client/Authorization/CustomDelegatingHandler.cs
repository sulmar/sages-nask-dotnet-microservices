using Microsoft.AspNetCore.Components.WebAssembly;
using Blazored.LocalStorage;

namespace BlazorApp.Authorization;

public class CustomDelegatingHandler(ILocalStorageService Storage) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var accessToken = await Storage.GetItemAsStringAsync("access-token");

        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", accessToken);

        Console.WriteLine(accessToken);

        return await base.SendAsync(request, cancellationToken);
    }
}
