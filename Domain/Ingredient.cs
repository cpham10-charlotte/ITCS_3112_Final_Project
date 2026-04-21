using ITCS_3112_Final_Project.Enums;

namespace ITCS_3112_Final_Project.Domain;

/// <summary>
/// Represents a single ingredient and its basic dietary metadata.
/// </summary>
public sealed class Ingredient : IEquatable<Ingredient>
{
    public string Id { get; }
    public string Name { get; }
    public VeganEnum VeganStatus { get; }

    public Ingredient(string id, string name, VeganEnum veganStatus)
    {
        if (!IsValidId(id))
        {
            throw new ArgumentException("Ingredient id cannot be null or whitespace.", nameof(id));
        }

        if (!IsValidName(name))
        {
            throw new ArgumentException("Ingredient name cannot be null or whitespace.", nameof(name));
        }

        Id = id.Trim();
        Name = name.Trim();
        VeganStatus = veganStatus;
    }

    /// <summary>
    /// Returns true when the id is acceptable for an ingredient.
    /// </summary>
    public static bool IsValidId(string? id) => !string.IsNullOrWhiteSpace(id);

    /// <summary>
    /// Returns true when the name is acceptable for an ingredient.
    /// </summary>
    public static bool IsValidName(string? name) => !string.IsNullOrWhiteSpace(name);

    public bool Equals(Ingredient? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        // Prefer Id when both are non-empty; otherwise fall back to name comparison.
        if (!string.IsNullOrWhiteSpace(Id) && !string.IsNullOrWhiteSpace(other.Id))
        {
            return string.Equals(Id, other.Id, StringComparison.Ordinal);
        }

        return string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase);
    }

    public override bool Equals(object? obj) => obj is Ingredient other && Equals(other);

    public override int GetHashCode()
    {
        if (!string.IsNullOrWhiteSpace(Id))
        {
            return StringComparer.Ordinal.GetHashCode(Id);
        }

        return StringComparer.OrdinalIgnoreCase.GetHashCode(Name);
    }

    public static bool operator ==(Ingredient? left, Ingredient? right) => Equals(left, right);

    public static bool operator !=(Ingredient? left, Ingredient? right) => !Equals(left, right);
}
