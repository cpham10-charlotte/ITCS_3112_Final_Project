using ITCS_3112_Final_Project.Contracts;
using ITCS_3112_Final_Project.Domain;

namespace ITCS_3112_Final_Project.Repositories;

/// <summary>
/// In-memory pantry storage keyed by user id.
/// </summary>
public sealed class PantryRepository : IPantryRepository
{
    private readonly Dictionary<string, Pantry> _pantries = new(StringComparer.Ordinal);

    public Pantry? GetByUserId(string userId) =>
        string.IsNullOrWhiteSpace(userId)
            ? null
            : _pantries.TryGetValue(userId.Trim(), out var pantry) ? pantry : null;

    public void Save(Pantry pantry)
    {
        ArgumentNullException.ThrowIfNull(pantry);
        _pantries[pantry.UserId.Trim()] = pantry;
    }
}
