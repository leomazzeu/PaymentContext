using Domain.Entities;
using Domain.Repositories;

namespace Tests.Mocks;

public class FakeStudentRepository : IStudentRepository
{
    public bool DocumentExists(string document)
    {
        if (document == "12312312312")
            return true;

        return false;
    }

    public bool EmailExists(string email)
    {
        if (email == "batman@gmail.com")
            return true;

        return false;
    }

    public void CreateSubscription(Student student)
    {
    }
}