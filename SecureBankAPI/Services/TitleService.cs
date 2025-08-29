// <copyright file="TitleService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace RealEstateAPI.Services;

/// <summary>
/// Service for managing page titles across components
/// </summary>
public class TitleService
{
    /// <summary>
    /// Event that is raised when the title changes
    /// </summary>
    public event EventHandler<string>? TitleChanged;

    /// <summary>
    /// Sets the current page title and notifies subscribers
    /// </summary>
    /// <param name="title">The new page title</param>
    public void SetTitle(string title)
    {
        this.TitleChanged?.Invoke(this, title);
    }

    /// <summary>
    /// Clears the current page title and notifies subscribers
    /// </summary>
    public void ClearTitle()
    {
        this.TitleChanged?.Invoke(this, string.Empty);
    }
}
