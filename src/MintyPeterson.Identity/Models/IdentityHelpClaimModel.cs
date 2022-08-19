// <copyright file="IdentityHelpClaimModel.cs" company="Tom Cook">
// Copyright (c) Tom Cook. All rights reserved.
// </copyright>

namespace MintyPeterson.Identity.Models
{
  /// <summary>
  /// Represents a <see cref="System.Security.Claims.Claim"/> in a <see cref="IdentityHelpModel"/>.
  /// </summary>
  public class IdentityHelpClaimModel
  {
    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    public string Value { get; set; } = string.Empty;
  }
}
