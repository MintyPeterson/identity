// <copyright file="IdentitySignInViewModel.cs" company="Tom Cook">
// Copyright (c) Tom Cook. All rights reserved.
// </copyright>

namespace MintyPeterson.Identity.Models
{
  /// <summary>
  /// Provides a view model for <see cref="Controllers.IdentityController.SignInAsync" />.
  /// </summary>
  public class IdentitySignInViewModel
  {
    /// <summary>
    /// Gets or sets the action.
    /// </summary>
    public string? Action { get; set; }

    /// <summary>
    /// Gets or sets the return URL.
    /// </summary>
    public string? ReturnUrl { get; set; }

    /// <summary>
    /// Gets or sets the email address.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    public string? Password { get; set; }
  }
}
