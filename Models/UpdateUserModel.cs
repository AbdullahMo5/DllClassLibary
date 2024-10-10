using Goorge.Model;

namespace Goorge.Models
{
    public class UpdateUserModel : CreateUserModel
    {
        public ulong login { get; set; }

        public UpdateUserModel(CreateUserModel model)
        {
            email = model.email;
            group = model.group;
            leverage = model.leverage;
            email = model.email;
            name = model.name;
            phone = model.phone;
            city = model.city;
            comment = model.comment;
            country = model.country;
            state = model.state;
        }
        public UpdateUserModel()
        {

        }
    }
}
