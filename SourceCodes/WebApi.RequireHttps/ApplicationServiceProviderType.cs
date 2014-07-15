namespace Aliencube.WebApi.RequireHttps
{
    /// <summary>
    /// This determines the application service provider types.
    /// </summary>
    public enum ApplicationServiceProviderType
    {
        /// <summary>
        /// Identifies unknown application service provider.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Identifies default application service provider.
        /// </summary>
        Default = 1,

        /// <summary>
        /// Identifies the application service provider is AppHarbor.
        /// </summary>
        AppHarbor = 2,
    }
}