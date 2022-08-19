// <copyright file="IClientStoreExtensions.cs" company="Tom Cook">
// Copyright (c) Tom Cook. All rights reserved.
// </copyright>

namespace MintyPeterson.Identity.Extensions
{
  using System.Threading.Tasks;
  using Duende.IdentityServer.Stores;

  /// <summary>
  /// Provides extension methods for <see cref="IClientStore"/>.
  /// </summary>
  public static class IClientStoreExtensions
  {
    /// <summary>
    /// Determines whether the client is configured to use PKCE.
    /// </summary>
    /// <param name="store">The store.</param>
    /// <param name="clientId">The client identifier.</param>
    /// <returns>A value indicating if the client is configured to use PKCE or not.</returns>
    public static async Task<bool> IsPkceClientAsync(
      this IClientStore store, string clientId)
    {
      if (string.IsNullOrWhiteSpace(clientId))
      {
        return false;
      }

      var client = await store.FindEnabledClientByIdAsync(clientId);

      return client?.RequirePkce == true;
    }
  }
}
