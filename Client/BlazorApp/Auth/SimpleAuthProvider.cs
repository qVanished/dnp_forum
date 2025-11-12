using System;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using ApiContracts;
using ClassLibrary1;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace BlazorApp.Auth;

public class SimpleAuthProvider : AuthenticationStateProvider
{
    private readonly HttpClient httpClient;
    private readonly IJSRuntime jsRuntime;
    private string primary_cache;

    public SimpleAuthProvider(HttpClient httpClient, IJSRuntime runtime) {
        this.httpClient = httpClient;
        this.jsRuntime = runtime;
    }

    //TODO: session not logging in to the same user in new tab
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string userAsJson = "";
        try
        {
            if (string.IsNullOrEmpty(primary_cache))
            {
                userAsJson = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
            }
            else
            {
                userAsJson = primary_cache;
            }
        }
        catch (InvalidOperationException e)
        {
            return new AuthenticationState(new());
        }

        if (string.IsNullOrEmpty(userAsJson))
        {
            return new AuthenticationState(new());
        }

        GetUserDTO userDTO = JsonSerializer.Deserialize<GetUserDTO>(userAsJson)!;

        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, userDTO.Username),
            new Claim("Id", userDTO.Id.ToString())
        };

        ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth");
        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);

        return new AuthenticationState(claimsPrincipal);
    }

    public async Task LoginAsync(string userName, string password)
    {
        HttpResponseMessage response = await httpClient.PostAsJsonAsync("auth", new CreateUserDTO(userName, password));

        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode) { throw new Exception(content); }

        GetUserDTO userDto = JsonSerializer.Deserialize<GetUserDTO>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

        string serializedData = JsonSerializer.Serialize(userDto);
        primary_cache = serializedData;
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serializedData);


        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, userDto.Username),
            new Claim("Id", userDto.Id.ToString()),
            new Claim(ClaimTypes.Role, "user"),
            new Claim(ClaimTypes.Role, "admin")

        };
        ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth");

        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity); NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));

        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(claimsPrincipal))
        );
    }

    public async Task LogoutAsync()
    {
        ClaimsPrincipal claimsPrincipal = new();
        primary_cache = string.Empty;
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }
}
