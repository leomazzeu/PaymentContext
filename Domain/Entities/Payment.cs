using Domain.ValueObjects;
using Flunt.Notifications;
using Flunt.Validations;
using Shared.Entities;

namespace Domain.Entities;

public abstract class Payment : Entity
{
    protected Payment(DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, string payer, Document document, Address address, Email email)
    {
        Number = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10).ToUpper();
        PaidDate = paidDate;
        ExpireDate = expireDate;
        Total = total;
        TotalPaid = totalPaid;
        Payer = payer;
        Document = document;
        Address = address;
        Email = email;
        
        AddNotifications(new Contract()
            .Requires()
            .IsLowerOrEqualsThan(0, Total, "Payment.Total", "O total não pode ser menor ou igual a zero")
            .IsGreaterOrEqualsThan(0, TotalPaid, "Payment.TotalPaid", "O valor pago é menor que o valor total do pagamento")
        );
    }

    public string Number { get; private set; }
    public DateTime PaidDate { get; private set; }
    public DateTime ExpireDate { get; private set; }
    public decimal Total { get; private set; }
    public decimal TotalPaid { get; private set; }
    public string Payer { get; private set; }
    public Document Document { get; private set; }
    public Address Address { get; private set; }
    public Email Email { get; private set; }
}