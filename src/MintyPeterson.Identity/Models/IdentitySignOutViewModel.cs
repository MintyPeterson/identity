// <copyright file="IdentitySignOutViewModel.cs" company="Tom Cook">
// Copyright (c) Tom Cook. All rights reserved.
// </copyright>

namespace MintyPeterson.Identity.Models
{
  /// <summary>
  /// Provides a view model for <see cref="Controllers.IdentityController.SignOutAsync(IdentitySignOutViewModel)" />.
  /// </summary>
  public class IdentitySignOutViewModel
  {
    /// <summary>
    /// Gets or sets the logout identifier.
    /// </summary>
    public string? LogoutId { get; set; }
  }
}
