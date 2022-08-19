// <copyright file="DefaultIdentityDbContext.cs" company="Tom Cook">
// Copyright (c) Tom Cook. All rights reserved.
// </copyright>

namespace MintyPeterson.Identity
{
  using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore;

  /// <summary>
  /// Provides a default <see cref="IdentityDbContext"/>.
  /// </summary>
  public class DefaultIdentityDbContext : IdentityDbContext<DefaultIdentityUser>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultIdentityDbContext"/> class.
    /// </summary>
    /// <param name="options">The <see cref="DbContextOptions"/>.</param>
    public DefaultIdentityDbContext(DbContextOptions<DefaultIdentityDbContext> options)
      : base(options)
    {
    }
  }
}