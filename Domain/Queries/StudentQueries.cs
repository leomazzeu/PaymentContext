using System.Linq.Expressions;
using Domain.Entities;

namespace Domain.Queries;

public static class StudentQueries
{
    public static Expression<Func<Student, bool>> GetStudentInfo(string document)
    {
        return x => x.Document.Number == document;
    }
}