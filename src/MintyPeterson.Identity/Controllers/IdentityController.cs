// <copyright file="IdentityController.cs" company="Tom Cook">
// Copyright (c) Tom Cook. All rights reserved.
// </copyright>

namespace MintyPeterson.Identity.Controllers
{
  using Duende.IdentityServer;
  using Duende.IdentityServer.Models;
  using Duende.IdentityServer.Services;
  using Duende.IdentityServer.Stores;
  using Microsoft.AspNetCore.Authentication;
  using Microsoft.AspNetCore.Authorization;
  using Microsoft.AspNetCore.Identity;
  using Microsoft.AspNetCore.Mvc;
  using MintyPeterson.Identity.Extensions;
  using MintyPeterson.Identity.Models;

  /// <summary>
  /// Provides actions for authentication and authorisation.
  /// </summary>
  [Controller]
  public class IdentityController : Controller
  {
    /// <summary>
    /// Stores the <see cref="IIdentityServerInteractionService"/> dependency.
    /// </summary>
    private readonly IIdentityServerInteractionService interactionService;

    /// <summary>
    /// Stores the <see cref="IIdentityServerTools"/> dependency.
    /// </summary>
    private readonly IIdentityServerTools identityServerTools;

    /// <summary>
    /// Stores the <see cref="IClientStore"/> dependency.
    /// </summary>
    private readonly IClientStore clientStore;

    /// <summary>
    /// Stores the <see cref="SignInManager{DefaultIdentityUser}"/> dependency.
    /// </summary>
    private readonly SignInManager<DefaultIdentityUser> signInManager;

    /// <summary>
    /// Stores the <see cref="UserManager{DefaultIdentityUser}"/> dependency.
    /// </summary>
    private readonly UserManager<DefaultIdentityUser> userManager;

    /// <summary>
    /// Stores the <see cref="ILogger"/> dependency.
    /// </summary>
    private readonly ILogger<IdentityController> loggerService;

    /// <summary>
    /// Initializes a new instance of the <see cref="IdentityController"/> class.
    /// </summary>
    /// <param name="interactionService">An <see cref="IIdentityServerInteractionService"/>.</param>
    /// <param name="clientStore">A <see cref="IClientStore"/>.</param>
    /// <param name="signInManager">A <see cref="SignInManager{DefaultIdentityUser}"/>.</param>
    /// <param name="userManager">A <see cref="UserManager{DefaultIdentityUser}"/>.</param>
    /// <param name="identityServerTools">An <see cref="IIdentityServerTools"/>.</param>
    /// <param name="loggerService">An <see cref="ILogger"/>.</param>
    public IdentityController(
      IIdentityServerInteractionService interactionService,
      IClientStore clientStore,
      SignInManager<DefaultIdentityUser> signInManager,
      UserManager<DefaultIdentityUser> userManager,
      IIdentityServerTools identityServerTools,
      ILogger<IdentityController> loggerService)
    {
      this.interactionService = interactionService;
      this.identityServerTools = identityServerTools;
      this.clientStore = clientStore;
      this.signInManager = signInManager;
      this.userManager = userManager;
      this.loggerService = loggerService;
    }

    /// <summary>
    /// Displays an error message.
    /// </summary>
    /// <param name="errorId">An error identifier.</param>
    /// <returns>A <see cref="IActionResult"/>.</returns>
    [AllowAnonymous]
    [Route("/Error")]
    public async Task<IActionResult> ErrorAsync(string errorId)
    {
      var model = new IdentityErrorViewModel();

      // Get the error message from identity server.
      var errorMessage = await this.interactionService.GetErrorContextAsync(
        errorId);

      if (errorMessage != null)
      {
        model.Error = errorMessage;
      }

      return this.View("Error", model);
    }

    /// <summary>
    /// Displays support information.
    /// </summary>
    /// <returns>An <see cref="IActionResult"/>.</returns>
    [AllowAnonymous]
    [HttpGet("/")]
    public async Task<IActionResult> Help()
    {
      var authenticateResult = await this.HttpContext.AuthenticateAsync();

      if (authenticateResult.Succeeded)
      {
        return this.View(new IdentityHelpModel
        {
          IsAuthenticated = true,

          Claims = authenticateResult.Principal.Claims.Select(
            c => new IdentityHelpClaimModel
            {
              Type = c.Type,
              Value = c.Value,
            }),
        });
      }

      return this.View();
    }

