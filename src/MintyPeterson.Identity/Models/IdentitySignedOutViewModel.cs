// <copyright file="IdentitySignedOutViewModel.cs" company="Tom Cook">
// Copyright (c) Tom Cook. All rights reserved.
// </copyright>

namespace MintyPeterson.Identity.Models
{
  /// <summary>
  /// Provides a view model following <see cref="Controllers.IdentityController.SignOutAsync(IdentitySignOutViewModel)" />.
  /// </summary>
  public class IdentitySignedOutViewModel
  {
    /// <summary>
    /// Gets or sets the URL to redirect to.
    /// </summary>
    public string? RedirectUrl { get; set; }

    /// <summary>
    /// Gets or sets the sign out frame URL.
    /// </summary>
    public string? SignOutFrameUrl { get; set; }

    /// <summary>
    /// Gets or sets the logout identifier.
    /// </summary>
    public string? LogoutId { get; set; }
  }
}
