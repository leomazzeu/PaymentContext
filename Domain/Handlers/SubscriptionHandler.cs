using Domain.Commands;
using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using Domain.Services;
using Domain.ValueObjects;
using Flunt.Notifications;
using Shared.Commands;
using Shared.Handlers;

namespace Domain.Handlers;

public class SubscriptionHandler : Notifiable, IHandler<CreateBoletoSubscriptionCommand>
{
    private readonly IStudentRepository _repository;
    private readonly IEmailService _emailService;

    public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
    {
        _repository = repository;
        _emailService = emailService;
    }
    
    public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
    {
        //Fail fast validation
        command.Validate();
        if (command.Invalid)
        {
            AddNotifications(command);
            return new CommandResult(false, "Não foi possível realizar sua assinatura");
        }
            
        // Verificar se documento está cadastrado
        if(_repository.DocumentExists(command.Document))
            AddNotification("Document", "Este CPF já está em uso");
        
        // Verificar se e-mail está cadastrado
        if(_repository.EmailExists(command.Email))
            AddNotification("Email", "Este email já está em uso");
        
        // Gerar os VO's
        var name = new Name(command.FirstName, command.LastName);
        var document = new Document(command.Document, EDocumentType.CPF);
        var email = new Email(command.Email);
        
        var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

        // Gerar as entidades
        var student = new Student(name, document, email);
        var subscription = new Subscription(DateTime.Now.AddMonths(1));
        var payment = new BoletoPayment(command.PaidDate, command.ExpireDate, command.Total, command.TotalPaid,
            command.Payer, new Document(command.PayerDocument, command.PayerDocumentType), address, email, command.BarCode, command.BoletoNumber);
        
        // Relacionamentos
        subscription.AddPayment(payment);
        student.AddSubscription(subscription);
        
        // Agrupar as validações
        AddNotifications(name, document, email, address, student, subscription, payment);
        
        // Checar as notificações
        if (Invalid)
            return new CommandResult(false, "Não foi possível realizar a sua assinatura");
        
        // Salvar as informações
        _repository.CreateSubscription(student);
        
        // Enviar e-mail de boas vindas
        _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem-vindo", "Sua assinatura foi criada");
        
        // Retornar informações
        return new CommandResult(true, "Cadastro assinatura realizado com sucesso");
    }
}