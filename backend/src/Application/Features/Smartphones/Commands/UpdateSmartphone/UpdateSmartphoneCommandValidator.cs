using FluentValidation;

namespace Application.Features.Smartphones.Commands.UpdateSmartphone;

public class UpdateSmartphoneCommandValidator : AbstractValidator<UpdateSmartphoneCommand>
{
    public UpdateSmartphoneCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Marque).NotEmpty().WithMessage("La marque est obligatoire.").MaximumLength(100);
        RuleFor(x => x.Modele).NotEmpty().WithMessage("Le modèle est obligatoire.").MaximumLength(100);
        RuleFor(x => x.Prix).GreaterThan(0).WithMessage("Le prix doit être supérieur à 0.");
        RuleFor(x => x.Stock).GreaterThanOrEqualTo(0).WithMessage("Le stock ne peut pas être négatif.");
    }
}