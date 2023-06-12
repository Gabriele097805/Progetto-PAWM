using System.ComponentModel.DataAnnotations;

namespace ToDoList.Configuration;

public class GitHubConfiguration
{
    [Required(AllowEmptyStrings = false)]
    public string? ClientId { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string? ClientSecret  { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string? GitHubOAuthFallbackUri { get; set; }
}
