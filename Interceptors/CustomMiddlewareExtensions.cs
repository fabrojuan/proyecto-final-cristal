namespace MVPSA_V2022.Interceptors
{
    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserMiddleware(this IApplicationBuilder builder) {
            return builder.UseMiddleware<RequestUserMiddleware>();
        }
    }
}
