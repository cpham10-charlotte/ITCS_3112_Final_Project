using ITCS_3112_Final_Project.Domain;

namespace ITCS_3112_Final_Project.Repositories;

/// <summary>
/// Data access abstraction for user pantries.
/// </summary>
public interface IPantryRepository
{
    Pantry? GetByUserId(string userId);

    void Save(Pantry pantry);
}
