// <copyright file="LocalizedIdentityErrorDescriber.cs" company="Tom Cook">
// Copyright (c) Tom Cook. All rights reserved.
// </copyright>

namespace MintyPeterson.Identity
{
  using Microsoft.AspNetCore.Identity;

  /// <summary>
  /// Provides a localized <see cref="IdentityErrorDescriber"/>.
  /// </summary>
  public class LocalizedIdentityErrorDescriber : IdentityErrorDescriber
  {
    /// <summary>
    /// Provides a default <see cref="IdentityError"/>.
    /// </summary>
    /// <returns>A <see cref="IdentityError"/>.</returns>
    public override IdentityError DefaultError() =>
      new IdentityError
      {
        Code = nameof(this.DefaultError),
        Description = Resources.Strings.UnexpectedError,
      };

    /// <summary>
    /// Provides a duplicate user name <see cref="IdentityError"/>.
    /// </summary>
    /// <param name="userName">A user name.</param>
    /// <returns>An <see cref="IdentityError"/>.</returns>
    public override IdentityError DuplicateUserName(string userName) =>
      new IdentityError
      {
        Code = nameof(this.DuplicateUserName),
        Description = Resources.Strings.EmailAlreadyRegistered,
      };

    /// <summary>
    /// Provides a password too short <see cref="IdentityError"/>.
    /// </summary>
    /// <param name="length">A password length.</param>
    /// <returns>An <see cref="IdentityError"/>.</returns>
    public override IdentityError PasswordTooShort(int length) =>
      new IdentityError
      {
        Code = nameof(this.PasswordTooShort),

        Description =
          string.Format(Resources.Strings.PasswordTooShort, length),
      };

    /// <summary>
    /// Provides a non-alphanumeric requirement <see cref="IdentityError"/>.
    /// </summary>
    /// <returns>A <see cref="IdentityError"/>.</returns>
    public override IdentityError PasswordRequiresNonAlphanumeric() =>
      new IdentityError
      {
        Code = nameof(this.PasswordRequiresNonAlphanumeric),

        Description =
          Resources.Strings.PasswordRequiresNonAlphanumericCharacter,
      };

    /// <summary>
    /// Provides a digit requirements <see cref="IdentityError"/>.
    /// </summary>
    /// <returns>A <see cref="IdentityError"/>.</returns>
    public override IdentityError PasswordRequiresDigit() =>
      new IdentityError
      {
        Code = nameof(this.PasswordRequiresDigit),
        Description = Resources.Strings.PasswordRequiresDigit,
      };

    /// <summary>
    /// Provides a lowercase character requirement <see cref="IdentityError"/>.
    /// </summary>
    /// <returns>A <see cref="IdentityError"/>.</returns>
    public override IdentityError PasswordRequiresLower() =>
      new IdentityError
      {
        Code = nameof(this.PasswordRequiresLower),
        Description = Resources.Strings.PasswordRequiresLowercaseCharacter,
      };

    /// <summary>
    /// Provides an uppercase character requirement <see cref="IdentityError"/>.
    /// </summary>
    /// <returns>A <see cref="IdentityError"/>.</returns>
    public override IdentityError PasswordRequiresUpper() =>
      new IdentityError
      {
        Code = nameof(this.PasswordRequiresUpper),
        Description = Resources.Strings.PasswordRequiresUppercaseCharacter,
      };
  }
}