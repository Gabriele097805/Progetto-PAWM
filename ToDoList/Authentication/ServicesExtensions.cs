using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using ToDoList.Configuration;
using ToDoList.Models;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Authentication;

public static class AuthenticationConfigurationExtensions
{
    private static GitHubConfiguration ExtractConfiguration(this ConfigurationManager configuration)
    {
        var githubConfiguration = new GitHubConfiguration();
        configuration.Bind(nameof(GitHubConfiguration), githubConfiguration);
        return githubConfiguration;
    }

    private static OAuthEvents GetOAuthEvents() =>
        new OAuthEvents
        {
            OnCreatingTicket = async context =>
            {
                var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
                var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                response.EnsureSuccessStatusCode();
                var bodyAsString = await response.Content.ReadAsStringAsync();
                JsonElement user = JsonDocument.Parse(bodyAsString).RootElement;
                context.RunClaimActions(user!);
            }
        };

    public static IServiceCollection AddGitHubOAuth(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = "GitHub";
        })
        .AddCookie()
        .AddOAuth("GitHub", options =>
        {
            var githubConfiguration = configuration.ExtractConfiguration();
            options.ClientId = githubConfiguration.ClientId ?? 
                throw new ArgumentNullException("GitHub client id is null");
            options.ClientSecret = githubConfiguration.ClientSecret ?? 
                throw new ArgumentNullException("GitHub client secret is null");
            options.CallbackPath = new PathString("/api/signin-github");
            options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
            options.TokenEndpoint = "https://github.com/login/oauth/access_token";
            options.UserInformationEndpoint = "https://api.github.com/user";
            options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
            options.ClaimActions.MapJsonKey(ClaimTypes.Name, "login");
            options.ClaimActions.MapJsonKey(ClaimTypes.Uri, "html_url");
            options.ClaimActions.MapJsonKey(ClaimTypes.Thumbprint, "avatar_url");
            options.Events = GetOAuthEvents();
        });

        return services;
    }

    private static string? GetClaimValue(this IEnumerable<Claim> claims, string claimType) =>
        claims.FirstOrDefault(c => c.Type == claimType)?.Value;

    private static int? GetClaimIntValue(this IEnumerable<Claim> claims, string claimType)
    {
        var claimValue = claims.GetClaimValue(claimType);
        
        if (claimValue is not null && int.TryParse(claimValue, out var intValue))
        {
            return intValue;
        }

        return null;
    }

    public static User? TryGetUser(this ControllerBase controller) 
    {
        var claims = controller.User.Claims;

        var info = (
            claims.GetClaimIntValue(ClaimTypes.NameIdentifier),
            claims.GetClaimValue(ClaimTypes.Name),
            claims.GetClaimValue(ClaimTypes.Thumbprint),
            claims.GetClaimValue(ClaimTypes.Uri)
        );

        return info switch
        {
            (var id, var name, var avatar, var uri) when id is {} notNullId && !string.IsNullOrWhiteSpace(name) => 
                new User(notNullId, name, uri, avatar),
            _ => 
                null
        };
    }

    /// <summary>
    /// Gets the user, throwing an exception if it does not find it in the controller.
    /// </summary>
    /// <param name="controller">The controller.</param>
    /// <returns>The user</returns>
    /// <exception cref="ToDoList.Authentication.UserNotFoundException">
    /// If the user is null, it throws this exception
    /// </exception>
    public static User GetUser(this ControllerBase controller) =>
        controller.TryGetUser() switch 
        {
            null => throw new UserNotFoundException(),
            var u => u
        };
}

public class UserNotFoundException : Exception { }