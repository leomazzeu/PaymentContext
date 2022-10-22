using Flunt.Validations;
using Shared.ValueObjects;

namespace Domain.ValueObjects;

public class Name : ValueObject
{
    public Name(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;

        AddNotifications(new Contract()
            .Requires()
            .HasMinLen(FirstName, 3, "Name.FirstName", "Nome deve conter no mínimo 3 caracteres")
            .HasMinLen(LastName, 3, "Name.LastName", "Sobrenome deve conter no mínimo 3 caracteres")
            .HasMaxLen(FirstName, 30, "Name.FirstName", "Nome deve conter no máximo 30 caracteres")
            .HasMaxLen(LastName, 30, "Name.LastName", "Sobrenome deve conter no máximo 30 caracteres")
        );
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    public override string ToString()
    {
        return $"{FirstName} {LastName}";
    }
}