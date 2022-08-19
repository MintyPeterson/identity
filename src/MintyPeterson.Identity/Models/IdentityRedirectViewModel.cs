// <copyright file="IdentityRedirectViewModel.cs" company="Tom Cook">
// Copyright (c) Tom Cook. All rights reserved.
// </copyright>

namespace MintyPeterson.Identity.Models
{
  /// <summary>
  /// Provides a view model for a redirect from <see cref="Controllers.IdentityController" />.
  /// </summary>
  public class IdentityRedirectViewModel
  {
    /// <summary>
    /// Gets or sets the URL to redirect to.
    /// </summary>
    public string? RedirectUrl { get; set; }
  }
}