    /// <summary>
    /// Signs the user in.
    /// </summary>
    /// <param name="model">A <see cref="IdentitySignInViewModel"/>.</param>
    /// <returns>A <see cref="IActionResult"/>.</returns>
    [AllowAnonymous]
    [HttpPost("/SignIn")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SignInAsync(IdentitySignInViewModel model)
    {
      var context = await this.interactionService.GetAuthorizationContextAsync(
        model.ReturnUrl);

      if (model.Action != "SignIn")
      {
        if (context != null)
        {
          await this.interactionService.GrantConsentAsync(
            context,
            new ConsentResponse { Error = AuthorizationError.AccessDenied });

          if (await this.clientStore.IsPkceClientAsync(
            context.Client.ClientId))
          {
            return
              this.View(
                "Redirect",
                new IdentityRedirectViewModel
                {
                  RedirectUrl = model.ReturnUrl,
                });
          }

          return this.Redirect(model.ReturnUrl ?? "/");
        }
        else
        {
          return this.RedirectToAction("Help");
        }
      }

      if (this.ModelState.IsValid)
      {
        var result =
          await this.signInManager.PasswordSignInAsync(
            model.Email!, model.Password!, true, false);

        if (result.Succeeded)
        {
          if (this.interactionService.IsValidReturnUrl(model.ReturnUrl))
          {
            if (context != null)
            {
              if (await this.clientStore.IsPkceClientAsync(
                context.Client.ClientId))
              {
                return
                  this.View(
                    "Redirect",
                    new IdentityRedirectViewModel
                    {
                      RedirectUrl = model.ReturnUrl,
                    });
              }

              return this.Redirect(model.ReturnUrl ?? "/");
            }
          }

          return this.RedirectToAction("Help");
        }

        this.ModelState.AddModelError(
          string.Empty,
          Identity.Resources.Strings.EmailOrPasswordIncorrect);
      }

      return this.View("SignIn", model);
    }

    /// <summary>
    /// Allows the user to sign in.
    /// </summary>
    /// <param name="returnUrl">A return URL.</param>
    /// <returns>A <see cref="ViewResult"/>.</returns>
    [AllowAnonymous]
    [HttpGet("/SignIn")]
    public ViewResult SignIn(string? returnUrl)
    {
      var model =
        new IdentitySignInViewModel
        {
          ReturnUrl = returnUrl,
        };

      return this.View("SignIn", model);
    }

    /// <summary>
    /// Signs the user out.
    /// </summary>
    /// <param name="model">A <see cref="IdentitySignOutViewModel"/>.</param>
    /// <returns>A <see cref="IActionResult"/>.</returns>
    [HttpPost("/SignOut")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SignOutAsync(
      IdentitySignOutViewModel model)
    {
      var context = await this.interactionService.GetLogoutContextAsync(
        model.LogoutId);

      if (this.User?.Identity?.IsAuthenticated == true)
      {
        await this.signInManager.SignOutAsync();
      }

      if (string.IsNullOrWhiteSpace(context?.PostLogoutRedirectUri))
      {
        return this.RedirectToAction("Help");
      }

      return
        this.View(
          "SignedOut",
          new IdentitySignedOutViewModel
          {
            LogoutId = model.LogoutId,
            RedirectUrl = context.PostLogoutRedirectUri,
            SignOutFrameUrl = context.SignOutIFrameUrl,
          });
    }

    /// <summary>
    /// Allows the user to sign out.
    /// </summary>
    /// <param name="logoutId">A logout identifier.</param>
    /// <returns>A <see cref="IActionResult"/>.</returns>
    [HttpGet("/SignOut")]
    public async Task<IActionResult> SignOutAsync(string logoutId)
    {
      var model =
        new IdentitySignOutViewModel
        {
          LogoutId = logoutId,
        };

      var context = await this.interactionService.GetLogoutContextAsync(
        logoutId);

      if (context?.ShowSignoutPrompt == false)
      {
        return await this.SignOutAsync(model);
      }

      return this.View("SignOut", model);
    }
  }
}
