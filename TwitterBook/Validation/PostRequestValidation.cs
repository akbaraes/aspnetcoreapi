using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterBook.Contracts.V1.Request;

namespace TwitterBook.Validation
{
    public class PostRequestValidation : AbstractValidator<PostRequest>
    {
        public PostRequestValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Please enter the  name").Matches("^[a-zA-Z0-9 ]*$").WithMessage("Please enter the valid name");

            RuleFor(x => x.Name).Must(x => !x.Contains("Akbar"));

        }
    }
}
