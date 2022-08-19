// <copyright file="IdentityErrorViewModel.cs" company="Tom Cook">
// Copyright (c) Tom Cook. All rights reserved.
// </copyright>

namespace MintyPeterson.Identity.Models
{
  using Duende.IdentityServer.Models;

  /// <summary>
  /// Provides a view model for <see cref="Controllers.IdentityController.ErrorAsync" />.
  /// </summary>
  public class IdentityErrorViewModel
  {
    /// <summary>
    /// Gets or sets the identity server <see cref="ErrorMessage"/>.
    /// </summary>
    public ErrorMessage? Error { get; set; }
  }
}