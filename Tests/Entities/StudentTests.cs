using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;

namespace Tests.Entities;

[TestClass]
public class StudentTests
{
    private readonly Name _name;
    private readonly Document _document;
    private readonly Address _address;
    private readonly Email _email;
    private readonly Student _student;
    private readonly Subscription _subscription;

    public StudentTests()
    {
        _name = new Name("Bruce", "Wayne");
        _document = new Document("12312312312", EDocumentType.CPF);
        _email = new Email("batman@gmail.com");
        _student = new Student(_name, _document, _email);
        _subscription = new Subscription(null);
        _address = new Address("Rua 1", "1234", "Bairro Legal", "Gotham", "SP", "BR", "1231123");
    }
    
    [TestMethod]
    public void ShouldReturnErrorWhenHadActiveSubscription()
    {
        var payment = new PayPalPayment(DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Wayne Corp", _document, _address, _email, "123321123");
        _subscription.AddPayment(payment);
        _student.AddSubscription(_subscription);
        _student.AddSubscription(_subscription);
        Assert.IsTrue(_student.Invalid);
    }
    
    [TestMethod]
    public void ShouldReturnSuccessWhenSubscriptionHasNoPayment()
    {
        _student.AddSubscription(_subscription);
        Assert.IsTrue(_student.Invalid);
    }
    
    [TestMethod]
    public void ShouldReturnSuccessWhenAddSubscription()
    {
        var payment = new PayPalPayment(DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Wayne Corp", _document, _address, _email, "123321123");
        _subscription.AddPayment(payment);
        _student.AddSubscription(_subscription);
        Assert.IsTrue(_student.Valid);
    }
}