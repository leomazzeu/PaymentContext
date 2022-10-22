using Domain.Commands;
using Domain.Handlers;
using Tests.Mocks;

namespace Tests.Handlers;

[TestClass]
public class SubscriptionHandlerTests
{
    // Red, Green, Refactor
    [TestMethod]
    public void ShouldReturnErrorWhenDocumentExists()
    {
        var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
        var command = new CreateBoletoSubscriptionCommand();
        
        // propriedades do command
        //
        //
        //

        handler.Handle(command);
        Assert.AreEqual(false, handler.Valid);
    }
}