namespace Recipes.Helpers
{

    public sealed class MyRegisterRequest
    {
        public string? Email { get; init; }
        public string? Password { get; init; }
        public string? UserName { get; set; }
    }
}