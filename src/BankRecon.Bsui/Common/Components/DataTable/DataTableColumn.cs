namespace BankRecon.Bsui.Common.Components.DataTable;

using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;

/// <summary>
/// Represents a column definition for the DataTable component.
/// </summary>
/// <typeparam name="TItem">The type of item being displayed.</typeparam>
public class DataTableColumn<TItem>
{
    /// <summary>
    /// Gets or sets the column header text.
    /// </summary>
    public string Header { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the property name for sorting and identification.
    /// </summary>
    public string PropertyName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the property accessor expression.
    /// </summary>
    public Expression<Func<TItem, object?>>? Property { get; set; }

    /// <summary>
    /// Gets or sets the column width (CSS value, e.g., "100px" or "20%").
    /// </summary>
    public string? Width { get; set; }

    /// <summary>
    /// Gets or sets whether this column is sortable.
    /// </summary>
    public bool IsSortable { get; set; } = true;

    /// <summary>
    /// Gets or sets a custom template for rendering cell content.
    /// </summary>
    public RenderFragment<TItem>? CellTemplate { get; set; }
}
