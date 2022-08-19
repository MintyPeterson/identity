// <copyright file="ValidationErrorSummaryTagHelper.cs" company="Tom Cook">
// Copyright (c) Tom Cook. All rights reserved.
// </copyright>

namespace MintyPeterson.Identity.Helpers
{
  using System;
  using Microsoft.AspNetCore.Html;
  using Microsoft.AspNetCore.Mvc.Rendering;
  using Microsoft.AspNetCore.Mvc.TagHelpers;
  using Microsoft.AspNetCore.Mvc.ViewFeatures;
  using Microsoft.AspNetCore.Razor.TagHelpers;

  /// <summary>
  /// An <see cref="ITagHelper"/> implementation targeting any HTML element with an asp-validation-error-summary attribute.
  /// </summary>
  public class ValidationErrorSummaryTagHelper : TagHelper
  {
    /// <inheritdoc />
    public override int Order => -1000;

    /// <summary>
    /// Gets or sets the <see cref="ViewContext"/> of the executing view.
    /// </summary>
    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext? ViewContext { get; set; }

    /// <inheritdoc />
    public override void Process(
      TagHelperContext context, TagHelperOutput output)
    {
      if (context == null)
      {
        throw new ArgumentNullException(nameof(context));
      }

      if (output == null)
      {
        throw new ArgumentNullException(nameof(output));
      }

      var tagBuilder =
        ValidationErrorSummaryTagHelper.GenerateValidationSummary(
          this.ViewContext);

      if (tagBuilder == null)
      {
        output.SuppressOutput();

        return;
      }

      output.TagName = "div";

      output.MergeAttributes(tagBuilder);
      output.PostContent.AppendHtml(tagBuilder);
    }

    /// <summary>
    /// Generates a validation summary.
    /// </summary>
    /// <param name="viewContext">The <see cref="ViewContext"/>.</param>
    /// <returns>A <see cref="TagBuilder"/>.</returns>
    private static TagBuilder? GenerateValidationSummary(
      ViewContext? viewContext)
    {
      if (viewContext == null)
      {
        throw new ArgumentNullException(nameof(viewContext));
      }

      var viewData = viewContext.ViewData;

      if (viewData.ModelState.IsValid)
      {
        return null;
      }

      var htmlSummary = new TagBuilder("ul");

      foreach (var modelState in viewData.ModelState)
      {
        for (var i = 0; i < modelState.Value.Errors.Count; i++)
        {
          var listItem = new TagBuilder("li");

          listItem.InnerHtml.SetContent(
            modelState.Value.Errors[i].ErrorMessage);

          htmlSummary.InnerHtml.AppendLine(listItem);
        }
      }

      return htmlSummary;
    }
  }
}
