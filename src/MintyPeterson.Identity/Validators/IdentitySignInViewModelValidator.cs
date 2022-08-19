// <copyright file="IdentitySignInViewModelValidator.cs" company="Tom Cook">
// Copyright (c) Tom Cook. All rights reserved.
// </copyright>

namespace MintyPeterson.Identity.Validators
{
  using FluentValidation;
  using MintyPeterson.Identity.Models;

  /// <summary>
  /// Provides a <see cref="AbstractValidator{IdentitySignInViewModelValidator}"/>.
  /// </summary>
  public class IdentitySignInViewModelValidator : AbstractValidator<IdentitySignInViewModel>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="IdentitySignInViewModelValidator"/> class.
    /// </summary>
    public IdentitySignInViewModelValidator()
    {
      this.RuleFor(m => m.Email)
        .NotEmpty()
        .WithMessage(Resources.Strings.EmailFieldRequired);

      this.RuleFor(m => m.Password)
        .NotEmpty()
        .WithMessage(Resources.Strings.PasswordFieldRequired);
    }
  }
}
