// <copyright file="IdentityHelpModel.cs" company="Tom Cook">
// Copyright (c) Tom Cook. All rights reserved.
// </copyright>

namespace MintyPeterson.Identity.Models
{
  using Microsoft.AspNetCore.Authentication;

  /// <summary>
  /// Provides a view model for <see cref="Controllers.IdentityController.Help" />.
  /// </summary>
  public class IdentityHelpModel
  {
    /// <summary>
    /// Gets or sets a value indicating whether the user is authenticated or not.
    /// </summary>
    public bool IsAuthenticated { get; set; } = false;

    /// <summary>
    /// Gets or sets the user name.
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="IEnumerable{IdentityHelpClaimModel}"/>.
    /// </summary>
    public IEnumerable<IdentityHelpClaimModel>? Claims { get; set; }
  }
}
